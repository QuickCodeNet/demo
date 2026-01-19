using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickCode.Demo.EmailManagerModule.Persistence.Migrations
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
                name: "BLACK_LISTS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RECIPIENT = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    REASON = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    REASON_TYPE = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BLACK_LISTS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "MESSAGE_TEMPLATES",
                columns: table => new
                {
                    NAME = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CONTENT = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TYPE = table.Column<string>(type: "nvarchar(250)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MESSAGE_TEMPLATES", x => x.NAME);
                });

            migrationBuilder.CreateTable(
                name: "SENDERS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    EMAIL_ADDRESS = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    API_URL = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    USERNAME = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PASSWORD = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    PRIORITY = table.Column<int>(type: "int", nullable: false, defaultValueSql: "1"),
                    DAILY_LIMIT = table.Column<int>(type: "int", nullable: false, defaultValueSql: "1000"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SENDERS", x => x.ID);
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
                    IS_ACTIVE = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    PRIORITY = table.Column<int>(type: "int", nullable: false, defaultValueSql: "1"),
                    TEMPLATE_NAME = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CAMPAIGNS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CAMPAIGNS_MESSAGE_TEMPLATES_TEMPLATE_NAME",
                        column: x => x.TEMPLATE_NAME,
                        principalTable: "MESSAGE_TEMPLATES",
                        principalColumn: "NAME",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OTP_MESSAGES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RECIPIENT = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    OTP_CODE = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    HASH_CODE = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TEMPLATE_NAME = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    STATUS = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    EXPIRE_TIME_SECONDS = table.Column<int>(type: "int", nullable: false),
                    ATTEMPT_COUNT = table.Column<int>(type: "int", nullable: false, defaultValueSql: "0"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OTP_MESSAGES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OTP_MESSAGES_MESSAGE_TEMPLATES_TEMPLATE_NAME",
                        column: x => x.TEMPLATE_NAME,
                        principalTable: "MESSAGE_TEMPLATES",
                        principalColumn: "NAME",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MESSAGES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CAMPAIGN_ID = table.Column<int>(type: "int", nullable: false),
                    RECIPIENT = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SUBJECT = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    BODY = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TEMPLATE_NAME = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    STATUS = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    ATTEMPT_COUNT = table.Column<int>(type: "int", nullable: false, defaultValueSql: "0"),
                    LAST_ATTEMPT_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SENT_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MESSAGES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MESSAGES_CAMPAIGNS_CAMPAIGN_ID",
                        column: x => x.CAMPAIGN_ID,
                        principalTable: "CAMPAIGNS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MESSAGES_MESSAGE_TEMPLATES_TEMPLATE_NAME",
                        column: x => x.TEMPLATE_NAME,
                        principalTable: "MESSAGE_TEMPLATES",
                        principalColumn: "NAME",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OTP_MESSAGE_LOGS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SENDER_ID = table.Column<int>(type: "int", nullable: true),
                    OTP_MESSAGE_ID = table.Column<int>(type: "int", nullable: false),
                    RECIPIENT = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    OTP_CODE = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    HASH_CODE = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TEMPLATE_NAME = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    STATUS = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    EXPIRE_TIME_SECONDS = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OTP_MESSAGE_LOGS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OTP_MESSAGE_LOGS_MESSAGE_TEMPLATES_TEMPLATE_NAME",
                        column: x => x.TEMPLATE_NAME,
                        principalTable: "MESSAGE_TEMPLATES",
                        principalColumn: "NAME",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OTP_MESSAGE_LOGS_OTP_MESSAGES_OTP_MESSAGE_ID",
                        column: x => x.OTP_MESSAGE_ID,
                        principalTable: "OTP_MESSAGES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OTP_MESSAGE_LOGS_SENDERS_SENDER_ID",
                        column: x => x.SENDER_ID,
                        principalTable: "SENDERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OTP_MESSAGE_QUEUES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SENDER_ID = table.Column<int>(type: "int", nullable: true),
                    OTP_MESSAGE_ID = table.Column<int>(type: "int", nullable: false),
                    RECIPIENT = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    OTP_CODE = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    HASH_CODE = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PRIORITY = table.Column<int>(type: "int", nullable: false, defaultValueSql: "1"),
                    STATUS = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    PROCESS_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OTP_MESSAGE_QUEUES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_OTP_MESSAGE_QUEUES_OTP_MESSAGES_OTP_MESSAGE_ID",
                        column: x => x.OTP_MESSAGE_ID,
                        principalTable: "OTP_MESSAGES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OTP_MESSAGE_QUEUES_SENDERS_SENDER_ID",
                        column: x => x.SENDER_ID,
                        principalTable: "SENDERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MESSAGE_LOGS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MESSAGE_ID = table.Column<int>(type: "int", nullable: false),
                    CAMPAIGN_ID = table.Column<int>(type: "int", nullable: false),
                    SENDER_ID = table.Column<int>(type: "int", nullable: true),
                    ATTEMPT_DATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RECIPIENT = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SUBJECT = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    BODY = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TEMPLATE_NAME = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    STATUS = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    ATTEMPT_COUNT = table.Column<int>(type: "int", nullable: false, defaultValueSql: "0"),
                    SENT_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MESSAGE_LOGS", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MESSAGE_LOGS_MESSAGES_MESSAGE_ID",
                        column: x => x.MESSAGE_ID,
                        principalTable: "MESSAGES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MESSAGE_LOGS_MESSAGE_TEMPLATES_TEMPLATE_NAME",
                        column: x => x.TEMPLATE_NAME,
                        principalTable: "MESSAGE_TEMPLATES",
                        principalColumn: "NAME",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MESSAGE_LOGS_SENDERS_SENDER_ID",
                        column: x => x.SENDER_ID,
                        principalTable: "SENDERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MESSAGE_QUEUES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MESSAGE_ID = table.Column<int>(type: "int", nullable: false),
                    CAMPAIGN_ID = table.Column<int>(type: "int", nullable: false),
                    SENDER_ID = table.Column<int>(type: "int", nullable: true),
                    RECIPIENT = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    SUBJECT = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    BODY = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PRIORITY = table.Column<int>(type: "int", nullable: false, defaultValueSql: "1"),
                    STATUS = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    PROCESS_DATE = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MESSAGE_QUEUES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MESSAGE_QUEUES_CAMPAIGNS_CAMPAIGN_ID",
                        column: x => x.CAMPAIGN_ID,
                        principalTable: "CAMPAIGNS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MESSAGE_QUEUES_MESSAGES_MESSAGE_ID",
                        column: x => x.MESSAGE_ID,
                        principalTable: "MESSAGES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MESSAGE_QUEUES_SENDERS_SENDER_ID",
                        column: x => x.SENDER_ID,
                        principalTable: "SENDERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BLACK_LISTS_IsDeleted",
                table: "BLACK_LISTS",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_CAMPAIGNS_IsDeleted",
                table: "CAMPAIGNS",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_CAMPAIGNS_TEMPLATE_NAME",
                table: "CAMPAIGNS",
                column: "TEMPLATE_NAME");

            migrationBuilder.CreateIndex(
                name: "IX_MESSAGE_LOGS_IsDeleted",
                table: "MESSAGE_LOGS",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_MESSAGE_LOGS_MESSAGE_ID",
                table: "MESSAGE_LOGS",
                column: "MESSAGE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MESSAGE_LOGS_SENDER_ID",
                table: "MESSAGE_LOGS",
                column: "SENDER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MESSAGE_LOGS_TEMPLATE_NAME",
                table: "MESSAGE_LOGS",
                column: "TEMPLATE_NAME");

            migrationBuilder.CreateIndex(
                name: "IX_MESSAGE_QUEUES_CAMPAIGN_ID",
                table: "MESSAGE_QUEUES",
                column: "CAMPAIGN_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MESSAGE_QUEUES_IsDeleted",
                table: "MESSAGE_QUEUES",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_MESSAGE_QUEUES_MESSAGE_ID",
                table: "MESSAGE_QUEUES",
                column: "MESSAGE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MESSAGE_QUEUES_SENDER_ID",
                table: "MESSAGE_QUEUES",
                column: "SENDER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MESSAGES_CAMPAIGN_ID",
                table: "MESSAGES",
                column: "CAMPAIGN_ID");

            migrationBuilder.CreateIndex(
                name: "IX_MESSAGES_IsDeleted",
                table: "MESSAGES",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_MESSAGES_TEMPLATE_NAME",
                table: "MESSAGES",
                column: "TEMPLATE_NAME");

            migrationBuilder.CreateIndex(
                name: "IX_OTP_MESSAGE_LOGS_IsDeleted",
                table: "OTP_MESSAGE_LOGS",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_OTP_MESSAGE_LOGS_OTP_MESSAGE_ID",
                table: "OTP_MESSAGE_LOGS",
                column: "OTP_MESSAGE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_OTP_MESSAGE_LOGS_SENDER_ID",
                table: "OTP_MESSAGE_LOGS",
                column: "SENDER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_OTP_MESSAGE_LOGS_TEMPLATE_NAME",
                table: "OTP_MESSAGE_LOGS",
                column: "TEMPLATE_NAME");

            migrationBuilder.CreateIndex(
                name: "IX_OTP_MESSAGE_QUEUES_IsDeleted",
                table: "OTP_MESSAGE_QUEUES",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_OTP_MESSAGE_QUEUES_OTP_MESSAGE_ID",
                table: "OTP_MESSAGE_QUEUES",
                column: "OTP_MESSAGE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_OTP_MESSAGE_QUEUES_SENDER_ID",
                table: "OTP_MESSAGE_QUEUES",
                column: "SENDER_ID");

            migrationBuilder.CreateIndex(
                name: "IX_OTP_MESSAGES_IsDeleted",
                table: "OTP_MESSAGES",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_OTP_MESSAGES_TEMPLATE_NAME",
                table: "OTP_MESSAGES",
                column: "TEMPLATE_NAME");

            migrationBuilder.CreateIndex(
                name: "IX_SENDERS_IsDeleted",
                table: "SENDERS",
                column: "IsDeleted",
                filter: "IsDeleted = 0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AUDIT_LOGS");

            migrationBuilder.DropTable(
                name: "BLACK_LISTS");

            migrationBuilder.DropTable(
                name: "MESSAGE_LOGS");

            migrationBuilder.DropTable(
                name: "MESSAGE_QUEUES");

            migrationBuilder.DropTable(
                name: "OTP_MESSAGE_LOGS");

            migrationBuilder.DropTable(
                name: "OTP_MESSAGE_QUEUES");

            migrationBuilder.DropTable(
                name: "MESSAGES");

            migrationBuilder.DropTable(
                name: "OTP_MESSAGES");

            migrationBuilder.DropTable(
                name: "SENDERS");

            migrationBuilder.DropTable(
                name: "CAMPAIGNS");

            migrationBuilder.DropTable(
                name: "MESSAGE_TEMPLATES");
        }
    }
}
