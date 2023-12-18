using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace MotorBrakeTestApp
{
    public partial class 补偿参数设定 : Form
    {
        public 补偿参数设定()
        {
            InitializeComponent();
        }

        private void 补偿参数设定_Load(object sender, EventArgs e)
        {
            ReadParameterFromXml();
        }


        /// <summary>
        /// 将参数保存到XML
        /// </summary>
        /// <param name="filepath"></param>
        private void WriteParameterToXml()
        {
            try
            {

                string strAppPath = Application.StartupPath; //获得可执bai行文件的du路径。zhi
                string strConfigPath = strAppPath + "\\MyXml\\" + SaveDataClassIndex.voltage+".xml"; //自己调dao整一下相对zhuan路shu径。
                //xml序列化到test.xml文件中
                SaveDataClass saveDataClass = new SaveDataClass();

                saveDataClass.list.Add(textBox_R1补偿.Text);
                saveDataClass.list.Add(textBox_R2补偿.Text);
                saveDataClass.list.Add(textBox_额定电流补偿.Text);
                saveDataClass.list.Add(textBox_额定功率补偿.Text);
                saveDataClass.list.Add(textBox_60电流补偿.Text);
                saveDataClass.list.Add(textBox_60功率补偿.Text);


                saveDataClass.list.Add(textBox_跑合转矩补偿.Text);
                saveDataClass.list.Add(textBox_制动转矩补偿.Text);
                saveDataClass.list.Add(textBox_残留转矩补偿.Text);

                using (FileStream fsWriter = new FileStream(strConfigPath, FileMode.Create, FileAccess.Write))
                {
                    XmlSerializer xs = new XmlSerializer(typeof(SaveDataClass));
                    xs.Serialize(fsWriter, saveDataClass);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("保存参数文件XML错误");
            }

        }
        /// <summary>
        /// 将参数从XML中读取
        /// </summary>
        /// <param name="filepath"></param>
        private bool ReadParameterFromXml()
        {
            try
            {

                string strAppPath = Application.StartupPath; //获得可执bai行文件的du路径。zhi
                string strConfigPath = strAppPath + "\\MyXml\\" + SaveDataClassIndex.voltage + ".xml"; //自己调dao整一下相对zhuan路shu径。

                //判断是否存在这个文件
                if (File.Exists(strConfigPath))
                {
                    //再做操作
                    //从test.xml文件中反序列化出来
                    SaveDataClass saveDataClass = new SaveDataClass();
                    using (FileStream fsReader = new FileStream(strConfigPath, FileMode.Open, FileAccess.Read))
                    {
                        XmlSerializer xs = new XmlSerializer(typeof(SaveDataClass));
                        saveDataClass = xs.Deserialize(fsReader) as SaveDataClass;
                        if (saveDataClass.list.Count > 0)
                        {
                            int listCounts = 0;

                            textBox_R1补偿.Text = saveDataClass.list[listCounts++];
                            textBox_R2补偿.Text = saveDataClass.list[listCounts++];
                            textBox_额定电流补偿.Text = saveDataClass.list[listCounts++];
                            textBox_额定功率补偿.Text = saveDataClass.list[listCounts++];
                            textBox_60电流补偿.Text = saveDataClass.list[listCounts++];
                            textBox_60功率补偿.Text = saveDataClass.list[listCounts++];

                            textBox_跑合转矩补偿.Text = saveDataClass.list[listCounts++];
                            textBox_制动转矩补偿.Text = saveDataClass.list[listCounts++];
                            textBox_残留转矩补偿.Text = saveDataClass.list[listCounts++];

                        }

                    }
                    return true;
                }
                else
                {
                    //如果判断不存在这个文件 则读取PLC 内容
                    return false;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("读取参数文件XML错误");
                return false;
            }


        }

        private void btn保存_Click(object sender, EventArgs e)
        {
            WriteParameterToXml();
        }
    }
}
