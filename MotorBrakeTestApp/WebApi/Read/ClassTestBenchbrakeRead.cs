using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassTestBench_brake_Search_Output

{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class BrakeRoutineStandard
    {
        public int id { get; set; }
        public string identifier { get; set; }
        public int resistanceDuration { get; set; }
        public int referenceTemperature { get; set; }
        public double resistanceCoefficient { get; set; }
        public int insulationResistanceDuration { get; set; }
        public int insulationResistanceVoltage { get; set; }
        public int insulationResistanceMin { get; set; }
        public int withStandDuration { get; set; }
        public int withStandVoltage { get; set; }
        public int leakageCurrentMax { get; set; }
        public int grindingSpeed { get; set; }
        public int grindingTimeMin { get; set; }
        public int grindingTimeMax { get; set; }
        public double decreasedVoltageRatio { get; set; }
        public double residualTorqueMax { get; set; }
        public string notes { get; set; }
        public int fullVoltageTime { get; set; }
        public int decreasedVoltageTime { get; set; }
    }

    public class BrakeRoutineStandardDetail
    {
        public int id { get; set; }
        public double resistanceBstsMin { get; set; }
        public double resistanceBstsMax { get; set; }
        public object brakeRectifierId { get; set; }
        public BrakeRoutineStandard brakeRoutineStandard { get; set; }
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

    public class BrakeRoutineStandardTorque
    {
        public int id { get; set; }
        public BrakeRoutineStandard brakeRoutineStandard { get; set; }
        public double ratedTorque { get; set; }
        public double grindingTorqueMin { get; set; }
        public double grindingTorqueMax { get; set; }
        public double staticTorqueMin { get; set; }
        public double brakeTorqueMin { get; set; }
        public double brakeTorqueMax { get; set; }
    }

    public class Brake
    {
        public int id { get; set; }
        public string partNo { get; set; }
        public bool checkedNum { get; set; }
        public string identifier { get; set; }
        public int? ratedVoltage { get; set; }
        public object ratedTorque { get; set; }
        public object specifications { get; set; }
        public BrakeRoutineStandardDetail brakeRoutineStandardDetail { get; set; }
        public BrakeRoutineStandardTorque brakeRoutineStandardTorque { get; set; }
        public string notes { get; set; }
        public object lastModifyDate { get; set; }
    }

    public class Data
    {
        public int id { get; set; }
        public object ratedTorque { get; set; }
        public Brake brake { get; set; }
        public string orderNo { get; set; }
        public string orderImportTime { get; set; }
        public string orderModifyTime { get; set; }
        public int quantity { get; set; }
        public string notes { get; set; }
        public int finishedQuantity { get; set; }
        public object finished { get; set; }
        public object testStartTime { get; set; }
        public object testEndTime { get; set; }
    }

    public class ClassTestBenchbrakeRead
    {
        public int status { get; set; }
        public string error { get; set; }
        public string msg { get; set; }
        public object count { get; set; }
        public Data data { get; set; }
    }


}
