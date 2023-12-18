using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorBrakeTestApp.WebApi.SuZhouMotor
{
    public class BrakeReportTestBenchData
    {
        /// <summary>
        /// 制动器出厂测试ID
        /// </summary>
        public int brakeRoutineTestId { get; set; }
        /// <summary>
        /// 阻抗是否测试通过
        /// </summary>
        public bool resistancePassed { get; set; }
        /// <summary>
        /// 测试时间 注意格式
        /// </summary>
        public string testTime { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int sequence { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool valid { get; set; }
        /// <summary>
        /// bs电阻
        /// </summary>
        public double resistanceBs { get; set; }
        /// <summary>
        /// ts电阻
        /// </summary>
        public double resistanceTs { get; set; }
        /// <summary>
        /// 温度
        /// </summary>
        public double temperature { get; set; }
        /// <summary>
        /// 绝缘电阻电压
        /// </summary>
        public double insulationResistanceVoltage { get; set; }
        /// <summary>
        /// 绝缘电阻
        /// </summary>
        public double insulationResistance { get; set; }
        /// <summary>
        /// 耐压电压
        /// </summary>
        public double withStandVoltage { get; set; }
        /// <summary>
        /// 泄露电流
        /// </summary>
        public double leakageCurrent { get; set; }
        /// <summary>
        /// 满压电压
        /// </summary>
        public double fullVoltage { get; set; }
        /// <summary>
        /// 满压电流
        /// </summary>
        public double fullVoltageCurrent { get; set; }
        /// <summary>
        /// 满压功率
        /// </summary>
        public double fullVoltagePower { get; set; }
        /// <summary>
        /// 磨合转矩
        /// </summary>
        public double grindingTorque { get; set; }
        /// <summary>
        /// 动态转矩 制动转矩
        /// </summary>
        public double dynamicTorque { get; set; }
        /// <summary>
        /// 满压残留转矩
        /// </summary>
        public double fullVoltageResidualTorque { get; set; }
        /// <summary>
        /// 降电压
        /// </summary>
        public double decreasedVoltage { get; set; }
        /// <summary>
        /// 降电压电流
        /// </summary>
        public double decreasedVoltageCurrent { get; set; }
        /// <summary>
        /// 降电压功率
        /// </summary>
        public double decreasedVoltagePower { get; set; }
        /// <summary>
        /// 降压残留转矩
        /// </summary>
        public double decreasedVoltageResidualTorque { get; set; }
        /// <summary>
        /// 静态转矩
        /// </summary>
        public double staticTorque { get; set; }
        /// <summary>
        /// 修正电阻bs
        /// </summary>
        public double correctedResistanceBs { get; set; }
        /// <summary>
        /// 修正电阻ts
        /// </summary>
        public double correctedResistanceTs { get; set; }
        /// <summary>
        /// 修正满压电流
        /// </summary>
        public double correctedFullVoltageCurrent { get; set; }
        /// <summary>
        /// 修正满压功率
        /// </summary>
        public double correctedFullVoltagePower { get; set; }
        /// <summary>
        /// 绝缘电阻是否合格
        /// </summary>
        public bool insulationResistancePassed { get; set; }
        /// <summary>
        /// 耐压是否合格
        /// </summary>
        public bool withStandPassed { get; set; }
        /// <summary>
        /// 满压电流是否合格
        /// </summary>
        public bool fullVoltageCurrentPassed { get; set; }
        /// <summary>
        /// 满压功率是否合格
        /// </summary>
        public bool fullVoltagePowerPassed { get; set; }
        /// <summary>
        /// 磨合转矩是否合格
        /// </summary>
        public bool grindingTorquePassed { get; set; }
        /// <summary>
        /// 动态转矩是否合格
        /// </summary>
        public bool dynamicTorquePassed { get; set; }
        /// <summary>
        /// 满压残留转矩是否合格
        /// </summary>
        public bool fullVoltageResidualTorquePassed { get; set; }
        /// <summary>
        /// 降压残留转矩是否合格
        /// </summary>
        public bool decreasedVoltageResidualTorquePassed { get; set; }
        /// <summary>
        /// 静态转矩是否合格
        /// </summary>
        public bool staticTorquePassed { get; set; }
        /// <summary>
        /// 测试是否合格
        /// </summary>
        public bool passed { get; set; }
        /// <summary>
        /// 测试台SN
        /// </summary>
        public string testBenchSn { get; set; }
        /// <summary>
        /// 测试用时
        /// </summary>
        public int testDuration { get; set; }
    }
}
