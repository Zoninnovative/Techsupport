﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Ticketing_System.Repositoy
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using Ticketing_System.Core;
    
    public partial class  db_Zon_TechSupportEntities : DbContext
    {
        public  db_Zon_TechSupportEntities()
            : base("name=db_Zon_TechSupportEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<Mst_Project> Mst_Project { get; set; }
        public virtual DbSet<Mst_Project_Documents> Mst_Project_Documents { get; set; }
        public virtual DbSet<Mst_ProjectUsers> Mst_ProjectUsers { get; set; }
        public virtual DbSet<Mst_Task> Mst_Task { get; set; }
        public virtual DbSet<Mst_TaskPriority> Mst_TaskPriority { get; set; }
        public virtual DbSet<Mst_TaskStatus> Mst_TaskStatus { get; set; }
        public virtual DbSet<Mst_TaskType> Mst_TaskType { get; set; }
        public virtual DbSet<Trans_Ticket> Trans_Ticket { get; set; }
    }
}
