using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace 异步刷新进度条1
{
    public partial class Form1 : Form
    {
        delegate void AsynUpdateUI(int step);

        public Form1()
        {
            InitializeComponent();
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            int taskCount = 10000; //任务量为10000
            this.pgbWrite.Maximum = taskCount;
            this.pgbWrite.Value = 0;

            DataWrite dataWrite = new DataWrite();//实例化一个写入数据的类
            dataWrite.UpdateUIDelegate += UpdataUIStatus;//绑定更新任务状态的委托
            dataWrite.TaskCallBack += Accomplish;//绑定完成任务要调用的委托

            Thread thread = new Thread(new ParameterizedThreadStart(dataWrite.Write));
            thread.IsBackground = true;
            thread.Start(taskCount);
        }

        //更新UI
        private void UpdataUIStatus(int step)
        {
            if (InvokeRequired)
            {
                Console.WriteLine("12");
                this.Invoke(new AsynUpdateUI(delegate(int s)
                {
                    this.pgbWrite.Value += s;
                    this.lblWriteStatus.Text = this.pgbWrite.Value.ToString() + "/" + this.pgbWrite.Maximum.ToString();
                }), step);
            }
            else
            {
                Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>");
                // this.pgbWrite.Value += step;
                //this.lblWriteStatus.Text = this.pgbWrite.Value.ToString() + "/" + this.pgbWrite.Maximum.ToString();
            }
        }

        //完成任务时需要调用
        private void Accomplish()
        {
            //还可以进行其他的一些完任务完成之后的逻辑处理
            MessageBox.Show("任务完成");
        }
    }
}
