﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebEmployees
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class MySQLEntities : DbContext
    {
        public MySQLEntities()
            : base("name=MySQLEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<canceledorders> canceledorders { get; set; }
        public virtual DbSet<customers> customers { get; set; }
        public virtual DbSet<districts> districts { get; set; }
        public virtual DbSet<masters> masters { get; set; }
        public virtual DbSet<orders> orders { get; set; }
        public virtual DbSet<status> status { get; set; }
        public virtual DbSet<districtsmasters> districtsmasters { get; set; }
        public virtual DbSet<worksorders> worksorders { get; set; }
        public virtual DbSet<worktypes> worktypes { get; set; }
        public virtual DbSet<worktypesmasters> worktypesmasters { get; set; }
        public virtual DbSet<works> works { get; set; }
    }
}
