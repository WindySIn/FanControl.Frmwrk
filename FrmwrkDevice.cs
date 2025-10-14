using FanControl.Plugins;

namespace FanControl.Frmwrk
{
    internal class FrmwrkDevice
    {
        public FrmwrkDevice(IPluginLogger logger)
        {
            Logger = logger;

            APUFanSpeedSensor = new APUFanSpeed();
            APUFanDutyControl = new APUFanDuty();
            Sys1FanSpeedSensor = new Sys1FanSpeed();
            Sys1FanDutyControl = new Sys1FanDuty();
            Sys2FanSpeedSensor = new Sys2FanSpeed();
            Sys2FanDutyControl = new Sys2FanDuty();
        }
        public class APUFanSpeed : IPluginSensor
        {
            public string Name => _name;
            readonly string _name;
            public float? Value => _value;
            readonly float? _value;
            public string Id => _id;
            readonly string _id;

            public APUFanSpeed()
            {
                _name = "APU Fan Speed";
                _value = FrmwrkCLIWrapper.GetAPUFanSpeed();
                _id = "APU Fan Speed";
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
            readonly float? _value;
            public string Id => _id;
            readonly string _id;

            public APUFanDuty()
            {
                _name = "APU Fan Duty";
                _value = FrmwrkCLIWrapper.GetAPUFanSpeed();
                _id = "APU Fan Duty";
            }

            public void Set(float val)
            {
                FrmwrkCLIWrapper.SetAPUFanDuty((int)val);
            }

            public void Reset()
            {
                var _sys1FanSpeed = FrmwrkCLIWrapper.GetFanSpeeds()[0];
                var _sys2FanSpeed = FrmwrkCLIWrapper.GetFanSpeeds()[2];

                FrmwrkCLIWrapper.ResetFanControl(); // Currently, this resets all fans to auto control, so we need to re-apply the other fan speeds.
                FrmwrkCLIWrapper.SetSys1FanRPM(_sys1FanSpeed);
                FrmwrkCLIWrapper.SetSys2FanRPM(_sys2FanSpeed);
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
            readonly float? _value;
            public string Id => _id;
            readonly string _id;

            public Sys1FanSpeed()
            {
                _name = "Sys1 Fan Speed";
                _value = FrmwrkCLIWrapper.GetSys1FanSpeed();
                _id = "Sys1 Fan Speed";
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
            readonly float? _value;
            public string Id => _id;
            readonly string _id;

            public Sys1FanDuty()
            {
                _name = "Sys1 Fan Duty";
                _value = FrmwrkCLIWrapper.GetSys1FanSpeed();
                _id = "Sys1 Fan Duty";
            }

            public void Set(float val)
            {
                FrmwrkCLIWrapper.SetSys1FanDuty((int)val);
            }

            public void Reset()
            {
                var _apuFanSpeed = FrmwrkCLIWrapper.GetFanSpeeds()[1];
                var _sys2FanSpeed = FrmwrkCLIWrapper.GetFanSpeeds()[2];

                FrmwrkCLIWrapper.ResetFanControl(); // Currently, this resets all fans to auto control, so we need to re-apply the other fan speeds.
                FrmwrkCLIWrapper.SetAPUFanRPM(_apuFanSpeed);
                FrmwrkCLIWrapper.SetSys2FanRPM(_sys2FanSpeed);
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
            readonly float? _value;
            public string Id => _id;
            readonly string _id;

            public Sys2FanSpeed()
            {
                _name = "Sys2 Fan Speed";
                _value = FrmwrkCLIWrapper.GetSys2FanSpeed();
                _id = "Sys2 Fan Speed";
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
            readonly float? _value;
            public string Id => _id;
            readonly string _id;

            public Sys2FanDuty()
            {
                _name = "Sys2 Fan Duty";
                _value = FrmwrkCLIWrapper.GetSys2FanSpeed();
                _id = "Sys2 Fan Duty";
            }

            public void Set(float val)
            {
                FrmwrkCLIWrapper.SetSys2FanDuty((int)val);
            }

            public void Reset()
            {
                var _apuFanSpeed = FrmwrkCLIWrapper.GetFanSpeeds()[1];
                var _sys1FanSpeed = FrmwrkCLIWrapper.GetFanSpeeds()[0];

                FrmwrkCLIWrapper.ResetFanControl(); // Currently, this resets all fans to auto control, so we need to re-apply the other fan speeds.
                FrmwrkCLIWrapper.SetAPUFanRPM(_apuFanSpeed);
                FrmwrkCLIWrapper.SetSys1FanRPM(_sys1FanSpeed);
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