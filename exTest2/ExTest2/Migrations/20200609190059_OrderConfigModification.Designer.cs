﻿// <auto-generated />
using System;
using ExTest2.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ExTest2.Migrations
{
    [DbContext(typeof(s18827DbContext))]
    [Migration("20200609190059_OrderConfigModification")]
    partial class OrderConfigModification
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ExTest2.Entities.Confectionery", b =>
                {
                    b.Property<int>("IdConfectionery")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<float>("PricePerItem")
                        .HasColumnType("real");

                    b.Property<string>("Type")
                        .HasColumnType("nvarchar(40)")
                        .HasMaxLength(40);

                    b.HasKey("IdConfectionery");

                    b.ToTable("Confectioneries");
                });

            modelBuilder.Entity("ExTest2.Entities.Confectionery_Order", b =>
                {
                    b.Property<int>("IdOrder")
                        .HasColumnType("int");

                    b.Property<int>("IdConfectionery")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("IdOrder", "IdConfectionery");

                    b.HasIndex("IdConfectionery");

                    b.ToTable("Confectioneries_Orders");
                });

            modelBuilder.Entity("ExTest2.Entities.Customer", b =>
                {
                    b.Property<int>("IdCustomer")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.HasKey("IdCustomer");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("ExTest2.Entities.Employee", b =>
                {
                    b.Property<int>("IdEmployee")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.HasKey("IdEmployee");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("ExTest2.Entities.Order", b =>
                {
                    b.Property<int>("IdOrder")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateAccepted")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateFinished")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValue(null);

                    b.Property<int>("IdCustomer")
                        .HasColumnType("int");

                    b.Property<int>("IdEmployee")
                        .HasColumnType("int");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(255)")
                        .HasMaxLength(255);

                    b.HasKey("IdOrder");

                    b.HasIndex("IdCustomer");

                    b.HasIndex("IdEmployee");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("ExTest2.Entities.Confectionery_Order", b =>
                {
                    b.HasOne("ExTest2.Entities.Confectionery", "Confectionery")
                        .WithMany("Confectioneries_Orders")
                        .HasForeignKey("IdConfectionery")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExTest2.Entities.Order", "Order")
                        .WithMany("Confectioneries_Orders")
                        .HasForeignKey("IdOrder")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ExTest2.Entities.Order", b =>
                {
                    b.HasOne("ExTest2.Entities.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("IdCustomer")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ExTest2.Entities.Employee", "Employee")
                        .WithMany("Orders")
                        .HasForeignKey("IdEmployee")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}