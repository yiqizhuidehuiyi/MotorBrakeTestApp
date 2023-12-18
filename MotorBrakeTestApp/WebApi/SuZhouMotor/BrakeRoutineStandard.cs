using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorBrakeTestApp.WebApi.SuZhouMotor
{
    public class BrakeRoutineStandard
    {
        public int id { get; set; }//id
        public string identifier { get; set; }//型号
        public int resistance_duration { get; set; }//电阻测试时间
        public int reference_temperature { get; set; }//基准温度
        public double resistance_coefficient { get; set; }//电阻温度系数
        public int insulation_resistance_duration { get; set; }//绝缘电阻测试时间
        public int insulation_resistance_voltage { get; set; }//绝缘电阻测试电压
        public int insulation_resistance_min { get; set; }//绝缘电阻min
        public int with_stand_duration { get; set; }//耐压测试时间
        public int with_stand_voltage { get; set; }//耐压电压
        public int leakage_current_max { get; set; }//泄露电流max
        public int full_voltage_time { get; set; }
        public int decreased_voltage_time { get; set; }
        public double decreased_voltage_ratio { get; set; }
        public int grinding_speed { get; set; }
        public int grinding_time_min { get; set; }
        public int grinding_time_max { get; set; }
        public double residual_torque_max { get; set; }
        public string notes { get; set; }//说明
    }
}
