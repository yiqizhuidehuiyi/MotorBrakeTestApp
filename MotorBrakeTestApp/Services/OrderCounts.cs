using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorBrakeTestApp.Services
{
    public class OrderCounts
    {
        [Key]
        [Column("Order")]
        public string Order { get; set; }

        [Column("BrakeCount")]
        public int BrakeCount { get; set; }
    }
}
