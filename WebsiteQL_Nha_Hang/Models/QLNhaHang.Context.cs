﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebsiteQL_Nha_Hang.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QL_NhaHangEntities : DbContext
    {
        public QL_NhaHangEntities()
            : base("name=QL_NhaHangEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Ban> Ban { get; set; }
        public virtual DbSet<ChiTiet> ChiTiet { get; set; }
        public virtual DbSet<HoaDon> HoaDon { get; set; }
        public virtual DbSet<MonAn> MonAn { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
    }
}