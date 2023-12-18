using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorBrakeTestApp.WebApi.SuZhouMotor
{
    public class RoutineTestUser
    {
        public int id { get; set; }//自动编号
        public string identifier { get; set; }//用户名
        public string passwords { get; set; }//密码
        public bool valid { get; set; }//是否可用
        public int user_group { get; set; }//用户组:0普通用户，1管理员
        public string notes { get; set; }//备注
    }
}
