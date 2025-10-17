using System;
using System.Diagnostics;
using System.Threading.Tasks;
using FanControl.Plugins;

namespace FanControl.Frmwrk
{
    internal static class FrmwrkCLIWrapper
    {
        public static string frmwrk_tools_executable = "Plugins\\Frmwrk\\framework_tool.exe";

        public static Dictionary<string, object> ThermalData = new(StringComparer.OrdinalIgnoreCase);
        public static List<int> FanSpeeds = [];

        public static IPluginLogger? Logger;

        internal static void Initialize()
        {
            GetThermalData();
        }

        internal static void Update()
        {
            ThermalData.Clear();
            FanSpeeds.Clear();

            GetThermalData();
        }

        private static string FrmwrkToolsCall(string arguments)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = frmwrk_tools_executable,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using Process process = new Process { StartInfo = startInfo };
            process.Start();
            process.StandardInput.WriteLine(); // Send newline to close the framework_tool CLI
            process.StandardInput.Close();
            string output = process.StandardOutput.ReadToEnd();
            string error = process.StandardError.ReadToEnd();

            if (process.ExitCode != 0)
            {
                throw new Exception($"Error executing {frmwrk_tools_executable} with arguments '{arguments}': {error}");
            }

            return output;
        }

        internal static void GetThermalData()
        {
            TemperatureParser.ParseToDictionary(FrmwrkToolsCall("--thermal"), ThermalData);

            foreach (var entry in ThermalData
                    .Where(kv => kv.Key.StartsWith("Fan Speed", StringComparison.OrdinalIgnoreCase))
                    .Select(kv => kv.Value)
                    .Where(s => s != null)
                    .ToList())
                {
                    if (entry is List<string> speedList)
                    {
                        foreach (var speedStr in speedList)
                        {
                            if (int.TryParse(speedStr.Trim(), out int speed))
                            {
                                FanSpeeds.Add(speed);
                            }
                        }
                    }
                    else if (entry is string speedStr)
                    {
                        if (int.TryParse(speedStr.Trim(), out int speed))
                        {
                            FanSpeeds.Add(speed);
                        }
                    }
                }
        }
        
        internal static int GetAPUFanSpeed()
        {
            return FanSpeeds[0]; // If there are multiple fans, the first fan speed is always the APU fan. However if only the APU fan is plugged in, it's then the **second** fan. Why, Framework, why...
        }

        internal static int GetSys1FanSpeed()
        {
            return FanSpeeds[1]; // The first fan speed is always the System1 fan
        }

        internal static int GetSys2FanSpeed()
        {
            return FanSpeeds[2]; // The third fan speed is always the System2 fan
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
            SetFanDuty(0, duty);
        }

        internal static void SetSys1FanDuty(int duty)
        {
            SetFanDuty(1, duty);
        }

        internal static void SetSys2FanDuty(int duty)
        {
            SetFanDuty(2, duty);
        }

        internal static void SetAPUFanRPM(int rpm)
        {
            SetFanRPM(0, rpm);
        }

        internal static void SetSys1FanRPM(int rpm)
        {
            SetFanRPM(1, rpm);
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
