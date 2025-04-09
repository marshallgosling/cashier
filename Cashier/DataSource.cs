using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace Cashier
{
    enum DataSourceErr
    {
        Success,
        FileNotExist,
    }

    class Clothing
    {
        private string tagCode;


        public string TagCode
        {
            get { return this.tagCode; }
            set { this.tagCode = value; }
        }

    }

    class DataSource
    {
        private Hashtable m_hashData = new Hashtable();
        private bool m_isInited = false;

        public DataSourceErr Init(string dataFile)
        {
            //查询文件是否在
            if (!File.Exists(dataFile))
            {
                return DataSourceErr.FileNotExist;
            }

            //从文件读取数据，分析，并加入到Hashtable中去
            uint linesCount = 0;
            StreamReader sr = new StreamReader(dataFile, Encoding.GetEncoding("GB18030"));
            string fileData = sr.ReadToEnd();
            string[] dataArray = fileData.Split('\n');

            string line;
            //while ((line = sr.ReadLine()) != null) 
            while (linesCount < dataArray.Length)
            {
                line = dataArray[linesCount];
                ++linesCount;
                //Trace.WriteLine(line);

                string[] sArray = line.Split('\t');
                if (sArray.Length <= 4)
                {
                    Console.WriteLine("Not enough string, just {0} len, data is {1}.\n",
                        sArray.Length, line);
                    //char[] bytes = line.ToCharArray();
                    continue;
                }

                Clothing clo = new Clothing();
                clo.TagCode = sArray[1];


                m_hashData.Add(clo.TagCode, clo);
            }

            string str = linesCount.ToString() + " lines has been added.";
            Trace.WriteLine(str);

            lock ((object)m_isInited)
            {
                m_isInited = true;
            }

            return DataSourceErr.Success;
        }

        public Clothing GetClothing(string key)
        {

            return null == key ? null : (Clothing)m_hashData[key];
        }

        public bool IsInited()
        {
            lock ((object)m_isInited)
            {
                return m_isInited;
            }
        }
    }


    class Excel
    {
        #region 写入excel
        public static bool ListToExcel(string dataFile, ArrayList list, String orderNo, int columns=1)
        {
            bool result = false;
            IWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet(orderNo);//创建一个名称为Sheet0的表;
            IRow row;//（第一行写标题)
            //row.CreateCell(0).SetCellValue("标题1");//第一列标题，以此类推
            //row.CreateCell(1).SetCellValue("标题2");
            //row.CreateCell(2).SetCellValue("标题3");
            int count = list.Count;//
            int max = 65535;//最大行数限制
            if (count < max)
            {
                //每一行依次写入
                for (int i = 0; i < list.Count; )
                {
                    row = sheet.CreateRow(i);

                    for (int j = 0; j < columns && i < list.Count; j++)
                    {
                        row.CreateCell(j).SetCellValue(list[i].ToString());
                        i++;
                    }
                
                }
                //文件写入的位置
                using (FileStream fs = File.OpenWrite(dataFile))
                {
                    workbook.Write(fs);//向打开的这个xls文件中写入数据  
                    result = true;
                }
            }
            else
            {
                Console.WriteLine("超过行数限制！");
                result = false;
            }

            return result;

        }
        #endregion

    }
}