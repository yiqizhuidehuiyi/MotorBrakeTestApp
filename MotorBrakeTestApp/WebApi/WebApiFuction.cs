using ClassLogin_Input;
using ClassLogin_Output;
//using ClassTestBench_brake_Input;
using ClassTestBench_brake_Search_Output;
//using MotorBrakeTestApp.Sample;
using MotorBrakeTestApp.WebApi;
using MotorBrakeTestApp.WebApi.BrakeInfos;
using MotorBrakeTestApp.WebApi.JsonTo;
using MotorBrakeTestApp.WebApi.Read.BrakeElectricTest;
using MotorBrakeTestApp.WebApi.Read.Universal;
using MotorBrakeTestApp.WebApi.SuZhouMotor;
using MotorBrakeTestApp.WebApi.ToJson;
using MotorBrakeTestApp.WebApi.WebServer;
using MotorBrakeTestApp.WebApi.Write;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace MotorBrakeTestApp
{
    public class WebApiFuction
    {
        private static log4net.ILog log = log4net.LogManager.GetLogger(typeof(WebApiFuction));
        //private TestMotorSample testMotorSample;

        //public WebApiFuction(TestMotorSample testMotorSample)
        //{
        //    this.testMotorSample = testMotorSample;
        //}

        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        /// 
        public static RoutineTestUser Login(LoginInInput loginInInput)
        {
            string sendpost = UniversalDataToJson.FromLoginRequestData(loginInInput);//拼Json访问地址
            string backJson = HttpClientHelper.HttpPost("/login", sendpost);//post请求
            log.Debug("接口返回原始数据:" + backJson);
            LoginInOutput responseBody = JsonToResponseBody.ToResponseBody(backJson);//解析响应数据；获取body部分
            log.Debug("序列化body数据:" + responseBody.data);
            if (responseBody.data == null) return null;
            LoginRespData loginRespData = JsonToUniversalData.ToLoginRespData(responseBody.data);//解析body.data数据
            HttpClientHelper.TokenValue = loginRespData.token;//获取token放入持久层
            RoutineTestUser routineTestUser = ToMotorEntity.ToRoutineTestUser(loginRespData);//转换电机实体类
            return routineTestUser;
        }


        /// <summary>
        /// 307-按工单号查询工单信息(制动器）
        /// </summary>
        /// <param name="orderNo"></param>
        /// <returns></returns>
        public static BrakeOrder GetBrakesByOrderNo(string orderNo)
        {
            var getUrl = $"/testBench/brake/order/getByOrderNo/{orderNo}";
            string backJson = HttpClientHelper.HttpGet(getUrl);
            log.Debug("接口返回工单信息:" + backJson);
            LoginInOutput responseBody = JsonToResponseBody.ToResponseBody(backJson);//解析响应数据；获取body部分
            log.Debug("实例化body数据:" + responseBody.data);
            if (responseBody.data == null) { return null; }
            BrakeOrderResponseData brakeOrderResponseData = JsonToBrakeElectricTest.TosBrakeOrderDataResponseData(responseBody.data);
            BrakeOrder brakeOrder = ToMotorEntity.ConvertToBrakeOrderData(brakeOrderResponseData);
            return brakeOrder;
        }

        /// <summary>
        /// 308-制动器电性能检测结果上报
        /// </summary>
        /// <returns></returns>
        public string UploadBrakeReport(ClassTestBenchbrakeWrite classTestBenchbrakeWrite)
        {
            string sendPost = JsonToBrakeElectricTest.FromloadBrakeResport(classTestBenchbrakeWrite);
            log.Debug($"结果上传数据: {sendPost}");
            string backJson = HttpClientHelper.HttpPost("/testBench/brake/report", sendPost);
            log.Debug($"结果上传数据应答: {backJson}");
            LoginInOutput responseBody = JsonToResponseBody.ToResponseBody(backJson);
            return responseBody.status.ToString();
        }


        /// <summary>
        /// 制动器电性能检测结果上报
        /// </summary>
        /// <returns></returns>
        //public ClassTestBenchbrakeRequest WriteSceneReport()
        //{
        //    ClassTestBench_brake_Input.ClassTestBenchbrakeRequest testBenchbrakeWrite = new ClassTestBench_brake_Input.ClassTestBenchbrakeRequest();
        //    testBenchbrakeWrite.brakeRoutineTestId = testMotorSample.BrakeReportTestBenchData.brakeRoutineTestId;//工单data.id
        //    testBenchbrakeWrite.resistancePassed = testMotorSample.BrakeReportTestBenchData.resistancePassed;
        //    testBenchbrakeWrite.testTime = testMotorSample.BrakeReportTestBenchData.testTime;
        //    testBenchbrakeWrite.sequence = testMotorSample.BrakeReportTestBenchData.sequence;
        //    testBenchbrakeWrite.valid = testMotorSample.BrakeReportTestBenchData.valid;
        //    testBenchbrakeWrite.resistanceBs = testMotorSample.BrakeReportTestBenchData.resistanceBs;
        //    testBenchbrakeWrite.resistanceTs = testMotorSample.BrakeReportTestBenchData.resistanceTs;
        //    testBenchbrakeWrite.temperature = testMotorSample.BrakeReportTestBenchData.temperature;
        //    testBenchbrakeWrite.insulationResistanceVoltage = testMotorSample.BrakeReportTestBenchData.insulationResistanceVoltage;
        //    testBenchbrakeWrite.insulationResistance = testMotorSample.BrakeReportTestBenchData.insulationResistance;
        //    testBenchbrakeWrite.withStandVoltage = testMotorSample.BrakeReportTestBenchData.withStandVoltage;
        //    testBenchbrakeWrite.leakageCurrent = testMotorSample.BrakeReportTestBenchData.leakageCurrent;
        //    testBenchbrakeWrite.fullVoltage = testMotorSample.BrakeReportTestBenchData.fullVoltage;
        //    testBenchbrakeWrite.fullVoltageCurrent = testMotorSample.BrakeReportTestBenchData.fullVoltageCurrent;
        //    testBenchbrakeWrite.fullVoltagePower = testMotorSample.BrakeReportTestBenchData.fullVoltagePower;
        //    testBenchbrakeWrite.grindingTorque = testMotorSample.BrakeReportTestBenchData.grindingTorque;
        //    testBenchbrakeWrite.dynamicTorque = testMotorSample.BrakeReportTestBenchData.dynamicTorque;
        //    testBenchbrakeWrite.fullVoltageResidualTorque = testMotorSample.BrakeReportTestBenchData.fullVoltageResidualTorque;
        //    testBenchbrakeWrite.decreasedVoltage = testMotorSample.BrakeReportTestBenchData.decreasedVoltage;
        //    testBenchbrakeWrite.decreasedVoltageCurrent = testMotorSample.BrakeReportTestBenchData.decreasedVoltageCurrent;
        //    testBenchbrakeWrite.decreasedVoltagePower = testMotorSample.BrakeReportTestBenchData.decreasedVoltagePower;
        //    testBenchbrakeWrite.decreasedVoltageResidualTorque = testMotorSample.BrakeReportTestBenchData.decreasedVoltageResidualTorque;
        //    testBenchbrakeWrite.staticTorque = testMotorSample.BrakeReportTestBenchData.staticTorque;
        //    testBenchbrakeWrite.correctedResistanceBs = testMotorSample.BrakeReportTestBenchData.correctedResistanceBs;
        //    testBenchbrakeWrite.correctedResistanceTs = testMotorSample.BrakeReportTestBenchData.correctedResistanceTs;
        //    testBenchbrakeWrite.correctedFullVoltageCurrent = testMotorSample.BrakeReportTestBenchData.correctedFullVoltageCurrent;
        //    testBenchbrakeWrite.correctedFullVoltagePower = testMotorSample.BrakeReportTestBenchData.correctedFullVoltagePower;
        //    testBenchbrakeWrite.insulationResistancePassed = testMotorSample.BrakeReportTestBenchData.insulationResistancePassed;
        //    testBenchbrakeWrite.withStandPassed = testMotorSample.BrakeReportTestBenchData.withStandPassed;
        //    testBenchbrakeWrite.fullVoltageCurrentPassed = testMotorSample.BrakeReportTestBenchData.fullVoltageCurrentPassed;
        //    testBenchbrakeWrite.fullVoltagePowerPassed = testMotorSample.BrakeReportTestBenchData.fullVoltagePowerPassed;
        //    testBenchbrakeWrite.grindingTorquePassed = testMotorSample.BrakeReportTestBenchData.grindingTorquePassed;
        //    testBenchbrakeWrite.dynamicTorquePassed = testMotorSample.BrakeReportTestBenchData.dynamicTorquePassed;
        //    testBenchbrakeWrite.fullVoltageResidualTorquePassed = testMotorSample.BrakeReportTestBenchData.fullVoltageResidualTorquePassed;
        //    testBenchbrakeWrite.decreasedVoltageResidualTorquePassed = testMotorSample.BrakeReportTestBenchData.decreasedVoltageResidualTorquePassed;
        //    testBenchbrakeWrite.staticTorquePassed = testMotorSample.BrakeReportTestBenchData.staticTorquePassed;
        //    testBenchbrakeWrite.passed = testMotorSample.BrakeReportTestBenchData.passed;
        //    testBenchbrakeWrite.testBenchSn = testMotorSample.BrakeReportTestBenchData.testBenchSn;
        //    testBenchbrakeWrite.testDuration = testMotorSample.BrakeReportTestBenchData.testDuration;
        //    return testBenchbrakeWrite;
        //}


        public void ExportInfos()
        {

        }

        //public static void login(out string token)
        //{
        //    // return token;
        //    //string url = "http://81.70.205.37:8889/login";
        //    //ClassLogin_Input.LoginInInput loginInInput = new ClassLogin_Input.LoginInInput();
        //    //loginInInput.clientType = "testBench_brake";
        //    //loginInInput.username = "sa";
        //    //loginInInput.password = "Abcd1234";
        //    //var json = JsonConvert.SerializeObject(loginInInput);     //将实例类序列化为JSON格式
        //    //var body = json.Replace("\\", string.Empty);
        //    //string getJson = HttpHelper.HttpPost(url, body);
        //    //log.DebugFormat("登录接口返回数据:" + getJson);
        //    //ClassLogin_Output.LoginInOutput loginInOutput = (ClassLogin_Output.LoginInOutput)JsonConvert.DeserializeObject(getJson, typeof(ClassLogin_Output.LoginInOutput));     //将JSON格式反序列化为实例类
        //    //token = loginInOutput.data.token;
        //    //tokens = token;//传递token 
        //    //return loginInOutput.status;
        //}


        /// <summary>
        /// 按工单号读取API信息
        /// </summary>
        /// <param name="productOrderId">in</param>
        /// <param name="brakeId">out</param>
        /// <param name="brakeProductId"></param>
        /// <param name="brakeQuality"></param>
        /// <param name="brakeIdentifier"></param>
        /// <param name="brakeVoltage"></param>
        /// <param name="brakeTorque"></param>
        /// <param name="notes"></param>
        /// <returns></returns>
        //public static ClassTestBench_brake_Search_Output.ClassTestBenchbrakeRead ReadProductWorkOrderId(string productOrderId, out int status, out int brakeId, out int brakeProductId, out int brakeQuality, out string brakeIdentifier, int brakeVoltage, double brakeTorque, out string notes)
        //{
        //    string url = "http://81.70.205.37:8889/testBench/brake/order/getByOrderNo/";
        //    url = url + productOrderId;
        //    string getJson = HttpHelper.HttpGet(url, tokens);
        //    log.DebugFormat("制动器工单返回数据:" + getJson);
        //    JsonSerializerSettings jSetting = new JsonSerializerSettings();
        //    jSetting.NullValueHandling = NullValueHandling.Ignore;
        //    ClassTestBench_brake_Search_Output.ClassTestBenchbrakeRead classTestBenchbrakeRead = JsonConvert.DeserializeObject<ClassTestBench_brake_Search_Output.ClassTestBenchbrakeRead>(getJson, jSetting);    //将JSON格式反序列化为实例类
        //    var GetJsonToClass = classTestBenchbrakeRead;
        //    brakeId = GetJsonToClass.data.id; //产品ID 写入的时候要用这个 不是工单号  和物料 号  先读出来          
        //    brakeProductId = Convert.ToInt32(GetJsonToClass.data.brake.partNo); //物料号 partno          
        //    brakeQuality = GetJsonToClass.data.quantity; //总数量           
        //    brakeIdentifier = GetJsonToClass.data.brake.identifier; //型号
        //    //电压
        //    //brakeVoltage = GetJsonToClass.data.brake.ratedVoltage;
        //    //转矩
        //    //brakeTorque = GetJsonToClass.data.brake.ratedTorque;

        //    //整体说明 brake BE03/5Nm/400AC/200DC....
        //    notes = GetJsonToClass.data.notes;
        //    string str1 = notes;
        //    string[] str3 = str1.Split('/');
        //    if (str3.Length > 2)
        //    {
        //        brakeVoltage = Convert.ToInt32(str3[2].Substring(0, str3[2].IndexOf("A")));//电压
        //        brakeTorque = Convert.ToDouble(str3[1].Replace(",", ".").Substring(0, str3[1].IndexOf("N")));//转矩
        //    }
        //    else
        //    {
        //        brakeVoltage = 0;
        //        brakeTorque = 0;
        //    }
        //    if (brakeIdentifier == null)
        //    {
        //        //型号
        //        int index = notes.LastIndexOf(" ");
        //        int index_s = notes.IndexOf("/");
        //        if (index_s > index)
        //        {
        //            string mode = notes.Substring(index + 1, index_s - index - 1);
        //            brakeIdentifier = mode;
        //        }
        //        else
        //        {
        //            brakeIdentifier = "";
        //        }
        //    }
        //    status = GetJsonToClass.status;
        //    return GetJsonToClass;
        //}

        /// <summary>
        /// 制动器电性能检测结果上报
        /// </summary>
        /// <param name="testBenchbrakeWrite"></param>
        /// <returns></returns>
        //public static int WriteProductId(object testBenchbrakeWrite)
        //{
        //    string url = "http://81.70.205.37:8889/testBench/brake/report";
        //    var json = JsonConvert.SerializeObject(testBenchbrakeWrite);        //将实例类序列化为JSON格式
        //    var body = json.Replace("\\", "");
        //    string getJson = HttpHelper.HttpPost(url, body, tokens);
        //    log.DebugFormat("制动器电性能检测结果上报:" + getJson);
        //    ClassTestBench_brake_Output.ClassTestBenchbrakeRead testBenchbrakeOutput = (ClassTestBench_brake_Output.ClassTestBenchbrakeRead)JsonConvert.DeserializeObject(getJson, typeof(ClassTestBench_brake_Output.ClassTestBenchbrakeRead));     //将JSON格式反序列化为实例类
        //    int status = testBenchbrakeOutput.Status;
        //    return status;
        //}

        /// <summary>
        /// 制动器电性能检测结果上报
        /// </summary>
        /// <returns></returns>
        public static ClassTestBenchbrakeWrite WriteProductIdReport(GlobalData.Product[] product)
        {
            ClassTestBenchbrakeWrite testBenchbrakeWrite = new ClassTestBenchbrakeWrite();
            testBenchbrakeWrite.brakeRoutineTestId = Convert.ToInt32(product[0].ProductID);//制动器出厂测试id
            testBenchbrakeWrite.testTime = product[0].TestDate;//测试时间
            testBenchbrakeWrite.sequence = Convert.ToInt32(product[0].sequence);//序号
            testBenchbrakeWrite.valid = true;//是否有效
            testBenchbrakeWrite.resistanceBs = Convert.ToDouble(product[0].R1);//bs电阻
            testBenchbrakeWrite.resistanceTs = Convert.ToDouble(product[0].R2);//ts电镀
            testBenchbrakeWrite.temperature = Convert.ToDouble(product[0].Temper);//温度
            testBenchbrakeWrite.insulationResistanceVoltage = Convert.ToDouble(product[0].Rm_Voltage);//绝缘电阻电压
            testBenchbrakeWrite.insulationResistance = Convert.ToDouble(product[0].Rm);//绝缘电阻
            testBenchbrakeWrite.withStandVoltage = Convert.ToDouble(product[0].WSVoltage);//耐压电压
            testBenchbrakeWrite.leakageCurrent = Convert.ToDouble(product[0].LeakCurrent);//泄露电流
            testBenchbrakeWrite.fullVoltage = Convert.ToDouble(product[0].RD_Voltage);//满压电压
            testBenchbrakeWrite.fullVoltageCurrent = Convert.ToDouble(product[0].RD_Current);//满压电流
            testBenchbrakeWrite.fullVoltagePower = Convert.ToDouble(product[0].RD_Power);//满压功率
            testBenchbrakeWrite.grindingTorque = Convert.ToDouble(product[0].RunTorque);//磨合转矩
            testBenchbrakeWrite.dynamicTorque = Convert.ToDouble(product[0].BreakTorque);//动态转矩
            testBenchbrakeWrite.fullVoltageResidualTorque = Convert.ToDouble(product[0].RemainTorque);//满压残留转矩
            testBenchbrakeWrite.decreasedVoltage = Convert.ToDouble(product[0].Voltage60p);//降电压
            testBenchbrakeWrite.decreasedVoltageCurrent = Convert.ToDouble(product[0].Current60p);//降电压电流
            testBenchbrakeWrite.decreasedVoltagePower = Convert.ToDouble(product[0].Power60p);//降电压动律
            testBenchbrakeWrite.decreasedVoltageResidualTorque = Convert.ToDouble(product[0].RemainTorque);//降压残留转矩
            testBenchbrakeWrite.staticTorque = Convert.ToDouble(product[0].BreakTorque);//静态转矩
            testBenchbrakeWrite.correctedResistanceBs = Convert.ToDouble(product[0].R1_Correction);//修正电阻bs
            testBenchbrakeWrite.correctedResistanceTs = Convert.ToDouble(product[0].R2_Correction);//修正电阻ts
            testBenchbrakeWrite.correctedFullVoltageCurrent = Convert.ToDouble(product[0].RD_Current_Correction);//修正满压电流
            testBenchbrakeWrite.correctedFullVoltagePower = Convert.ToDouble(product[0].RD_Power_Correction);//修正满压功率
            testBenchbrakeWrite.resistancePassed = product[0].Comparison_R;//直流电阻是否合格
            testBenchbrakeWrite.insulationResistancePassed = product[0].Comparison_Rm;//绝缘电阻是否合格
            testBenchbrakeWrite.withStandPassed = product[0].Comparison_LeakCurrent;//耐压是否合格
            testBenchbrakeWrite.fullVoltageCurrentPassed = product[0].Comparison_RDCurrent;//满压电流是否合格
            testBenchbrakeWrite.fullVoltagePowerPassed = product[0].Comparison_RDPower;//满压功率是否合格
            testBenchbrakeWrite.grindingTorquePassed = product[0].Comparison_RunTorque;//磨合转矩是否合格
            testBenchbrakeWrite.dynamicTorquePassed = product[0].Comparison_BreakTorque;//动态转矩是否合格
            testBenchbrakeWrite.fullVoltageResidualTorquePassed = product[0].Comparison_RemainTorque;//满压残留转矩是否合格
            testBenchbrakeWrite.decreasedVoltageResidualTorquePassed = product[0].Comparison_RemainTorque;//降压残留转矩是否合格
            testBenchbrakeWrite.staticTorquePassed = product[0].Comparison_BreakTorque;//静态转矩是否合格
            testBenchbrakeWrite.passed = product[0].Comparison_Total;//测试是否合格
            testBenchbrakeWrite.testBenchSn = "Brake002";//测试台SN
            testBenchbrakeWrite.testDuration = Convert.ToInt32(product[0].UseTime);//测试用时
            return testBenchbrakeWrite;
        }


    }
}
