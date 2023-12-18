using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorBrakeTestApp
{
    public class Zebra
    {
        public static string SendZebraFunctionEX(string productSquence)
        {
            string information = "";

            DateTime dt = DateTime.Now;

            //型号
            string mode = WebApiGlobalClass.brakeIdentifier;
            //工单号
            string brakeProductWorkOrderId = WebApiGlobalClass.brakeWorkOrderId.ToString();
            //物料号
            string brakeProductId = WebApiGlobalClass.brakeProductId.ToString();
            //产品编号 不是工单号
            string brakeId = WebApiGlobalClass.brakeId.ToString();
            //序列号
            string squence = productSquence;
            //电压
            string voltage = WebApiGlobalClass.brakeVoltage.ToString();
            //转矩
            string torque = WebApiGlobalClass.brakeTorque.ToString();
            //整体型号说明 
            string notes = WebApiGlobalClass.notes;

            string squenceString = squence.ToString().PadLeft(4, '0');
            string DDMMYYYY = dt.ToString("ddMMyyyy");
            string HHmmss = dt.ToString("HHmmss");

            string ddMMyy = dt.ToString("ddMMyy");

            string QRcode = "2550" + "#" + brakeProductId + "#" + brakeProductId + squenceString + "#" + brakeProductWorkOrderId + "#" + DDMMYYYY + "#" + HHmmss;

            string print01 = @"^XA~TA000~JSN^LT0^MNW^MTT^PON^PMN^LH0,0^JMA^PR4,4~SD20^JUS^LRN^CI0^XZ" + "\r\n";
            string print02 = @"^XA" + "\r\n";
            string print03 = @"^MMT" + "\r\n";
            string print04 = @"^PW650" + "\r\n";
            string print05 = @"^LL0236" + "\r\n";
            string print06 = @"^LS0" + "\r\n";
            string print07 = @"^FO160,0^GFA,01920,01920,00020,:Z64:" + "\r\n";
            string print08 = @"eJzt07Fu2zAQBmAKQiEgi8aOeoQuRbsEUh4pBYK6g2ISyAP0Ebr7KVxkyNixk1EKfoETlPZoUNX1jqRkt3G3dgsNAfaH3+LxTlLqef2P9cqojSxzsVEXGxOMnCKib7QtSFW0FcqJkpWkGgq5kuhqsSpaRQRsX8iWZFKumW1biYUtNJFleyB7NES0fD2gfYnm7Q+hjE5yWz3NtuXrnmAxLo9MtGYx7bTRo8o025VuYw5KQ2ySs/wlmM1VNH00UNGcmAuGbPjIF6wQ0KV9jQp7cw5oNlislxsFm6KNdGKhp0oHs7PJ2YyYjzaf1zQ/F1OpL9y6o62Qy0JuHeJXxDC2ufeS+0RxlKpIVgWLI8/ExjC/j7OpSoMOpkudzBTkJMezL+eczeN/o8XbwRl7LEJ9tkAsMIw3nFdqsQVNJU1PrIjjCCbn5a2mPOWKo415yp2zMvXPZmJuMfOHcVkYnkEecoYw7xvmZvmdyFKOn11Qu91uqzQbpFalGZyubP2E/r5e/PYrp7rWtrF8mDu0PjQwx9dvViAf7LAbcMk5ks/gu5T7fik28iPUD90QzdZ1e6CJxoMfhr2Ndlm3t7Bu20PfzzZJDtZu7X3v9+nd4vsdJEc9+PtpzulBcrQHSma5vg7e39zgZ1jdpT3qte5kX78HnYya8bbn+pzvjlaNbTiHH6DJY826+tCO+lpfH96x6ZgrXTtKX5yHOubO9e95/ev1Cx7ne5U=:DEFB" + "\r\n";
            string print09 = @"^FO320,0^GFA,02560,02560,00020,:Z64:" + "\r\n";
            string print10 = @"eJzt1DFuwzAMBVAbHgR0UW+go+ho8g16jSBLr6CiB8gVDHQokKXu5qCCWdJqbFJUUA9Fp2hyXghF/KbSNPf1v6uN2sywz+y4z9ykzdcsaQsVg3mngaK2Yt0MvbYQtXllJvmhNDs5FYIdtbnRqhDcaJT5QVuInQoGTYUAfVuzMgQEZR1QqbYyGDNXLNGxpVGzNSuDoWkpg6Ga0mgvk343OlsZDFkZDPVahLB8rFkRTN5ehpBNNpyPJqcotyXrsslgXM3Grfq6/Pl4PDw8SzvBZbLvIpjwAZfk3kQI4UT2KgyeYJr9Cw+hBXvLeh4M2tCEIILpZjQPwkyykYwHYxPVzSIYO6G5WYSAdj6g8f8dvFgwuSSC8QMagjK8W3xiQsx1fGJqBj1ZKu3zEX+XTQw+5jOzicHBy701sDbcZRPXC4/1Y14YTICbbcFgB2gh9CwYuxi+D2b4tFhkf8ie7DK7yILBnS3NwcCCWeyL3olZG95OuoWwdVmzNRh+p65f16wDtuJtM9yG22Z31nUVa7mtLd3XX65vzZ139A==:8BAC" + "\r\n";
            string print11 = @"^BY109,110^FT43,147^BXN,5,200,0,0,1,~" + "\r\n";
            string print12 = @"^FH\^FD" + QRcode + "^FS" + "\r\n";
            string print13 = @"^FT40,184^A0N,33,33^FH\^FDUse only with SEW-Rectifier^FS" + "\r\n";
            string print14 = @"^FT40,219^A0N,33,31^FH\^FDID:2550." + brakeProductWorkOrderId + squenceString + "." + ddMMyy + "^FS" + "\r\n";
            string print15 = @"^FT180,148^A0N,33,33^FH\^FDMN:" + brakeProductId + squenceString + "^FS" + "\r\n";
            string print16 = @"^FT611,216^A0B,33,33^FH\^FD" + brakeProductId + "^FS" + "\r\n";
            string print17 = @"^FT583,214^A0B,33,33^FH\^FDWB:" + voltage + "^FS " + "\r\n";
            string print77 = @"^FT555,140^A0B,33,33^FH\^FDNm^FS" + "\r\n";
            string print18 = @"^FT555,214^A0B,33,33^FH\^FD" + torque + "^FS" + "\r\n";
            //左边
            string print19 = @"^FT526,215^A0B,33,33^FH\^FD" + mode + "^FS" + "\r\n";
            string print20 = @"^FT180,113^A0N,33,33^FH\^FD" + mode.Substring(0, 4) + "^FS" + "\r\n";
            string print21 = @"^FO487,1^GB0,233,5^FS" + "\r\n";
            string print22 = @"^PQ1,0,1,Y^XZ";

            StringBuilder printResult = new StringBuilder();
            printResult.Append(print01);
            printResult.Append(print02);
            printResult.Append(print03);
            printResult.Append(print04);
            printResult.Append(print05);
            printResult.Append(print06);
            printResult.Append(print07);
            printResult.Append(print08);
            printResult.Append(print09);
            printResult.Append(print10);
            printResult.Append(print11);
            printResult.Append(print12);
            printResult.Append(print13);
            printResult.Append(print14);
            printResult.Append(print15);
            printResult.Append(print16);
            printResult.Append(print17);
            printResult.Append(print77);
            printResult.Append(print18);
            printResult.Append(print19);
            printResult.Append(print20);
            printResult.Append(print21);
            printResult.Append(print22);
            information = printResult.ToString();

            return information;
        }
        public static string SendZebraFunction(string productSquence)
        {
            string information = "";

            DateTime dt = DateTime.Now;

            //型号
            string mode = WebApiGlobalClass.brakeIdentifier;
            //工单号
            string brakeProductWorkOrderId = WebApiGlobalClass.brakeWorkOrderId.ToString();
            //物料号
            string brakeProductId = WebApiGlobalClass.brakeProductId.ToString();
            //产品编号 不是工单号
            string brakeId = WebApiGlobalClass.brakeId.ToString();
            //序列号
            string squence = productSquence;
            //电压
            string voltage = WebApiGlobalClass.brakeVoltage.ToString();
            //转矩
            string torque = WebApiGlobalClass.brakeTorque.ToString();
            //整体型号说明 
            string notes = WebApiGlobalClass.notes;

            string squenceString = squence.ToString().PadLeft(4, '0');
            string DDMMYYYY = dt.ToString("ddMMyyyy");
            string HHmmss = dt.ToString("HHmmss");

            string ddMMyy = dt.ToString("ddMMyy");

            //string QRcode = "2550" + "#" + brakeProductId + "#" + brakeProductId + squenceString + "#" + brakeProductWorkOrderId + "#" + DDMMYYYY + "#" + HHmmss;
            string QRcode = "2550" + "#" + brakeProductId + "#" + brakeProductId + "#" + squenceString + "#" + brakeProductWorkOrderId + "#" + DDMMYYYY + "#" + HHmmss;

            string print01 = @"^XA~TA000~JSN^LT0^MNW^MTT^PON^PMN^LH0,0^JMA^PR4,4~SD20^JUS^LRN^CI0^XZ" + "\r\n";
            string print02 = @"^XA" + "\r\n";
            string print03 = @"^MMT" + "\r\n";
            string print04 = @"^PW650" + "\r\n";
            string print05 = @"^LL0236" + "\r\n";
            string print06 = @"^LS0" + "\r\n";
            string print07 = @"^FO160,0^GFA,01920,01920,00020,:Z64:" + "\r\n";
            string print08 = @"eJzt07Fu2zAQBmAKQiEgi8aOeoQuRbsEUh4pBYK6g2ISyAP0Ebr7KVxkyNixk1EKfoETlPZoUNX1jqRkt3G3dgsNAfaH3+LxTlLqef2P9cqojSxzsVEXGxOMnCKib7QtSFW0FcqJkpWkGgq5kuhqsSpaRQRsX8iWZFKumW1biYUtNJFleyB7NES0fD2gfYnm7Q+hjE5yWz3NtuXrnmAxLo9MtGYx7bTRo8o025VuYw5KQ2ySs/wlmM1VNH00UNGcmAuGbPjIF6wQ0KV9jQp7cw5oNlislxsFm6KNdGKhp0oHs7PJ2YyYjzaf1zQ/F1OpL9y6o62Qy0JuHeJXxDC2ufeS+0RxlKpIVgWLI8/ExjC/j7OpSoMOpkudzBTkJMezL+eczeN/o8XbwRl7LEJ9tkAsMIw3nFdqsQVNJU1PrIjjCCbn5a2mPOWKo415yp2zMvXPZmJuMfOHcVkYnkEecoYw7xvmZvmdyFKOn11Qu91uqzQbpFalGZyubP2E/r5e/PYrp7rWtrF8mDu0PjQwx9dvViAf7LAbcMk5ks/gu5T7fik28iPUD90QzdZ1e6CJxoMfhr2Ndlm3t7Bu20PfzzZJDtZu7X3v9+nd4vsdJEc9+PtpzulBcrQHSma5vg7e39zgZ1jdpT3qte5kX78HnYya8bbn+pzvjlaNbTiHH6DJY826+tCO+lpfH96x6ZgrXTtKX5yHOubO9e95/ev1Cx7ne5U=:DEFB" + "\r\n";
            string print09 = @"^FO320,0^GFA,02560,02560,00020,:Z64:" + "\r\n";
            string print10 = @"eJzt1DFuwzAMBVAbHgR0UW+go+ho8g16jSBLr6CiB8gVDHQokKXu5qCCWdJqbFJUUA9Fp2hyXghF/KbSNPf1v6uN2sywz+y4z9ykzdcsaQsVg3mngaK2Yt0MvbYQtXllJvmhNDs5FYIdtbnRqhDcaJT5QVuInQoGTYUAfVuzMgQEZR1QqbYyGDNXLNGxpVGzNSuDoWkpg6Ga0mgvk343OlsZDFkZDPVahLB8rFkRTN5ehpBNNpyPJqcotyXrsslgXM3Grfq6/Pl4PDw8SzvBZbLvIpjwAZfk3kQI4UT2KgyeYJr9Cw+hBXvLeh4M2tCEIILpZjQPwkyykYwHYxPVzSIYO6G5WYSAdj6g8f8dvFgwuSSC8QMagjK8W3xiQsx1fGJqBj1ZKu3zEX+XTQw+5jOzicHBy701sDbcZRPXC4/1Y14YTICbbcFgB2gh9CwYuxi+D2b4tFhkf8ie7DK7yILBnS3NwcCCWeyL3olZG95OuoWwdVmzNRh+p65f16wDtuJtM9yG22Z31nUVa7mtLd3XX65vzZ139A==:8BAC" + "\r\n";
            string print11 = @"^BY109,110^FT43,147^BXN,5,200,0,0,1,~" + "\r\n";
            string print12 = @"^FH\^FD" + QRcode + "^FS" + "\r\n";
            string print13 = @"^FT40,184^A0N,33,33^FH\^FDUse only with SEW-Rectifier^FS" + "\r\n";
            //string print14 = @"^FT40,219^A0N,33,31^FH\^FDID:2550." + brakeProductWorkOrderId + squenceString + "." + ddMMyy + "^FS" + "\r\n";
            string print14 = @"^FT40,219^A0N,33,31^FH\^FDID:2550." + brakeProductWorkOrderId + squenceString + "." + DDMMYYYY + "^FS" + "\r\n";
            string print15 = @"^FT180,148^A0N,33,33^FH\^FDMN:" + brakeProductId + squenceString + "^FS" + "\r\n";
            string print16 = @"^FT611,216^A0B,33,33^FH\^FD" + brakeProductId + "^FS" + "\r\n";
            string print17 = @"^FT583,214^A0B,33,33^FH\^FDWB:" + voltage + "^FS " + "\r\n";
            string print77 = @"^FT555,140^A0B,33,33^FH\^FDNm^FS" + "\r\n";
            string print18 = @"^FT555,214^A0B,33,33^FH\^FD" + torque + "^FS" + "\r\n";
            //左边
            string print19 = @"^FT526,215^A0B,33,33^FH\^FD" + mode + "^FS" + "\r\n";
            //string print20 = @"^FT180,113^A0N,33,33^FH\^FD" + mode.Substring(0, 4) + "^FS" + "\r\n";
            string print20 = @"^FT180,113^A0N,33,33^FH\^FD" + mode + "^FS" + "\r\n";
            string print21 = @"^FO487,1^GB0,233,5^FS" + "\r\n";
            string print22 = @"^PQ1,0,1,Y^XZ";

            StringBuilder printResult = new StringBuilder();
            printResult.Append(print01);
            printResult.Append(print02);
            printResult.Append(print03);
            printResult.Append(print04);
            printResult.Append(print05);
            printResult.Append(print06);
            printResult.Append(print07);
            printResult.Append(print08);
            //printResult.Append(print09);
            //printResult.Append(print10);
            printResult.Append(print11);
            printResult.Append(print12);
            printResult.Append(print13);
            printResult.Append(print14);
            printResult.Append(print15);
            printResult.Append(print16);
            printResult.Append(print17);
            printResult.Append(print77);
            printResult.Append(print18);
            printResult.Append(print19);
            printResult.Append(print20);
            printResult.Append(print21);
            printResult.Append(print22);
            information = printResult.ToString();

            return information;
        }
    }
}
