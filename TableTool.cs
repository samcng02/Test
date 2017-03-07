using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Diagnostics;
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

        public static DataTable JoinTable2(DataTable leftTable, DataTable rightTable, string[] colLeft, string[] colRight)
        {
            //set col 
            DataTable resultTable = leftTable.Clone();
            foreach (var col in rightTable.Columns)
            {
                if (!resultTable.Columns.Contains(col.ToString()))
                    resultTable.Columns.Add(col.ToString());
            }
           
            foreach (DataRow rowLeftTable in leftTable.Rows)
            {
                foreach (DataRow rowRightTable in rightTable.Rows)
                {
                    //check condition
                    bool condition = true;
                    for (int i = 0; i < colLeft.Length; i++)
                    {
                        if (rowLeftTable[colLeft[i]] != rowRightTable[colRight[i]])
                        {
                            condition = false;
                            break;
                        }
                    }

                    if (condition)
                    {
                        var dr = resultTable.NewRow();//bb create new
                        foreach (var col in resultTable.Columns)
                        {
                            string colName = col.ToString();
                            dr[colName] = leftTable.Columns.Contains(colName) ? rowLeftTable[colName] : rowRightTable[colName];
                        }

                        resultTable.Rows.Add(dr);
                    }
                }
            }
            return resultTable;
        }

        public static DataTable LeftJoinTable(DataTable leftTable, DataTable rightTable, string[] colLeft, string[] colRight)
        {
            DataTable resultTable = leftTable.Copy();
            foreach (var col in rightTable.Columns)
            {
                if (!resultTable.Columns.Contains(col.ToString()))
                    resultTable.Columns.Add(col.ToString());
            }


            foreach (DataRow dr in resultTable.Rows)
            {
                foreach (DataRow rowRightTable in rightTable.Rows)
                {
                    //check condition
                    bool condition = true;
                    for (int i = 0; i < colLeft.Length; i++)
                    {
                        if (dr[colLeft[i]] != rowRightTable[colRight[i]])
                        {
                            condition = false;
                            break;
                        }
                    }

                    if (condition)
                    {
                        //datarow is reference, so row changed -> table changed
                        foreach (var col in rightTable.Columns)
                        {
                            dr[col.ToString()] = rowRightTable[col.ToString()];
                        }
                    }
                }
            }
            return resultTable;
        }

        public static DataTable RightJoinTable(DataTable leftTable, DataTable rightTable, string[] colLeft, string[] colRight)
        {
            DataTable resultTable = rightTable.Copy();
            foreach (var col in leftTable.Columns)
            {
                if (!resultTable.Columns.Contains(col.ToString()))
                    resultTable.Columns.Add(col.ToString());
            }

            foreach (DataRow dr in resultTable.Rows)
            {
                foreach (DataRow rowLeftTable in leftTable.Rows)
                {
                    //check condition
                    bool condition = true;
                    for (int i = 0; i <  colLeft.Length; i++)
                    {
                        if (dr[colLeft[i]] != rowLeftTable[colRight[i]])
                        {
                            condition = false;
                            break;
                        }
                    }

                    if (condition)
                    {
                        //datarow is reference, so row changed -> table changed
                        foreach (var col in leftTable.Columns)
                        {
                            dr[col.ToString()] = rowLeftTable[col.ToString()];
                        }
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

                foreach (var dr in drs)
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

        //create data test
        public static DataSet SetDataTest()
        {
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

            DataSet ds = new DataSet();
            ds.Tables.Add(table1);
            ds.Tables.Add(table2);
            return ds;
        }

        public static void Main()
        {
            //create data test
            DataTable table1 = SetDataTest().Tables[0];
            DataTable table2 = SetDataTest().Tables[1];
            DataTable rs = new DataTable();

            //timer
            var s1 = Stopwatch.StartNew();

            //test
            //rs = Merge(table1, table2);
            //rs = JoinTable(table1, table2);
            //rs = JoinTable2(table1, table2,
            //    new string[] { "id" },
            //    new string[] { "id" }
            //    );
            //rs = LeftJoinTable(table1, table2,
            //    new string[] { "id" },
            //    new string[] { "id" }
            //    );
            //rs = RightJoinTable(table1, table2,
            //    new string[] { "id" },
            //    new string[] { "id" }
            //    );
            // rs = AddMultiCol();  
            //rs = Filter(rs, "id=01 or id=03");
            s1.Stop();
            Console.WriteLine(s1.Elapsed.TotalMilliseconds);
            Console.Read();
        }
    }
}
