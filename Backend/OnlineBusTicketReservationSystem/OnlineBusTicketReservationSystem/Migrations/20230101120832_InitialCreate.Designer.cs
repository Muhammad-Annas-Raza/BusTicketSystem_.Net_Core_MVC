﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineBusTicketReservationSystem.Models;

#nullable disable

namespace OnlineBusTicketReservationSystem.Migrations
{
    [DbContext(typeof(MyContext))]
    [Migration("20230101120832_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("OnlineBusTicketReservationSystem.Models.tbl_bus", b =>
                {
                    b.Property<long>("bus_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("bus_id"), 1L, 1);

                    b.Property<DateTime>("Created_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("bus_NumberPlate")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("bus_OrganizationName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<bool>("bus_available")
                        .HasColumnType("bit");

                    b.Property<string>("bus_category")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("bus_destination")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("bus_noOfSeats")
                        .HasColumnType("int");

                    b.Property<DateTime?>("bus_startingTime")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("bus_ticketPrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<long>("fk_user_id")
                        .HasColumnType("bigint");

                    b.HasKey("bus_id");

                    b.HasIndex("bus_NumberPlate")
                        .IsUnique();

                    b.HasIndex("fk_user_id");

                    b.ToTable("tbl_bus");
                });

            modelBuilder.Entity("OnlineBusTicketReservationSystem.Models.tbl_busSeats", b =>
                {
                    b.Property<long>("busSeat_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("busSeat_id"), 1L, 1);

                    b.Property<DateTime>("Created_at")
                        .HasColumnType("datetime2");

                    b.Property<int>("busSeat_SeatNumber")
                        .HasColumnType("int");

                    b.Property<int?>("busSeat_customerAge")
                        .HasColumnType("int");

                    b.Property<decimal?>("busSeat_customerDiscountPercentage")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("busSeat_customerDiscountedTicketPrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("busSeat_customerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("busSeat_customerReachedAmountCollected")
                        .HasColumnType("bit");

                    b.Property<decimal?>("busSeat_customerTicketPrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("busSeat_isBooked")
                        .HasColumnType("bit");

                    b.Property<long>("fk_bus_id")
                        .HasColumnType("bigint");

                    b.HasKey("busSeat_id");

                    b.HasIndex("fk_bus_id");

                    b.ToTable("tbl_busSeats");
                });

            modelBuilder.Entity("OnlineBusTicketReservationSystem.Models.tbl_discount", b =>
                {
                    b.Property<long>("discount_id")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("Created_at")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("discount_0_TO_5")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("discount_13_TO_50")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("discount_51")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("discount_6_TO_12")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<long>("fk_bus_id")
                        .HasColumnType("bigint");

                    b.HasKey("discount_id");

                    b.HasIndex("fk_bus_id")
                        .IsUnique();

                    b.ToTable("tbl_discount");
                });

            modelBuilder.Entity("OnlineBusTicketReservationSystem.Models.tbl_feedback", b =>
                {
                    b.Property<long>("feedback_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("feedback_id"), 1L, 1);

                    b.Property<DateTime>("Created_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("feedback_Message")
                        .HasColumnType("text");

                    b.Property<string>("feedback_subject")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("feedback_useremail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("feedback_username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("feedback_id");

                    b.ToTable("tbl_feedback");
                });

            modelBuilder.Entity("OnlineBusTicketReservationSystem.Models.tbl_history", b =>
                {
                    b.Property<long>("history_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("history_id"), 1L, 1);

                    b.Property<DateTime>("Created_at")
                        .HasColumnType("datetime2");

                    b.Property<long>("fk_user_id")
                        .HasColumnType("bigint");

                    b.Property<string>("history_CustomerAge")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("history_CustomerBusCategory")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("history_CustomerBusNumber")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("history_CustomerBusOrganizationName")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<DateTime>("history_CustomerBusStartingTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("history_CustomerDestination")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("history_CustomerDiscountPercentage")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<decimal?>("history_CustomerDiscountedTicketPrice")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("history_CustomerName")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("history_CustomerTicketPrice")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("history_CustormerSeatno")
                        .HasColumnType("int");

                    b.HasKey("history_id");

                    b.HasIndex("fk_user_id");

                    b.ToTable("tbl_history");
                });

            modelBuilder.Entity("OnlineBusTicketReservationSystem.Models.tbl_sale", b =>
                {
                    b.Property<long>("sale_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("sale_id"), 1L, 1);

                    b.Property<DateTime>("Created_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("Sale_busCategory")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sale_busDestination")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sale_busNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sale_busOrganizationName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Sale_busStartingTime")
                        .HasColumnType("datetime2");

                    b.Property<long>("fk_user_id")
                        .HasColumnType("bigint");

                    b.Property<int>("sale_TotalnoOfSeats")
                        .HasColumnType("int");

                    b.Property<int>("sale_noOfSoldSeats")
                        .HasColumnType("int");

                    b.Property<decimal>("sale_totalAmountCollectedFromOneBus")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("sale_id");

                    b.HasIndex("fk_user_id");

                    b.ToTable("tbl_sale");
                });

            modelBuilder.Entity("OnlineBusTicketReservationSystem.Models.tbl_user", b =>
                {
                    b.Property<long>("user_id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("user_id"), 1L, 1);

                    b.Property<DateTime>("Created_at")
                        .HasColumnType("datetime2");

                    b.Property<string>("bus_NumberPlateForConductor")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("organization_description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("organization_logo")
                        .HasColumnType("text");

                    b.Property<string>("organization_name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("user_approved")
                        .HasColumnType("bit");

                    b.Property<bool>("user_emailVerified")
                        .HasColumnType("bit");

                    b.Property<string>("user_email_phone")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<long?>("user_id_ForConductor")
                        .HasColumnType("bigint");

                    b.Property<string>("user_name")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("user_password")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("user_role")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("user_verification_code")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("user_id");

                    b.HasIndex("user_email_phone")
                        .IsUnique();

                    b.ToTable("tbl_user");
                });

            modelBuilder.Entity("OnlineBusTicketReservationSystem.Models.tbl_bus", b =>
                {
                    b.HasOne("OnlineBusTicketReservationSystem.Models.tbl_user", "tbl_user")
                        .WithMany("tbl_bus")
                        .HasForeignKey("fk_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("tbl_user");
                });

            modelBuilder.Entity("OnlineBusTicketReservationSystem.Models.tbl_busSeats", b =>
                {
                    b.HasOne("OnlineBusTicketReservationSystem.Models.tbl_bus", "tbl_bus")
                        .WithMany("tbl_busSeats")
                        .HasForeignKey("fk_bus_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("tbl_bus");
                });

            modelBuilder.Entity("OnlineBusTicketReservationSystem.Models.tbl_discount", b =>
                {
                    b.HasOne("OnlineBusTicketReservationSystem.Models.tbl_bus", "tbl_bus")
                        .WithOne("tbl_discount")
                        .HasForeignKey("OnlineBusTicketReservationSystem.Models.tbl_discount", "fk_bus_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("tbl_bus");
                });

            modelBuilder.Entity("OnlineBusTicketReservationSystem.Models.tbl_history", b =>
                {
                    b.HasOne("OnlineBusTicketReservationSystem.Models.tbl_user", "tbl_user")
                        .WithMany("tbl_history")
                        .HasForeignKey("fk_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("tbl_user");
                });

            modelBuilder.Entity("OnlineBusTicketReservationSystem.Models.tbl_sale", b =>
                {
                    b.HasOne("OnlineBusTicketReservationSystem.Models.tbl_user", "tbl_user")
                        .WithMany("tbl_sale")
                        .HasForeignKey("fk_user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("tbl_user");
                });

            modelBuilder.Entity("OnlineBusTicketReservationSystem.Models.tbl_bus", b =>
                {
                    b.Navigation("tbl_busSeats");

                    b.Navigation("tbl_discount")
                        .IsRequired();
                });

            modelBuilder.Entity("OnlineBusTicketReservationSystem.Models.tbl_user", b =>
                {
                    b.Navigation("tbl_bus");

                    b.Navigation("tbl_history");

                    b.Navigation("tbl_sale");
                });
#pragma warning restore 612, 618
        }
    }
}
