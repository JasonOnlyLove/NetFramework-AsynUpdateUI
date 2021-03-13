using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PandoraTool
{
    public class SaveModel
    {
        /// <summary>
        /// 导出内容
        /// </summary>
       public DataGridView dataGridView { get; set; }

        /// <summary>
        /// 导出内容
        /// </summary>
        public DataTable dataTable { get; set; }

        /// <summary>
        /// 导出文件名
        /// </summary>
        public string fileName { get; set; }
    }
}
