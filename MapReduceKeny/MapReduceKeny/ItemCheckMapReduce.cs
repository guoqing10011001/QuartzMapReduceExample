using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MapReduceKeny
{
    public class ItemCheckMapReduce
    {
        private DataTable inputList;

        public String conn
        {
            get;
            set;
        }

        public void mapper()
        {


            using (SqlSugarClient db = new SqlSugarClient(conn))
            {
                inputList = db.GetDataTable("select * from Equipment_PstTextAnalysis");
            }


        }

        public DataTable result;

        public void reducer()
        {
            int col = inputList.Columns.Count;
            string[,] array = new string[inputList.Rows.Count, col];
            for (int i = 0; i < inputList.Rows.Count; i++)
            {
                for (int j = 0; j < inputList.Columns.Count; j++)
                {
                    array[i, j] = inputList.Rows[i][j].ToString().ToUpper().Trim();
                }
            }

            string[,] targetArray = Rotate(array);

            DataTable dt = new DataTable();

            for (int i = 0; i < targetArray.GetUpperBound(1); i++)
            {
                dt.Columns.Add(targetArray[0, i]);
            }
            dt.Columns.Add("Time");
            dt.Columns.Add("TestTime");
            dt.Columns.Add("TestValue");

            int x = targetArray.GetUpperBound(0); //一维 
            int y = targetArray.GetUpperBound(1); //二维 


            for (int j = 1; j < x + 1; j++)
            {
                DataRow row = dt.NewRow();

                row["ITEM"] = targetArray[j, 0];
                row["FREQ(MHZ)"] = targetArray[j, 1];
                row["MAX"] = targetArray[j, 2];
                row["MIN"] = targetArray[j, 3];
                row["Time"] = targetArray[0, 4];
                row["TestTime"] = targetArray[0, 4];
                row["TestValue"] = targetArray[j, 4];

                dt.Rows.Add(row);

            }


            result = dt;
        }



        /// <summary>
        /// 二维数组转置函数
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static string[,] Rotate(string[,] array)
        {
            int x = array.GetUpperBound(0); //一维 
            int y = array.GetUpperBound(1); //二维 
            string[,] newArray = new string[y + 1, x + 1]; //构造转置二维数组
            for (int i = 0; i <= x; i++)
            {
                for (int j = 0; j <= y; j++)
                {
                    newArray[j, i] = array[i, j];
                }
            }
            return newArray;
        }

        /// <summary>
        /// 将二维列表(List)转换成二维数组，二维数组转置，然后将二维数组转换成列表
        /// </summary>
        /// <param name="original"></param>
        /// <returns></returns>
        public static List<List<string>> Rotate(List<List<string>> original)
        {
            List<string>[] array = original.ToArray();
            List<List<string>> lists = new List<List<string>>();
            if (array.Length == 0)
            {
                throw new IndexOutOfRangeException("Index OutOf Range");
            }
            int x = array[0].Count;
            int y = original.Count;

            //将列表抓换成数组
            string[,] twoArray = new string[y, x];
            for (int i = 0; i < y; i++)
            {
                int j = 0;
                foreach (string s in array[i])
                {
                    twoArray[i, j] = s;
                    j++;
                }
            }

            string[,] newTwoArray = new string[x, y];
            newTwoArray = Rotate(twoArray);//转置

            //二维数组转换成二维List集合
            for (int i = 0; i < x; i++)
            {
                List<string> list = new List<string>();
                for (int j = 0; j < y; j++)
                {
                    list.Add(newTwoArray[i, j]);
                }
                lists.Add(list);
            }
            return lists;
        }

        static void Main(string[] args)
        {
            List<List<string>> sourceList = new List<List<string>>(); //测试的二维List
            for (int i = 0; i < 4; i++)
            {
                List<string> list = new List<string>();
                for (int j = 0; j < 2; j++)
                {
                    list.Add(i.ToString() + j.ToString());
                }
                sourceList.Add(list);
            }

            //显示原列表
            Console.WriteLine("Source List:");
            for (int i = 0; i < sourceList.Count; i++)
            {
                string soureResult = string.Empty;
                for (int j = 0; j < sourceList[i].Count; j++)
                {
                    soureResult += sourceList[i][j] + "  ";
                }
                Console.WriteLine(soureResult);
            }

            List<List<string>> dstList = Rotate(sourceList);
            //显示转置后的列表
            Console.WriteLine("Destiney List:");
            for (int i = 0; i < dstList.Count; i++)
            {
                string dstResult = string.Empty;
                for (int j = 0; j < dstList[i].Count; j++)
                {
                    dstResult += dstList[i][j] + "  ";
                }
                Console.WriteLine(dstResult);
            }

            Console.ReadLine();
        }

    }
}
