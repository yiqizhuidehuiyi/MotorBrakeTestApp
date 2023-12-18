using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace MotorBrakeTestApp.Services
{
    internal class MathHelper
    {
        /************************************************
        * 电阻温度换算公式： 
        * R2=R1*(T+t2)/(T+t1) 
        * t1-----绕组温度 
        * T------电阻温度常数(铜线取235，铝线取225 默认铜线) 
        * t2-----换算温度(默认 20 °C) 
        * R1----测量电阻值 
        * R2----换算电阻值
        ************************************************/
        public static double CorrectResistance(double R1, double t1, double T = 235, double t2 = 20)
        {
            double R2 = R1 * (T + t2) / (T + t1);
            return R2;
        }
    }
}
