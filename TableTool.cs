using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Collections;

namespace TestTable
{
    public class Class1
    {        
        //inner joint
        public static DataTable JoinTable(DataTable leftTable, DataTable rightTable)
        {
            DataTable resultTable = leftTable.Clone();
            resultTable.Columns.Add("age");     

            foreach (DataRow rowLeftTable in leftTable.Rows)
            {
                foreach (DataRow rowRightTable in rightTable.Rows)
                {
                    if (rowLeftTable["id"] == rowRightTable["id"])
                    {
                        DataRow dr = resultTable.NewRow();  //new row each time
                        dr["id"] = rowLeftTable["id"];
                        dr["name"] = rowLeftTable["name"];
                        dr["age"] = rowRightTable["age"];

                        //add row
                        resultTable.Rows.Add(dr);
                    }
                }
            }
            return resultTable;
        }

        //left join
        public static DataTable LeftJoinTable(DataTable leftTable, DataTable rightTable)
        {
            DataTable resultTable = leftTable.Copy();
            resultTable.Columns.Add("age");

            foreach (DataRow dr in resultTable.Rows)
            {
                foreach (DataRow rowRightTable in rightTable.Rows)
                {
                    if (dr["id"] == rowRightTable["id"])
                    {
                        //datarow is reference, so row changed -> table changed
                        dr["age"] = rowRightTable["age"];
                    }
                }
            }
            return resultTable;
        }

        //right join
        public static DataTable RightJoinTable(DataTable leftTable, DataTable rightTable)
        {
            DataTable resultTable = rightTable.Copy();
            resultTable.Columns.Add("name");

            foreach (DataRow dr in resultTable.Rows)
            {
                foreach (DataRow rowLeftTable in leftTable.Rows)
                {
                    if (dr["id"] == rowLeftTable["id"])
                    {
                        //datarow is reference, so row changed -> table changed
                        dr["name"] = rowLeftTable["name"];
                    }
                }
            }
            return resultTable;
        }
        
        //filter
        public static DataTable Filter(DataTable dt, string filter)
        {
            DataTable dtDes = dt.Clone();
            DataRow[] drs=  dt.Select(filter);

            for (int i = 0; i < drs.Length; i++)
            {
                DataRow dr = dtDes.NewRow();
                for (int j = 0; j < drs[i].ItemArray.Length; j++)
                    dr[j] = drs[i].ItemArray[j];

                dtDes.Rows.Add(dr);
            }
            return dtDes;
        }
        
        //merge
        public static DataTable Merge(DataTable leftTable, DataTable rightTable)
        {
            DataTable resultTable = leftTable.Copy();
            leftTable.PrimaryKey = new DataColumn[1] { leftTable.Columns["id"] };
            rightTable.PrimaryKey = new DataColumn[1] { rightTable.Columns["id"] };
            resultTable.Merge(rightTable);
            return resultTable;
        }

        public static void Main()
        {
            DataTable table1 = new DataTable();
            table1.Columns.Add("id");
            table1.Columns.Add("name");
            //add col
            table1.Rows.Add("01", "sang");
            table1.Rows.Add("02", "suong");
            table1.Rows.Add("03", "oanh");

            DataTable table2 = new DataTable();
            table2.Columns.Add("id");
            table2.Columns.Add("age");
            //add col
            table2.Rows.Add("01", "20");
            table2.Rows.Add("02", "30");
            table2.Rows.Add("04", "50");

            //test
            DataTable rs = new DataTable();
            rs = JoinTable(table1, table2);
            rs = LeftJoinTable(table1, table2);
            rs = RightJoinTable(table1, table2);
        }
    }
}
