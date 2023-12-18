using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PublicClass
{
    public class UpPluseClass
    {

        public static bool oldpv_1 = false;
        /// <summary>
        ///  上升沿只执行一次
        /// </summary>
        /// <param name="pv">信号点</param>
        /// <param name="flag">标志位</param> 
        public static void UpPluse_1(bool pv, out bool flag)
        {

            if (pv && !oldpv_1)
            {
                flag = true;
               
            }
            else
            {
                flag = false;
            }
            oldpv_1 = pv;


        }


        public static bool oldpv_2 = false;
        /// <summary>
        ///  上升沿只执行一次
        /// </summary>
        /// <param name="pv">信号点</param>
        /// <param name="flag">标志位</param> 
        public static void UpPluse_2(bool pv, out bool flag)
        {

            if (pv && !oldpv_2)
            {
                flag = true;

            }
            else
            {
                flag = false;
            }
            oldpv_2 = pv;


        }


        public static bool oldpv_3 = false;
        /// <summary>
        ///  上升沿只执行一次
        /// </summary>
        /// <param name="pv">信号点</param>
        /// <param name="flag">标志位</param> 
        public static void UpPluse_3(bool pv, out bool flag)
        {

            if (pv && !oldpv_3)
            {
                flag = true;

            }
            else
            {
                flag = false;
            }
            oldpv_3 = pv;


        }


        public static bool oldpv_4 = false;
        /// <summary>
        ///  上升沿只执行一次
        /// </summary>
        /// <param name="pv">信号点</param>
        /// <param name="flag">标志位</param> 
        public static void UpPluse_4(bool pv, out bool flag)
        {

            if (pv && !oldpv_4)
            {
                flag = true;

            }
            else
            {
                flag = false;
            }
            oldpv_4 = pv;


        }


        public static bool oldpv_5 = false;
        /// <summary>
        ///  上升沿只执行一次
        /// </summary>
        /// <param name="pv">信号点</param>
        /// <param name="flag">标志位</param> 
        public static void UpPluse_5(bool pv, out bool flag)
        {

            if (pv && !oldpv_5)
            {
                flag = true;

            }
            else
            {
                flag = false;
            }
            oldpv_5 = pv;


        }


        public static bool oldpv_6 = false;
        /// <summary>
        ///  上升沿只执行一次
        /// </summary>
        /// <param name="pv">信号点</param>
        /// <param name="flag">标志位</param> 
        public static void UpPluse_6(bool pv, out bool flag)
        {

            if (pv && !oldpv_6)
            {
                flag = true;

            }
            else
            {
                flag = false;
            }
            oldpv_6 = pv;


        }


        public static bool oldpv_7 = false;
        /// <summary>
        ///  上升沿只执行一次
        /// </summary>
        /// <param name="pv">信号点</param>
        /// <param name="flag">标志位</param> 
        public static void UpPluse_7(bool pv, out bool flag)
        {

            if (pv && !oldpv_7)
            {
                flag = true;

            }
            else
            {
                flag = false;
            }
            oldpv_7 = pv;


        }


        public static bool oldpv_8 = false;
        /// <summary>
        ///  上升沿只执行一次
        /// </summary>
        /// <param name="pv">信号点</param>
        /// <param name="flag">标志位</param> 
        public static void UpPluse_8(bool pv, out bool flag)
        {

            if (pv && !oldpv_8)
            {
                flag = true;

            }
            else
            {
                flag = false;
            }
            oldpv_8 = pv;


        }


        public static bool oldpv_9 = false;
        /// <summary>
        ///  上升沿只执行一次
        /// </summary>
        /// <param name="pv">信号点</param>
        /// <param name="flag">标志位</param> 
        public static void UpPluse_9(bool pv, out bool flag)
        {

            if (pv && !oldpv_9)
            {
                flag = true;

            }
            else
            {
                flag = false;
            }
            oldpv_9 = pv;


        }


        public static bool oldpv_10 = false;
        /// <summary>
        ///  上升沿只执行一次
        /// </summary>
        /// <param name="pv">信号点</param>
        /// <param name="flag">标志位</param> 
        public static void UpPluse_10(bool pv, out bool flag)
        {

            if (pv && !oldpv_10)
            {
                flag = true;

            }
            else
            {
                flag = false;
            }
            oldpv_10 = pv;


        }
    }
}
