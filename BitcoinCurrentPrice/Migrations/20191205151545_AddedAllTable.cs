using Microsoft.EntityFrameworkCore.Migrations;

namespace BitcoinCurrentPrice.Migrations
{
    public partial class AddedAllTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EURs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<string>(nullable: true),
                    symbol = table.Column<string>(nullable: true),
                    rate = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    rate_float = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EURs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GBPs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<string>(nullable: true),
                    symbol = table.Column<string>(nullable: true),
                    rate = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    rate_float = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GBPs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Times",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    updated = table.Column<string>(nullable: true),
                    updatedISO = table.Column<string>(nullable: true),
                    updateduk = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Times", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "USDs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    code = table.Column<string>(nullable: true),
                    symbol = table.Column<string>(nullable: true),
                    rate = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    rate_float = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USDs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bpis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USDId = table.Column<int>(nullable: false),
                    GBPId = table.Column<int>(nullable: false),
                    EURId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bpis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bpis_EURs_EURId",
                        column: x => x.EURId,
                        principalTable: "EURs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bpis_GBPs_GBPId",
                        column: x => x.GBPId,
                        principalTable: "GBPs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bpis_USDs_USDId",
                        column: x => x.USDId,
                        principalTable: "USDs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RootObjects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TimeId = table.Column<int>(nullable: true),
                    disclaimer = table.Column<string>(nullable: true),
                    chartName = table.Column<string>(nullable: true),
                    BpiId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RootObjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RootObjects_Bpis_BpiId",
                        column: x => x.BpiId,
                        principalTable: "Bpis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RootObjects_Times_TimeId",
                        column: x => x.TimeId,
                        principalTable: "Times",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bpis_EURId",
                table: "Bpis",
                column: "EURId");

            migrationBuilder.CreateIndex(
                name: "IX_Bpis_GBPId",
                table: "Bpis",
                column: "GBPId");

            migrationBuilder.CreateIndex(
                name: "IX_Bpis_USDId",
                table: "Bpis",
                column: "USDId");

            migrationBuilder.CreateIndex(
                name: "IX_RootObjects_BpiId",
                table: "RootObjects",
                column: "BpiId");

            migrationBuilder.CreateIndex(
                name: "IX_RootObjects_TimeId",
                table: "RootObjects",
                column: "TimeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RootObjects");

            migrationBuilder.DropTable(
                name: "Bpis");

            migrationBuilder.DropTable(
                name: "Times");

            migrationBuilder.DropTable(
                name: "EURs");

            migrationBuilder.DropTable(
                name: "GBPs");

            migrationBuilder.DropTable(
                name: "USDs");
        }
    }
}
