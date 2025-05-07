using CommunicationProject.CommunicationEntiy;
using CommunicationProject.CommunicationWay;
using CommunicationProject.CommunucationPropertie;
using HslCommunication;
using HslCommunication.Profinet.Melsec.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TESTFORM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //定义连接，参数1：连接名称，参数2：连接类型；
            ComEntiy ComType = new CommunicationProject.CommunicationEntiy.ComEntiy("示例用连接", ComWay_Enum.CommunicationWay.松下Mewtocol串口);

            //连接属性汇总
            ConnectPropertie connectPropertie = new ConnectPropertie();

            //告知连接属性我们是用哪种类型：
            connectPropertie.CommunicationWay = ComWay_Enum.CommunicationWay.松下ModbusRTU;

            //COM口连接的属性：
            connectPropertie.Com = "COM2";
            connectPropertie.IBaudRate = 115200;
            connectPropertie.IDataBits = 8;
            connectPropertie.IParity = Parity.None;
            connectPropertie.IStopBits = StopBits.One;

            //给连接对象设置属性
            ComType.ComWay.SetConnectPropertie(connectPropertie);

            //启动连接，返回连接是否成功
            bool ConnectState = ComType.ComWay.Connect();

            //连接状态更新。
            ComType.ComWay.IsConnected = ConnectState;

            //读取BOOL地址
            //PLCResult<bool> plc_result_bool = ComType.IPLCWay.ReadBool("R100");
            //bool result_bool = plc_result_bool.Content;

            //读取FLOAT地址
            //PLCResult<float> plc_result_float = ComType.IPLCWay.ReadFloat("D300");
            //float result_float = plc_result_float.Content;

            //读取INT地址
            PLCResult<int> plc_result_int = ComType.IPLCWay.ReadInt32("D300");
            int result_int = plc_result_int.Content;

            //数组也是可以的，参数是，1. 起始地址 2. 找起始地址后多少位，里面有很多Multi方法，自己去找找用一下；
            ////比如我从地址 DT300开始，找到DT310，那就 300，10
            //PLCResult<int[]> plc_result_int_arr = ComType.IPLCWay.ReadMultiInt32("D300", 10);
            //int[] ints = plc_result_int_arr.Content;

            //写值也是同理；
            PLCResult write_result = ComType.IPLCWay.WriterInt32("D300", 2);
            if (write_result.IsSuccess)
            {
                //成功写入
            }
            else
            {
                //否则
                Console.WriteLine(write_result.ErrorMessage);
            }


            //断开连接
            ComType.ComWay.DisConnect();



        }

        private void button2_Click(object sender, EventArgs e)
        {
            HslCommunication.Profinet.Panasonic.PanasonicMewtocol panasonicMewtocol = new HslCommunication.Profinet.Panasonic.PanasonicMewtocol();

            panasonicMewtocol.SerialPortInni("COM2",115200);

            panasonicMewtocol.Open();

            if (panasonicMewtocol.IsOpen())
            {

            }

            panasonicMewtocol.Write("D300", 1);



            OperateResult<int> _or = panasonicMewtocol.ReadInt32("D300");

            bool is_succ = _or.IsSuccess;

            int i_result = _or.Content;

            string i_result_msg = _or.Message;

            int i_result_ercode = _or.ErrorCode;


            panasonicMewtocol.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ComEntiy ComTypeCustom = new ComEntiy("松下Mewtocol网口", ComWay_Enum.CommunicationWay.松下Mewtocol网口);
            ConnectPropertie connectPropertieCustom = new ConnectPropertie();

            string sIP = "128.0.1.11";
            string sPort = "10001";

            connectPropertieCustom.CommunicationWay = ComWay_Enum.CommunicationWay.松下Mewtocol网口;

            connectPropertieCustom.IPAddress = sIP.Trim();
            connectPropertieCustom.Port = Convert.ToInt32(sPort.Trim());

            ComTypeCustom.ComWay.SetConnectPropertie(connectPropertieCustom);

            //启动连接，返回连接是否成功
            bool ConnectState = ComTypeCustom.ComWay.Connect();

            //读取INT地址
            PLCResult<int> plc_result_int = ComTypeCustom.IPLCWay.ReadInt32("D300");
            int result_int = plc_result_int.Content;

            //写值也是同理；
            PLCResult write_result = ComTypeCustom.IPLCWay.WriterInt32("D300", 2);
            if (write_result.IsSuccess)
            {
                //成功写入
            }
            else
            {
                //否则
                Console.WriteLine(write_result.ErrorMessage);
            }


            //断开连接
            ComTypeCustom.ComWay.DisConnect();
        }
    }
}
