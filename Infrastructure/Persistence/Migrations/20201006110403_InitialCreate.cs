using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bank.Infrastructure.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountTypes",
                columns: table => new
                {
                    AccountTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountTypeDescription = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountTypes", x => x.AccountTypeId);
                });

            migrationBuilder.CreateTable(
                name: "CustomerAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(nullable: false),
                    BankBranch = table.Column<int>(nullable: false),
                    BankAccount = table.Column<int>(nullable: false),
                    AccountTypeId = table.Column<int>(nullable: false),
                    FarePlanId = table.Column<int>(nullable: false),
                    IsActiveAccount = table.Column<bool>(nullable: false),
                    Balance = table.Column<decimal>(type: "Money", nullable: false, defaultValue: 0m),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    RegistrationNumber = table.Column<string>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FarePlans",
                columns: table => new
                {
                    FarePlanId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FarePlanDescription = table.Column<string>(nullable: false),
                    FreeTransfersQuantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FarePlans", x => x.FarePlanId);
                });

            migrationBuilder.CreateTable(
                name: "Transfers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<decimal>(type: "Money", nullable: false),
                    From = table.Column<string>(nullable: false),
                    To = table.Column<string>(nullable: false),
                    Tax = table.Column<decimal>(type: "Money", nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    ModifiedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AccountTypes",
                columns: new[] { "AccountTypeId", "AccountTypeDescription" },
                values: new object[,]
                {
                    { 1, "corrente" },
                    { 2, "poupanca" }
                });

            migrationBuilder.InsertData(
                table: "CustomerAccounts",
                columns: new[] { "Id", "AccountTypeId", "Balance", "BankAccount", "BankBranch", "CustomerId", "FarePlanId", "IsActiveAccount" },
                values: new object[,]
                {
                    { 1, 1, 100m, 1, 1, 1, 1, true },
                    { 2, 1, 100m, 2, 1, 2, 1, true }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Name", "RegistrationNumber" },
                values: new object[,]
                {
                    { 1, "A", "123" },
                    { 2, "B", "456" }
                });

            migrationBuilder.InsertData(
                table: "FarePlans",
                columns: new[] { "FarePlanId", "FarePlanDescription", "FreeTransfersQuantity" },
                values: new object[,]
                {
                    { 1, "servicos essenciais", 2 },
                    { 2, "basico", 4 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountTypes");

            migrationBuilder.DropTable(
                name: "CustomerAccounts");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "FarePlans");

            migrationBuilder.DropTable(
                name: "Transfers");
        }
    }
}
