using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteQL_Nha_Hang.Models;
namespace WebsiteQL_Nha_Hang.Controllers
{
    public class AdminController : Controller
    {
        QL_NhaHangEntities db = new QL_NhaHangEntities();
        // GET: Admin
        public ActionResult Index()
        {
            if (Session["username"] ==null && Session["password"]== null)
            {
                return RedirectToAction("Index", "Ban");
            }
            return View(db.MonAn.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Loai = new SelectList(db.LoaiMon.ToList(), "MaLoaiMon", "TenLoai");
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(MonAn monan, HttpPostedFileBase FileUpload, string Loai)
        {
            monan.Loai = Loai;
            if (FileUpload == null)
            {
                ViewBag.Loai = new SelectList(db.LoaiMon.ToList(), "MaLoaiMon", "TenLoai");
                ViewBag.ThongBao = "Xin chọn hình ảnh !";
                return View();
            }
            if (ModelState.IsValid)

            {
                var fileName = Path.GetFileName(FileUpload.FileName);
                var path = Path.Combine(Server.MapPath("~/Image"), fileName);
                if (System.IO.File.Exists(path))
                {
                    ViewBag.Loai = new SelectList(db.LoaiMon.ToList(), "MaLoaiMon", "TenLoai");
                    ViewBag.ThongBao = "Hình ảnh đã tồn tại !";
                    return View();
                }

                else
                {
                    FileUpload.SaveAs(path);
                }

                monan.Anh = FileUpload.FileName;
                db.MonAn.Add(monan);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int MaMonAn)
        {
           
            MonAn monan = db.MonAn.SingleOrDefault(n => n.MaMon == MaMonAn);
            if (monan == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.Loai = new SelectList(db.LoaiMon.ToList(), "MaLoaiMon", "TenLoai");
            return View(monan);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(MonAn monan , string Loai)
        {
            monan.Loai = Loai;
            if (ModelState.IsValid)
            {
                db.Entry(monan).State = System.Data.Entity.EntityState.Modified;
               
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Details(int MaMonAn)
        {
            MonAn monan = db.MonAn.SingleOrDefault(n => n.MaMon == MaMonAn);
            if (monan == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(monan);
        }
        [HttpGet]
        public ActionResult Delete(int MaMonAn)
        {
            MonAn monan = db.MonAn.SingleOrDefault(n => n.MaMon == MaMonAn);
            if (monan == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(monan);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateInput(false)]
        public ActionResult OkDelete(int MaMonAn)
        {
            MonAn monan = db.MonAn.SingleOrDefault(n => n.MaMon == MaMonAn);
            if (monan == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.MonAn.Remove(monan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult QuanLyBanAn()
        {
            if (Session["username"].ToString() == null && Session["password"].ToString() == null)
            {
                return RedirectToAction("Index", "Ban");
            }
            var listBan = db.Ban.ToList();
            return View(listBan);
        }
        public ActionResult ThanhToan(int MaHD)
        {
            Session["MaHD"] = MaHD;
            TongHoaDon.TongTien = 0;
            TongHoaDon.TongSoLuong = 0;
            List<ChiTiet> chitiet = db.ChiTiet.Where(n => n.MaHD == MaHD).ToList();
            return View(chitiet);
        }
        public ActionResult XacNhanThanhToan()
        {
            if(Session["MaHD"]==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            int Ma = int.Parse(Session["MaHD"].ToString());
            HoaDon hoadon = db.HoaDon.SingleOrDefault(n => n.MaHD == Ma);
            hoadon.TinhTrang = 1;
            Ban ban = db.Ban.SingleOrDefault(n => n.MaHD == Ma);
            ban.TinhTangBan = 0;
            ban.GhiChu = null;
            ban.MaHD = null;
            db.SaveChanges(); 
            return RedirectToAction("QuanLyBanAn");
        }

        public ActionResult QuanLiHoaDon()
        {
            List<HoaDon> hoadon = db.HoaDon.ToList();
            return View(hoadon);
        }
        public ActionResult XemChiTietHoaDon(int MaHD)
        {
            List<ChiTiet> chitiet = db.ChiTiet.Where(n => n.MaHD == MaHD).ToList();
            if(chitiet==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            TongHoaDon.TongTien = 0;
            TongHoaDon.TongSoLuong = 0;
            return View(chitiet);
        }

        public ActionResult Che()
        {
            List<MonAn> List = db.MonAn.Where(n => n.LoaiMon.MaLoaiMon == "3").ToList();
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
        public ActionResult MonSong()
        {
            List<MonAn> List = db.MonAn.Where(n => n.LoaiMon.MaLoaiMon == "5").ToList();
            return View(List);
        }
        public ActionResult Nem()
        {
            List<MonAn> List = db.MonAn.Where(n => n.LoaiMon.MaLoaiMon == "2").ToList();
            return View(List);
        }
    }
}