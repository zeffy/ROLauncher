//------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
//------------------------------------------------------------

using System.Diagnostics;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using System.Text;

namespace System.Runtime
{

    //copied from System.Web.HttpUtility code (renamed here) to remove dependency on System.Web.dll
    internal static class UrlUtility
    {
        //  Query string parsing support
        public static NameValueCollection ParseQueryString(string query) {
            return ParseQueryString(query, Encoding.UTF8);
        }

        public static NameValueCollection ParseQueryString(string query, Encoding encoding) {
            if (query == null) {
                throw new ArgumentNullException("query");
            }

            if (encoding == null) {
                throw new ArgumentNullException("encoding");
            }

            if (query.Length > 0 && query[0] == '?') {
                query = query.Substring(1);
            }

            return new HttpValueCollection(query, encoding);
        }

        private static string UrlEncodeUnicodeStringToStringInternal(string s, bool ignoreAscii) {

            if (s == null)
                return null;

            int l = s.Length;
            StringBuilder sb = new StringBuilder(l);

            for (int i = 0; i < l; i++) {
                char ch = s[i];

                if ((ch & 0xff80) == 0) {  // 7 bit?
                    if (ignoreAscii || IsSafe(ch)) {
                        sb.Append(ch);
                    } else if (ch == ' ') {
                        sb.Append('+');
                    } else {
                        sb.Append('%');
                        sb.Append(IntToHex((ch >> 4) & 0xf));
                        sb.Append(IntToHex((ch) & 0xf));
                    }
                } else { // arbitrary Unicode?
                    sb.Append("%u");
                    sb.Append(IntToHex((ch >> 12) & 0xf));
                    sb.Append(IntToHex((ch >> 8) & 0xf));
                    sb.Append(IntToHex((ch >> 4) & 0xf));
                    sb.Append(IntToHex((ch) & 0xf));
                }
            }

            return sb.ToString();
        }

        private static string UrlDecodeStringFromStringInternal(string s, Encoding e) {
            if (s == null) {
                return null;
            }

            int count = s.Length;
            UrlDecoder helper = new UrlDecoder(count, e);

            // go through the string's chars collapsing %XX and %uXXXX and
            // appending each char as char, with exception of %XX constructs
            // that are appended as bytes

            for (int pos = 0; pos < count; pos++) {
                char ch = s[pos];

                if (ch == '+') {
                    ch = ' ';
                } else if (ch == '%' && pos < count - 2) {
                    if (s[pos + 1] == 'u' && pos < count - 5) {
                        int h1 = HexToInt(s[pos + 2]);
                        int h2 = HexToInt(s[pos + 3]);
                        int h3 = HexToInt(s[pos + 4]);
                        int h4 = HexToInt(s[pos + 5]);

                        if (h1 >= 0 && h2 >= 0 && h3 >= 0 && h4 >= 0) {   // valid 4 hex chars
                            ch = (char)((h1 << 12) | (h2 << 8) | (h3 << 4) | h4);
                            pos += 5;

                            // only add as char
                            helper.AddChar(ch);
                            continue;
                        }
                    } else {
                        int h1 = HexToInt(s[pos + 1]);
                        int h2 = HexToInt(s[pos + 2]);

                        if (h1 >= 0 && h2 >= 0) {     // valid 2 hex chars
                            byte b = (byte)((h1 << 4) | h2);
                            pos += 2;

                            // don't add as char
                            helper.AddByte(b);
                            continue;
                        }
                    }
                }

                if ((ch & 0xFF80) == 0) {
                    helper.AddByte((byte)ch); // 7 bit have to go as bytes because of Unicode
                } else {
                    helper.AddChar(ch);
                }
            }

            return helper.GetString();
        }

        // Private helpers for URL encoding/decoding
        private static int HexToInt(char h) {
            return (h >= '0' && h <= '9') ? h - '0' :
            (h >= 'a' && h <= 'f') ? h - 'a' + 10 :
            (h >= 'A' && h <= 'F') ? h - 'A' + 10 :
            -1;
        }

        private static char IntToHex(int n) {
            Debug.Assert(n < 0x10, "n < 0x10");

            if (n <= 9) {
                return (char)(n + (int)'0');
            } else {
                return (char)(n - 10 + (int)'a');
            }
        }

        // Set of safe chars, from RFC 1738.4 minus '+'
        internal static bool IsSafe(char ch) {
            if (ch >= 'a' && ch <= 'z' || ch >= 'A' && ch <= 'Z' || ch >= '0' && ch <= '9') {
                return true;
            }

            switch (ch) {
                case '-':
                case '_':
                case '.':
                case '!':
                case '*':
                case '\'':
                case '(':
                case ')':
                    return true;
            }

            return false;
        }

        // Internal class to facilitate URL decoding -- keeps char buffer and byte buffer, allows appending of either chars or bytes
        private class UrlDecoder
        {
            private int _bufferSize;

            // Accumulate characters in a special array
            private int _numChars;

