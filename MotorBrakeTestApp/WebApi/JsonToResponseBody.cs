using ClassLogin_Output;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorBrakeTestApp.WebApi
{
    public class JsonToResponseBody
    {
        /// <summary>
        /// 响应的值
        /// </summary>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        public static LoginInOutput ToResponseBody(string jsonText)
        {
            LoginInOutput back = JsonConvert.DeserializeObject<LoginInOutput>(jsonText);
            return back;
        }
    }
}
