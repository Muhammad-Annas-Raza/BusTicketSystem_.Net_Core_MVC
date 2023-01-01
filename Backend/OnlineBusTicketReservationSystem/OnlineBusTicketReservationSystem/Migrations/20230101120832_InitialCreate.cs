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
                name: "tbl_feedback",
                columns: table => new
                {
                    feedback_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    feedback_username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    feedback_useremail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    feedback_subject = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    feedback_Message = table.Column<string>(type: "text", nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_feedback", x => x.feedback_id);
                });

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
                    user_emailVerified = table.Column<bool>(type: "bit", nullable: false),
                    user_approved = table.Column<bool>(type: "bit", nullable: false),
                    organization_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    organization_description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    organization_logo = table.Column<string>(type: "text", nullable: true),
                    bus_NumberPlateForConductor = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    user_id_ForConductor = table.Column<long>(type: "bigint", nullable: true),
                    user_role = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_user", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_bus",
                columns: table => new
                {
                    bus_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    bus_NumberPlate = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    bus_startingTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    bus_destination = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    bus_ticketPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    bus_noOfSeats = table.Column<int>(type: "int", nullable: true),
                    bus_category = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    bus_OrganizationName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    bus_available = table.Column<bool>(type: "bit", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
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
                name: "tbl_history",
                columns: table => new
                {
                    history_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    history_CustomerName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    history_CustomerAge = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    history_CustomerTicketPrice = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    history_CustomerDiscountPercentage = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    history_CustomerDiscountedTicketPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    history_CustomerDestination = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    history_CustormerSeatno = table.Column<int>(type: "int", nullable: false),
                    history_CustomerBusOrganizationName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    history_CustomerBusStartingTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    history_CustomerBusNumber = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    history_CustomerBusCategory = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fk_user_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_history", x => x.history_id);
                    table.ForeignKey(
                        name: "FK_tbl_history_tbl_user_fk_user_id",
                        column: x => x.fk_user_id,
                        principalTable: "tbl_user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_sale",
                columns: table => new
                {
                    sale_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    sale_TotalnoOfSeats = table.Column<int>(type: "int", nullable: false),
                    sale_noOfSoldSeats = table.Column<int>(type: "int", nullable: false),
                    Sale_busStartingTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Sale_busNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sale_busDestination = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sale_busOrganizationName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sale_busCategory = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    sale_totalAmountCollectedFromOneBus = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fk_user_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_sale", x => x.sale_id);
                    table.ForeignKey(
                        name: "FK_tbl_sale_tbl_user_fk_user_id",
                        column: x => x.fk_user_id,
                        principalTable: "tbl_user",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_busSeats",
                columns: table => new
                {
                    busSeat_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    busSeat_SeatNumber = table.Column<int>(type: "int", nullable: false),
                    busSeat_isBooked = table.Column<bool>(type: "bit", nullable: false),
                    busSeat_customerAge = table.Column<int>(type: "int", nullable: true),
                    busSeat_customerTicketPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    busSeat_customerDiscountPercentage = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    busSeat_customerDiscountedTicketPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    busSeat_customerName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    busSeat_customerReachedAmountCollected = table.Column<bool>(type: "bit", nullable: false),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fk_bus_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_busSeats", x => x.busSeat_id);
                    table.ForeignKey(
                        name: "FK_tbl_busSeats_tbl_bus_fk_bus_id",
                        column: x => x.fk_bus_id,
                        principalTable: "tbl_bus",
                        principalColumn: "bus_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tbl_discount",
                columns: table => new
                {
                    discount_id = table.Column<long>(type: "bigint", nullable: false),
                    discount_0_TO_5 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    discount_6_TO_12 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    discount_13_TO_50 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    discount_51 = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Created_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    fk_bus_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_discount", x => x.discount_id);
                    table.ForeignKey(
                        name: "FK_tbl_discount_tbl_bus_fk_bus_id",
                        column: x => x.fk_bus_id,
                        principalTable: "tbl_bus",
                        principalColumn: "bus_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_bus_bus_NumberPlate",
                table: "tbl_bus",
                column: "bus_NumberPlate",
                unique: true);

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
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tbl_history_fk_user_id",
                table: "tbl_history",
                column: "fk_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_sale_fk_user_id",
                table: "tbl_sale",
                column: "fk_user_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_user_user_email_phone",
                table: "tbl_user",
                column: "user_email_phone",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_busSeats");

            migrationBuilder.DropTable(
                name: "tbl_discount");

            migrationBuilder.DropTable(
                name: "tbl_feedback");

            migrationBuilder.DropTable(
                name: "tbl_history");

            migrationBuilder.DropTable(
                name: "tbl_sale");

            migrationBuilder.DropTable(
                name: "tbl_bus");

            migrationBuilder.DropTable(
                name: "tbl_user");
        }
    }
}