            private char[] _charBuffer;

            // Accumulate bytes for decoding into characters in a special array
            private int _numBytes;

            private byte[] _byteBuffer;

            // Encoding to convert chars to bytes
            private Encoding _encoding;

            private void FlushBytes() {
                if (_numBytes > 0) {
                    _numChars += _encoding.GetChars(_byteBuffer, 0, _numBytes, _charBuffer, _numChars);
                    _numBytes = 0;
                }
            }

            internal UrlDecoder(int bufferSize, Encoding encoding) {
                _bufferSize = bufferSize;
                _encoding = encoding;

                _charBuffer = new char[bufferSize];
                // byte buffer created on demand
            }

            internal void AddChar(char ch) {
                if (_numBytes > 0) {
                    FlushBytes();
                }

                _charBuffer[_numChars++] = ch;
            }

            internal void AddByte(byte b) {
                // if there are no pending bytes treat 7 bit bytes as characters
                // this optimization is temp disable as it doesn't work for some encodings

                //if (_numBytes == 0 && ((b & 0x80) == 0)) {
                //    AddChar((char)b);
                //}
                //else

                {
                    if (_byteBuffer == null) {
                        _byteBuffer = new byte[_bufferSize];
                    }

                    _byteBuffer[_numBytes++] = b;
                }
            }

            internal string GetString() {
                if (_numBytes > 0) {
                    FlushBytes();
                }

                if (_numChars > 0) {
                    return new String(_charBuffer, 0, _numChars);
                } else {
                    return string.Empty;
                }
            }
        }

        [Serializable]
        public class HttpValueCollection : NameValueCollection
        {
            public HttpValueCollection()
                : base() {
            }

            internal HttpValueCollection(string str, Encoding encoding)
                : base(StringComparer.OrdinalIgnoreCase) {
                if (!string.IsNullOrEmpty(str)) {
                    FillFromString(str, true, encoding);
                }

                IsReadOnly = false;
            }

            protected HttpValueCollection(SerializationInfo info, StreamingContext context)
                : base(info, context) {
            }

            internal void FillFromString(string s, bool urlencoded, Encoding encoding) {
                int l = (s != null) ? s.Length : 0;
                int i = 0;

                while (i < l) {
                    // find next & while noting first = on the way (and if there are more)

                    int si = i;
                    int ti = -1;

                    while (i < l) {
                        char ch = s[i];

                        if (ch == '=') {
                            if (ti < 0)
                                ti = i;
                        } else if (ch == '&') {
                            break;
                        }

                        i++;
                    }

                    // extract the name / value pair

                    string name = null;
                    string value = null;

                    if (ti >= 0) {
                        name = s.Substring(si, ti - si);
                        value = s.Substring(ti + 1, i - ti - 1);
                    } else {
                        value = s.Substring(si, i - si);
                    }

                    // add name / value pair to the collection

                    if (urlencoded) {
                        base.Add(
                           UrlUtility.UrlDecodeStringFromStringInternal(name, encoding),
                           UrlUtility.UrlDecodeStringFromStringInternal(value, encoding));
                    } else {
                        base.Add(name, value);
                    }

                    // trailing '&'

                    if (i == l - 1 && s[i] == '&') {
                        base.Add(null, string.Empty);
                    }

                    i++;
                }
            }

            public override string ToString() {
                return ToString(true, null);
            }

            private string ToString(bool urlencoded, IDictionary excludeKeys) {
                int n = Count;
                if (n == 0)
                    return string.Empty;

                StringBuilder s = new StringBuilder();
                string key, keyPrefix, item;

                for (int i = 0; i < n; i++) {
                    key = GetKey(i);

                    if (excludeKeys != null && key != null && excludeKeys[key] != null) {
                        continue;
                    }
                    if (urlencoded) {
                        key = UrlUtility.UrlEncodeUnicodeStringToStringInternal(key, false);
                    }
                    keyPrefix = (!string.IsNullOrEmpty(key)) ? (key + "=") : string.Empty;

                    ArrayList values = (ArrayList)BaseGet(i);
                    int numValues = (values != null) ? values.Count : 0;

                    if (s.Length > 0) {
                        s.Append('&');
                    }

                    if (numValues == 1) {
                        s.Append(keyPrefix);
                        item = (string)values[0];
                        if (urlencoded)
                            item = UrlUtility.UrlEncodeUnicodeStringToStringInternal(item, false);
                        s.Append(item);
                    } else if (numValues == 0) {
                        s.Append(keyPrefix);
                    } else {
                        for (int j = 0; j < numValues; j++) {
                            if (j > 0) {
                                s.Append('&');
                            }
                            s.Append(keyPrefix);
                            item = (string)values[j];
                            if (urlencoded) {
                                item = UrlUtility.UrlEncodeUnicodeStringToStringInternal(item, false);
                            }
                            s.Append(item);
                        }
                    }
                }

                return s.ToString();
            }
        }
    }
}
