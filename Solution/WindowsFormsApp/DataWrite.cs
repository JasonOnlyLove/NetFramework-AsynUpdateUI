using log4net;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PandoraTool
{
    public class DataWrite
    {

        //这里的 loginfo 和 log4net.config 里的名字要一样
        public static readonly ILog log_info = LogManager.GetLogger("loginfo");
        //这里的 logerror 和 log4net.config 里的名字要一样
        public static readonly ILog log_error = LogManager.GetLogger("logerror");

        public delegate void UpdateUI(int step, string message);//声明一个更新主线程的委托
        public UpdateUI UpdateUIDelegate;

        public delegate void AccomplishTask();//声明一个在完成任务时通知主线程的委托
        public AccomplishTask TaskCallBack;

        public void Write(object fileObj)
        {
            FileType fileType = FileType.TXT;
            string[] fileFullNames = (string[])fileObj;
            string connStr = @"Data Source=" + System.AppDomain.CurrentDomain.BaseDirectory + "db\\FilesDb.db;Initial Catalog=main;Integrated Security=True;Max Pool Size=10";

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
                                CreateTable(dbConnection, cmd, tableName, lines, false, fileFullName, fileType, item + 1, fileFullNames.Length);
                            }

                            else
                            {
                                //数据库表存在
                                MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
                                DialogResult dr = MessageBox.Show("【" + tableName + "】已经存在，继续导入吗?", "系统提示", messButton, MessageBoxIcon.Warning);

                                if (dr == DialogResult.OK)//如果点击“确定”按钮
                                {
                                    CreateTable(dbConnection, cmd, tableName, lines, true, fileFullName, fileType, item + 1, fileFullNames.Length);
                                }
                            }
                        }
                    }
                }
            }
            //任务完成时通知主线程作出相应的处理
            TaskCallBack();
        }
        /// <summary>
        /// 读取文件，创建数据库表，写入数据
        /// </summary>
        /// <param name="dbConnection"></param>
        /// <param name="cmd"></param>
        /// <param name="tableName"></param>
        /// <param name="lines"></param>
        private void CreateTable(SQLiteConnection dbConnection, SQLiteCommand cmd, String tableName, string[] lines, bool isExist, string fileFullName, FileType fileType, int indexFile, int allFileNumber)
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

                stringBuilder.AppendLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "导入第【" + indexFile + "/" + allFileNumber + "个】文件【" + fileFullName + "】成功...");
                log_info.Info(stringBuilder.ToString());

                //写入一条数据，调用更新主线程ui状态的委托
                UpdateUIDelegate(1, stringBuilder.ToString());
            }
            catch (Exception ex)
            {
                stringBuilder.AppendLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "导入第【" + indexFile + "/" + allFileNumber + "个】文件【" + fileFullName + "】失败...");
                stringBuilder.AppendLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + "第【" + insertNum + "行】写入出错...");
                stringBuilder.AppendLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + insertStr);
                stringBuilder.AppendLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + ex.Message);
                UpdateUIDelegate(1, stringBuilder.ToString());

                log_error.Error(stringBuilder.ToString());
                tr.Rollback();//回滚
            }
        }
    }
}
