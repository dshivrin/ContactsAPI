﻿using ContactsAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsAPI.Data.Models
{
    public partial class ContactContext : DbContext
    {
        private readonly IConfiguration configuration;

        public ContactContext(DbContextOptions<ContactContext> options, IConfiguration configuration)
           : base(options)
        {
            this.configuration = configuration;
        }
        public virtual DbSet<Contact> Contact { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var connectionString = configuration.GetConnectionString("contacts");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ContactsAPI.Data.Models.Contact", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<DateTime>("BirthDate")
                    .HasColumnType("datetime2");

                b.Property<string>("Email")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Gender")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<string>("Phone")
                    .IsRequired()
                    .HasColumnType("nvarchar(max)");

                b.Property<int>("SocialNumber")
                    .HasColumnType("int");

                b.HasKey("Id");

                b.ToTable("Contacts");
            });
        }
    }
}
