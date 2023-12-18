using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorBrakeTestApp.WebApi.Read.Universal
{
    public class LoginRespData
    {
        /// <summary>
        /// 权限集合
        /// </summary>
        [JsonProperty("permission")]
        public string[] Permission { get; set; }
        public string token { get; set; }   //后端访问令牌
        public User user { get; set; }  //登录用户

        /// <summary>
        /// 登录用户
        ///// </summary>
        public class User
        {
            public int id { get; set; }    //用户ID
            public string realname { get; set; }   //真实姓名
            public string username { get; set; }   //用户名
            public Org org { get; set; }   //组织机构
        }

        /// <summary>
        /// 组织机构
        ///// </summary>
        public class Org
        {
            public string id { get; set; } //组织机构ID
            public string name { get; set; }   //组织机构名称
        }
    }
}
