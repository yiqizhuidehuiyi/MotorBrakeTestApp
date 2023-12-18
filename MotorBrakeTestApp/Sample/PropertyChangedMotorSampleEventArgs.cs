using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorBrakeTestApp.Sample
{
    public class PropertyChangedMotorSampleEventArgs : EventArgs
    {
        public string PropertyChangeName { get; set; }
    }
}
