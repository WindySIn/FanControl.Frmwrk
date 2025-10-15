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
            APUFanDutyControl = new APUFanDuty();
            Sys1FanSpeedSensor = new Sys1FanSpeed();
            Sys1FanDutyControl = new Sys1FanDuty();
            Sys2FanSpeedSensor = new Sys2FanSpeed();
            Sys2FanDutyControl = new Sys2FanDuty();
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
                FrmwrkCLIWrapper.GetAPUFanSpeed();
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

            public APUFanDuty()
            {
                _name = "APU Fan Duty";
                _value = null;
                _id = "frmwrk/duty/apu";
            }

            public void Set(float val)
            {
                FrmwrkCLIWrapper.SetAPUFanDuty((int)val);
                _value = val;
            }

            public void Reset(bool ResetAllFansToAuto)
            {
                FrmwrkCLIWrapper.ResetFanControl(); // Currently, this resets all fans to auto control, so we need to re-apply the other fan speeds if only one fan is being reset.
                if (!ResetAllFansToAuto)
                {
                    FrmwrkCLIWrapper.SetSys1FanRPM(FrmwrkCLIWrapper.GetSys1FanSpeed());
                    FrmwrkCLIWrapper.SetSys2FanRPM(FrmwrkCLIWrapper.GetSys2FanSpeed());
                }
                _value = null;
            }

            public void Reset()
            {
                Reset(false);
            }

            public void Update()
            {
                FrmwrkCLIWrapper.GetAPUFanSpeed();
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
                FrmwrkCLIWrapper.GetSys1FanSpeed();
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

            public Sys1FanDuty()
            {
                _name = "Sys1 Fan Duty";
                _value = null;
                _id = "frmwrk/duty/sys1";
            }

            public void Set(float val)
            {
                FrmwrkCLIWrapper.SetSys1FanDuty((int)val);
                _value = val;
            }

            public void Reset(bool ResetAllFansToAuto)
            {
                FrmwrkCLIWrapper.ResetFanControl(); // Currently, this resets all fans to auto control, so we need to re-apply the other fan speeds if only one fan is being reset.
                if (!ResetAllFansToAuto)
                {
                    FrmwrkCLIWrapper.SetAPUFanRPM(FrmwrkCLIWrapper.GetAPUFanSpeed());
                    FrmwrkCLIWrapper.SetSys2FanRPM(FrmwrkCLIWrapper.GetSys2FanSpeed());
                }
                _value = null;
            }

            public void Reset()
            {
                Reset(false);
            }

            public void Update()
            {
                FrmwrkCLIWrapper.GetSys1FanSpeed();
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
                FrmwrkCLIWrapper.GetSys2FanSpeed();
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

            public Sys2FanDuty()
            {
                _name = "Sys2 Fan Duty";
                _value = null;
                _id = "frmwrk/duty/sys2";
            }

            public void Set(float val)
            {
                FrmwrkCLIWrapper.SetSys2FanDuty((int)val);
                _value = val;
            }

            public void Reset(bool ResetAllFansToAuto)
            {
                FrmwrkCLIWrapper.ResetFanControl(); // Currently, this resets all fans to auto control, so we need to re-apply the other fan speeds if only one fan is being reset.
                if (!ResetAllFansToAuto)
                {
                    FrmwrkCLIWrapper.SetAPUFanRPM(FrmwrkCLIWrapper.GetAPUFanSpeed());
                    FrmwrkCLIWrapper.SetSys1FanRPM(FrmwrkCLIWrapper.GetSys1FanSpeed());
                }
                _value = null;
            }

            public void Reset()
            {
                Reset(false);
            }

            public void Update()
            {
                FrmwrkCLIWrapper.GetSys2FanSpeed();
            }
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