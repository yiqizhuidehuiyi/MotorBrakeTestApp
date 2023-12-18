using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorBrakeTestApp.WebApi.SuZhouMotor
{
    public class BrakeRoutineStandardDetail
    {
        public int id { get; set; }
        public int brake_routine_standard_id { get; set; }
        public int rated_voltage { get; set; }
        public double resistance_bs_min { get; set; }
        public double resistance_bs_max { get; set; }
        public double resistance_ts_min { get; set; }
        public double resistance_ts_max { get; set; }
        public double resistance_bsts_min { get; set; }
        public double resistance_bsts_max { get; set; }
        public double full_voltage_current_min { get; set; }
        public double full_voltage_current_max { get; set; }
        public double full_voltage_power_min { get; set; }
        public double full_voltage_power_max { get; set; }
        public int dc_voltage { get; set; }
        public double decreased_voltage_current_min { get; set; }
        public double decreased_voltage_current_max { get; set; }
        public double decreased_voltage_power_min { get; set; }
        public double decreased_voltage_power_max { get; set; }
        public int brake_rectifier_id { get; set; }

        public SuZhouMotor.BrakeRoutineStandard BrakeRoutineStandard { get; set; }
    }
}
