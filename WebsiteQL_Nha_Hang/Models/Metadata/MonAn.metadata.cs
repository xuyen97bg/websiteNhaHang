using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebsiteQL_Nha_Hang.Models
{
    [MetadataTypeAttribute(typeof(MonAnMetadata))]
    public partial class MonAn
    {
		internal sealed class MonAnMetadata
		{
			[Display(Name ="Mã món")]
            public int MaMon { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập dữ cho trường này")]
            [Display(Name = "Tên món")]
            public string TenMon { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập dữ cho trường này")]
            [Display(Name = "Mô tả")]
            public string MoTa { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập dữ cho trường này")]
            [Display(Name = "Giá")]
            public Nullable<double> Gia { get; set; }
            [Required(ErrorMessage = "Vui lòng nhập dữ cho trường này")]
            [Display(Name = "Loại")]
            public string Loai { get; set; }
            [Display(Name = "Ảnh")]
            public string Anh { get; set; }
        }
    }
}