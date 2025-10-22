Plugin for FanControl to interact with the Framework Desktop's EC using framework_tool.exe

Note: due to a quirk with the way that framework_tool.exe reports fan thermal data, the plugin is currently designed to work with my own setup with 2 fans: APU and GPU. For reasons I don't understand, if you have only the APU fan installed, then the tool reports it as the second fan. If you have two fans installed, then APU fan becomes fan 1 and your other fan becomes fan 2. Bear this in mind when configuring Fan Control.

**Installation**
- Copy the folder into FanControl\Plugins\

**Disclaimer**
Should go without saying, but _use at your own risk_. I've made this for my own use case. At the minimum, I suggest you pay attention to configuration quirks and keep an eye on your system temps when you first use this.
