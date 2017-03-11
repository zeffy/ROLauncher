using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Security.Cryptography;

namespace ROLauncher
{
    public partial class MainForm : Form
    {
        private static class NativeMethods
        {
            private const uint ECM_FIRST = 0x1500;
            internal const uint EM_SETCUEBANNER = ECM_FIRST + 1;

            internal const uint WM_ENTERSIZEMOVE = 0x0231;
            internal const uint WM_EXITSIZEMOVE = 0x0232;

            [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
            internal static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, UIntPtr wParam, StringBuilder lParam);

            [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
            private static extern uint GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);

            internal static string GetPrivateProfileString(string lpAppName, string lpKeyName, string lpDefault, string lpFileName) {
                var sb = new StringBuilder(1024);
                NativeMethods.GetPrivateProfileString(lpAppName, lpKeyName, lpDefault, sb, (uint)sb.Capacity, lpFileName);
                return sb.ToString();
            }

            [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
            internal static extern IntPtr LoadImage([Optional] IntPtr hinst, string lpszName, uint uType, int cxDesired, int cyDesired, uint fuLoad);
        }

        private string m_MyComGamesExePath;
        private string m_MyComGamesIniPath;
        private string m_ROInstallDirectory;
        private string m_ROLastXmlPath;

        private int m_CurrentBuildId;

        private string m_ROGameId;
        private string m_ROProjectId;
        private string m_ROClientExePath;
        private string m_MyComCodeParam;
        private string m_MyComUserIdParam;

        private Random m_Rnd;

        private int m__c_Min;
        private int m__c_Max;

        private int m_ExtCfgChannelId;

        private uint m_MyComUserId;
        private string m_MyComSessionKey;
        private string m_MyComRefreshToken;

        public MainForm() {
            InitializeComponent();
            InitializeFields();
        }

        private void InitializeFields() {
            m_MyComGamesExePath = Registry.GetValue(@"HKEY_CURRENT_USER\Software\MyComGames", "1",
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "MyComGames\\MyComGames.exe")) as string;
            m_MyComGamesIniPath = Path.ChangeExtension(m_MyComGamesExePath, "ini");
            m_ROInstallDirectory = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Uninstall\Revelation Online", "InstallLocation", null) as string;
            m_ROLastXmlPath = Path.Combine(m_ROInstallDirectory, "-gup-\\last.xml");

            var last = XDocument.Load(m_ROLastXmlPath);
            var manifest = last.Root;
            m_CurrentBuildId = (int)manifest.Attribute("Build");

            var misc = manifest.Element("Misc");
            m_ROGameId = (string)misc.Attribute("GAMEID") ?? "13.2000026";
            int index = m_ROGameId.IndexOf('.');

            m_ROProjectId = index > -1 ? m_ROGameId.Substring(m_ROGameId.IndexOf('.') + 1) : "2000026";
            m_ROClientExePath = Path.Combine(m_ROInstallDirectory, (string)misc.Attribute("EXEFILENAME") ?? "game\\tianyu.exe");
            m_MyComCodeParam = (string)misc.Attribute("MYCOMCODEPARAM") ?? "-my_com_code";
            m_MyComUserIdParam = (string)misc.Attribute("MYCOMUSERIDPARAM") ?? "-mycom_user_id";
            m_Rnd = new Random();

            m__c_Min = 359435;
            m__c_Max = 2136436470;
            if (!int.TryParse(NativeMethods.GetPrivateProfileString("ExtCfg", "Channel", "35", m_MyComGamesIniPath), out m_ExtCfgChannelId)) {
                m_ExtCfgChannelId = 35;
            }
            if (Properties.Settings.Default.KeepRefreshToken && !string.IsNullOrWhiteSpace(Properties.Settings.Default.RefreshTokenPD)) {
                m_MyComRefreshToken = BitConverter.ToString(ProtectedData.Unprotect(Convert.FromBase64String(Properties.Settings.Default.RefreshTokenPD),
                    Encoding.UTF8.GetBytes("c@qa[S/6tCY=iz$*qhR3[+?e"), DataProtectionScope.CurrentUser)).Replace("-", "").ToLowerInvariant();
            }
        }

