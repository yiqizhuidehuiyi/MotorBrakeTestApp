using MotorBrakeTestApp.WebApi.BrakeInfos;
using MotorBrakeTestApp.WebApi.SuZhouMotor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MotorBrakeTestApp.Sample
{
    public class TestMotorSample
    {
        //制动器
        private BrakeOrder brakeOrder;//制动器工单
        private BrakeReportTestBenchData brakeReportTestBenchData;//制动器电性能检测上传
        public event EventHandler<PropertyChangedMotorSampleEventArgs> PropertyChangedEventHandler;

        public BrakeOrder BrakeOrder
        {
            get => brakeOrder;
            set => SetProperty(ref brakeOrder, value);
        }

        public BrakeReportTestBenchData BrakeReportTestBenchData
        {
            get => brakeReportTestBenchData;
            set => SetProperty(ref brakeReportTestBenchData, value);
        }

        protected virtual bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(storage, value)) return false;
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        protected void OnPropertyChanged(string propertyChangeName)
        {
            PropertyChangedMotorSampleEventArgs eventArgs = new PropertyChangedMotorSampleEventArgs();
            eventArgs.PropertyChangeName = propertyChangeName;
            PropertyChangedEventHandler?.Invoke(this, eventArgs);
        }
    }
}
