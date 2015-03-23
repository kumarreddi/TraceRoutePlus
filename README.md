# Traceroute Plus

Traceroute Plus is a traceroute utility I wrote for educational purposes in C&sharp;. Usage information can be found by calling it without any parameters: `tracerouteplus.exe`.

## Download
Currently binaries are only available for windows, but it should compile fine with Mono too.

Name			| Link
----------------|-------------------------------
Release Binary	| [TraceRoutePlus.exe](https://github.com/sbrl/TraceRoutePlus/raw/master/TraceRoutePlus/bin/Release/TraceRoutePlus.exe)
Debug Binary	| [TraceRoutePlus.exe](https://github.com/sbrl/TraceRoutePlus/raw/master/TraceRoutePlus/bin/Debug/TraceRoutePlus.exe)

## Building
You can either build this with Visual Studio 2013, mono (gmcs I think?), or `csc.exe`.

### Visual Studio 2013
Open the project, and then go to `Build -> Build Solution`, or press `CTRL + SHIFT + B`.

### csc.exe
Execute this in the `TraceRoutePlus` folder:
```
...\TraceRoutePlus> csc /out:TraceRoutePlus.exe *.cs
```

## Examples
Example traceroute:
```bash
C:\>tracerouteplus github.com
Traceroute Plus
---------------
By Starbeamrainbowlabs <https://starbeamrainbowlabs.com>

=== github.com ===
 1: xxx.xxx.xxx.xxx 1ms
 2: xxx.xxx.xxx.xxx 33ms
 3: xxx.xxx.xxx.xxx 36ms
 4: xxx.xxx.xxx.xxx 54ms
 5: 4.69.149.18     119ms
 6: 4.53.116.102    115ms
 7: 192.30.252.207  118ms
 8: 192.30.252.130   118ms
=== github.com end ===
```
(Some ip addresses have been blanked out for security)

## Todo
 - (optional) DNS lookups
 - ...? (create an issue, or even better a pull request!)