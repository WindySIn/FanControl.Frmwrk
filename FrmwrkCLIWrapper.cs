using System;
using System.Diagnostics;

namespace FanControl.Frmwrk
{
    internal static class FrmwrkCLIWrapper
    {
        internal static void Initialize()
        {
            // Any initialization code if needed
        }
        
        public static string frmwrk_tools_executable = "Plugins\\framework_tool.exe";

        private static string FrmwrkToolsCall(string arguments)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = frmwrk_tools_executable,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using Process process = new Process { StartInfo = startInfo };
            process.Start();
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();
            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                throw new Exception($"Error executing {frmwrk_tools_executable} with arguments '{arguments}': {error}");
            }

            return output;
        }

        internal static Dictionary<string, object> GetThermal()
        {
            string output = FrmwrkToolsCall($"--thermal");
            return TemperatureParser.ParseToDictionary(output);
        }

        internal static List<int> GetFanSpeeds()
        {
            var speeds = new List<int>();
            foreach (var entry in GetThermal()
                .Where(kv => kv.Key.StartsWith("Fan Speed", StringComparison.OrdinalIgnoreCase))
                .Select(kv => kv.Value?.ToString())
                .Where(s => s != null)
                .ToList())
            {
                if (entry != null && int.TryParse(entry.Trim(), out int speed))
                {
                    speeds.Add(speed);
                }
            }
            return speeds;
        }

        internal static int GetAPUFanSpeed()
        {
            return GetFanSpeeds()[1]; // The second fan speed is always the APU fan
        }

        internal static int GetSys1FanSpeed()
        {
            return GetFanSpeeds()[0]; // The first fan speed is always the System1 fan
        }

        internal static int GetSys2FanSpeed()
        {
            return GetFanSpeeds()[2]; // The third fan speed is always the System2 fan
        }

        internal static void SetFanDuty(int fan, int duty)
        {
            FrmwrkToolsCall($"--fansetduty {fan} {duty}");
        }

        internal static void SetFanRPM(int fan, int rpm)
        {
            FrmwrkToolsCall($"--fansetrpm {fan} {rpm}");
        }

        internal static void SetAPUFanDuty(int duty)
        {
            SetFanDuty(1, duty);
        }

        internal static void SetSys1FanDuty(int duty)
        {
            SetFanDuty(0, duty);
        }

        internal static void SetSys2FanDuty(int duty)
        {
            SetFanDuty(2, duty);
        }

        internal static void SetAPUFanRPM(int rpm)
        {
            SetFanRPM(1, rpm);
        }

        internal static void SetSys1FanRPM(int rpm)
        {
            SetFanRPM(0, rpm);
        }

        internal static void SetSys2FanRPM(int rpm)
        {
            SetFanRPM(2, rpm);
        }

        internal static void ResetFanControl()
        {
            FrmwrkToolsCall($"--autofanctrl");
        }
    }
}