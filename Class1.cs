﻿using System;
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
        public static DataTable JoinTable1(DataTable leftTable, DataTable rightTable)
        {
            DataTable resultTable = leftTable.Copy();
            //leftTable.PrimaryKey = new DataColumn[1] { leftTable.Columns["id"] };
            //rightTable.PrimaryKey = new DataColumn[1] { rightTable.Columns["id"] };
            resultTable.Merge(rightTable);
            return resultTable;
        }

        
        public static DataTable JoinTable2(DataTable leftTable, DataTable rightTable)
        {
            DataTable resultTable = leftTable.Clone();
            resultTable.Columns.Add("age");     

            foreach (DataRow rowLeftTable in leftTable.Rows)
            {
                foreach (DataRow rowRightTable in rightTable.Rows)
                {
                    if (rowLeftTable["id"] == rowRightTable["id"])
                    {
                        DataRow dr = resultTable.NewRow();
                        dr["id"] = rowLeftTable["id"];
                        dr["name"] = rowLeftTable["name"];
                        dr["age"] = rowRightTable["age"];

                        resultTable.Rows.Add(dr);
                    }
                }
            }
            return resultTable;
        }

        public static DataTable LeftJoinTable2(DataTable leftTable, DataTable rightTable)
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

        public static DataTable RightJoinTable2(DataTable leftTable, DataTable rightTable)
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

        public static void Main()
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

            DataTable rs = new DataTable();
            rs = JoinTable2(table1, table2);
            rs = LeftJoinTable2(table1, table2);
            rs = RightJoinTable2(table1, table2);
        }
    }
}