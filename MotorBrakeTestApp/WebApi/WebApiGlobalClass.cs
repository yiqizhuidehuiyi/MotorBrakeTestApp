using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorBrakeTestApp
{
    public  class WebApiGlobalClass
    {
        /// <summary>
        /// 权限
        /// </summary>
        public static string Token { get; set; }

        /// <summary>
        /// 工单号
        /// </summary>
        public static int brakeWorkOrderId { get; set; }
        /// <summary>
        /// 产品ID 写入的时候要用这个 不是工单号 和物料 号  先读出来
        /// </summary>
        public static int brakeId { get; set; }
        /// <summary>
        /// 物料号
        /// </summary>
        public static int brakeProductId { get; set; }
        /// <summary>
        /// 总数量
        /// </summary>
        public static int brakeQuality { get; set; }

        /// <summary>
        /// 型号
        /// </summary>
        public static string brakeIdentifier { get; set; }
        /// <summary>
        /// 电压
        /// </summary>
        public static int brakeVoltage { get; set; }
        /// <summary>
        /// 转矩
        /// </summary>
        public static double brakeTorque { get; set; }

        /// <summary>
        /// 整体型号说明 
        /// </summary>
        public static string notes { get; set; }


    }
}
