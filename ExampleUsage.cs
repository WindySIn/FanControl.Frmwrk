using System;

public static class ExampleUsage
{

    private static string sample =
@"F75303_APU:   44 C
F75303_DDR:   46 C
F75303_AMB:   45 C
APU:          47 C
Temp 4:       47 C
Fan Speed:     0 RPM
Fan Speed:  2357 RPM
Fan Speed:     0 RPM";
    public static void ParseToJsonDemo()
    {
        var json = TemperatureParser.ParseToJson(sample, new Dictionary<string, object>());
        Console.WriteLine($"JSON-ified output: {json}");
    }
}

/* framework_tool [OPTIONS]
      --flash-gpu-descriptor <fgd> <fgd>

  -v, --verbose...
          Increase logging verbosity
  -q, --quiet...
          Decrease logging verbosity
      --versions
          List current firmware versions
      --version
          Show tool version information (Add -vv for more details)
      --features
          Show features support by the firmware
      --esrt
          Display the UEFI ESRT table
      --device <DEVICE>
          [possible values: bios, ec, pd0, pd1, rtm01, rtm23, ac-left, ac-right]
      --compare-version <COMPARE_VERSION>

      --power
          Show current power status of battery and AC (Add -vv for more details)
      --thermal
          Print thermal information (Temperatures and Fan speed)
      --sensors
          Print sensor information (ALS, G-Sensor)
      --fansetduty [<FANSETDUTY>...]
          Set fan duty cycle (0-100%)
      --fansetrpm [<FANSETRPM>...]
          Set fan RPM (limited by EC fan table max RPM)
      --autofanctrl
          Turn on automatic fan speed control
      --pdports
          Show information about USB-C PD ports
      --info
          Show info from SMBIOS (Only on UEFI)
      --pd-info
          Show details about the PD controllers
      --pd-reset <PD_RESET>
          Reset a specific PD controller (for debugging only)
      --pd-disable <PD_DISABLE>
          Disable all ports on a specific PD controller (for debugging only)
      --pd-enable <PD_ENABLE>
          Enable all ports on a specific PD controller (for debugging only)
      --dp-hdmi-info
          Show details about connected DP or HDMI Expansion Cards
      --dp-hdmi-update <UPDATE_BIN>
          Update the DisplayPort or HDMI Expansion Card
      --audio-card-info
          Show details about connected Audio Expansion Cards (Needs root privileges)
      --privacy
          Show privacy switch statuses (camera and microphone)
      --pd-bin <PD_BIN>
          Parse versions from PD firmware binary file
      --ec-bin <EC_BIN>
          Parse versions from EC firmware binary file
      --capsule <CAPSULE>
          Parse UEFI Capsule information from binary file
      --dump <DUMP>
          Dump extracted UX capsule bitmap image to a file
      --h2o-capsule <H2O_CAPSULE>
          Parse UEFI Capsule information from binary file
      --dump-ec-flash <DUMP_EC_FLASH>
          Dump EC flash contents
      --flash-ec <FLASH_EC>
          Flash EC (RO+RW) with new firmware from file - may render your hardware unbootable!
      --flash-ro-ec <FLASH_RO_EC>
          Flash EC with new RO firmware from file - may render your hardware unbootable!
      --flash-rw-ec <FLASH_RW_EC>
          Flash EC with new RW firmware from file
      --intrusion
          Show status of intrusion switch
      --inputdeck
          Show status of the input modules (Framework 16 only)
      --inputdeck-mode <INPUTDECK_MODE>
          Set input deck power mode [possible values: auto, off, on] (Framework 16 only) [possible values: auto, off, on]
      --expansion-bay
          Show status of the expansion bay (Framework 16 only)
      --charge-limit [<CHARGE_LIMIT>]
          Get or set max charge limit
      --charge-current-limit <CHARGE_CURRENT_LIMIT>...
          Set max charge current limit
      --charge-rate-limit <CHARGE_RATE_LIMIT>...
          Set max charge current limit
      --get-gpio [<GET_GPIO>]
          Get GPIO value by name or all, if no name provided
      --fp-led-level [<FP_LED_LEVEL>]
          Get or set fingerprint LED brightness level [possible values: high, medium, low, ultra-low, auto]
      --fp-brightness [<FP_BRIGHTNESS>]
          Get or set fingerprint LED brightness percentage
      --kblight [<KBLIGHT>]
          Set keyboard backlight percentage or get, if no value provided
      --remap-key <REMAP_KEY> <REMAP_KEY> <REMAP_KEY>
          Remap a key by changing the scancode
      --rgbkbd <RGBKBD> <RGBKBD>...
          Set the color of <key> to <RGB>. Multiple colors for adjacent keys can be set at once. <key> <RGB> [<RGB> ...] Example: 0 0xFF000 0x00FF00 0x0000FF
      --tablet-mode <TABLET_MODE>
          Set tablet mode override [possible values: auto, tablet, laptop]
      --touchscreen-enable <TOUCHSCREEN_ENABLE>
          Enable/disable touchscreen [possible values: true, false]
      --stylus-battery
          Check stylus battery level (USI 2.0 stylus only)
      --console <CONSOLE>
          Get EC console, choose whether recent or to follow the output [possible values: recent, follow]
      --reboot-ec <REBOOT_EC>
          Control EC RO/RW jump [possible values: reboot, jump-ro, jump-rw, cancel-jump, disable-jump]
      --ec-hib-delay [<EC_HIB_DELAY>]
          Get or set EC hibernate delay (S5 to G3)
      --hash <HASH>
          Hash a file of arbitrary data
      --driver <DRIVER>
          Select which driver is used. By default portio is used [possible values: portio, cros-ec, windows]
      --pd-addrs <PD_ADDRS> <PD_ADDRS> <PD_ADDRS>
          Specify I2C addresses of the PD chips (Advanced)
      --pd-ports <PD_PORTS> <PD_PORTS> <PD_PORTS>
          Specify I2C ports of the PD chips (Advanced)
  -t, --test
          Run self-test to check if interaction with EC is possible
  -f, --force
          Force execution of an unsafe command - may render your hardware unbootable!
      --dry-run
          Simulate execution of a command (e.g. --flash-ec)
      --flash-gpu-descriptor-file <FLASH_GPU_DESCRIPTOR_FILE>
          File to write to the gpu EEPROM
      --dump-gpu-descriptor-file <DUMP_GPU_DESCRIPTOR_FILE>
          File to dump the gpu EEPROM to
  -h, --help
          Print help
*/