        protected override void WndProc(ref Message m) {
            switch ((uint)m.Msg) {
                case NativeMethods.WM_ENTERSIZEMOVE:
                    this.Opacity = 0.75;
                    break;
                case NativeMethods.WM_EXITSIZEMOVE:
                    this.Opacity = 1;
                    break;
            }
            base.WndProc(ref m);
        }

        private void MainForm_Load(object sender, EventArgs e) {
            NativeMethods.SendMessage(userLoginTextBox.Handle, NativeMethods.EM_SETCUEBANNER, (UIntPtr)1, new StringBuilder("Email or phone number"));
            NativeMethods.SendMessage(passwordTextBox.Handle, NativeMethods.EM_SETCUEBANNER, (UIntPtr)1, new StringBuilder("Password"));
            SetButtonFlatAppearance(logInButton, FlatAppearanceStyle.Disabled);
            SetButtonFlatAppearance(playButton, FlatAppearanceStyle.Disabled);
            autoStartCheckBox.Text = string.Format(autoStartCheckBox.Text, Properties.Settings.Default.AutoLaunchDelayInSeconds);
        }

        private async void MainForm_ShownAsync(object sender, EventArgs e) {
            if (!string.IsNullOrWhiteSpace(Properties.Settings.Default.MyComUsername)) {
                userLoginTextBox.Text = Properties.Settings.Default.MyComUsername;
            } else {
                userLoginTextBox.Text = NativeMethods.GetPrivateProfileString("Main", "MyComUserLogin", "", m_MyComGamesIniPath);
            }

            if (!string.IsNullOrWhiteSpace(userLoginTextBox.Text)) {
                if (!string.IsNullOrWhiteSpace(m_MyComRefreshToken)) {
                    NativeMethods.SendMessage(passwordTextBox.Handle, NativeMethods.EM_SETCUEBANNER, (UIntPtr)1, new StringBuilder("Password [entered]"));
                    rememberMeCheckBox.Checked = true;
                    SetButtonFlatAppearance(logInButton, FlatAppearanceStyle.Disabled);
                } else {
                    passwordTextBox.Focus();
                }
            }
            autoStartCheckBox.Checked = Properties.Settings.Default.AutoLaunchAfterLogin;
            if (Properties.Settings.Default.CheckForROUpdates) {
                await CheckForROUpdatesAsync();
            }
            if (!string.IsNullOrWhiteSpace(m_MyComRefreshToken) && !string.IsNullOrWhiteSpace(userLoginTextBox.Text)) {
                if (await GCAuthAsync(m_MyComRefreshToken) && Properties.Settings.Default.AutoLaunchAfterLogin) {
                    statusBarLabel.Text = $"Auto-starting game in {Properties.Settings.Default.AutoLaunchDelayInSeconds} seconds...";
                    await Task.Delay(TimeSpan.FromSeconds(Properties.Settings.Default.AutoLaunchDelayInSeconds));
                    playButton.PerformClick();
                }
            }
        }

