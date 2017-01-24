using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace LinqToSql
{
    public class Class1
    {
        static DBLINQToSQLDataContext db = new DBLINQToSQLDataContext();    
        static void Main()
        {
            LoadList();
            Console.Read();
        }

        static void LoadList()
        {
            var ret = db.DMSVs.ToList();
            //foreach (var item in ret)
            //    Console.WriteLine("{0} {1} {2}",item.MaSV, item.HoSV, item.TenSV);

            var dt = from sv in db.DMSVs
                     join khoa in db.DMKhoas
                     on sv.MaKhoa equals khoa.MaKhoa
                     select new { sv.MaSV, sv.HoSV, sv.TenSV, khoa.MaKhoa, khoa.TenKhoa };
            foreach (var row in dt)
                Console.WriteLine("{0} {1} {2} {3}", row.MaSV, row.HoSV, row.TenSV, row.TenKhoa);
        }
    }
}
