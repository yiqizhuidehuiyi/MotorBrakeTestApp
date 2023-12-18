using ClassTestBench_brake_Search_Output;
using MotorBrakeTestApp.WebApi.BrakeInfos;
using MotorBrakeTestApp.WebApi.Read.BrakeElectricTest;
using MotorBrakeTestApp.WebApi.Read.Universal;
using MotorBrakeTestApp.WebApi.SuZhouMotor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorBrakeTestApp.WebApi
{
    internal class ToMotorEntity
    {
        /// <summary>
        /// 登录信息转换成对应实体类
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static RoutineTestUser ToRoutineTestUser(LoginRespData data)
        {
            if (data == null) return null;
            RoutineTestUser routineTestUser = new RoutineTestUser();
            routineTestUser.id = data.user.id;
            routineTestUser.identifier = data.user.username;
            routineTestUser.notes = data.user.realname;
            return routineTestUser;
        }

        public static BrakeOrder ConvertToBrakeOrderData(BrakeOrderResponseData brakeOrderResponseData)
        {
            BrakeOrder brakeOrder = new BrakeOrder();
            brakeOrder.id = brakeOrderResponseData.id;//工单id
            brakeOrder.ratedTorque = brakeOrderResponseData.ratedTorque;
            if (brakeOrderResponseData.brake != null)
            {
                brakeOrder.brake = brakeOrderResponseData.brake;
            }
            if (brakeOrderResponseData.brake.brakeRoutineStandardDetail != null)
            {
                brakeOrder.brake.brakeRoutineStandardDetail =
                    brakeOrderResponseData.brake.brakeRoutineStandardDetail;
            }
            if (brakeOrderResponseData.brake.brakeRoutineStandardDetail.brakeRoutineStandard != null)
            {
                brakeOrder.brake.brakeRoutineStandardDetail.brakeRoutineStandard =
                    brakeOrderResponseData.brake.brakeRoutineStandardDetail.brakeRoutineStandard;
            }
            if (brakeOrderResponseData.brake.brakeRoutineStandardTorque != null)
            {
                brakeOrder.brake.brakeRoutineStandardTorque =
                     brakeOrderResponseData.brake.brakeRoutineStandardTorque;
            }
            brakeOrder.orderNo = brakeOrderResponseData.orderNo;
            brakeOrder.orderImportTime = brakeOrderResponseData.orderImportTime;
            brakeOrder.quantity = Convert.ToString(brakeOrderResponseData.quantity);
            brakeOrder.notes = brakeOrderResponseData.notes;
            brakeOrder.finishedQuantity = brakeOrderResponseData.finishedQuantity;
            brakeOrder.finished = brakeOrderResponseData.finished.GetValueOrDefault();
            brakeOrder.testStartTime = brakeOrderResponseData.testStartTime;
            brakeOrder.testEndTime = brakeOrderResponseData.testEndTime;
            return brakeOrder;
        }

        private static Brake ConvertMotorBrakes(Brake brakeInfos)
        {
            Brake brake = new Brake();
            brake.id = brakeInfos.id;
            return brake;
        }


        public static SuZhouMotor.BrakeRoutineStandardDetail ToBrakeRoutineStandardDetail(BrakeRoutineStandardDetailResponseData responseData)
        {
            SuZhouMotor.BrakeRoutineStandardDetail brakeRoutineStandardDetail = ConvertBrakeRoutineStandardDetail(responseData);
            return brakeRoutineStandardDetail;
        }

        private static SuZhouMotor.BrakeRoutineStandardDetail ConvertBrakeRoutineStandardDetail(BrakeRoutineStandardDetailResponseData responseData)
        {
            SuZhouMotor.BrakeRoutineStandardDetail brakeRoutineStandardDetail = new SuZhouMotor.BrakeRoutineStandardDetail();
            brakeRoutineStandardDetail.id = responseData.id;
            brakeRoutineStandardDetail.brake_routine_standard_id = responseData.brakeRoutineStandard.id.GetValueOrDefault();
            brakeRoutineStandardDetail.resistance_bsts_min = responseData.resistanceBstsMin ?? 0;
            brakeRoutineStandardDetail.resistance_bsts_max = responseData.resistanceBstsMax ?? 0;
            brakeRoutineStandardDetail.brake_rectifier_id = responseData.brakeRectifierId.GetValueOrDefault();
            brakeRoutineStandardDetail.rated_voltage = responseData.ratedVoltage;
            brakeRoutineStandardDetail.resistance_bs_min = responseData.resistanceBsMin;
            brakeRoutineStandardDetail.resistance_bs_max = responseData.resistanceBsMax;
            brakeRoutineStandardDetail.resistance_ts_min = responseData.resistanceTsMin;
            brakeRoutineStandardDetail.resistance_ts_max = responseData.resistanceTsMax;
            brakeRoutineStandardDetail.full_voltage_current_min = responseData.fullVoltageCurrentMin;
            brakeRoutineStandardDetail.full_voltage_current_max = responseData.fullVoltageCurrentMax;
            brakeRoutineStandardDetail.full_voltage_power_min = responseData.fullVoltagePowerMin;
            brakeRoutineStandardDetail.full_voltage_power_max = responseData.fullVoltagePowerMax;
            brakeRoutineStandardDetail.dc_voltage = responseData.dcVoltage;
            brakeRoutineStandardDetail.decreased_voltage_current_min = responseData.decreasedVoltageCurrentMin;
            brakeRoutineStandardDetail.decreased_voltage_current_max = responseData.decreasedVoltageCurrentMax;
            brakeRoutineStandardDetail.decreased_voltage_power_max = responseData.decreasedVoltagePowerMax;
            brakeRoutineStandardDetail.decreased_voltage_power_min = responseData.decreasedVoltagePowerMin;
            return brakeRoutineStandardDetail;
        }

    }
}