        private async Task CheckForROUpdatesAsync() {
            statusBarLabel.Text = "Checking for updates, please wait...";
            using (var client = new HttpClient()) {
                client.BaseAddress = new Uri("https://static.gc.my.com");
                client.DefaultRequestHeaders.Accept.ParseAdd("*/*");
                client.DefaultRequestHeaders.AcceptEncoding.ParseAdd("gzip");
                client.DefaultRequestHeaders.UserAgent.ParseAdd("Downloader/1960");
                client.DefaultRequestHeaders.Pragma.ParseAdd("no-cache");

                var ub = new UriBuilder(client.BaseAddress);
                ub.Path = "/trnts/patches/revelation_online_head.xml";

                var hvc = new UrlUtility.HttpValueCollection();
                hvc.Add("gid", m_ROGameId);
                hvc.Add(null, "_c_" + m_Rnd.Next(m__c_Min, m__c_Max));
                ub.Query = hvc.ToString();

                int latest;
                using (var resp = await client.GetAsync(ub.ToString())) {
                    var xd = XDocument.Load(await resp.Content.ReadAsStreamAsync());
                    latest = (int)xd.Root.Attribute("BuildId");
                }
                if (latest > m_CurrentBuildId) {
                    statusBarLabel.Visible = false;
                    updateAvailableLinkLabel.Visible = true;
                } else {
                    statusBarLabel.Text = "Your client is up to date!";
                }
            }
        }

        private void forgotPasswordLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start("https://account.my.com/us/password_reset/");
        }

        private async void logInButton_ClickAsync(object sender, EventArgs e) {
            if (await GCAuthAsync() && Properties.Settings.Default.AutoLaunchAfterLogin) {
                statusBarLabel.Text = $"Auto-starting game in {Properties.Settings.Default.AutoLaunchDelayInSeconds} seconds...";
                await Task.Delay(TimeSpan.FromSeconds(Properties.Settings.Default.AutoLaunchDelayInSeconds));
                playButton.PerformClick();
            }
        }

        private async void playButton_ClickAsync(object sender, EventArgs e) {
            SetButtonFlatAppearance(logInButton, FlatAppearanceStyle.Disabled);
            SetButtonFlatAppearance(playButton, FlatAppearanceStyle.Disabled);
            userLoginTextBox.Enabled = false;
            passwordTextBox.Enabled = false;
            rememberMeCheckBox.Enabled = false;
            autoStartCheckBox.Enabled = false;
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://authdl.my.com");
            client.DefaultRequestHeaders.Accept.ParseAdd("*/*");
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Downloader/1960");

            statusBarLabel.Text = "Logging into Revelation Online, please wait...";
            string str = $"<?xml version=\"1.0\" encoding=\"UTF-8\"?><Login SessionKey=\"{m_MyComSessionKey}\" UserId=\"5300216073977249290\" UserId2=\"16908145580284250563\" ProjectId=\"2000026\" ShardId=\"0\" FirstLink=\"_1lp=0&amp;_1ld=2046937_0\" Language=\"\"/>";
            var hc = new StringContent(str, Encoding.UTF8, "application/x-www-form-urlencoded");

            string code;
            using (var resp = await client.PostAsync("/mygc.php?hint=Login", hc)) {
                var xd = XDocument.Load(await resp.Content.ReadAsStreamAsync());
                code = (string)xd.Root.Attribute("Code");
            }

            statusBarLabel.Text = "Starting game, have fun! This window will close in 5 seconds.";
            string cl = $"{m_MyComCodeParam}{code} {m_MyComUserIdParam}{m_MyComUserId}";
            var psi = new ProcessStartInfo(m_ROClientExePath, cl);
            psi.WorkingDirectory = Path.GetDirectoryName(m_ROClientExePath);
            Process.Start(psi);
            await Task.Delay(TimeSpan.FromSeconds(5));
            this.Close();
        }

