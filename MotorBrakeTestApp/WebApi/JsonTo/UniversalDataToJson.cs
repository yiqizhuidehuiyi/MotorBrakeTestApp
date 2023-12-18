using ClassLogin_Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorBrakeTestApp.WebApi.ToJson
{
    public class UniversalDataToJson
    {
        public static string FromLoginRequestData(LoginInInput loginInInput)
        {
            string jsonText = JsonConvert.SerializeObject(loginInInput);
            return jsonText;
        }
    }
}
