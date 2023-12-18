using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorBrakeTestApp.WebApi.Read.BrakeElectricTest
{
    public class BrakeRoutineStandardResponseData
    {
        public int? id { get; set; }
        public string identifier { get; set; }
        public int? resistanceDuration { get; set; }
        public int? referenceTemperature { get; set; }
        public double? resistanceCoefficient { get; set; }
        public int? insulationResistanceDuration { get; set; }
        public int? insulationResistanceVoltage { get; set; }
        public int? insulationResistanceMin { get; set; }
        public int? withStandDuration { get; set; }
        public int? withStandVoltage { get; set; }
        public int? leakageCurrentMax { get; set; }
        public int? grindingSpeed { get; set; }
        public int? grindingTimeMin { get; set; }
        public int? grindingTimeMax { get; set; }
        public double? decreasedVoltageRatio { get; set; }
        public double? residualTorqueMax { get; set; }
        public string notes { get; set; }
        public int? fullVoltageTime { get; set; }
        public int? decreasedVoltageTime { get; set; }
    }
}
