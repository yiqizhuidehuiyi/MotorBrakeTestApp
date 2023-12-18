using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorBrakeTestApp.WebApi.Read.BrakeElectricTest
{
    public class BrakeRoutineStandardDetailResponseData
    {
        public int id { get; set; }
        public BrakeRoutineStandardResponseData brakeRoutineStandard { get; set; }

        public double? resistanceBstsMin { get; set; }
        public double? resistanceBstsMax { get; set; }
        public int? brakeRectifierId { get; set; }
        public int ratedVoltage { get; set; }
        public double resistanceBsMin { get; set; }
        public double resistanceBsMax { get; set; }
        public double resistanceTsMin { get; set; }
        public double resistanceTsMax { get; set; }
        public double fullVoltageCurrentMin { get; set; }
        public double fullVoltageCurrentMax { get; set; }
        public double fullVoltagePowerMin { get; set; }
        public double fullVoltagePowerMax { get; set; }
        public int dcVoltage { get; set; }
        public double decreasedVoltageCurrentMin { get; set; }
        public double decreasedVoltageCurrentMax { get; set; }
        public double decreasedVoltagePowerMin { get; set; }
        public double decreasedVoltagePowerMax { get; set; }
    }
}
