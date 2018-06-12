using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteQL_Nha_Hang.Models
{
    public class ThongTin
    {
        static public int MaBan { get; set; }
        static public string HoTen { get; set; }
        static public string SDT { get; set; }
        static public string GhiChu { get; set; }
        public ThongTin(int maban,string x, string y, string z)
        {
            MaBan = maban;
            HoTen = x;
            SDT = y;
            GhiChu = z;
        }
    }
}