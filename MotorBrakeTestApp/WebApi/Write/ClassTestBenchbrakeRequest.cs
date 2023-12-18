using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassTestBench_brake_Input
{
    public class ClassTestBenchbrakeRequest
    {
        /// <summary>
        /// 制动器出厂测试ID
        /// </summary>
        [JsonProperty("brakeRoutineTestId")]
        public int brakeRoutineTestId { get; set; }
        /// <summary>
        /// 阻抗是否测试通过
        /// </summary>
        [JsonProperty("resistancePassed")]
        public bool resistancePassed { get; set; }
        /// <summary>
        /// 测试时间 注意格式
        /// </summary>
        [JsonProperty("testTime")]
        public string testTime { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        [JsonProperty("sequence")]
        public int sequence { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        [JsonProperty("valid")]
        public bool valid { get; set; }
        /// <summary>
        /// bs电阻
        /// </summary>
        [JsonProperty("resistanceBs")]
        public double resistanceBs { get; set; }
        /// <summary>
        /// ts电阻
        /// </summary>
        [JsonProperty("resistanceTs")]
        public double resistanceTs { get; set; }
        /// <summary>
        /// 温度
        /// </summary>
        [JsonProperty("temperature")]
        public double temperature { get; set; }
        /// <summary>
        /// 绝缘电阻电压
        /// </summary>
        [JsonProperty("insulationResistanceVoltage")]
        public double insulationResistanceVoltage { get; set; }
        /// <summary>
        /// 绝缘电阻
        /// </summary>
        [JsonProperty("insulationResistance")]
        public double insulationResistance { get; set; }
        /// <summary>
        /// 耐压电压
        /// </summary>
        [JsonProperty("withStandVoltage")]
        public double withStandVoltage { get; set; }
        /// <summary>
        /// 泄露电流
        /// </summary>
        [JsonProperty("leakageCurrent")]
        public double leakageCurrent { get; set; }
        /// <summary>
        /// 满压电压
        /// </summary>
        [JsonProperty("fullVoltage")]
        public double fullVoltage { get; set; }
        /// <summary>
        /// 满压电流
        /// </summary>
        [JsonProperty("fullVoltageCurrent")]
        public double fullVoltageCurrent { get; set; }
        /// <summary>
        /// 满压功率
        /// </summary>
        [JsonProperty("fullVoltagePower")]
        public double fullVoltagePower { get; set; }
        /// <summary>
        /// 磨合转矩
        /// </summary>
        [JsonProperty("grindingTorque")]
        public double grindingTorque { get; set; }
        /// <summary>
        /// 动态转矩 制动转矩
        /// </summary>
        [JsonProperty("dynamicTorque")]
        public double dynamicTorque { get; set; }
        /// <summary>
        /// 满压残留转矩
        /// </summary>
        [JsonProperty("fullVoltageResidualTorque")]
        public double fullVoltageResidualTorque { get; set; }
        /// <summary>
        /// 降电压
        /// </summary>
        [JsonProperty("decreasedVoltage")]
        public double decreasedVoltage { get; set; }
        /// <summary>
        /// 降电压电流
        /// </summary>
        [JsonProperty("decreasedVoltageCurrent")]
        public double decreasedVoltageCurrent { get; set; }
        /// <summary>
        /// 降电压功率
        /// </summary>
        [JsonProperty("decreasedVoltagePower")]
        public double decreasedVoltagePower { get; set; }
        /// <summary>
        /// 降压残留转矩
        /// </summary>
        [JsonProperty("decreasedVoltageResidualTorque")]
        public double decreasedVoltageResidualTorque { get; set; }
        /// <summary>
        /// 静态转矩
        /// </summary>
        [JsonProperty("staticTorque")]
        public double staticTorque { get; set; }
        /// <summary>
        /// 修正电阻bs
        /// </summary>
        [JsonProperty("correctedResistanceBs")]
        public double correctedResistanceBs { get; set; }
        /// <summary>
        /// 修正电阻ts
        /// </summary>
        [JsonProperty("correctedResistanceTs")]
        public double correctedResistanceTs { get; set; }
        /// <summary>
        /// 修正满压电流
        /// </summary>
        [JsonProperty("correctedFullVoltageCurrent")]
        public double correctedFullVoltageCurrent { get; set; }
        /// <summary>
        /// 修正满压功率
        /// </summary>
        [JsonProperty("correctedFullVoltagePower")]
        public double correctedFullVoltagePower { get; set; }
        /// <summary>
        /// 绝缘电阻是否合格
        /// </summary>
        [JsonProperty("insulationResistancePassed")]
        public bool insulationResistancePassed { get; set; }
        /// <summary>
        /// 耐压是否合格
        /// </summary>
        [JsonProperty("withStandPassed")]
        public bool withStandPassed { get; set; }
        /// <summary>
        /// 满压电流是否合格
        /// </summary>
        [JsonProperty("fullVoltageCurrentPassed")]
        public bool fullVoltageCurrentPassed { get; set; }
        /// <summary>
        /// 满压功率是否合格
        /// </summary>
        [JsonProperty("fullVoltagePowerPassed")]
        public bool fullVoltagePowerPassed { get; set; }
        /// <summary>
        /// 磨合转矩是否合格
        /// </summary>
        [JsonProperty("grindingTorquePassed")]
        public bool grindingTorquePassed { get; set; }
        /// <summary>
        /// 动态转矩是否合格
        /// </summary>
        [JsonProperty("dynamicTorquePassed")]
        public bool dynamicTorquePassed { get; set; }
        /// <summary>
        /// 满压残留转矩是否合格
        /// </summary>
        [JsonProperty("fullVoltageResidualTorquePassed")]
        public bool fullVoltageResidualTorquePassed { get; set; }
        /// <summary>
        /// 降压残留转矩是否合格
        /// </summary>
        [JsonProperty("decreasedVoltageResidualTorquePassed")]
        public bool decreasedVoltageResidualTorquePassed { get; set; }
        /// <summary>
        /// 静态转矩是否合格
        /// </summary>
        [JsonProperty("staticTorquePassed")]
        public bool staticTorquePassed { get; set; }
        /// <summary>
        /// 测试是否合格
        /// </summary>
        [JsonProperty("passed")]
        public bool passed { get; set; }
        /// <summary>
        /// 测试台SN
        /// </summary>
        [JsonProperty("testBenchSn")]
        public string testBenchSn { get; set; }
        /// <summary>
        /// 测试用时
        /// </summary>
        [JsonProperty("testDuration")]
        public int testDuration { get; set; }
    }
}
