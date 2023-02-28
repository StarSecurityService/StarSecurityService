using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarSecurityService.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Feeds",
                columns: table => new
                {
                    feedID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    content = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    tag = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    postTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Feeds__A0A7D53F74E77D8E", x => x.feedID);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    roleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    roleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Role__CD98460AD0382E40", x => x.roleID);
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    serviceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    serviceName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    price = table.Column<double>(type: "float", nullable: true),
                    image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Service__4550733FF324B268", x => x.serviceID);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    commentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    feedID = table.Column<int>(type: "int", nullable: true),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    firstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    lastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    content = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    postTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Comment__CDDE91BDF50C4423", x => x.commentID);
                    table.ForeignKey(
                        name: "FK__Comment__feedID__38996AB5",
                        column: x => x.feedID,
                        principalTable: "Feeds",
                        principalColumn: "feedID");
                });

            migrationBuilder.CreateTable(
                name: "FeedImg",
                columns: table => new
                {
                    feedimgID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    image = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    feedID = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__FeedImg__9426699EC538D1E5", x => x.feedimgID);
                    table.ForeignKey(
                        name: "FK__FeedImg__feedID__36B12243",
                        column: x => x.feedID,
                        principalTable: "Feeds",
                        principalColumn: "feedID");
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    accountID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    roleID = table.Column<int>(type: "int", nullable: true),
                    password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    phone = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    firstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    lastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    cardID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Account__F267253E2F19DD28", x => x.accountID);
                    table.ForeignKey(
                        name: "FK__Account__roleID__35BCFE0A",
                        column: x => x.roleID,
                        principalTable: "Role",
                        principalColumn: "roleID");
                });

            migrationBuilder.CreateTable(
                name: "Feedback",
                columns: table => new
                {
                    feedbackID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    serviceID = table.Column<int>(type: "int", nullable: true),
                    email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    firstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    lastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    content = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    postTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Feedback__2613FDC42606923C", x => x.feedbackID);
                    table.ForeignKey(
                        name: "FK__Feedback__servic__37A5467C",
                        column: x => x.serviceID,
                        principalTable: "Service",
                        principalColumn: "serviceID");
                });

            migrationBuilder.CreateTable(
                name: "Guard",
                columns: table => new
                {
                    guardID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    lastName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    serviceID = table.Column<int>(type: "int", nullable: true),
                    phone = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    height = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    weight = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    status = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    avatar = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    cardID = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Guard__17D718315613E8CF", x => x.guardID);
                    table.ForeignKey(
                        name: "FK__Guard__serviceID__398D8EEE",
                        column: x => x.serviceID,
                        principalTable: "Service",
                        principalColumn: "serviceID");
                });

            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    orderID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    serviceID = table.Column<int>(type: "int", nullable: true),
                    accountID = table.Column<int>(type: "int", nullable: true),
                    amount = table.Column<int>(type: "int", nullable: true),
                    duration = table.Column<int>(type: "int", nullable: true),
                    time = table.Column<DateTime>(type: "datetime", nullable: true),
                    total = table.Column<double>(type: "float", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Order__0809337D2375614C", x => x.orderID);
                    table.ForeignKey(
                        name: "FK__Order__accountID__3B75D760",
                        column: x => x.accountID,
                        principalTable: "Account",
                        principalColumn: "accountID");
                    table.ForeignKey(
                        name: "FK__Order__serviceID__3A81B327",
                        column: x => x.serviceID,
                        principalTable: "Service",
                        principalColumn: "serviceID");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    orderdetailsID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    guardID = table.Column<int>(type: "int", nullable: true),
                    orderID = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__OrderDet__6497933891AC32F4", x => x.orderdetailsID);
                    table.ForeignKey(
                        name: "FK__OrderDeta__guard__3C69FB99",
                        column: x => x.guardID,
                        principalTable: "Guard",
                        principalColumn: "guardID");
                    table.ForeignKey(
                        name: "FK__OrderDeta__order__3D5E1FD2",
                        column: x => x.orderID,
                        principalTable: "Order",
                        principalColumn: "orderID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_roleID",
                table: "Account",
                column: "roleID");

            migrationBuilder.CreateIndex(
                name: "IX_Comment_feedID",
                table: "Comment",
                column: "feedID");

            migrationBuilder.CreateIndex(
                name: "IX_Feedback_serviceID",
                table: "Feedback",
                column: "serviceID");

            migrationBuilder.CreateIndex(
                name: "IX_FeedImg_feedID",
                table: "FeedImg",
                column: "feedID");

            migrationBuilder.CreateIndex(
                name: "IX_Guard_serviceID",
                table: "Guard",
                column: "serviceID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_accountID",
                table: "Order",
                column: "accountID");

            migrationBuilder.CreateIndex(
                name: "IX_Order_serviceID",
                table: "Order",
                column: "serviceID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_guardID",
                table: "OrderDetails",
                column: "guardID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_orderID",
                table: "OrderDetails",
                column: "orderID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "Feedback");

            migrationBuilder.DropTable(
                name: "FeedImg");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "Feeds");

            migrationBuilder.DropTable(
                name: "Guard");

            migrationBuilder.DropTable(
                name: "Order");

            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "Role");
        }
    }
}
