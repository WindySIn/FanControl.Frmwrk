using FanControl.Plugins;

namespace FanControl.Frmwrk
{
    public class FrmwrkPlugin : IPlugin2
    {
        public string Name => "FrmwrkPlugin";
        internal IPluginLogger Logger;
        FrmwrkDevice? device;

        public FrmwrkPlugin(IPluginLogger logger)
        {
            Logger = logger;
        }

        public void Initialize()
        {
            device = new FrmwrkDevice(Logger);
            FrmwrkCLIWrapper.Logger = Logger;
        }

        public void Load(IPluginSensorsContainer container)
        {
            if (device == null)
            {
                throw new InvalidOperationException("FrmwrkPlugin not initialized before Load() call.");
            }
            else
            {
                FrmwrkCLIWrapper.Update();

                container.FanSensors.Add(device.APUFanSpeedSensor);
                container.ControlSensors.Add(device.APUFanDutyControl);

                container.FanSensors.Add(device.Sys1FanSpeedSensor);
                container.ControlSensors.Add(device.Sys1FanDutyControl);

                container.FanSensors.Add(device.Sys2FanSpeedSensor);
                container.ControlSensors.Add(device.Sys2FanDutyControl);
            }
        }

        public void Close()
        {
            if (device == null)
            {
                throw new InvalidOperationException("FrmwrkPlugin not initialized before Close() call.");
            }
            else
            {
                device.Reset();
            }
        }

        public void Update()
        {
            if (device == null)
            {
                throw new InvalidOperationException("FrmwrkPlugin not initialized before Update() call.");
            }
            else
            {
                FrmwrkCLIWrapper.Update();

                device.APUFanSpeedSensor.Update();
                device.APUFanDutyControl.Update();
                device.Sys1FanSpeedSensor.Update();
                device.Sys1FanDutyControl.Update();
                device.Sys2FanSpeedSensor.Update();
                device.Sys2FanDutyControl.Update();
            }
        }
    }    
}
