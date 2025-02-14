﻿// <auto-generated />
using System;
using BookShopBusiness;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookShopBusiness.Migrations
{
    [DbContext(typeof(BookDbContext))]
    partial class BookDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BookShopBusiness.Book", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookId"));

                    b.Property<string>("BookName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("BookId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("BookShopBusiness.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoryId"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("BookShopBusiness.Shipping", b =>
                {
                    b.Property<int>("ShippingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShippingId"));

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateShip")
                        .HasColumnType("datetime2");

                    b.Property<string>("LocationShip")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserApproveId")
                        .HasColumnType("int");

                    b.Property<int>("UserSubmitId")
                        .HasColumnType("int");

                    b.HasKey("ShippingId");

                    b.HasIndex("BookId");

                    b.HasIndex("UserApproveId");

                    b.HasIndex("UserSubmitId");

                    b.ToTable("Shippings");
                });

            modelBuilder.Entity("BookShopBusiness.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BookShopBusiness.Book", b =>
                {
                    b.HasOne("BookShopBusiness.Category", "Category")
                        .WithMany("Books")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("BookShopBusiness.Shipping", b =>
                {
                    b.HasOne("BookShopBusiness.Book", "Book")
                        .WithMany("Shippings")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookShopBusiness.User", "UserApprove")
                        .WithMany("ApprovedShippings")
                        .HasForeignKey("UserApproveId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("BookShopBusiness.User", "UserSubmit")
                        .WithMany("SubmittedShippings")
                        .HasForeignKey("UserSubmitId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("UserApprove");

                    b.Navigation("UserSubmit");
                });

            modelBuilder.Entity("BookShopBusiness.Book", b =>
                {
                    b.Navigation("Shippings");
                });

            modelBuilder.Entity("BookShopBusiness.Category", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("BookShopBusiness.User", b =>
                {
                    b.Navigation("ApprovedShippings");

                    b.Navigation("SubmittedShippings");
                });
#pragma warning restore 612, 618
        }
    }
}
