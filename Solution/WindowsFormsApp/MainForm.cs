

using log4net;
using PandoraTool;
using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace WindowsFormsApp
{
    public partial class MainForm : Form
    {
        //这里的 loginfo 和 log4net.config 里的名字要一样
        public static readonly ILog log_info = LogManager.GetLogger("loginfo");
        //这里的 logerror 和 log4net.config 里的名字要一样
        public static readonly ILog log_error = LogManager.GetLogger("logerror");

        delegate void AsynUpdateUI(int step);

        delegate void AsynAccomplishTask();

        public MainForm()
        {
            InitializeComponent();
        }

        public int addNumber = 0;

        private void openFile_Click(object sender, EventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();
            try
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtInfo.Text = (DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "开始导入文件...") + Environment.NewLine;
                    //创建连接字符串
                    string connStr = @"Data Source=" + System.AppDomain.CurrentDomain.BaseDirectory + "db\\FilesDb.db;Initial Catalog=main;Integrated Security=True;Max Pool Size=10";

                    // System.IO.Path.GetFullPath(openFileDialog1.FileName);                             //绝对路径

                    // System.IO.Path.GetExtension(openFileDialog1.FileName);                          //文件扩展名

                    //System.IO.Path.GetFileNameWithoutExtension(openFileDialog1.FileName);//文件名没有扩展名

                    //System.IO.Path.GetFileName(openFileDialog1.FileName);                          //得到文件

                    //System.IO.Path.GetDirectoryName(openFileDialog1.FileName);                  //得到路径
                    string[] fileFullNames = openFileDialog.FileNames;
                    FileType fileType = FileType.TXT;
                    if (fileFullNames.Length > 0)
                    {
                        for (int item = 0; item < fileFullNames.Length; item++)
                        {
                            string filePath = fileFullNames[item];
                            string fileFullName = Path.GetFileName(filePath);

                            string tableName = Path.GetFileNameWithoutExtension(filePath).ToLower().ToString();

                            string extFileName = Path.GetExtension(filePath);
                            if (extFileName.Contains("txt"))
                            {
                                fileType = FileType.TXT;
                            }
                            if (extFileName.Contains("csv"))
                            {
                                fileType = FileType.CSV;
                            }

                            Console.WriteLine(extFileName);

                            string[] lines = File.ReadAllLines(filePath, Encoding.GetEncoding("GB2312"));

                            if (lines.Length > 0)
                            {
                                //创建连接对象
                                using (SQLiteConnection dbConnection = new SQLiteConnection(connStr))
                                {
                                    dbConnection.Open();
                                    //创建命令对象
                                    using (SQLiteCommand cmd = new SQLiteCommand(dbConnection))
                                    {
                                        if (tableName.Length > 18)
                                        {
                                            tableName = tableName.Substring(18).Trim();
                                        }

                                        string existTable = "SELECT COUNT(*) FROM sqlite_master where type = 'table' and name = '" + tableName + "'";

                                        cmd.CommandText = existTable;

                                        if (0 == Convert.ToInt32(cmd.ExecuteScalar()))
                                        {
                                            //数据库表不存在
                                            CreateTable(dbConnection, cmd, tableName, lines, false, fileFullName, fileType);
                                        }

                                        else
                                        {
                                            //数据库表存在
                                            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                                            DialogResult dr = MessageBox.Show("【" + tableName + "】已经存在，继续导入吗?", "系统提示", messButton, MessageBoxIcon.Warning);

                                            if (dr == DialogResult.OK)//如果点击“确定”按钮
                                            {
                                                CreateTable(dbConnection, cmd, tableName, lines, true, fileFullName, fileType);
                                            }
                                        }
                                        if (treeView.Nodes.Count > 0)
                                        {
                                            treeView.Nodes.Clear();
                                        }

                                        //返回所有数据表
                                        TreeList(null);
                                    }
                                }
                            }
                        }
                    }

                    stringBuilder.AppendLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "导入文件结束...");
                    txtInfo.Text += stringBuilder.ToString();
                }
            }
            catch (Exception ex)
            {
                log_info.Error(ex.Message);
                stringBuilder.AppendLine(ex.Message);
                stringBuilder.AppendLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "导入文件结束...");
            }
        }
        /// <summary>
        /// 返回所有数据表
        /// </summary>
        /// <param name="dropStr">删除表语句</param>
        private void TreeList(string dropStr)
        {
            //创建连接字符串
            string connStr = @"Data Source=" + System.AppDomain.CurrentDomain.BaseDirectory + "db\\FilesDb.db;Initial Catalog=main;Integrated Security=True;Max Pool Size=10";
            SQLiteTransaction tr = null;
            //创建连接对象
            try
            {
                using (SQLiteConnection dbConnection = new SQLiteConnection(connStr))
                {
                    dbConnection.Open();
                    tr = dbConnection.BeginTransaction();//事务开始
                    //创建命令对象
                    using (SQLiteCommand cmd = new SQLiteCommand(dbConnection))
                    {
                        if (dropStr != null)
                        {
                            cmd.CommandText = dropStr;
                            cmd.ExecuteNonQuery();
                        }
                        //返回所有数据表
                        if (treeView.Nodes.Count > 0)
                        {
                            treeView.Nodes.Clear();
                        }
                        string queryTables = "SELECT name FROM sqlite_master where type = 'table'";
                        cmd.CommandText = queryTables;
                        TreeNode rootNode = new TreeNode();
                        rootNode.Text = "全部";
                        rootNode.SelectedImageIndex = 0;

                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                rootNode.Nodes.Add(reader["name"].ToString(), reader["name"].ToString(), 2, 2);
                            }
                        }
                        tr.Commit();
                        treeView.Nodes.Add(rootNode);
                        treeView.ExpandAll();
                        if (dropStr != null)
                        {
                            txtInfo.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + dropStr;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                log_error.Error(ex.Message);
                tr.Rollback();//回滚
            }
        }

        /// <summary>
        /// 读取文件，创建数据库表，写入数据
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="cmd"></param>
        /// <param name="tableName"></param>
        /// <param name="lines"></param>
        private void CreateTable(SQLiteConnection dbConnection, SQLiteCommand cmd, String tableName, string[] lines, bool isExist, string fileFullName, FileType fileType)
        {
            SQLiteTransaction tr = dbConnection.BeginTransaction();//事务开始
            string insertStr = null;
            StringBuilder stringBuilder = new StringBuilder();
            int insertNum = 0;
            int columnCount = 0;
            try
            {
                //表存在，则删除表
                if (isExist)
                {
                    cmd.CommandText = "DROP TABLE '" + tableName + "'";
                    cmd.ExecuteNonQuery();
                }
                //创建数据库表
                string createNewTableStr = "CREATE TABLE '" + tableName + "' (";

                string[] columns = null;

                //txt 文件
                if (fileType == FileType.TXT)
                {
                    columns = lines[0].Split('\t');
                }
                //csv 文件
                if (fileType == FileType.CSV)
                {
                    columns = lines[0].Split(',');
                }
                if (columns != null && columns.Length > 0)
                {
                    //插入表头
                    for (int m = 0; m < columns.Length; m++)
                    {
                        if (!String.IsNullOrEmpty(columns[m]))
                        {

                            if (columns[m].ToLower().Contains("time"))
                            {
                                createNewTableStr += (columns[m] + " TIMESTAMP,");
                            }
                            else
                            {
                                createNewTableStr += (columns[m] + " TEXT,");
                            }
                            columnCount++;
                        }
                    }
                    createNewTableStr = createNewTableStr.Substring(0, createNewTableStr.Length - 1) + (")");
                    cmd.CommandText = createNewTableStr;
                    cmd.ExecuteNonQuery();
                    //插入数据
                    for (int index = 1; index < lines.Length; index++)
                    {
                        string[] values = null;

                        //txt 文件
                        if (fileType == FileType.TXT)
                        {
                            values = lines[index].Split('\t');
                        }
                        //csv 文件
                        if (fileType == FileType.CSV)
                        {
                            values = lines[index].Split(',');
                        }
                        string insertValues = null;
                        for (int item = 0; item < columnCount; item++)
                        {
                            string value = values[item].Replace("'", "");
                            DateTime dateTime;
                            bool isDateTime = DateTime.TryParse(value, out dateTime);

                            //能转成时间格式
                            if (isDateTime && value.Contains("/"))
                            {
                                insertValues += ("'" + dateTime.ToString("yyyy-MM-dd HH:mm:ss") + "',");
                            }
                            else
                            {
                                insertValues += ("'" + value + "',");
                            }

                            //insertValues += ("'" + values[item] + "',");
                        }
                        //表插入内容
                        insertStr = "INSERT INTO '" + tableName + "' VALUES(" + insertValues.Substring(0, insertValues.Length - 1) + ")";
                        cmd.CommandText = insertStr;
                        insertNum = index + 1;
                        cmd.ExecuteNonQuery();
                    }
                }
                tr.Commit();//把事务调用的更改保存到数据库中，事务结束
                stringBuilder.AppendLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "导入文件【" + fileFullName + "】成功...");
                log_info.Info(stringBuilder.ToString());
            }
            catch (Exception ex)
            {
                stringBuilder.AppendLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "导入文件【" + fileFullName + "】失败...");
                stringBuilder.AppendLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "第【" + insertNum + "行】写入出错...");
                stringBuilder.AppendLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + insertStr);
                stringBuilder.AppendLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + ex.Message);
                log_error.Error(stringBuilder.ToString());
                tr.Rollback();//回滚
            }
            finally
            {
                txtInfo.Text += stringBuilder.ToString();
            }
        }

        private void moreFilter_Click(object sender, EventArgs e)
        {
            string queryStr = txtFilter.Text.Trim();
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(queryStr);
            if (!String.IsNullOrEmpty(queryStr))
            {
                try
                {
                    //创建连接字符串
                    string connStr = @"Data Source=" + System.AppDomain.CurrentDomain.BaseDirectory + "db\\FilesDb.db;Initial Catalog=main;Integrated Security=True;Max Pool Size=10";

                    //创建连接对象
                    using (SQLiteConnection dbConnection = new SQLiteConnection(connStr))
                    {
                        dbConnection.Open();
                        //创建命令对象
                        using (SQLiteDataAdapter sqlDataAdapter = new SQLiteDataAdapter(queryStr, dbConnection))
                        {
                            txtInfo.Text = queryStr;
                            DataTable data = new DataTable();
                            sqlDataAdapter.Fill(data);
                            dataGridView.DataSource = data;
                            stringBuilder.AppendLine("共【" + data.Rows.Count + "】条");
                        }
                    }

                }
                catch (Exception ex)
                {
                    stringBuilder.AppendLine(ex.Message);
                    log_error.Error(ex.Message);
                }
                finally
                {

                    txtInfo.Text = stringBuilder.ToString();
                }
            }
            else
            {
                MessageBox.Show("请输入自定义查询内容！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        #region 删除表
        private void delBtn_Click(object sender, EventArgs e)
        {
            TreeNode treeNode = treeView.SelectedNode;
            if (treeNode != null && treeNode.Text != "全部")
            {
                string tableName = treeNode.Text;
                //数据库表存在
                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                DialogResult dr = MessageBox.Show("确定要删除【" + tableName + "】吗?", "系统提示", messButton, MessageBoxIcon.Warning);
                if (dr == DialogResult.OK)//如果点击“确定”按钮
                {
                    string dropStr = "DROP TABLE IF EXISTS '" + tableName + "'";
                    //返回所有数据表
                    TreeList(dropStr);
                }
            }
            else
            {
                MessageBox.Show("请选择要删除的数据表！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        private void cleanBtn_Click(object sender, EventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();
            TreeNode rootNode = treeView.SelectedNode;
            if (rootNode != null && rootNode.Text == "全部")
            {
                try
                {
                    //创建连接字符串
                    string connStr = @"Data Source=" + System.AppDomain.CurrentDomain.BaseDirectory + "db\\FilesDb.db;Initial Catalog=main;Integrated Security=True;Max Pool Size=10";

                    //创建连接对象
                    using (SQLiteConnection dbConnection = new SQLiteConnection(connStr))
                    {
                        //数据库表存在
                        MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                        DialogResult dr = MessageBox.Show("确定要清空数据库吗?", "系统提示", messButton, MessageBoxIcon.Warning);
                        if (dr == DialogResult.OK)//如果点击“确定”按钮
                        {
                            //创建命令对象
                            dbConnection.Open();
                            using (SQLiteCommand sQLiteCommand = new SQLiteCommand(dbConnection))
                            {
                                SQLiteTransaction tr = dbConnection.BeginTransaction();//事务开始
                                try
                                {
                                    TreeNodeCollection childrenNode = rootNode.Nodes;
                                    foreach (TreeNode treeNode in childrenNode)
                                    {
                                        string dropStr = "DROP TABLE IF EXISTS '" + treeNode.Text + "'";
                                        sQLiteCommand.CommandText = dropStr;
                                        stringBuilder.AppendLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + dropStr);
                                        sQLiteCommand.ExecuteNonQuery();
                                    }

                                    tr.Commit();//把事务调用的更改保存到数据库中，事务结束
                                    if (treeView.Nodes.Count > 0)
                                    {
                                        treeView.Nodes.Clear();
                                    }
                                }
                                catch (Exception ex)
                                {
                                    stringBuilder.AppendLine(ex.Message);
                                    log_error.Error(ex.Message);
                                    tr.Rollback();//回滚
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    stringBuilder.AppendLine(ex.Message);
                    log_error.Error(ex.Message);
                }
                finally
                {
                    txtInfo.Text = stringBuilder.ToString();
                }
            }
            else
            {
                MessageBox.Show("请选择全部节点！", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //返回所有数据表
            TreeList(null);
        }

        private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode treeNode = e.Node;
            StringBuilder stringBuilder = new StringBuilder();
            if (treeNode != null && treeNode.Text != "全部")
            {
                try
                {
                    //创建连接字符串
                    string connStr = @"Data Source=" + System.AppDomain.CurrentDomain.BaseDirectory + "db\\FilesDb.db;Initial Catalog=main;Integrated Security=True;Max Pool Size=10";

                    string tableName = treeNode.Text;

                    //创建连接对象
                    using (SQLiteConnection dbConnection = new SQLiteConnection(connStr))
                    {
                        //创建命令对象
                        dbConnection.Open();

                        string queryStr = "SELECT * from '" + tableName + "'";
                        stringBuilder.AppendLine(queryStr);

                        using (SQLiteDataAdapter sqlDataAdapter = new SQLiteDataAdapter(queryStr, dbConnection))
                        {
                            DataTable data = new DataTable();
                            sqlDataAdapter.Fill(data);
                            dataGridView.DataSource = data;
                            stringBuilder.AppendLine("共【" + data.Rows.Count + "】条");
                        }
                    }
                }
                catch (Exception ex)
                {
                    stringBuilder.AppendLine(ex.Message);
                    log_error.Error(ex.Message);
                }
                finally
                {
                    txtInfo.Text = stringBuilder.ToString();
                    tabAllInfo.SelectedIndex = 1;
                }
            }
        }

        private void downBtn_Click(object sender, EventArgs e)
        {
            DialogResult dr = saveFileDialog.ShowDialog();
            string fileName = saveFileDialog.FileName;
            if (dr == DialogResult.OK && !string.IsNullOrEmpty(fileName))
            {
                if (dataGridView.Rows.Count > 0)
                {
                    SaveModel saveModel = new SaveModel();
                    saveModel.dataTable = ((DataTable)dataGridView.DataSource).Copy();
                    saveModel.fileName = fileName;
                    //线程池
                    ThreadPool.QueueUserWorkItem(new WaitCallback(DataTableToCSV), saveModel);
                }
            }
        }

        static ReaderWriterLockSlim LogWriteLock = new ReaderWriterLockSlim();

        public void DataTableToCSV(object fileName)
        {
            SaveModel saveModel = (SaveModel)fileName;
            DataTable dataTable = saveModel.dataTable;
            string strLine = "";
            try
            {
                //FileStream是指向网络或者硬盘的一个文件的对象
                using (FileStream fs = new FileStream(saveModel.fileName, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs, Encoding.Default))
                    {
                        LogWriteLock.EnterWriteLock();
                        //表头
                        for (int i = 0; i < dataTable.Columns.Count; i++)
                        {
                            strLine += (dataTable.Columns[i].ColumnName.ToLower().Trim() + ",");
                        }
                        strLine.Remove(strLine.Length - 1);
                        sw.WriteLine(strLine);
                        strLine = "";
                        //表的内容
                        for (int j = 0; j < dataTable.Rows.Count; j++)
                        {
                            strLine = "";
                            int colCount = dataTable.Columns.Count;
                            for (int k = 0; k < colCount; k++)
                            {
                                if (k > 0 && k < colCount)
                                {
                                    strLine += ",";
                                }
                                if (dataTable.Rows[j][k] == null)
                                {
                                    strLine += "";
                                }
                                else
                                {
                                    string cell = dataTable.Rows[j][k].ToString().Trim();
                                    //防止里面含有特殊符号
                                    cell = cell.Replace("\"", "\"\"");
                                    cell = "\"" + cell + "\"";
                                    strLine += cell;
                                }
                            }
                            sw.WriteLine(strLine);
                        }
                    }
                }
                MessageBox.Show("导出成功", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("导出失败", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                log_error.Error(ex.Message);
                txtInfo.Text = ex.Message;
            }
            finally
            {
                LogWriteLock.ExitWriteLock();
            }
        }
        //更新UI
        private void UpdataUIStatus(int step, string message)
        {
            if (InvokeRequired)
            {
                this.Invoke(new AsynUpdateUI(delegate (int s)
                {
                    this.addNumber += s;
                    this.txtInfo.Text += message;
                }), step);
            }
            else
            {
                this.addNumber += step;
                this.txtInfo.Text += message;
            }
        }

        //完成任务时需要调用
        private void Accomplish()
        {

            //还可以进行其他的一些完任务完成之后的逻辑处理
            if (InvokeRequired)
            {
                this.Invoke(new AsynAccomplishTask(delegate ()
                {
                    this.txtInfo.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "导入文件结束...";
                    TreeList(null);
                }));
            }
            else
            {
                this.txtInfo.Text += DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "导入文件结束...";
                TreeList(null);
            }
        }

        private void txtBtn_Click(object sender, EventArgs e)
        {
            try
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtInfo.Text = (DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "开始导入文件...") + Environment.NewLine;
                    string[] fileFullNames = openFileDialog.FileNames;
                    if (fileFullNames.Length > 0)
                    {
                        DataWrite dataWrite = new DataWrite();//实例化一个写入数据的类
                        dataWrite.UpdateUIDelegate += UpdataUIStatus;//绑定更新任务状态的委托
                        dataWrite.TaskCallBack += Accomplish;//绑定完成任务要调用的委托
                        //线程池
                        ThreadPool.QueueUserWorkItem(new WaitCallback(dataWrite.Write), fileFullNames);
                        //Thread thread = new Thread(new ParameterizedThreadStart(dataWrite.Write));
                        //thread.IsBackground = true;
                        //thread.Start(fileFullNames);
                    }
                }
            }
            catch (Exception ex)
            {
                log_info.Error(ex.Message);
            }
        }
    }
}
