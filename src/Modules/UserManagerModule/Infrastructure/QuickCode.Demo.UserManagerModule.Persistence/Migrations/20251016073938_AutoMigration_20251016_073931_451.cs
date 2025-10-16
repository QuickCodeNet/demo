using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuickCode.Demo.UserManagerModule.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AutoMigration_20251016_073931_451 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApiMethodDefinitions",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    HttpMethod = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    ControllerName = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    UrlPath = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiMethodDefinitions", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ColumnTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IosComponentName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IosType = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IconCode = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ColumnTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PermissionGroups",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionGroups", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "PortalMenus",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Text = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    Tooltip = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ActionName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    OrderNo = table.Column<int>(type: "int", nullable: false),
                    ParentName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortalMenus", x => x.Key);
                });

            migrationBuilder.CreateTable(
                name: "PortalPermissions",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortalPermissions", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "PortalPermissionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortalPermissionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TableComboboxSettings",
                columns: table => new
                {
                    TableName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    IdColumn = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    TextColumns = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    StringFormat = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TableComboboxSettings", x => x.TableName);
                });

            migrationBuilder.CreateTable(
                name: "KafkaEvents",
                columns: table => new
                {
                    TopicName = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ApiMethodDefinitionKey = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KafkaEvents", x => x.TopicName);
                    table.ForeignKey(
                        name: "FK_KafkaEvents_ApiMethodDefinitions_ApiMethodDefinitionKey",
                        column: x => x.ApiMethodDefinitionKey,
                        principalTable: "ApiMethodDefinitions",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ApiPermissionGroups",
                columns: table => new
                {
                    PermissionGroupName = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    ApiMethodDefinitionKey = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiPermissionGroups", x => new { x.PermissionGroupName, x.ApiMethodDefinitionKey });
                    table.ForeignKey(
                        name: "FK_ApiPermissionGroups_ApiMethodDefinitions_ApiMethodDefinitionKey",
                        column: x => x.ApiMethodDefinitionKey,
                        principalTable: "ApiMethodDefinitions",
                        principalColumn: "Key",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApiPermissionGroups_PermissionGroups_PermissionGroupName",
                        column: x => x.PermissionGroupName,
                        principalTable: "PermissionGroups",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PermissionGroupName = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_PermissionGroups_PermissionGroupName",
                        column: x => x.PermissionGroupName,
                        principalTable: "PermissionGroups",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PortalPermissionGroups",
                columns: table => new
                {
                    PortalPermissionName = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    PermissionGroupName = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    PortalPermissionTypeId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortalPermissionGroups", x => new { x.PortalPermissionName, x.PermissionGroupName, x.PortalPermissionTypeId });
                    table.ForeignKey(
                        name: "FK_PortalPermissionGroups_PermissionGroups_PermissionGroupName",
                        column: x => x.PermissionGroupName,
                        principalTable: "PermissionGroups",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PortalPermissionGroups_PortalPermissionTypes_PortalPermissionTypeId",
                        column: x => x.PortalPermissionTypeId,
                        principalTable: "PortalPermissionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PortalPermissionGroups_PortalPermissions_PortalPermissionName",
                        column: x => x.PortalPermissionName,
                        principalTable: "PortalPermissions",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TopicWorkflows",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KafkaEventsTopicName = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    WorkflowContent = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicWorkflows", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopicWorkflows_KafkaEvents_KafkaEventsTopicName",
                        column: x => x.KafkaEventsTopicName,
                        principalTable: "KafkaEvents",
                        principalColumn: "TopicName",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Value = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Token = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    IsRevoked = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeletedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApiPermissionGroups_ApiMethodDefinitionKey",
                table: "ApiPermissionGroups",
                column: "ApiMethodDefinitionKey");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PermissionGroupName",
                table: "AspNetUsers",
                column: "PermissionGroupName");

            migrationBuilder.CreateIndex(
                name: "IX_ColumnTypes_IsDeleted",
                table: "ColumnTypes",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_KafkaEvents_ApiMethodDefinitionKey",
                table: "KafkaEvents",
                column: "ApiMethodDefinitionKey");

            migrationBuilder.CreateIndex(
                name: "IX_PortalPermissionGroups_PermissionGroupName",
                table: "PortalPermissionGroups",
                column: "PermissionGroupName");

            migrationBuilder.CreateIndex(
                name: "IX_PortalPermissionGroups_PortalPermissionTypeId",
                table: "PortalPermissionGroups",
                column: "PortalPermissionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_IsDeleted",
                table: "RefreshTokens",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_UserId",
                table: "RefreshTokens",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TopicWorkflows_IsDeleted",
                table: "TopicWorkflows",
                column: "IsDeleted",
                filter: "IsDeleted = 0");

            migrationBuilder.CreateIndex(
                name: "IX_TopicWorkflows_KafkaEventsTopicName",
                table: "TopicWorkflows",
                column: "KafkaEventsTopicName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiPermissionGroups");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "ColumnTypes");

            migrationBuilder.DropTable(
                name: "PortalMenus");

            migrationBuilder.DropTable(
                name: "PortalPermissionGroups");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "TableComboboxSettings");

            migrationBuilder.DropTable(
                name: "TopicWorkflows");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "PortalPermissionTypes");

            migrationBuilder.DropTable(
                name: "PortalPermissions");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "KafkaEvents");

            migrationBuilder.DropTable(
                name: "PermissionGroups");

            migrationBuilder.DropTable(
                name: "ApiMethodDefinitions");
        }
    }
}
