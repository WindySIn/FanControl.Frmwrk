using FanControl.Plugins;

namespace FanControl.Frmwrk
{
    internal class FrmwrkDevice
    {
        public FrmwrkDevice(IPluginLogger logger)
        {
            Logger = logger;

            FrmwrkCLIWrapper.Initialize();

            APUFanSpeedSensor = new APUFanSpeed();
            APUFanDutyControl = new APUFanDuty(this);
            Sys1FanSpeedSensor = new Sys1FanSpeed();
            Sys1FanDutyControl = new Sys1FanDuty(this);
            Sys2FanSpeedSensor = new Sys2FanSpeed();
            Sys2FanDutyControl = new Sys2FanDuty(this);
        }

        public void Reset()
        {
            FrmwrkCLIWrapper.ResetFanControl();
        }
        public class APUFanSpeed : IPluginSensor
        {
            public string Name => _name;
            readonly string _name;
            public float? Value => _value;
            float? _value;
            public string Id => _id;
            readonly string _id;

            public APUFanSpeed()
            {
                _name = "APU Fan Speed";
                _value = FrmwrkCLIWrapper.GetAPUFanSpeed();
                _id = "frmwrk/speed/apu";
            }

            public void Update()
            {
                _value = FrmwrkCLIWrapper.GetAPUFanSpeed();
            }
        }
        public class APUFanDuty : IPluginControlSensor
        {
            public string Name => _name;
            readonly string _name;
            public float? Value => _value;
            float? _value;
            public string Id => _id;
            readonly string _id;
            readonly FrmwrkDevice _device;

            public APUFanDuty(FrmwrkDevice device)
            {
                _name = "APU Fan Duty";
                _value = null;
                _id = "frmwrk/duty/apu";
                _device = device;
            }

            public void Set(float val)
            {
                if (val != _value)
                {    
                    FrmwrkCLIWrapper.SetAPUFanDuty((int)val);
                    _value = val;
                }
            }

            public void Reset()
            {
                FrmwrkDevice.ResetFanControls(_device);
            }

            public void Update()
            {
                // _value = FrmwrkCLIWrapper.GetAPUFanSpeed();
            }

            internal void Nullify()
            {
                _value = null;
            }
        }

        public class Sys1FanSpeed : IPluginSensor
        {
            public string Name => _name;
            readonly string _name;
            public float? Value => _value;
            float? _value;
            public string Id => _id;
            readonly string _id;

            public Sys1FanSpeed()
            {
                _name = "Sys1 Fan Speed";
                _value = FrmwrkCLIWrapper.GetSys1FanSpeed();
                _id = "frmwrk/speed/sys1";
            }

            public void Update()
            {
                _value = FrmwrkCLIWrapper.GetSys1FanSpeed();
            }
        }
        public class Sys1FanDuty : IPluginControlSensor
        {
            public string Name => _name;
            readonly string _name;
            public float? Value => _value;
            float? _value;
            public string Id => _id;
            readonly string _id;
            FrmwrkDevice _device;

            public Sys1FanDuty(FrmwrkDevice device)
            {
                _name = "Sys1 Fan Duty";
                _value = null;
                _id = "frmwrk/duty/sys1";
                _device = device;
            }

            public void Set(float val)
            {
                if (val != _value)
                {
                    FrmwrkCLIWrapper.SetSys1FanDuty((int)val);
                    _value = val;
                }
            }

            public void Reset()
            {
                FrmwrkDevice.ResetFanControls(_device);
            }

            public void Update()
            {
                // _value = FrmwrkCLIWrapper.GetSys1FanSpeed();
            }

            internal void Nullify()
            {
                _value = null;
            }
        }

        public class Sys2FanSpeed : IPluginSensor
        {
            public string Name => _name;
            readonly string _name;
            public float? Value => _value;
            float? _value;
            public string Id => _id;
            readonly string _id;

            public Sys2FanSpeed()
            {
                _name = "Sys2 Fan Speed";
                _value = FrmwrkCLIWrapper.GetSys2FanSpeed();
                _id = "frmwrk/speed/sys2";
            }

            public void Update()
            {
                _value = FrmwrkCLIWrapper.GetSys2FanSpeed();
            }
        }
        public class Sys2FanDuty : IPluginControlSensor
        {
            public string Name => _name;
            readonly string _name;
            public float? Value => _value;
            float? _value;
            public string Id => _id;
            readonly string _id;
            FrmwrkDevice _device;

            public Sys2FanDuty(FrmwrkDevice device)
            {
                _name = "Sys2 Fan Duty";
                _value = null;
                _id = "frmwrk/duty/sys2";
                _device = device;
            }

            public void Set(float val)
            {
                if (val != _value)
                {
                    FrmwrkCLIWrapper.SetSys2FanDuty((int)val);
                    _value = val;
                }
            }

            public void Reset()
            {
                FrmwrkDevice.ResetFanControls(_device);
            }

            public void Update()
            {
                // _value = FrmwrkCLIWrapper.GetSys2FanSpeed();
            }

            internal void Nullify()
            {
                _value = null;
            }
        }

        private static void ResetFanControls(FrmwrkDevice device)
        {
            // TODO: Revisit using enum to specify which fan to reset and then calling updates on the other fans to re-apply their speeds
            device.APUFanDutyControl.Nullify();
            device.Sys1FanDutyControl.Nullify();
            device.Sys2FanDutyControl.Nullify();
            FrmwrkCLIWrapper.ResetFanControl(); // Currently, this resets all fans to auto control, so we need to re-apply the other fan speeds if only one fan is being reset.
        }
        
        public APUFanSpeed APUFanSpeedSensor;
        public APUFanDuty APUFanDutyControl;
        public Sys1FanSpeed Sys1FanSpeedSensor;
        public Sys1FanDuty Sys1FanDutyControl;
        public Sys2FanSpeed Sys2FanSpeedSensor;
        public Sys2FanDuty Sys2FanDutyControl;
        internal IPluginLogger Logger;
    }
}