        private async Task<bool> GCAuthAsync(string refreshToken = null) {
            SetButtonFlatAppearance(logInButton, FlatAppearanceStyle.Disabled);
            SetButtonFlatAppearance(playButton, FlatAppearanceStyle.Disabled);
            userLoginTextBox.Enabled = false;
            passwordTextBox.Enabled = false;
            rememberMeCheckBox.Enabled = false;
            autoStartCheckBox.Enabled = false;
            statusBarLabel.Text = "Connecting to authentication server...";
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://authdl.my.com");
            client.DefaultRequestHeaders.Accept.ParseAdd("*/*");
            client.DefaultRequestHeaders.UserAgent.ParseAdd("Downloader/1960");
            string username = userLoginTextBox.Text.Trim();
            string str;
            if (!string.IsNullOrEmpty(refreshToken)) {
                str = $"<?xml version=\"1.0\" encoding=\"UTF-8\"?><Auth RefreshToken=\"{refreshToken}\" ChannelId=\"{m_ExtCfgChannelId}\"/>";
            } else {
                str = $"<?xml version=\"1.0\" encoding=\"UTF-8\"?><Auth Username=\"{username}\" Password=\"{passwordTextBox.Text}\" ChannelId=\"{m_ExtCfgChannelId}\"/>";
            }
            var hc = new StringContent(str, Encoding.UTF8, "application/x-www-form-urlencoded");

            using (var resp = await client.PostAsync("/mygc.php?hint=Auth", hc)) {
                var xd = XDocument.Load(await resp.Content.ReadAsStreamAsync());
                if (xd.Root.Name == "Error") {
                    int statusCode = (int)xd.Root.Attribute("Status");
                    int errorCode = (int)xd.Root.Attribute("ErrorCode");
                    string text;
                    if (statusCode == 200) {
                        switch (errorCode) {
                            case 3:
                                text = "Incorrect username and password, please try again.";
                                break;
                            default:
                                text = "Error occured while attempting to log in ({errorCode}), please try again later.";
                                break;
                        }
                    } else {
                        text = "Error occured while attempting to log in (HTTP {statusCode}), please try again later.";
                    }
                    statusBarLabel.Text = text;
                    SetButtonFlatAppearance(logInButton, FlatAppearanceStyle.Blue);
                    userLoginTextBox.Enabled = true;
                    passwordTextBox.Enabled = true;
                    rememberMeCheckBox.Enabled = true;
                    autoStartCheckBox.Enabled = true;
                    return false;
                }
                m_MyComSessionKey = (string)xd.Root.Attribute("SessionKey");
                m_MyComUserId = (uint)xd.Root.Attribute("Uid");
                string rt = (string)xd.Root.Attribute("RefreshToken");
                if (!string.IsNullOrWhiteSpace(rt)) {
                    m_MyComRefreshToken = (string)xd.Root.Attribute("RefreshToken");
                }
            }

            statusBarLabel.Text = "Successfully logged in!";
            SetButtonFlatAppearance(playButton, FlatAppearanceStyle.Green);
            userLoginTextBox.Enabled = true;
            passwordTextBox.Enabled = true;
            rememberMeCheckBox.Enabled = true;
            autoStartCheckBox.Enabled = true;
            Properties.Settings.Default.MyComUsername = username;

            if (Properties.Settings.Default.KeepRefreshToken = rememberMeCheckBox.Checked) {
                Properties.Settings.Default.RefreshTokenPD = Convert.ToBase64String(ProtectedData.Protect(
                        ToByteArrayFromHex(m_MyComRefreshToken),
                        Encoding.UTF8.GetBytes("c@qa[S/6tCY=iz$*qhR3[+?e"),
                        DataProtectionScope.CurrentUser));
            } else {
                Properties.Settings.Default.RefreshTokenPD = null;
            }
            Properties.Settings.Default.Save();
            return true;
        }

        private void loginTextBox_TextChanged(object sender, EventArgs e) {
            if (!string.IsNullOrWhiteSpace(userLoginTextBox.Text) && !string.IsNullOrWhiteSpace(passwordTextBox.Text)) {
                SetButtonFlatAppearance(logInButton, FlatAppearanceStyle.Blue);
            } else {
                SetButtonFlatAppearance(logInButton, FlatAppearanceStyle.Disabled);
            }
        }

        private void passwordTextBox_TextChanged(object sender, EventArgs e) {
            if (!string.IsNullOrWhiteSpace(userLoginTextBox.Text) && !string.IsNullOrWhiteSpace(passwordTextBox.Text)) {
                SetButtonFlatAppearance(logInButton, FlatAppearanceStyle.Blue);
            } else {
                SetButtonFlatAppearance(logInButton, FlatAppearanceStyle.Disabled);
            }
        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e) {
            Process.Start(m_MyComGamesExePath, "mycomgames://show/13.2000026");
        }

