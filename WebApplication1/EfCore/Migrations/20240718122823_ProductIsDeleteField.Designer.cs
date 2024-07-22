﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication1.EfCore;

#nullable disable

namespace WebApplication1.EfCore.Migrations
{
    [DbContext(typeof(PriceListDbContext))]
    [Migration("20240718122823_ProductIsDeleteField")]
    partial class ProductIsDeleteField
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PriceColumnModelPriceListModel", b =>
                {
                    b.Property<long>("ColumnsId")
                        .HasColumnType("bigint");

                    b.Property<long>("PriceListsId")
                        .HasColumnType("bigint");

                    b.HasKey("ColumnsId", "PriceListsId");

                    b.HasIndex("PriceListsId");

                    b.ToTable("PriceColumnModelPriceListModel");
                });

            modelBuilder.Entity("WebApplication1.EfCore.Models.PriceColumnModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("PropName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<long>("PropTypeId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("PropTypeId");

                    b.HasIndex("PropName", "PropTypeId")
                        .IsUnique();

                    b.ToTable("PriceColumns");
                });

            modelBuilder.Entity("WebApplication1.EfCore.Models.PriceColumnPropTypeModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PriceColumnPropTypes");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Name = "Число"
                        },
                        new
                        {
                            Id = 2L,
                            Name = "Строка"
                        },
                        new
                        {
                            Id = 3L,
                            Name = "Текст"
                        });
                });

            modelBuilder.Entity("WebApplication1.EfCore.Models.PriceListModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PriceLists");
                });

            modelBuilder.Entity("WebApplication1.EfCore.Models.ProductColumnValueModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long>("ColumnId")
                        .HasColumnType("bigint");

                    b.Property<string>("ColumnValue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ProductModelId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ColumnId");

                    b.HasIndex("ProductModelId");

                    b.ToTable("ProductColumnValues");
                });

            modelBuilder.Entity("WebApplication1.EfCore.Models.ProductModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Articul")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("PriceListId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("PriceListId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("PriceColumnModelPriceListModel", b =>
                {
                    b.HasOne("WebApplication1.EfCore.Models.PriceColumnModel", null)
                        .WithMany()
                        .HasForeignKey("ColumnsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication1.EfCore.Models.PriceListModel", null)
                        .WithMany()
                        .HasForeignKey("PriceListsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApplication1.EfCore.Models.PriceColumnModel", b =>
                {
                    b.HasOne("WebApplication1.EfCore.Models.PriceColumnPropTypeModel", "PropType")
                        .WithMany()
                        .HasForeignKey("PropTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PropType");
                });

            modelBuilder.Entity("WebApplication1.EfCore.Models.ProductColumnValueModel", b =>
                {
                    b.HasOne("WebApplication1.EfCore.Models.PriceColumnModel", "Column")
                        .WithMany()
                        .HasForeignKey("ColumnId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication1.EfCore.Models.ProductModel", null)
                        .WithMany("ColumnValues")
                        .HasForeignKey("ProductModelId");

                    b.Navigation("Column");
                });

            modelBuilder.Entity("WebApplication1.EfCore.Models.ProductModel", b =>
                {
                    b.HasOne("WebApplication1.EfCore.Models.PriceListModel", "PriceList")
                        .WithMany("Products")
                        .HasForeignKey("PriceListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PriceList");
                });

            modelBuilder.Entity("WebApplication1.EfCore.Models.PriceListModel", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("WebApplication1.EfCore.Models.ProductModel", b =>
                {
                    b.Navigation("ColumnValues");
                });
#pragma warning restore 612, 618
        }
    }
}
