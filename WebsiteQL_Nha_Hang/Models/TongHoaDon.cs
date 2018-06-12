using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebsiteQL_Nha_Hang.Models
{
     public class TongHoaDon
    {
      static  public int TongSoLuong { get; set; }
       static public double TongTien { get; set; }

        public TongHoaDon(int x , double y)
        {
            TongSoLuong = x;
            TongTien = y;
        }
    }
}