﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication4.Data;

#nullable disable

namespace WebApplication4.Migrations
{
    [DbContext(typeof(GolfContext))]
    [Migration("20250313124238_SandraMigration")]
    partial class SandraMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebApplication4.Data.CartItems", b =>
                {
                    b.Property<int>("CartItemsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CartItemsId"));

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("TotalPrice")
                        .HasColumnType("int");

                    b.HasKey("CartItemsId");

                    b.HasIndex("ProductId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("WebApplication4.Data.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("WebApplication4.Data.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PostId"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("WebApplication4.Data.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<string>("ProdDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProdImage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProdName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ProdPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("ProductId");

                    b.ToTable("Product");

                    b.HasData(
                        new
                        {
                            ProductId = 1,
                            ProdDescription = "Fin driver av högsta kvalite",
                            ProdImage = "driver.jpg",
                            ProdName = "Golfklubba Driver",
                            ProdPrice = 2599m
                        },
                        new
                        {
                            ProductId = 2,
                            ProdDescription = "Järnklubba i världsklass",
                            ProdImage = "jarnklubba.jpg",
                            ProdName = "Golfklubba J�rn",
                            ProdPrice = 1999m
                        },
                        new
                        {
                            ProductId = 3,
                            ProdDescription = "Perfekt balans",
                            ProdImage = "putter.jpg",
                            ProdName = "Golfklubba Putter",
                            ProdPrice = 1699m
                        },
                        new
                        {
                            ProductId = 4,
                            ProdDescription = "Vattentålig",
                            ProdImage = "golfbag.jpg",
                            ProdName = "Golfbag",
                            ProdPrice = 1999m
                        },
                        new
                        {
                            ProductId = 5,
                            ProdDescription = "Bra grepp",
                            ProdImage = "handske.jpg",
                            ProdName = "Golfhandske",
                            ProdPrice = 299m
                        },
                        new
                        {
                            ProductId = 6,
                            ProdDescription = "Högkvalitativa bollar.",
                            ProdImage = "golfballs.jpg",
                            ProdName = "Golfbollar (12-pack)",
                            ProdPrice = 349m
                        },
                        new
                        {
                            ProductId = 7,
                            ProdDescription = "Perfekt när solen tittar fram",
                            ProdImage = "keps.jpg",
                            ProdName = "Golfkeps",
                            ProdPrice = 249m
                        },
                        new
                        {
                            ProductId = 8,
                            ProdDescription = "pegs i trä",
                            ProdImage = "peg.jpg",
                            ProdName = "peg (10-pack)",
                            ProdPrice = 39m
                        });
                });

            modelBuilder.Entity("WebApplication4.Data.Review", b =>
                {
                    b.Property<int>("ReviewId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReviewId"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ReviewId");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Review");
                });

            modelBuilder.Entity("WebApplication4.Data.SubPost", b =>
                {
                    b.Property<int>("SubPostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SubPostId"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.HasKey("SubPostId");

                    b.HasIndex("PostId");

                    b.ToTable("SubPost");
                });

            modelBuilder.Entity("WebApplication4.Data.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<int>("Admin")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Saldo")
                        .HasColumnType("int");

                    b.Property<string>("UserImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WebApplication4.Data.CartItems", b =>
                {
                    b.HasOne("WebApplication4.Data.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("WebApplication4.Data.Order", b =>
                {
                    b.HasOne("WebApplication4.Data.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication4.Data.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApplication4.Data.Post", b =>
                {
                    b.HasOne("WebApplication4.Data.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApplication4.Data.Review", b =>
                {
                    b.HasOne("WebApplication4.Data.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication4.Data.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApplication4.Data.SubPost", b =>
                {
                    b.HasOne("WebApplication4.Data.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");
                });
#pragma warning restore 612, 618
        }
    }
}
