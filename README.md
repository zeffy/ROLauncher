# ROLauncher

Launcher for Revelation Online NA/EU. This lets you start the game without needing to go through My.com Game Center.

# Features

* Auto-start the game after *n* seconds after you log in (you can edit this value in the `ROLauncher.exe.config` file under the `AutoLaunchDelayInSeconds` setting, the default is 5 seconds)
* Saves your credentials securely (your password is never saved anywhere after you log in for the first time)
* Notifies you if there is a new game update available, and gives you the option to start My.com Game Center to initiate it (this can be disabled with the `ROLauncher.exe.config` file under the `CheckForROUpdates` setting)

# Disclaimer

I have not been banned while developing this tool or using it, however please note that in the My.com Games [Terms of Service](https://legal.my.com/us/games/eula/) it states the following:

```
3.4 You are not permitted to:

  - modify, adapt, decompile, disassemble, change the Game Services or any of its components;
...
  - attempt to circumvent any security measures adopted in the Game Services, including blocking access by IP-address;
```

### Other findings

While reversing the auth protocol, I noticed that both Revelation Online (`tianyu.exe`) and My.com Game Center (`MyComGames.exe`) connect to at least a few hosts that seem to be irrelevant to the operation of the game.

* [`stat.gc.my.com`](https://whois.domaintools.com/stat.gc.my.com) - Lots of requests from `MyComGames.exe`. Probably used for software analytics.
* [`ac.ro.gmru.net`](https://whois.domaintools.com/ac.ro.gmru.net) - `tianyu.exe` sends many requests and receives responses to them, all encrypted (on top of HTTPS). I haven't had time to poke around and figure out how to decrypt the contents yet, so I'm not sure what it is.
* [`gad.netease.com`](https://whois.domaintools.com/gad.netease.com) - `tianyu.exe` sends uniquely identifying information unencrypted over HTTP including your MAC address and My.com UID.

Blocking these hosts with your `hosts` file or firewall doesn't appear to result in any odd behaviour in-game, and I personally highly recommend doing so. <sup>yes I'm paranoid</sup>
