using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteQL_Nha_Hang.Models
{
    public class HoaDonChiTiet
    {
        QL_NhaHangEntities db = new QL_NhaHangEntities();
        public int iMaMon { get; set; }
        public string TenMon { get; set; }
        public string Anh { get; set; }
        public DateTime ThoiGian { get; set; }
        public int SoLuong { get; set; }
        public double Gia { get; set; }
        public double ThanhTien { get { return(SoLuong*Gia); } }
        public HoaDonChiTiet(int MaMon)
        {
            iMaMon = MaMon;
            MonAn monan = db.MonAn.Single(n => n.MaMon == MaMon);
            TenMon = monan.TenMon;
            ThoiGian= DateTime.Now;
            SoLuong = 1;
            Gia = double.Parse(monan.Gia.ToString());
            Anh = monan.Anh;
        }
    }
}