        private void myComLogoPictureBox_Click(object sender, EventArgs e) {
            Process.Start("https://account.my.com/signup/?lang=en_US&signup_method=email%2Cphone&signup_birthday=0&signup_subscribe=1&signup_tou=1&signup_repeat=0&signup_captcha=1&signup_nickname=0&with_steam=0&with_mailru=0&client_id=ro.my.com");
        }

        enum FlatAppearanceStyle
        {
            Blue,
            Green,
            Disabled
        }

        private void SetButtonFlatAppearance(Button b, FlatAppearanceStyle c) {
            Color backColor = Color.Empty;
            Color overColor = Color.Empty;
            Color borderColor = Color.Empty;

            switch (c) {
                case FlatAppearanceStyle.Blue:
                    backColor = Color.FromArgb(0, 168, 255);
                    overColor = Color.FromArgb(46, 186, 244);
                    borderColor = Color.FromArgb(16, 163, 240);
                    break;
                case FlatAppearanceStyle.Green:
                    backColor = Color.FromArgb(105, 165, 90);
                    overColor = Color.FromArgb(135, 183, 123);
                    borderColor = Color.FromArgb(66, 120, 54);
                    break;
                case FlatAppearanceStyle.Disabled:
                    backColor = Color.FromArgb(244, 244, 244);
                    overColor = Color.FromArgb(249, 249, 249);
                    borderColor = Color.FromArgb(189, 189, 189);
                    break;
            }
            b.Enabled = c != FlatAppearanceStyle.Disabled;
            b.ForeColor = c == FlatAppearanceStyle.Disabled ? Color.Black : Color.White;
            b.FlatStyle = FlatStyle.Flat;
            b.BackColor = backColor;
            b.FlatAppearance.MouseOverBackColor = overColor;
            b.FlatAppearance.MouseDownBackColor = backColor;
            b.FlatAppearance.BorderColor = borderColor;
        }

        public static byte[] ToByteArrayFromHex(string hexString) {
            if (hexString.Length % 2 != 0) throw new ArgumentException("String must have an even length");
            var array = new byte[hexString.Length / 2];
            for (int i = 0; i < hexString.Length; i += 2) {
                array[i / 2] = ByteFromTwoChars(hexString[i], hexString[i + 1]);
            }
            return array;
        }

        private static byte ByteFromTwoChars(char p, char p_2) {
            byte ret;
            if (p <= '9' && p >= '0') {
                ret = (byte)((p - '0') << 4);
            } else if (p <= 'f' && p >= 'a') {
                ret = (byte)((p - 'a' + 10) << 4);
            } else if (p <= 'F' && p >= 'A') {
                ret = (byte)((p - 'A' + 10) << 4);
            } else throw new ArgumentException("Char is not a hex digit: " + p, "p");

            if (p_2 <= '9' && p_2 >= '0') {
                ret |= (byte)((p_2 - '0'));
            } else if (p_2 <= 'f' && p_2 >= 'a') {
                ret |= (byte)((p_2 - 'a' + 10));
            } else if (p_2 <= 'F' && p_2 >= 'A') {
                ret |= (byte)((p_2 - 'A' + 10));
            } else throw new ArgumentException("Char is not a hex digit: " + p_2, "p_2");

            return ret;
        }

        private void rememberMeCheckBox_CheckedChanged(object sender, EventArgs e) {
            Properties.Settings.Default.KeepRefreshToken = rememberMeCheckBox.Checked;
        }

        private void autoStartCheckBox_CheckedChanged(object sender, EventArgs e) {
            Properties.Settings.Default.AutoLaunchAfterLogin = autoStartCheckBox.Checked;
        }
    }
}
