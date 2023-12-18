using ClassLogin_Input;
using MotorBrakeTestApp.WebApi.SuZhouMotor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MotorBrakeTestApp.Services
{
    internal class LoginService
    {
        public static void AutoLogin()
        {
            LoginInInput loginInInput = new LoginInInput();
            loginInInput.username = "sa";
            loginInInput.password = "Abcd1234";
            loginInInput.clientType = "testBench_brake";
            RoutineTestUser routineTestUser = WebApiFuction.Login(loginInInput);
        }
    }
}
