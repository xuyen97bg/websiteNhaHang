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

            return View(db.MonAn.ToList());
        }
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(MonAn monan, HttpPostedFileBase FileUpload)
        {
            if (FileUpload == null)
            {
                ViewBag.ThongBao = "Xin chọn hình ảnh !";
                return View();
            }
            if (ModelState.IsValid)

            {
                var fileName = Path.GetFileName(FileUpload.FileName);
                var path = Path.Combine(Server.MapPath("~/Image"), fileName);
                if (System.IO.File.Exists(path))
                {
                    ViewBag.ThongBao = "Hình ảnh đã tồn tại !";
                }

                else
                {
                    FileUpload.SaveAs(path);
                }
                monan.Anh = FileUpload.FileName;
                db.MonAn.Add(monan);
                db.SaveChanges();
            }
            return View();
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
            return View(monan);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(MonAn monan)
        {
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
    }
}