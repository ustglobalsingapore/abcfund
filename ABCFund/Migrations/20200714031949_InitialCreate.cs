using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ABCFund.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fund",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StockId = table.Column<int>(nullable: false),
                    BenchmarkId = table.Column<int>(nullable: false),
                    ManagerId = table.Column<int>(nullable: false),
                    Size = table.Column<decimal>(nullable: false),
                    InitialFee = table.Column<decimal>(nullable: false),
                    Scheme = table.Column<string>(nullable: true),
                    AllInFee = table.Column<decimal>(nullable: false),
                    ExpenseRatio = table.Column<decimal>(nullable: false),
                    NAV = table.Column<decimal>(nullable: false),
                    DividendFrequency = table.Column<string>(nullable: true),
                    InceptionDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fund", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Historical",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StockId = table.Column<int>(nullable: false),
                    TradeDate = table.Column<DateTime>(nullable: false),
                    OpenPrice = table.Column<decimal>(nullable: false),
                    HighPrice = table.Column<decimal>(nullable: false),
                    LowPrice = table.Column<decimal>(nullable: false),
                    ClosePrice = table.Column<decimal>(nullable: false),
                    AdjPrice = table.Column<decimal>(nullable: false),
                    Volume = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historical", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Holding",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StockId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Symbol = table.Column<string>(nullable: true),
                    Weight = table.Column<decimal>(nullable: false),
                    Sector = table.Column<string>(nullable: true),
                    Industry = table.Column<string>(nullable: true),
                    Shares = table.Column<decimal>(nullable: false),
                    InitialValue = table.Column<decimal>(nullable: false),
                    FundId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holding", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Investor",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    ManagerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Periodic",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StockId = table.Column<int>(nullable: false),
                    TradeDate = table.Column<DateTime>(nullable: false),
                    OneDay = table.Column<decimal>(nullable: false),
                    OneMonth = table.Column<decimal>(nullable: false),
                    ThreeMonth = table.Column<decimal>(nullable: false),
                    SixMonth = table.Column<decimal>(nullable: false),
                    OneYear = table.Column<decimal>(nullable: false),
                    YearToDate = table.Column<decimal>(nullable: false),
                    SinceInception = table.Column<decimal>(nullable: false),
                    AnnualisedReturns = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Periodic", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stock",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Symbol = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Exchange = table.Column<string>(nullable: true),
                    Sector = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Currency = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stock", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Names = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fund");

            migrationBuilder.DropTable(
                name: "Historical");

            migrationBuilder.DropTable(
                name: "Holding");

            migrationBuilder.DropTable(
                name: "Investor");

            migrationBuilder.DropTable(
                name: "Periodic");

            migrationBuilder.DropTable(
                name: "Stock");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
