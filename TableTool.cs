using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
//
using System.Linq;
using System.Threading.Tasks;

namespace TestTable
{
    public class Class1
    {
        public static DataTable JoinTable(DataTable leftTable, DataTable rightTable)
        {
            var resultTable = leftTable.Clone();
            resultTable.Columns.Add("age");

            foreach (DataRow rowLeftTable in leftTable.Rows)
            {
                foreach (DataRow rowRightTable in rightTable.Rows)
                {
                    if (rowLeftTable["id"] == rowRightTable["id"])
                    {
                        var dr = resultTable.NewRow();
                        dr["id"] = rowLeftTable["id"];
                        dr["name"] = rowLeftTable["name"];
                        dr["age"] = rowRightTable["age"];

                        resultTable.Rows.Add(dr);
                    }
                }
            }
            return resultTable;
        }

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

        public static DataTable AddMultiCol()
        {
            DataTable dtDes = new DataTable();
            dtDes.Columns.AddRange(new DataColumn[]{
                new DataColumn("id"),
                new DataColumn("name"),
                new DataColumn("age")
            });
            return dtDes;
        }

        public static DataTable Filter(DataTable dt, string filter)
        {
            //v2.0
                DataTable dtDes = dt.Clone();
                DataRow[] drs = dt.Select(filter);

                foreach(var dr in drs)
                {
                    dtDes.ImportRow(dr);
                }
                return dtDes;
            //

            //v4.0
                //return dt.Select(filter).CopyToDataTable();
        }

        //merge 2 table
        //if primaryKey is defined, line same by id then result is 1 row
        //result is join + left join + right join
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
            //----------create data test--------------------
            DataTable table1 = new DataTable();
            table1.Columns.Add("id");
            table1.Columns.Add("name");

            DataTable table2 = new DataTable();
            table2.Columns.Add("id");
            table2.Columns.Add("age");

            table1.Rows.Add("01", "sang");
            table1.Rows.Add("02", "suong");
            table1.Rows.Add("03", "oanh");

            table2.Rows.Add("01", "20");
            table2.Rows.Add("02", "30");
            table2.Rows.Add("04", "50");
            //--------------------------------------------------

            DataTable rs = new DataTable();
            rs = Merge(table1, table2);
            //rs = JoinTable(table1, table2);
            // rs = LeftJoinTable(table1, table2);
            // rs = RightJoinTable(table1, table2);
            // rs = AddMultiCol();  
            //rs = Filter(rs, "id=01 or id=03");

            Console.Read();
        }
    }
}
