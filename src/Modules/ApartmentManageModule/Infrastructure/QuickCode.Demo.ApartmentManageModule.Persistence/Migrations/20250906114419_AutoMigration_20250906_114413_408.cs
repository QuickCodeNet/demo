using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickCode.Demo.ApartmentManageModule.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AutoMigration_20250906_114413_408 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CONTACTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PHONE = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    EMAIL = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IDENTITY_NUMBER = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ADDRESS = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CREATED_DATE = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CONTACTS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EXPENSE_TYPES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EXPENSE_TYPES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FEE_TYPES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FEE_TYPES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PAYMENT_MONTHS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PAYMENT_MONTHS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PAYMENT_TYPES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PAYMENT_TYPES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PAYMENT_YEARS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PAYMENT_YEARS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SITES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ADDRESS = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CITY = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DISTRICT = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    POSTAL_CODE = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    COUNTRY = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PHONE = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    EMAIL = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CREATED_DATE = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SITES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "APARTMENTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SITE_ID = table.Column<int>(type: "int", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ADDRESS = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TOTAL_FLATS = table.Column<int>(type: "int", nullable: false, defaultValueSql: "0"),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CREATED_DATE = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APARTMENTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_APARTMENTS_SITES_SITE_ID",
                        column: x => x.SITE_ID,
                        principalTable: "SITES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SITE_MANAGERS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SITE_ID = table.Column<int>(type: "int", nullable: false),
                    CONTACT_ID = table.Column<int>(type: "int", nullable: false),
                    START_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    END_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CREATED_DATE = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SITE_MANAGERS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SITE_MANAGERS_CONTACTS_CONTACT_ID",
                        column: x => x.CONTACT_ID,
                        principalTable: "CONTACTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SITE_MANAGERS_SITES_SITE_ID",
                        column: x => x.SITE_ID,
                        principalTable: "SITES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "APARTMENT_FEE_PLANS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SITE_ID = table.Column<int>(type: "int", nullable: false),
                    APARTMENT_ID = table.Column<int>(type: "int", nullable: false),
                    YEAR_ID = table.Column<int>(type: "int", nullable: false),
                    MONTH_ID = table.Column<int>(type: "int", nullable: false),
                    FEE_AMOUNT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APARTMENT_FEE_PLANS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_APARTMENT_FEE_PLANS_APARTMENTS_APARTMENT_ID",
                        column: x => x.APARTMENT_ID,
                        principalTable: "APARTMENTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_APARTMENT_FEE_PLANS_PAYMENT_MONTHS_MONTH_ID",
                        column: x => x.MONTH_ID,
                        principalTable: "PAYMENT_MONTHS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_APARTMENT_FEE_PLANS_PAYMENT_YEARS_YEAR_ID",
                        column: x => x.YEAR_ID,
                        principalTable: "PAYMENT_YEARS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_APARTMENT_FEE_PLANS_SITES_SITE_ID",
                        column: x => x.SITE_ID,
                        principalTable: "SITES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "COMMON_EXPENSES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SITE_ID = table.Column<int>(type: "int", nullable: false),
                    APARTMENT_ID = table.Column<int>(type: "int", nullable: false),
                    NAME = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    EXPENSE_TYPE_ID = table.Column<int>(type: "int", nullable: false),
                    YEAR_ID = table.Column<int>(type: "int", nullable: false),
                    MONTH_ID = table.Column<int>(type: "int", nullable: false),
                    EXPENSE_AMOUNT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PAID = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PAID_AT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PAYMENT_TYPE_ID = table.Column<int>(type: "int", nullable: false),
                    CREATED_DATE = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMMON_EXPENSES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_COMMON_EXPENSES_APARTMENTS_APARTMENT_ID",
                        column: x => x.APARTMENT_ID,
                        principalTable: "APARTMENTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_COMMON_EXPENSES_EXPENSE_TYPES_EXPENSE_TYPE_ID",
                        column: x => x.EXPENSE_TYPE_ID,
                        principalTable: "EXPENSE_TYPES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_COMMON_EXPENSES_PAYMENT_MONTHS_MONTH_ID",
                        column: x => x.MONTH_ID,
                        principalTable: "PAYMENT_MONTHS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_COMMON_EXPENSES_PAYMENT_TYPES_PAYMENT_TYPE_ID",
                        column: x => x.PAYMENT_TYPE_ID,
                        principalTable: "PAYMENT_TYPES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_COMMON_EXPENSES_PAYMENT_YEARS_YEAR_ID",
                        column: x => x.YEAR_ID,
                        principalTable: "PAYMENT_YEARS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_COMMON_EXPENSES_SITES_SITE_ID",
                        column: x => x.SITE_ID,
                        principalTable: "SITES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FLATS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    APARTMENT_ID = table.Column<int>(type: "int", nullable: false),
                    SITE_ID = table.Column<int>(type: "int", nullable: false),
                    FLAT_NUMBER = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    FLOOR = table.Column<int>(type: "int", nullable: true),
                    SQUARE_METERS = table.Column<int>(type: "int", nullable: true),
                    ROOM_COUNT = table.Column<int>(type: "int", nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CREATED_DATE = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FLATS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FLATS_APARTMENTS_APARTMENT_ID",
                        column: x => x.APARTMENT_ID,
                        principalTable: "APARTMENTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FLATS_SITES_SITE_ID",
                        column: x => x.SITE_ID,
                        principalTable: "SITES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EXPENSE_INSTALLMENTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SITE_ID = table.Column<int>(type: "int", nullable: false),
                    APARTMENT_ID = table.Column<int>(type: "int", nullable: false),
                    EXPENSE_ID = table.Column<int>(type: "int", nullable: false),
                    INSTALLMENT_NUMBER = table.Column<int>(type: "int", nullable: false),
                    TOTAL_INSTALLMENTS = table.Column<int>(type: "int", nullable: false),
                    INSTALLMENT_AMOUNT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DUE_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PAID = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PAID_AT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PAYMENT_TYPE_ID = table.Column<int>(type: "int", nullable: false),
                    CREATED_DATE = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EXPENSE_INSTALLMENTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_EXPENSE_INSTALLMENTS_APARTMENTS_APARTMENT_ID",
                        column: x => x.APARTMENT_ID,
                        principalTable: "APARTMENTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EXPENSE_INSTALLMENTS_COMMON_EXPENSES_EXPENSE_ID",
                        column: x => x.EXPENSE_ID,
                        principalTable: "COMMON_EXPENSES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EXPENSE_INSTALLMENTS_PAYMENT_TYPES_PAYMENT_TYPE_ID",
                        column: x => x.PAYMENT_TYPE_ID,
                        principalTable: "PAYMENT_TYPES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EXPENSE_INSTALLMENTS_SITES_SITE_ID",
                        column: x => x.SITE_ID,
                        principalTable: "SITES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FLAT_CONTACTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FLAT_ID = table.Column<int>(type: "int", nullable: false),
                    CONTACT_ID = table.Column<int>(type: "int", nullable: false),
                    RELATIONSHIP_TYPE = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    START_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    END_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CREATED_DATE = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FLAT_CONTACTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FLAT_CONTACTS_CONTACTS_CONTACT_ID",
                        column: x => x.CONTACT_ID,
                        principalTable: "CONTACTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FLAT_CONTACTS_FLATS_FLAT_ID",
                        column: x => x.FLAT_ID,
                        principalTable: "FLATS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FLAT_PAYMENTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SITE_ID = table.Column<int>(type: "int", nullable: false),
                    FLAT_ID = table.Column<int>(type: "int", nullable: false),
                    APARTMENT_ID = table.Column<int>(type: "int", nullable: false),
                    YEAR_ID = table.Column<int>(type: "int", nullable: false),
                    MONTH_ID = table.Column<int>(type: "int", nullable: false),
                    PAYMENT_AMOUNT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FEE_TYPE_ID = table.Column<int>(type: "int", nullable: false),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    FEE_PLAN_ID = table.Column<int>(type: "int", nullable: false),
                    EXPENSE_ID = table.Column<int>(type: "int", nullable: true),
                    PAID = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PAID_AT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PAYMENT_TYPE_ID = table.Column<int>(type: "int", nullable: false),
                    CREATED_DATE = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FLAT_PAYMENTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FLAT_PAYMENTS_APARTMENTS_APARTMENT_ID",
                        column: x => x.APARTMENT_ID,
                        principalTable: "APARTMENTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FLAT_PAYMENTS_APARTMENT_FEE_PLANS_FEE_PLAN_ID",
                        column: x => x.FEE_PLAN_ID,
                        principalTable: "APARTMENT_FEE_PLANS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FLAT_PAYMENTS_COMMON_EXPENSES_EXPENSE_ID",
                        column: x => x.EXPENSE_ID,
                        principalTable: "COMMON_EXPENSES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FLAT_PAYMENTS_FEE_TYPES_FEE_TYPE_ID",
                        column: x => x.FEE_TYPE_ID,
                        principalTable: "FEE_TYPES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FLAT_PAYMENTS_FLATS_FLAT_ID",
                        column: x => x.FLAT_ID,
                        principalTable: "FLATS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FLAT_PAYMENTS_PAYMENT_MONTHS_MONTH_ID",
                        column: x => x.MONTH_ID,
                        principalTable: "PAYMENT_MONTHS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FLAT_PAYMENTS_PAYMENT_TYPES_PAYMENT_TYPE_ID",
                        column: x => x.PAYMENT_TYPE_ID,
                        principalTable: "PAYMENT_TYPES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FLAT_PAYMENTS_PAYMENT_YEARS_YEAR_ID",
                        column: x => x.YEAR_ID,
                        principalTable: "PAYMENT_YEARS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FLAT_PAYMENTS_SITES_SITE_ID",
                        column: x => x.SITE_ID,
                        principalTable: "SITES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "USER_SITE_ACCESSES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USER_ID = table.Column<int>(type: "int", nullable: false),
                    SITE_ID = table.Column<int>(type: "int", nullable: false),
                    FLAT_ID = table.Column<int>(type: "int", nullable: true),
                    ACCESS_TYPE = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    GRANTED_DATE = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    GRANTED_BY = table.Column<int>(type: "int", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_SITE_ACCESSES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_USER_SITE_ACCESSES_FLATS_FLAT_ID",
                        column: x => x.FLAT_ID,
                        principalTable: "FLATS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_USER_SITE_ACCESSES_SITES_SITE_ID",
                        column: x => x.SITE_ID,
                        principalTable: "SITES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FLAT_EXPENSE_INSTALLMENTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SITE_ID = table.Column<int>(type: "int", nullable: false),
                    APARTMENT_ID = table.Column<int>(type: "int", nullable: false),
                    FLAT_ID = table.Column<int>(type: "int", nullable: false),
                    EXPENSE_ID = table.Column<int>(type: "int", nullable: false),
                    EXPENSE_INSTALLMENT_ID = table.Column<int>(type: "int", nullable: false),
                    FLAT_INSTALLMENT_NUMBER = table.Column<int>(type: "int", nullable: false),
                    TOTAL_FLAT_INSTALLMENTS = table.Column<int>(type: "int", nullable: false),
                    FLAT_INSTALLMENT_AMOUNT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DUE_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PAID = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    PAID_AT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PAYMENT_TYPE_ID = table.Column<int>(type: "int", nullable: false),
                    CREATED_DATE = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FLAT_EXPENSE_INSTALLMENTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FLAT_EXPENSE_INSTALLMENTS_APARTMENTS_APARTMENT_ID",
                        column: x => x.APARTMENT_ID,
                        principalTable: "APARTMENTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FLAT_EXPENSE_INSTALLMENTS_COMMON_EXPENSES_EXPENSE_ID",
                        column: x => x.EXPENSE_ID,
                        principalTable: "COMMON_EXPENSES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FLAT_EXPENSE_INSTALLMENTS_EXPENSE_INSTALLMENTS_EXPENSE_INSTALLMENT_ID",
                        column: x => x.EXPENSE_INSTALLMENT_ID,
                        principalTable: "EXPENSE_INSTALLMENTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FLAT_EXPENSE_INSTALLMENTS_FLATS_FLAT_ID",
                        column: x => x.FLAT_ID,
                        principalTable: "FLATS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FLAT_EXPENSE_INSTALLMENTS_PAYMENT_TYPES_PAYMENT_TYPE_ID",
                        column: x => x.PAYMENT_TYPE_ID,
                        principalTable: "PAYMENT_TYPES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FLAT_EXPENSE_INSTALLMENTS_SITES_SITE_ID",
                        column: x => x.SITE_ID,
                        principalTable: "SITES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_APARTMENT_FEE_PLANS_APARTMENT_ID",
                table: "APARTMENT_FEE_PLANS",
                column: "APARTMENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_APARTMENT_FEE_PLANS_IsDeleted",
                table: "APARTMENT_FEE_PLANS",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_APARTMENT_FEE_PLANS_MONTH_ID",
                table: "APARTMENT_FEE_PLANS",
                column: "MONTH_ID");

            migrationBuilder.CreateIndex(
                name: "IX_APARTMENT_FEE_PLANS_SITE_ID",
                table: "APARTMENT_FEE_PLANS",
                column: "SITE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_APARTMENT_FEE_PLANS_YEAR_ID",
                table: "APARTMENT_FEE_PLANS",
                column: "YEAR_ID");

            migrationBuilder.CreateIndex(
                name: "IX_APARTMENTS_IsDeleted",
                table: "APARTMENTS",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_APARTMENTS_SITE_ID",
                table: "APARTMENTS",
                column: "SITE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_COMMON_EXPENSES_APARTMENT_ID",
                table: "COMMON_EXPENSES",
                column: "APARTMENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_COMMON_EXPENSES_EXPENSE_TYPE_ID",
                table: "COMMON_EXPENSES",
                column: "EXPENSE_TYPE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_COMMON_EXPENSES_IsDeleted",
                table: "COMMON_EXPENSES",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_COMMON_EXPENSES_MONTH_ID",
                table: "COMMON_EXPENSES",
                column: "MONTH_ID");

            migrationBuilder.CreateIndex(
                name: "IX_COMMON_EXPENSES_PAYMENT_TYPE_ID",
                table: "COMMON_EXPENSES",
                column: "PAYMENT_TYPE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_COMMON_EXPENSES_SITE_ID",
                table: "COMMON_EXPENSES",
                column: "SITE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_COMMON_EXPENSES_YEAR_ID",
                table: "COMMON_EXPENSES",
                column: "YEAR_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CONTACTS_IsDeleted",
                table: "CONTACTS",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_EXPENSE_INSTALLMENTS_APARTMENT_ID",
                table: "EXPENSE_INSTALLMENTS",
                column: "APARTMENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_EXPENSE_INSTALLMENTS_EXPENSE_ID",
                table: "EXPENSE_INSTALLMENTS",
                column: "EXPENSE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_EXPENSE_INSTALLMENTS_IsDeleted",
                table: "EXPENSE_INSTALLMENTS",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_EXPENSE_INSTALLMENTS_PAYMENT_TYPE_ID",
                table: "EXPENSE_INSTALLMENTS",
                column: "PAYMENT_TYPE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_EXPENSE_INSTALLMENTS_SITE_ID",
                table: "EXPENSE_INSTALLMENTS",
                column: "SITE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_EXPENSE_TYPES_IsDeleted",
                table: "EXPENSE_TYPES",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_FEE_TYPES_IsDeleted",
                table: "FEE_TYPES",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_FLAT_CONTACTS_CONTACT_ID",
                table: "FLAT_CONTACTS",
                column: "CONTACT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FLAT_CONTACTS_FLAT_ID",
                table: "FLAT_CONTACTS",
                column: "FLAT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FLAT_CONTACTS_IsDeleted",
                table: "FLAT_CONTACTS",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_FLAT_EXPENSE_INSTALLMENTS_APARTMENT_ID",
                table: "FLAT_EXPENSE_INSTALLMENTS",
                column: "APARTMENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FLAT_EXPENSE_INSTALLMENTS_EXPENSE_ID",
                table: "FLAT_EXPENSE_INSTALLMENTS",
                column: "EXPENSE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FLAT_EXPENSE_INSTALLMENTS_EXPENSE_INSTALLMENT_ID",
                table: "FLAT_EXPENSE_INSTALLMENTS",
                column: "EXPENSE_INSTALLMENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FLAT_EXPENSE_INSTALLMENTS_FLAT_ID",
                table: "FLAT_EXPENSE_INSTALLMENTS",
                column: "FLAT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FLAT_EXPENSE_INSTALLMENTS_IsDeleted",
                table: "FLAT_EXPENSE_INSTALLMENTS",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_FLAT_EXPENSE_INSTALLMENTS_PAYMENT_TYPE_ID",
                table: "FLAT_EXPENSE_INSTALLMENTS",
                column: "PAYMENT_TYPE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FLAT_EXPENSE_INSTALLMENTS_SITE_ID",
                table: "FLAT_EXPENSE_INSTALLMENTS",
                column: "SITE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FLAT_PAYMENTS_APARTMENT_ID",
                table: "FLAT_PAYMENTS",
                column: "APARTMENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FLAT_PAYMENTS_EXPENSE_ID",
                table: "FLAT_PAYMENTS",
                column: "EXPENSE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FLAT_PAYMENTS_FEE_PLAN_ID",
                table: "FLAT_PAYMENTS",
                column: "FEE_PLAN_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FLAT_PAYMENTS_FEE_TYPE_ID",
                table: "FLAT_PAYMENTS",
                column: "FEE_TYPE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FLAT_PAYMENTS_FLAT_ID",
                table: "FLAT_PAYMENTS",
                column: "FLAT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FLAT_PAYMENTS_IsDeleted",
                table: "FLAT_PAYMENTS",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_FLAT_PAYMENTS_MONTH_ID",
                table: "FLAT_PAYMENTS",
                column: "MONTH_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FLAT_PAYMENTS_PAYMENT_TYPE_ID",
                table: "FLAT_PAYMENTS",
                column: "PAYMENT_TYPE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FLAT_PAYMENTS_SITE_ID",
                table: "FLAT_PAYMENTS",
                column: "SITE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FLAT_PAYMENTS_YEAR_ID",
                table: "FLAT_PAYMENTS",
                column: "YEAR_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FLATS_APARTMENT_ID",
                table: "FLATS",
                column: "APARTMENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_FLATS_IsDeleted",
                table: "FLATS",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_FLATS_SITE_ID",
                table: "FLATS",
                column: "SITE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PAYMENT_TYPES_IsDeleted",
                table: "PAYMENT_TYPES",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_SITE_MANAGERS_CONTACT_ID",
                table: "SITE_MANAGERS",
                column: "CONTACT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SITE_MANAGERS_IsDeleted",
                table: "SITE_MANAGERS",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_SITE_MANAGERS_SITE_ID",
                table: "SITE_MANAGERS",
                column: "SITE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SITES_IsDeleted",
                table: "SITES",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_USER_SITE_ACCESSES_FLAT_ID",
                table: "USER_SITE_ACCESSES",
                column: "FLAT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USER_SITE_ACCESSES_IsDeleted",
                table: "USER_SITE_ACCESSES",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_USER_SITE_ACCESSES_SITE_ID",
                table: "USER_SITE_ACCESSES",
                column: "SITE_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FLAT_CONTACTS");

            migrationBuilder.DropTable(
                name: "FLAT_EXPENSE_INSTALLMENTS");

            migrationBuilder.DropTable(
                name: "FLAT_PAYMENTS");

            migrationBuilder.DropTable(
                name: "SITE_MANAGERS");

            migrationBuilder.DropTable(
                name: "USER_SITE_ACCESSES");

            migrationBuilder.DropTable(
                name: "EXPENSE_INSTALLMENTS");

            migrationBuilder.DropTable(
                name: "APARTMENT_FEE_PLANS");

            migrationBuilder.DropTable(
                name: "FEE_TYPES");

            migrationBuilder.DropTable(
                name: "CONTACTS");

            migrationBuilder.DropTable(
                name: "FLATS");

            migrationBuilder.DropTable(
                name: "COMMON_EXPENSES");

            migrationBuilder.DropTable(
                name: "APARTMENTS");

            migrationBuilder.DropTable(
                name: "EXPENSE_TYPES");

            migrationBuilder.DropTable(
                name: "PAYMENT_MONTHS");

            migrationBuilder.DropTable(
                name: "PAYMENT_TYPES");

            migrationBuilder.DropTable(
                name: "PAYMENT_YEARS");

            migrationBuilder.DropTable(
                name: "SITES");
        }
    }
}
