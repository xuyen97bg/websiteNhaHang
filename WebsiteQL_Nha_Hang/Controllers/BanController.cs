﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsiteQL_Nha_Hang.Models;

namespace WebsiteQL_Nha_Hang.Controllers
{
    public class BanController : Controller
    {
        QL_NhaHangEntities db = new QL_NhaHangEntities();
        // GET: Ban
        public ActionResult Index()
        {
            var listBan = db.Ban.ToList();
            return View(listBan);
        }

        public ActionResult DatBan(int MaBan, FormCollection f)
        {
            ThongBao.SDT = null;
            //List<ThongTin> listThongTin = Session["ThongTin"] as List<ThongTin>; 
            //int i = int.Parse(f["txtSoLuong"].ToString());
            string HoTen, SDT, GhiChu;
            HoTen = string.Format(f["HoTen"].ToString());
            SDT = f["SDT"].ToString();
            GhiChu = f["GhiChu"].ToString();
            ThongTin thongtin = new ThongTin(MaBan, HoTen, SDT, GhiChu);
            Session["ThongTin"] = MaBan;
            if (HoTen == "")
            {
                ThongBao.HoTen = "Xin nhập họ tên";
                return RedirectToAction("Index", "Ban");
            }
            ThongBao.HoTen = null;
            if (SDT == "")
            {
                ThongBao.SDT = "Xin nhập SĐT";
                return RedirectToAction("Index", "Ban");
            }

            return RedirectToAction("Index", "Menu");
        }
        public ActionResult XemHoaDon(int MaHD)
        {
            TongHoaDon.TongTien = 0;
            TongHoaDon.TongSoLuong = 0;
            List<ChiTiet> chitiet = db.ChiTiet.Where(n => n.MaHD == MaHD).ToList();
            return View(chitiet);
        }
        public ActionResult Login(FormCollection f)
        {
            ViewBag.Baoloi = "";
            if (f["username"].ToString() == "xuyen" && f["password"].ToString() == "123456")
            {
                return RedirectToAction("Admin", "QuanLyBanAn");
            }
            ViewBag.Baoloi = "Sai tài khoản hoặc mật khẩu !!";
            return RedirectToAction("Index", "Ban");
        }
    }
}