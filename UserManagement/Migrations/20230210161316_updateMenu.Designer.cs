﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserManagement.Context;

namespace UserManagement.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20230210161316_updateMenu")]
    partial class updateMenu
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.32")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("UserManagement.Models.IErrorApplication", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CONTROLLER")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CREATE_BY")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CREATE_DATE")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("DELETE_MARK")
                        .HasColumnType("bit");

                    b.Property<string>("ERROR_LINE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ERROR_MESSAGE")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ERROT_DATE")
                        .HasColumnType("datetime2");

                    b.Property<string>("FUNCTION")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Menu_ID")
                        .HasColumnType("int");

                    b.Property<string>("PARAM")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("STATUS")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UPDATE_BY")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UPDATE_DATE")
                        .HasColumnType("datetime2");

                    b.Property<int>("User_ID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("User_ID");

                    b.ToTable("IErrorApplication");
                });

            modelBuilder.Entity("UserManagement.Models.Menu", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CREATE_BY")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CREATE_DATE")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("DELETE_MARK")
                        .HasColumnType("bit");

                    b.Property<string>("GROUP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MENU_CONTROLLER")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MENU_ICON")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MENU_NAME")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MENU_VIEW")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UPDATE_BY")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UPDATE_DATE")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("Menu");
                });

            modelBuilder.Entity("UserManagement.Models.MenuWithRole", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CREATE_BY")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CREATE_DATE")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("DELETE_MARK")
                        .HasColumnType("bit");

                    b.Property<int>("Menu_Id")
                        .HasColumnType("int");

                    b.Property<int>("Role_Id")
                        .HasColumnType("int");

                    b.Property<string>("UPDATE_BY")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UPDATE_DATE")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("Menu_Id");

                    b.HasIndex("Role_Id");

                    b.ToTable("MenuWithRole");
                });

            modelBuilder.Entity("UserManagement.Models.Role", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CREATE_BY")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CREATE_DATE")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("DELETE_MARK")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UPDATE_BY")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UPDATE_DATE")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("UserManagement.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CREATE_BY")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CREATE_DATE")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("DELETE_MARK")
                        .HasColumnType("bit");

                    b.Property<string>("EMAIL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NAMA_USER")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NO_HP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PASSWORD")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PIN")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("STATUS_USER")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UPDATE_BY")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UPDATE_DATE")
                        .HasColumnType("datetime2");

                    b.Property<string>("USERNAME")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("WA")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("User");
                });

            modelBuilder.Entity("UserManagement.Models.UserActivity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CREATE_BY")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CREATE_DATE")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("DELETE_MARK")
                        .HasColumnType("bit");

                    b.Property<int>("Menu_ID")
                        .HasColumnType("int");

                    b.Property<string>("Remarks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("User_ID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("Menu_ID");

                    b.HasIndex("User_ID");

                    b.ToTable("UserActivity");
                });

            modelBuilder.Entity("UserManagement.Models.UserFoto", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CREATE_BY")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CREATE_DATE")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("DELETE_MARK")
                        .HasColumnType("bit");

                    b.Property<string>("FOTO")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ID_USER")
                        .HasColumnType("int");

                    b.Property<string>("UPDATE_BY")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UPDATE_DATE")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.HasIndex("ID_USER");

                    b.ToTable("UserFoto");
                });

            modelBuilder.Entity("UserManagement.Models.UserRole", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CREATE_BY")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("CREATE_DATE")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("DELETE_MARK")
                        .HasColumnType("bit");

                    b.Property<int>("Role_Id")
                        .HasColumnType("int");

                    b.Property<string>("UPDATE_BY")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UPDATE_DATE")
                        .HasColumnType("datetime2");

                    b.Property<int>("User_Id")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("Role_Id");

                    b.HasIndex("User_Id");

                    b.ToTable("UserRole");
                });

            modelBuilder.Entity("UserManagement.Models.IErrorApplication", b =>
                {
                    b.HasOne("UserManagement.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("User_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UserManagement.Models.MenuWithRole", b =>
                {
                    b.HasOne("UserManagement.Models.Menu", "Menu")
                        .WithMany()
                        .HasForeignKey("Menu_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UserManagement.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("Role_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UserManagement.Models.UserActivity", b =>
                {
                    b.HasOne("UserManagement.Models.Menu", "Menu")
                        .WithMany()
                        .HasForeignKey("Menu_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UserManagement.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("User_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UserManagement.Models.UserFoto", b =>
                {
                    b.HasOne("UserManagement.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("ID_USER")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UserManagement.Models.UserRole", b =>
                {
                    b.HasOne("UserManagement.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("Role_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UserManagement.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("User_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
