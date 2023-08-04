﻿// <auto-generated />
using Async2.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Async2.Migrations
{
    [DbContext(typeof(AsyncDBContext))]
    [Migration("20230801100158_DataHotelAmenityRoom")]
    partial class DataHotelAmenityRoom
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Async2.Models.Amenity", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Amenities");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Name = "coffee maker"
                        },
                        new
                        {
                            ID = 2,
                            Name = "air conditioning"
                        },
                        new
                        {
                            ID = 3,
                            Name = "ocean view"
                        });
                });

            modelBuilder.Entity("Async2.Models.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("State")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StreetAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Hotels");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            City = "amman",
                            Country = "country 1",
                            Name = "Hotel 1",
                            Phone = "123-456-7890",
                            State = "state1",
                            StreetAddress = "Address1"
                        },
                        new
                        {
                            Id = 2,
                            City = "amman",
                            Country = "country 2",
                            Name = "Hotel 2",
                            Phone = "123-456-7890",
                            State = "state2",
                            StreetAddress = "Address2"
                        });
                });

            modelBuilder.Entity("Async2.Models.Room", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("Layout")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Rooms");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Layout = 0,
                            Name = "Room 1"
                        },
                        new
                        {
                            ID = 2,
                            Layout = 1,
                            Name = "Room 2"
                        },
                        new
                        {
                            ID = 3,
                            Layout = 2,
                            Name = "Room 3"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}