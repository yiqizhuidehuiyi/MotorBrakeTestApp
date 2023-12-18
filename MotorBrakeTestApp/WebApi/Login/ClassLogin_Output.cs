using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLogin_Output
{
    //如果好用，请收藏地址，帮忙分享。
    public class Org
    {
        /// <summary>
        /// 
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// SEW中国
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string description { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int level { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string seq { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string contacter { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string contactPhone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string parent { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string createTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string parentId { get; set; }
    }

    public class User
    {
        /// <summary>
        /// 
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 超级管理员
        /// </summary>
        public string realname { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string password { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string fixedPhone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<string> roles { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Org org { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string createTime { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string super { get; set; }
    }

    public class Data
    {
        /// <summary>
        /// 
        /// </summary>
        public List<string> permission { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public User user { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string token { get; set; }
    }

    public class LoginInOutput
    {
        /// <summary>
        /// 
        /// </summary>
        public int status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string error { get; set; }
        /// <summary>
        /// 登录成功
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public double? count { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public object data { get; set; }
    }

}
