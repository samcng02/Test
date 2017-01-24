using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Linq
{
    public class Class1
    {
        public static void Linq(DataTable dt)
        {
            int[] numbers = new int[7] { 0, 1, 2, 3, 4, 5, 6 };

            //var numQuery =
            //    from num in numbers
            //    where (num % 2) == 0
            //    select num;

            var numQuery1 = numbers.Select(num => num += 10);

            foreach (var number in numQuery1)
                Console.Write(number + " ");

            var filter = from row in dt.Select()
                         where row["id"] == "1"
                         select row;

            //IEnumerable<DataRow> ordered = dt.AsEnumerable()
            //    .Where(i => i.Field<String>("id") == "1")
            //    .Select(s => s);
            ////or
            //DataTable rs = dt.AsEnumerable()
            //    .Where(i => i.Field<String>("id") == "1")
            //    .CopyToDataTable();                 

            //foreach (DataRow i in rs.Rows)
            //{

            //}             
        }

        public static void Linq1(int[] arr)
        {
            var arrOdd = from num in arr
                         where num % 2 == 0
                         group num by num into a
                         select a;
            foreach (var num in arrOdd)
                foreach(var value in num)
                    Console.Write(value + " ");
        }

        public static void Linq2(List<SinhVien> listSv, List<Lop> listLop)
        {
            var filterSv = from sv in listSv
                           join lop in listLop
                           on sv.id equals lop.idSv
                           let age = Convert.ToInt32((DateTime.Now.ToString("yyyy"))) - Convert.ToInt32(sv.birthday)
                           //where sv.id == "01"
                           orderby sv.name ascending
                           select new
                           {
                               tenSv = sv.name,
                               tenLop = lop.name,
                               age
                           };

            foreach (var sv in filterSv)
                Console.WriteLine("{0} {1} {2}", sv.tenSv, sv.tenLop, sv.age);
        }

        public static void Main()
        {
            int[] arr = new int[] { 10, 11, 12, 13, 14, 15 };
            List<SinhVien> listSv = new List<SinhVien>(){
                new SinhVien("01","sang","1990"),
                new SinhVien("02","suong","1991"),
                new SinhVien("03", "oanh","1992")
            };
            List<Lop> listLop = new List<Lop>(){
                new Lop("a001","01","12a"),
                new Lop("a002","02","12b"),
                new Lop("a003","03","12c"),
            };
            Linq1(arr);
            //Linq2(listSv, listLop);


            Console.Read();
        }

    }

    public class SinhVien
    {
        public string id { get; set; }
        public string name { get; set; }
        public string birthday { get; set; }

        public SinhVien(string id, string name, string birthday)
        {
            this.id = id;
            this.name = name;
            this.birthday = birthday;
        }
    }

    public class Lop
    {
        public string id { get; set; }
        public string idSv { get; set; }
        public string name { get; set; }

        public Lop(string id, string idSv, string name)
        {
            this.id = id;
            this.idSv = idSv;
            this.name = name;
        }
    }
}
