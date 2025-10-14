using FanControl.Plugins;

namespace FanControl.Frmwrk
{
    public class FrmwrkPlugin : IPlugin2
    {
        public string Name => "FrmwrkPlugin";
        internal IPluginLogger Logger;
        FrmwrkDevice device;

        public FrmwrkPlugin(IPluginLogger logger)
        {
            Logger = logger;
            device = new FrmwrkDevice(logger);
        }

        public void Initialize()
        {
            // Initialization code here
        }

        public void Load(IPluginSensorsContainer container)
        {
            container.FanSensors.Add(device.APUFanSpeedSensor);
            container.ControlSensors.Add(device.APUFanDutyControl);

            container.FanSensors.Add(device.Sys1FanSpeedSensor);
            container.ControlSensors.Add(device.Sys1FanDutyControl);

            container.FanSensors.Add(device.Sys2FanSpeedSensor);
            container.ControlSensors.Add(device.Sys2FanDutyControl);
        }

        public void Close()
        {
            // Cleanup code here
        }

        public void Update()
        {
            FrmwrkCLIWrapper.Update();
            
            device.APUFanSpeedSensor.Update();
            device.Sys1FanSpeedSensor.Update();
            device.Sys2FanSpeedSensor.Update();
        }
    }
}