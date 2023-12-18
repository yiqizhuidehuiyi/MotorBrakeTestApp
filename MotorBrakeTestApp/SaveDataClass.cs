using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorBrakeTestApp
{
    /// <summary>
    /// 用来保存 补偿用 XML
    /// </summary>
    public class SaveDataClass
    {
        public List<string> list = new List<string>();
    }
    /// <summary>
    ///  全局变量 
    /// </summary>
    public static class SaveDataClassIndex
    {
       /// <summary>
       /// 用来命名XML用
       /// </summary>
       public static string voltage;
       public static string ProductId;
    }

    /// <summary>
    /// 工单号 与 数量 保存配置XML
    /// </summary>
    public class SaveDataProductIdClass
    {
        public  string ProductId;
        public  string ProductSequqence;
    }

    /// <summary>
    /// 关闭 打开软件 保存工单号到 Main 的 控件中
    /// </summary>
    public class SaveProductTxt
    {
        public string ProductIdTxt;
    }


}
