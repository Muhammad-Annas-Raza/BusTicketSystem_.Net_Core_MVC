using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineBusTicketReservationSystem.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_user",
                columns: table => new
                {
                    user_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    user_password = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    user_email_phone = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    user_verification_code = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    user_role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_user", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_bus",
                columns: table => new
                {
                    bus_id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    bus_organizationName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    bus_organizationDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    bus_startingTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    bus_destionation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    bus_ticketPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    bus_noOfSeats = table.Column<int>(type: "int", nullable: false),
                    bus_category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    bus_img_1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    bus_img_2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    bus_img_3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    bus_available = table.Column<bool>(type: "bit", nullable: false),
                    fk_user_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_bus", x => x.bus_id);
                    table.ForeignKey(
                        name: "FK_tbl_bus_tbl_user_fk_user_id",
                        column: x => x.fk_user_id,
                        principalTable: "tbl_user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_bookedSeat",
                columns: table => new
                {
                    bookedSeat_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    bookedSeat_customerAge = table.Column<int>(type: "int", nullable: false),
                    bookedSeat_customerTicketPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    bookedSeat_customerDiscountPercentage = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    bookedSeat_customerDiscTicketPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Bookedseat_customerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bookedseat_customerSeatno = table.Column<int>(type: "int", nullable: true),
                    bookedSeat_customerReached = table.Column<bool>(type: "bit", nullable: false),
                    fk_bus_id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_bookedSeat", x => x.bookedSeat_id);
                    table.ForeignKey(
                        name: "FK_tbl_bookedSeat_tbl_bus_fk_bus_id",
                        column: x => x.fk_bus_id,
                        principalTable: "tbl_bus",
                        principalColumn: "bus_id");
                });

            migrationBuilder.CreateTable(
                name: "tbl_busSeats",
                columns: table => new
                {
                    busSeats_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    busSeats_noOfSeats = table.Column<int>(type: "int", nullable: false),
                    busSeats_isBooked = table.Column<bool>(type: "bit", nullable: false),
                    fk_bus_id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_busSeats", x => x.busSeats_id);
                    table.ForeignKey(
                        name: "FK_tbl_busSeats_tbl_bus_fk_bus_id",
                        column: x => x.fk_bus_id,
                        principalTable: "tbl_bus",
                        principalColumn: "bus_id");
                });

            migrationBuilder.CreateTable(
                name: "tbl_discount",
                columns: table => new
                {
                    discount_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    discount_0_TO_5 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    discount_6_TO_12 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    discount_13_TO_50 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    discount_51 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    fk_bus_id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_discount", x => x.discount_id);
                    table.ForeignKey(
                        name: "FK_tbl_discount_tbl_bus_fk_bus_id",
                        column: x => x.fk_bus_id,
                        principalTable: "tbl_bus",
                        principalColumn: "bus_id");
                });

            migrationBuilder.CreateTable(
                name: "tbl_sale",
                columns: table => new
                {
                    sale_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sale_noOfSeatsSale = table.Column<int>(type: "int", nullable: false),
                    Sale_busStartingTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sale_busDestination = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sale_busOrganizationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sale_busCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sale_totalAmountCollectedFromOneBus = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    fk_bus_id = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_sale", x => x.sale_id);
                    table.ForeignKey(
                        name: "FK_tbl_sale_tbl_bus_fk_bus_id",
                        column: x => x.fk_bus_id,
                        principalTable: "tbl_bus",
                        principalColumn: "bus_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_bookedSeat_fk_bus_id",
                table: "tbl_bookedSeat",
                column: "fk_bus_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_bus_fk_user_id",
                table: "tbl_bus",
                column: "fk_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_busSeats_fk_bus_id",
                table: "tbl_busSeats",
                column: "fk_bus_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_discount_fk_bus_id",
                table: "tbl_discount",
                column: "fk_bus_id",
                unique: true,
                filter: "[fk_bus_id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_sale_fk_bus_id",
                table: "tbl_sale",
                column: "fk_bus_id",
                unique: true,
                filter: "[fk_bus_id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_user_user_email_phone",
                table: "tbl_user",
                column: "user_email_phone",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_bookedSeat");

            migrationBuilder.DropTable(
                name: "tbl_busSeats");

            migrationBuilder.DropTable(
                name: "tbl_discount");

            migrationBuilder.DropTable(
                name: "tbl_sale");

            migrationBuilder.DropTable(
                name: "tbl_bus");

            migrationBuilder.DropTable(
                name: "tbl_user");
        }
    }
}
