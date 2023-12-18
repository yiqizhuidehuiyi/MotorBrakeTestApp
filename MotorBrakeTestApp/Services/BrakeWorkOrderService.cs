using ClassTestBench_brake_Search_Output;
using MotorBrakeTestApp.WebApi.BrakeInfos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorBrakeTestApp.Services
{
    public class BrakeWorkOrderService
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(BrakeWorkOrderService));
        public BrakeOrder BrakeOrderNature { get; set; }

        public BrakeOrder QuertBrakeOrder(string orderNum)
        {
            try
            {
                BrakeOrder brakeOrder = WebApiFuction.GetBrakesByOrderNo(orderNum);
                BrakeOrderNature = brakeOrder;
            }
            catch (Exception e)
            {
                log.Error(e);
                return null;
            }
            return BrakeOrderNature;
        }

        public int GetOrderLastIndex(string order)
        {
            BrakeDbContext context = new BrakeDbContext();
            var result = context.OrderIndex.FirstOrDefault(e => e.Order == order);
            if (result != null)
            {
                return result.BrakeCount;
            }
            return 0;
        }

        public void SetOrderIndex(string order, int index)
        {
            BrakeDbContext context = new BrakeDbContext();
            var result = context.OrderIndex.FirstOrDefault(e => e.Order == order);
            if (result != null)
            {
                result.BrakeCount = index;
                return;
            }
            OrderCounts orderCounts = new OrderCounts();
            orderCounts.Order = order;
            orderCounts.BrakeCount = index;
            context.Add(orderCounts);
            context.SaveChanges();
        }
    }
}
