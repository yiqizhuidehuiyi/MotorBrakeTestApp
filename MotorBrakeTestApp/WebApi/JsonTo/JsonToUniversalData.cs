using MotorBrakeTestApp.WebApi.Read.Universal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorBrakeTestApp.WebApi.ToJson
{
    public class JsonToUniversalData
    {
        public static LoginRespData ToLoginRespData(object jsonObj)
        {
            if (jsonObj == null) return null;
            LoginRespData loginRespData = System.Text.Json.JsonSerializer.Deserialize<LoginRespData>(jsonObj.ToString());
            return loginRespData;
        }
    }
}
