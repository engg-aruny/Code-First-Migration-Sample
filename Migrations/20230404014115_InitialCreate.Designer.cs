﻿// <auto-generated />
using System;
using Code_First_Migration_Sample.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Code_First_Migration_Sample.Migrations
{
    [DbContext(typeof(SchoolDbContext))]
    [Migration("20230404014115_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Code_First_Migration_Sample.Models.StudentEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Students", (string)null);

                    b.HasData(
                        new
                        {
                            ID = 1,
                            DateOfBirth = new DateTime(2023, 4, 4, 7, 11, 15, 503, DateTimeKind.Local).AddTicks(5904),
                            Email = "testmohit@gmail.com",
                            FirstName = "Mohit",
                            LastName = "Yadav"
                        },
                        new
                        {
                            ID = 2,
                            DateOfBirth = new DateTime(2023, 4, 4, 7, 11, 15, 503, DateTimeKind.Local).AddTicks(5919),
                            Email = "testankitsharma@gmail.com",
                            FirstName = "Ankit",
                            LastName = "Sharma"
                        });
                });

            modelBuilder.Entity("Code_First_Migration_Sample.Models.TeacherEntity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("ID");

                    b.ToTable("Teachers", (string)null);

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Email = "testramesh@gmail.com",
                            FirstName = "Ramesh",
                            LastName = "Kumar",
                            PhoneNumber = "1234567890"
                        },
                        new
                        {
                            ID = 2,
                            Email = "testamitsharma@gmail.com",
                            FirstName = "Amit ",
                            LastName = "Sharma",
                            PhoneNumber = "1234517890"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
