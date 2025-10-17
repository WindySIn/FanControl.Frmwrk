using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace FanControl.Frmwrk
{
    internal class TestCLIWrapper
    {
        public static string cli_tools_executable = "D:\\framework_tool.exe";

        internal TestCLIWrapper()
        {
            // Constructor logic if needed
        }
        
        public async Task<String> RunCLICommandAsync(string arguments)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = cli_tools_executable,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using Process process = new Process { StartInfo = startInfo };
            process.Start();

            await process.StandardInput.WriteLineAsync(); // Send newline to close the framework_tool CLI
            process.StandardInput.Close();
            
            string output = await process.StandardOutput.ReadToEndAsync();
            string error = await process.StandardError.ReadToEndAsync();
            
            await process.WaitForExitAsync();

            if (!string.IsNullOrEmpty(error))
            {
                throw new Exception($"Error executing command: {error}");
            }

            return output.Trim();
        }
    }

    public class TestProgram
    {
        public static async Task Main(string[] args)
        {
            TestCLIWrapper cliWrapper = new TestCLIWrapper();
            try
            {
                string result = await cliWrapper.RunCLICommandAsync("--thermal");
                var resultProcessed = TemperatureParser.ParseToDictionary(result, new Dictionary<string, object>());

                List<int> fanSpeeds = new List<int>();

                foreach (var entry in resultProcessed
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
                                fanSpeeds.Add(speed);
                                Console.WriteLine($"Fan Speed: {speed}");
                            }
                        }
                    }
                    else if (entry is string speedStr)
                    {
                        if (int.TryParse(speedStr.Trim(), out int speed))
                        {
                            fanSpeeds.Add(speed);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
