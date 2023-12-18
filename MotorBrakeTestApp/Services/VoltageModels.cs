using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorBrakeTestApp.Services
{
    public class VoltageModels
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("voltage")]
        public double BrakeVoltage { get; set; }

        [Column("brake_model")]
        public string BrakeModel { get; set; }

        [Column("adapter")]
        public string Adapter { get; set; }

        [Column("add")]
        public int Address { get; set; }

        [Column("voltLow")]
        public int VoltageLow { get; set; }

        [Column("voltUp")]
        public int VoltageUp { get; set; }
        [Column("sizeLow")]
        public int BrakeSizeLow { get; set; }

        [Column("sizeUp")]
        public int BrakeSizeUp { get; set; }
    }
}
