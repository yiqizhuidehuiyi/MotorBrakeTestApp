using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLogin_Input
{
    //如果好用，请收藏地址，帮忙分享。
    public class LoginInInput
    {
        /// <summary>
        ///  客户端类型
        /// </summary>
        [JsonProperty("clientType")]
        public string clientType { get; set; }
        /// <summary>
        ///  用户名
        /// </summary>
        [JsonProperty("username")]
        public string username { get; set; }
        /// <summary>
        ///  密码
        /// </summary>
        [JsonProperty("password")]
        public string password { get; set; }
    }
}

