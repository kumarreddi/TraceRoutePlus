# Traceroute Plus

Traceroute Plus is a traceroute utility I wrote for educational purposes in C&sharp;. Usage information can be found by calling it without any parameters: `tracerouteplus.exe`.

## Features
 - Configurable ping options:
	 - Timeout
	 - Time to wait between each request
	 - Number of retries for failed pings
	 - Maximum hop count (default: unlimited, tells you when the limit was hit)
 - Tells you what went wrong (DestinationNetworkUnreachable, TimedOut, etc.)
 - Traceroute for multiple domains
	 - Read in domains from stdin
 - Bare output format for scripts
 - Guesses the round trip time for hosts that don't respond based on initial probe ping
	 - This is done by timing the amount of time the initial traceroute probe takes via System.Diagnotics.StopWatch
	 - This is usually out by at least ~10ms though I think because it recourds all the overhead of send the ping request as well

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

## License
TraceRoutePlus is licensed under the **Mozilla Public License 2.0**. A Copy of this license is located in the LICENSE file in the root of this repository.