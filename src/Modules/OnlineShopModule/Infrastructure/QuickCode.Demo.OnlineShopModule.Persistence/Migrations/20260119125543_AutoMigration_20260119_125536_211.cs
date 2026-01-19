using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickCode.Demo.OnlineShopModule.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AutoMigration_20260119_125536_211 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AUDIT_LOGS",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ENTITY_NAME = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ENTITY_ID = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ACTION = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    USER_ID = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    USER_NAME = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    USER_GROUP = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TIMESTAMP = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OLD_VALUES = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    NEW_VALUES = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    CHANGED_COLUMNS = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    IS_CHANGED = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CHANGE_SUMMARY = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    IP_ADDRESS = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    USER_AGENT = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CORRELATION_ID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IS_SUCCESS = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    ERROR_MESSAGE = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    HASH = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AUDIT_LOGS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CAMPAIGNS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    START_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    END_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    BUDGET = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TARGET_AUDIENCE = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    MIN_ORDER_AMOUNT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAMPAIGNS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "COMPANIES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SECTOR = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMPANIES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "COMPANY_INFOS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ADDRESS = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CITY = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    COUNTRY = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ABOUT_US = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    EMAIL = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PHONE = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    LONGITUDE = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LATITUDE = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    COMPANY_ICON_URL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    COMPANY_LOGO_URL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMPANY_INFOS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "HOME_PAGES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MAIN_TITLE = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TITLE = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TITLE_DESCRIPTION = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TITLE_IMAGE_URL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HOME_PAGES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PAYMENTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USER_ID = table.Column<int>(type: "int", nullable: false),
                    ORDER_ID = table.Column<int>(type: "int", nullable: false),
                    PAYMENT_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PAYMENT_METHOD = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    TRANSACTION_ID = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    AMOUNT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    STATUS = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PAYMENTS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCT_TYPES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IMAGE_URL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TITLE = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCT_TYPES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SHIPPING_INFOS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USER_ID = table.Column<int>(type: "int", nullable: false),
                    RECIPIENT_NAME = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ADDRESS_LINE = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CITY = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DISTRICT = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    POSTAL_CODE = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IS_DEFAULT = table.Column<bool>(type: "bit", nullable: false),
                    COUNTRY = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PHONE = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHIPPING_INFOS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "USERS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EMAIL = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    NAME = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IS_NEW = table.Column<bool>(type: "bit", nullable: false),
                    COMPANY_ID = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USERS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_USERS_COMPANIES_COMPANY_ID",
                        column: x => x.COMPANY_ID,
                        principalTable: "COMPANIES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCT_GROUPS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TITLE = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IMAGE_URL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CODE = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PRODUCT_TYPE_ID = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCT_GROUPS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PRODUCT_GROUPS_PRODUCT_TYPES_PRODUCT_TYPE_ID",
                        column: x => x.PRODUCT_TYPE_ID,
                        principalTable: "PRODUCT_TYPES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CARTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USER_ID = table.Column<int>(type: "int", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CARTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CARTS_USERS_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "USERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "COUPONS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    COUPON_CODE = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CAMPAIGN_ID = table.Column<int>(type: "int", nullable: false),
                    USER_ID = table.Column<int>(type: "int", nullable: false),
                    DISCOUNT_RATE = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DISCOUNT_AMOUNT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MAX_USAGE = table.Column<int>(type: "int", nullable: false),
                    USED_COUNT = table.Column<int>(type: "int", nullable: false),
                    START_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    END_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IS_SINGLE_USE = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COUPONS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_COUPONS_CAMPAIGNS_CAMPAIGN_ID",
                        column: x => x.CAMPAIGN_ID,
                        principalTable: "CAMPAIGNS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_COUPONS_USERS_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "USERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ORDERS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    USER_ID = table.Column<int>(type: "int", nullable: false),
                    ORDER_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TOTAL_AMOUNT = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    STATUS = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    PAYMENT_ID = table.Column<int>(type: "int", nullable: false),
                    SHIPPING_INFO_ID = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORDERS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ORDERS_PAYMENTS_PAYMENT_ID",
                        column: x => x.PAYMENT_ID,
                        principalTable: "PAYMENTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ORDERS_SHIPPING_INFOS_SHIPPING_INFO_ID",
                        column: x => x.SHIPPING_INFO_ID,
                        principalTable: "SHIPPING_INFOS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ORDERS_USERS_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "USERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PRODUCT_GROUP_ID = table.Column<int>(type: "int", nullable: false),
                    MODEL = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TITLE = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DETAILS = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    DESCRIPTION = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    OPTIONS = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    PRICE = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CURRENCY = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    STOCK_QUANTITY = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PRODUCTS_PRODUCT_GROUPS_PRODUCT_GROUP_ID",
                        column: x => x.PRODUCT_GROUP_ID,
                        principalTable: "PRODUCT_GROUPS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "USER_COUPONS",
                columns: table => new
                {
                    USER_ID = table.Column<int>(type: "int", nullable: false),
                    COUPON_ID = table.Column<int>(type: "int", nullable: false),
                    IS_USED = table.Column<bool>(type: "bit", nullable: false),
                    USED_AT = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER_COUPONS", x => new { x.USER_ID, x.COUPON_ID });
                    table.ForeignKey(
                        name: "FK_USER_COUPONS_COUPONS_COUPON_ID",
                        column: x => x.COUPON_ID,
                        principalTable: "COUPONS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_USER_COUPONS_USERS_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "USERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SHIPMENTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ORDER_ID = table.Column<int>(type: "int", nullable: false),
                    CARGO_COMPANY = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    TRACKING_CODE = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SHIPPED_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DELIVERED_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    STATUS = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    NOTE = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SHIPMENTS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SHIPMENTS_ORDERS_ORDER_ID",
                        column: x => x.ORDER_ID,
                        principalTable: "ORDERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CART_ITEMS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CART_ID = table.Column<int>(type: "int", nullable: false),
                    PRODUCT_ID = table.Column<int>(type: "int", nullable: false),
                    QUANTITY = table.Column<int>(type: "int", nullable: false),
                    UNIT_PRICE = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CART_ITEMS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CART_ITEMS_CARTS_CART_ID",
                        column: x => x.CART_ID,
                        principalTable: "CARTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CART_ITEMS_PRODUCTS_PRODUCT_ID",
                        column: x => x.PRODUCT_ID,
                        principalTable: "PRODUCTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ORDER_ITEMS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ORDER_ID = table.Column<int>(type: "int", nullable: false),
                    PRODUCT_ID = table.Column<int>(type: "int", nullable: false),
                    QUANTITY = table.Column<int>(type: "int", nullable: false),
                    UNIT_PRICE = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ORDER_ITEMS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ORDER_ITEMS_ORDERS_ORDER_ID",
                        column: x => x.ORDER_ID,
                        principalTable: "ORDERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ORDER_ITEMS_PRODUCTS_PRODUCT_ID",
                        column: x => x.PRODUCT_ID,
                        principalTable: "PRODUCTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCT_IMAGES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PRODUCT_ID = table.Column<int>(type: "int", nullable: false),
                    IMAGE_URL = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCT_IMAGES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PRODUCT_IMAGES_PRODUCTS_PRODUCT_ID",
                        column: x => x.PRODUCT_ID,
                        principalTable: "PRODUCTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCT_REVIEWS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PRODUCT_ID = table.Column<int>(type: "int", nullable: false),
                    USER_ID = table.Column<int>(type: "int", nullable: false),
                    RATING = table.Column<int>(type: "int", nullable: false),
                    COMMENT = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCT_REVIEWS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_PRODUCT_REVIEWS_PRODUCTS_PRODUCT_ID",
                        column: x => x.PRODUCT_ID,
                        principalTable: "PRODUCTS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PRODUCT_REVIEWS_USERS_USER_ID",
                        column: x => x.USER_ID,
                        principalTable: "USERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CAMPAIGNS_IsDeleted",
                table: "CAMPAIGNS",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_CART_ITEMS_CART_ID",
                table: "CART_ITEMS",
                column: "CART_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CART_ITEMS_IsDeleted",
                table: "CART_ITEMS",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_CART_ITEMS_PRODUCT_ID",
                table: "CART_ITEMS",
                column: "PRODUCT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CARTS_IsDeleted",
                table: "CARTS",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_CARTS_USER_ID",
                table: "CARTS",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_COMPANIES_IsDeleted",
                table: "COMPANIES",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_COMPANY_INFOS_IsDeleted",
                table: "COMPANY_INFOS",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_COUPONS_CAMPAIGN_ID",
                table: "COUPONS",
                column: "CAMPAIGN_ID");

            migrationBuilder.CreateIndex(
                name: "IX_COUPONS_IsDeleted",
                table: "COUPONS",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_COUPONS_USER_ID",
                table: "COUPONS",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_HOME_PAGES_IsDeleted",
                table: "HOME_PAGES",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_ORDER_ITEMS_IsDeleted",
                table: "ORDER_ITEMS",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_ORDER_ITEMS_ORDER_ID",
                table: "ORDER_ITEMS",
                column: "ORDER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ORDER_ITEMS_PRODUCT_ID",
                table: "ORDER_ITEMS",
                column: "PRODUCT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ORDERS_IsDeleted",
                table: "ORDERS",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_ORDERS_PAYMENT_ID",
                table: "ORDERS",
                column: "PAYMENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ORDERS_SHIPPING_INFO_ID",
                table: "ORDERS",
                column: "SHIPPING_INFO_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ORDERS_USER_ID",
                table: "ORDERS",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PAYMENTS_IsDeleted",
                table: "PAYMENTS",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_GROUPS_IsDeleted",
                table: "PRODUCT_GROUPS",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_GROUPS_PRODUCT_TYPE_ID",
                table: "PRODUCT_GROUPS",
                column: "PRODUCT_TYPE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_IMAGES_IsDeleted",
                table: "PRODUCT_IMAGES",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_IMAGES_PRODUCT_ID",
                table: "PRODUCT_IMAGES",
                column: "PRODUCT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_REVIEWS_IsDeleted",
                table: "PRODUCT_REVIEWS",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_REVIEWS_PRODUCT_ID",
                table: "PRODUCT_REVIEWS",
                column: "PRODUCT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_REVIEWS_USER_ID",
                table: "PRODUCT_REVIEWS",
                column: "USER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCT_TYPES_IsDeleted",
                table: "PRODUCT_TYPES",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCTS_IsDeleted",
                table: "PRODUCTS",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCTS_PRODUCT_GROUP_ID",
                table: "PRODUCTS",
                column: "PRODUCT_GROUP_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SHIPMENTS_IsDeleted",
                table: "SHIPMENTS",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_SHIPMENTS_ORDER_ID",
                table: "SHIPMENTS",
                column: "ORDER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SHIPPING_INFOS_IsDeleted",
                table: "SHIPPING_INFOS",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_USER_COUPONS_COUPON_ID",
                table: "USER_COUPONS",
                column: "COUPON_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USERS_COMPANY_ID",
                table: "USERS",
                column: "COMPANY_ID");

            migrationBuilder.CreateIndex(
                name: "IX_USERS_IsDeleted",
                table: "USERS",
                column: "IsDeleted",
                filter: "IsDeleted = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AUDIT_LOGS");

            migrationBuilder.DropTable(
                name: "CART_ITEMS");

            migrationBuilder.DropTable(
                name: "COMPANY_INFOS");

            migrationBuilder.DropTable(
                name: "HOME_PAGES");

            migrationBuilder.DropTable(
                name: "ORDER_ITEMS");

            migrationBuilder.DropTable(
                name: "PRODUCT_IMAGES");

            migrationBuilder.DropTable(
                name: "PRODUCT_REVIEWS");

            migrationBuilder.DropTable(
                name: "SHIPMENTS");

            migrationBuilder.DropTable(
                name: "USER_COUPONS");

            migrationBuilder.DropTable(
                name: "CARTS");

            migrationBuilder.DropTable(
                name: "PRODUCTS");

            migrationBuilder.DropTable(
                name: "ORDERS");

            migrationBuilder.DropTable(
                name: "COUPONS");

            migrationBuilder.DropTable(
                name: "PRODUCT_GROUPS");

            migrationBuilder.DropTable(
                name: "PAYMENTS");

            migrationBuilder.DropTable(
                name: "SHIPPING_INFOS");

            migrationBuilder.DropTable(
                name: "CAMPAIGNS");

            migrationBuilder.DropTable(
                name: "USERS");

            migrationBuilder.DropTable(
                name: "PRODUCT_TYPES");

            migrationBuilder.DropTable(
                name: "COMPANIES");
        }
    }
}
