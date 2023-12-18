//using ClassTestBench_brake_Input;
using MotorBrakeTestApp.WebApi.Read.BrakeElectricTest;
using MotorBrakeTestApp.WebApi.Read.Universal;
using MotorBrakeTestApp.WebApi.Write;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorBrakeTestApp.WebApi.JsonTo
{
    public class JsonToBrakeElectricTest
    {
        //301
        public static BrakeOrderResponseData TosBrakeOrderDataResponseData(object jsonObj)
        {
            if (jsonObj == null) return null;
            BrakeOrderResponseData responseDatas
                = System.Text.Json.JsonSerializer.Deserialize<BrakeOrderResponseData>(jsonObj.ToString());
            return responseDatas;
        }

        public static string FromloadBrakeResport(ClassTestBenchbrakeWrite classTestBenchbrakeWrite)
        {
            return JsonConvert.SerializeObject(classTestBenchbrakeWrite);
        }
    }
}
