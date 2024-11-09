using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TaxiCompany.Domain.Migrations
{
    /// <inheritdoc />
    public partial class postgres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "car",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    colour = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    model = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    serial_number = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    release_year = table.Column<DateTime>(type: "timestamp with time zone", maxLength: 4, nullable: false),
                    assigned_driver_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_car", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "client",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    full_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    phone_number = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_client", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "driver",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    full_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    phone_number = table.Column<string>(type: "character varying(11)", maxLength: 11, nullable: false),
                    passport = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    address = table.Column<string>(type: "character varying(80)", maxLength: 80, nullable: false),
                    assigned_car_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_driver", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "trip",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    departure = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    destination = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", maxLength: 10, nullable: false),
                    driving_time = table.Column<TimeOnly>(type: "time without time zone", maxLength: 10, nullable: false),
                    cost = table.Column<decimal>(type: "numeric", maxLength: 10, nullable: false),
                    assigned_client_id = table.Column<int>(type: "integer", nullable: false),
                    assigned_car_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trip", x => x.id);
                    table.ForeignKey(
                        name: "FK_trip_car_assigned_car_id",
                        column: x => x.assigned_car_id,
                        principalTable: "car",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_trip_client_assigned_client_id",
                        column: x => x.assigned_client_id,
                        principalTable: "client",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_car_serial_number",
                table: "car",
                column: "serial_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_driver_passport",
                table: "driver",
                column: "passport",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_trip_assigned_car_id",
                table: "trip",
                column: "assigned_car_id");

            migrationBuilder.CreateIndex(
                name: "IX_trip_assigned_client_id",
                table: "trip",
                column: "assigned_client_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "driver");

            migrationBuilder.DropTable(
                name: "trip");

            migrationBuilder.DropTable(
                name: "car");

            migrationBuilder.DropTable(
                name: "client");
        }
    }
}
