using ClassTestBench_brake_Search_Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorBrakeTestApp.WebApi.BrakeInfos
{
    public class BrakeOrder
    {
        public int id { get; set; }
        public string ratedTorque { get; set; }
        public Brake brake { get; set; }
        public string orderNo { get; set; }
        public string orderImportTime { get; set; }
        public string quantity { get; set; }
        public string notes { get; set; }
        public int finishedQuantity { get; set; }
        public bool finished { get; set; }
        public string testStartTime { get; set; }
        public string testEndTime { get; set; }
    }
}
