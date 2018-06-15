using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteQL_Nha_Hang.Models;

namespace WebsiteQL_Nha_Hang.Controllers
{
    public class MenuController : Controller
    {
        QL_NhaHangEntities db = new QL_NhaHangEntities();
        // GET: Menu
        public ActionResult Index()
        {
            // Không cho phép truy cập vào cotroller menu
            if (Session["ThongTin"] == null)
            {
                return RedirectToAction("Index", "Ban");
            }
            var listMenu = db.MonAn.ToList();
            return View(listMenu);
        }
        //public PartialViewResult Menu()
        //{
        //    var listMenu = db.MonAn.ToList();
        //    return PartialView(listMenu);
        //}
        public List<HoaDonChiTiet> LayHoaDonChiTiet()
        {
            List<HoaDonChiTiet> lstHoaDonChiTiet = Session["HoaDonChiTiet"] as List<HoaDonChiTiet>;
            // Nếu giỏ hàng chưa tồn tai thì tạo gioe hàng
            if (lstHoaDonChiTiet == null)
            {
                lstHoaDonChiTiet = new List<HoaDonChiTiet>();
                Session["HoaDonChiTiet"] = lstHoaDonChiTiet;
            }
            return lstHoaDonChiTiet;
        }
        // GET: HoaDonChiTiet
        public ActionResult ThemHoaDonChiTiet(int MaMonAn, string strURL)
        {

            //Nếu người dùng tự ý chỉnh URL truyền vào MaMonAn không có trong CSDL thì hiện ra trang báo lỗi.
            MonAn MonAn = db.MonAn.SingleOrDefault(n => n.MaMon == MaMonAn);
            if (MonAn == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lấy ra session giỏ hàng
            List<HoaDonChiTiet> lstHoaDonChiTiet = LayHoaDonChiTiet();
            //Kiểm tra sản phẩm có trong giỏ hàng hay chưa
            HoaDonChiTiet sanpham = lstHoaDonChiTiet.Find(n => n.iMaMon == MaMonAn);
            //Nếu sản phẩm chưa có trong giỏ thì thêm sp vào giỏ
            if (sanpham == null)
            {
                sanpham = new HoaDonChiTiet(MaMonAn);
                lstHoaDonChiTiet.Add(sanpham);
                TongTien();
                return Redirect(strURL);
            }
            else
            {
                sanpham.SoLuong++;
                return Redirect(strURL);
            }
        }
        public ActionResult CapNhatHoaDonChiTiet(int MaMonAn, string strURL, FormCollection f)
        {

            //Nếu người dùng tự ý chỉnh URL truyền vào MaMonAn không có trong CSDL thì hiện ra trang báo lỗi.
            MonAn MonAn = db.MonAn.SingleOrDefault(n => n.MaMon == MaMonAn);
            if (MonAn == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<HoaDonChiTiet> lstHoaDonChiTiet = LayHoaDonChiTiet();
            HoaDonChiTiet sanpham = lstHoaDonChiTiet.SingleOrDefault(n => n.iMaMon == MaMonAn);
            if (sanpham != null)
            {
                int i = int.Parse(f["txtSoLuong"].ToString());
                if (i != 0)
                {
                    sanpham.SoLuong = int.Parse(f["txtSoLuong"].ToString());
                }
            }
            TongTien();
            return Redirect(strURL);
        }
        public ActionResult XoaHoaDonChiTiet(int MaMonAn, string strURL)
        {
            //Nếu người dùng tự ý chỉnh URL truyền vào MaMonAn không có trong CSDL thì hiện ra trang báo lỗi.
            MonAn MonAn = db.MonAn.SingleOrDefault(n => n.MaMon == MaMonAn);
            if (MonAn == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<HoaDonChiTiet> lstHoaDonChiTiet = LayHoaDonChiTiet();
            HoaDonChiTiet sanpham = lstHoaDonChiTiet.SingleOrDefault(n => n.iMaMon == MaMonAn);
            if (sanpham != null)
            {
                lstHoaDonChiTiet.RemoveAll(n => n.iMaMon == MaMonAn);
            }
            TongTien();
            return RedirectPermanent(strURL);
        }
        //Xây  dựng bảng giỏ hàng
        public ActionResult HoaDonChiTiet()
        {

            List<HoaDonChiTiet> lstHoaDonChiTiet = LayHoaDonChiTiet();
            TongTien();
            return View(lstHoaDonChiTiet);
        }
        //Tính  tổng số lương và tổng tiền

        //Tính tổng số lượng
        private int TongSoLuong()
        {

            int sum = 0;
            List<HoaDonChiTiet> lstHoaDonChiTiet = Session["HoaDonChiTiet"] as List<HoaDonChiTiet>;
            if (lstHoaDonChiTiet != null)
            {
                sum = lstHoaDonChiTiet.Sum(n => n.SoLuong);
            }
            return sum;
        }
        void TongTien()
        {
            int sum = 0;
            double sum1 = 0;
            List<HoaDonChiTiet> lstHoaDonChiTiet = Session["HoaDonChiTiet"] as List<HoaDonChiTiet>;
            if (lstHoaDonChiTiet != null)
            {
                sum1 = lstHoaDonChiTiet.Sum(n => n.ThanhTien);
            }
            if (lstHoaDonChiTiet != null)
            {
                sum = lstHoaDonChiTiet.Sum(n => n.SoLuong);
            }
            TongHoaDon s = new TongHoaDon(sum, sum1);

        }
        public ActionResult GhiNhan()
        {
            Ban ban = db.Ban.SingleOrDefault(n => n.MaBan == ThongTin.MaBan);
            if(ban.TinhTangBan==1)
            {
                return RedirectToAction("Index", "Ban");
            }
            ban.TinhTangBan = 1;
            HoaDon hoadon = new HoaDon();
            hoadon.TenKH = ThongTin.HoTen;
            hoadon.NgayDat = DateTime.Now;
            hoadon.TongTien = TongHoaDon.TongTien;
            hoadon.SDT = ThongTin.SDT;
            hoadon.TinhTrang = 0;
            db.HoaDon.Add(hoadon);
            db.SaveChanges();
            //int MaHoaDon = hoadon.MaHD;
            int id = hoadon.MaHD;
            List<HoaDonChiTiet> lstHoaDonChiTiet = LayHoaDonChiTiet();
            ban.MaHD = hoadon.MaHD;
            ban.GhiChu = ThongTin.GhiChu;
            db.SaveChanges();
            foreach (var item in lstHoaDonChiTiet)
            {
                ChiTiet chitiet = new ChiTiet();
                chitiet.MaMon = item.iMaMon;
                chitiet.SoLuong = item.SoLuong;
                chitiet.ThanhTien = item.ThanhTien;
                chitiet.MaHD = hoadon.MaHD;
                db.ChiTiet.Add(chitiet);
                db.SaveChanges();
            }
            Session.RemoveAll();
            return RedirectToAction("Index", "Ban");
        }

        public ActionResult Che()
        {
            List<MonAn> List = db.MonAn.Where(n => n.LoaiMon.MaLoaiMon== "3").ToList();
            return View(List);
        }
        public ActionResult SuaChua()
        {
            List<MonAn> List = db.MonAn.Where(n => n.LoaiMon.MaLoaiMon == "4").ToList();
            return View(List);
        }
        public ActionResult Banh()
        {
            List<MonAn> List = db.MonAn.Where(n => n.LoaiMon.MaLoaiMon == "1").ToList();
            return View(List);
        }
        public ActionResult ChuaCheBien()
        {
            List<MonAn> List = db.MonAn.Where(n => n.LoaiMon.MaLoaiMon == "5").ToList();
            return View(List);
        }
        public ActionResult Nem()
        {
            List<MonAn> List = db.MonAn.Where(n => n.LoaiMon.MaLoaiMon == "2").ToList();
            return View(List);
        }
        public ActionResult XemChiTiet(int MaMon)
        {
            MonAn mon = db.MonAn.Single(n => n.MaMon == MaMon);
            if(mon==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(mon);
        }
    }
}