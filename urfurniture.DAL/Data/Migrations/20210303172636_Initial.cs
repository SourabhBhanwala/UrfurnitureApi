using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace urfurniture.DAL.Data.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TblDeliverableLocations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pincode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedTime = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblDeliverableLocations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblDeliveryCharges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedTime = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblDeliveryCharges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TblOptionGroups",
                columns: table => new
                {
                    OptionGroupId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionGroupName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedTime = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblOptionGroups", x => x.OptionGroupId);
                });

            migrationBuilder.CreateTable(
                name: "TblOrderStatusCodes",
                columns: table => new
                {
                    StatusCodeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderStatusDesc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UpdateTime = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblOrderStatusCodes", x => x.StatusCodeId);
                });

            migrationBuilder.CreateTable(
                name: "TblOtpConfirmations",
                columns: table => new
                {
                    OtpId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    Otp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UpdateTime = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblOtpConfirmations", x => x.OtpId);
                });

            migrationBuilder.CreateTable(
                name: "TblProductCategories",
                columns: table => new
                {
                    ProdctCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedTime = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblProductCategories", x => x.ProdctCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "TblRoles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    UpdatedTime = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblRoles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "TblUserPaymentMethodsCodes",
                columns: table => new
                {
                    PaymentMethodCode = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentMethodCodeDesc = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedTime = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblUserPaymentMethodsCodes", x => x.PaymentMethodCode);
                });

            migrationBuilder.CreateTable(
                name: "TblOptions",
                columns: table => new
                {
                    OptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OptionGroupRefId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedTime = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblOptions", x => x.OptionId);
                    table.ForeignKey(
                        name: "FK_TblOptions_TblOptionGroups_OptionGroupRefId",
                        column: x => x.OptionGroupRefId,
                        principalTable: "TblOptionGroups",
                        principalColumn: "OptionGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblCategoryImages",
                columns: table => new
                {
                    ImageId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedTime = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    CategoryRefId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblCategoryImages", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_TblCategoryImages_TblProductCategories_CategoryRefId",
                        column: x => x.CategoryRefId,
                        principalTable: "TblProductCategories",
                        principalColumn: "ProdctCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblSubCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ProductCategoryRefId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblSubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblSubCategories_TblProductCategories_ProductCategoryRefId",
                        column: x => x.ProductCategoryRefId,
                        principalTable: "TblProductCategories",
                        principalColumn: "ProdctCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblUsers",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedTime = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblUsers", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_TblUsers_TblRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "TblRoles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblProducts",
                columns: table => new
                {
                    ProductId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AddedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UpdateTime = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ProdctSubCategoryRefId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblProducts", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_TblProducts_TblSubCategories_ProdctSubCategoryRefId",
                        column: x => x.ProdctSubCategoryRefId,
                        principalTable: "TblSubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblOrders",
                columns: table => new
                {
                    OrderId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserRefId = table.Column<long>(type: "bigint", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Chargeid = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblOrders", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_TblOrders_TblUsers_UserRefId",
                        column: x => x.UserRefId,
                        principalTable: "TblUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblUserAddresses",
                columns: table => new
                {
                    AddressId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pincode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Landmark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedTime = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    UserRefId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblUserAddresses", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_TblUserAddresses_TblUsers_UserRefId",
                        column: x => x.UserRefId,
                        principalTable: "TblUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblUserPaymentMethods",
                columns: table => new
                {
                    UserPaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CardNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CardHolderName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ExpireMonth = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ExpireYear = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CvvNo = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedTime = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    UserRefId = table.Column<long>(type: "bigint", nullable: false),
                    UserPaymentMethodRefCode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblUserPaymentMethods", x => x.UserPaymentId);
                    table.ForeignKey(
                        name: "FK_TblUserPaymentMethods_TblUserPaymentMethodsCodes_UserPaymentMethodRefCode",
                        column: x => x.UserPaymentMethodRefCode,
                        principalTable: "TblUserPaymentMethodsCodes",
                        principalColumn: "PaymentMethodCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TblUserPaymentMethods_TblUsers_UserRefId",
                        column: x => x.UserRefId,
                        principalTable: "TblUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblUserShoppingCarts",
                columns: table => new
                {
                    ShoppingCartID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedTime = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    UserRefId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblUserShoppingCarts", x => x.ShoppingCartID);
                    table.ForeignKey(
                        name: "FK_TblUserShoppingCarts_TblUsers_UserRefId",
                        column: x => x.UserRefId,
                        principalTable: "TblUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblProductImages",
                columns: table => new
                {
                    ImageId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedTime = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ProductRefId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblProductImages", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_TblProductImages_TblProducts_ProductRefId",
                        column: x => x.ProductRefId,
                        principalTable: "TblProducts",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblProductMetadatas",
                columns: table => new
                {
                    ProductMetaId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    ProductRefId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblProductMetadatas", x => x.ProductMetaId);
                    table.ForeignKey(
                        name: "FK_TblProductMetadatas_TblProducts_ProductRefId",
                        column: x => x.ProductRefId,
                        principalTable: "TblProducts",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblProductOptions",
                columns: table => new
                {
                    ProductOptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OptionPriceIncrement = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedTime = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ProductRefId = table.Column<long>(type: "bigint", nullable: false),
                    OptionRefId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblProductOptions", x => x.ProductOptionId);
                    table.ForeignKey(
                        name: "FK_TblProductOptions_TblOptions_OptionRefId",
                        column: x => x.OptionRefId,
                        principalTable: "TblOptions",
                        principalColumn: "OptionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TblProductOptions_TblProducts_ProductRefId",
                        column: x => x.ProductRefId,
                        principalTable: "TblProducts",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblProductReviews",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    RatingValue = table.Column<short>(type: "smallint", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PublishAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedTime = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    UserRefId = table.Column<long>(type: "bigint", nullable: false),
                    ProductRefId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblProductReviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TblProductReviews_TblProducts_ProductRefId",
                        column: x => x.ProductRefId,
                        principalTable: "TblProducts",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TblProductReviews_TblUsers_UserRefId",
                        column: x => x.UserRefId,
                        principalTable: "TblUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblOrderItemDetails",
                columns: table => new
                {
                    OrderItemDetailId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalAmount = table.Column<float>(type: "real", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsCancel = table.Column<bool>(type: "bit", nullable: false),
                    PaymentStatus = table.Column<bool>(type: "bit", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Isactive = table.Column<bool>(type: "bit", nullable: false),
                    UpdateTime = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    OrderRefId = table.Column<long>(type: "bigint", nullable: false),
                    OrderStatusRefId = table.Column<int>(type: "int", nullable: false),
                    ProductRefId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblOrderItemDetails", x => x.OrderItemDetailId);
                    table.ForeignKey(
                        name: "FK_TblOrderItemDetails_TblOrders_OrderRefId",
                        column: x => x.OrderRefId,
                        principalTable: "TblOrders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TblOrderItemDetails_TblOrderStatusCodes_OrderStatusRefId",
                        column: x => x.OrderStatusRefId,
                        principalTable: "TblOrderStatusCodes",
                        principalColumn: "StatusCodeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TblOrderItemDetails_TblProducts_ProductRefId",
                        column: x => x.ProductRefId,
                        principalTable: "TblProducts",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblCartItems",
                columns: table => new
                {
                    CartId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SaveForLater = table.Column<bool>(type: "bit", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    TimeAdded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UpdateTime = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ProductRefId = table.Column<long>(type: "bigint", nullable: false),
                    UserShoppingCartRefId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblCartItems", x => x.CartId);
                    table.ForeignKey(
                        name: "FK_TblCartItems_TblProducts_ProductRefId",
                        column: x => x.ProductRefId,
                        principalTable: "TblProducts",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TblCartItems_TblUserShoppingCarts_UserShoppingCartRefId",
                        column: x => x.UserShoppingCartRefId,
                        principalTable: "TblUserShoppingCarts",
                        principalColumn: "ShoppingCartID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TblProductReviewImages",
                columns: table => new
                {
                    ImageId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    UpdatedTime = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    ReviewRefId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TblProductReviewImages", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_TblProductReviewImages_TblProductReviews_ReviewRefId",
                        column: x => x.ReviewRefId,
                        principalTable: "TblProductReviews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TblCartItems_ProductRefId",
                table: "TblCartItems",
                column: "ProductRefId");

            migrationBuilder.CreateIndex(
                name: "IX_TblCartItems_UserShoppingCartRefId",
                table: "TblCartItems",
                column: "UserShoppingCartRefId");

            migrationBuilder.CreateIndex(
                name: "IX_TblCategoryImages_CategoryRefId",
                table: "TblCategoryImages",
                column: "CategoryRefId");

            migrationBuilder.CreateIndex(
                name: "IX_TblOptions_OptionGroupRefId",
                table: "TblOptions",
                column: "OptionGroupRefId");

            migrationBuilder.CreateIndex(
                name: "IX_TblOrderItemDetails_OrderRefId",
                table: "TblOrderItemDetails",
                column: "OrderRefId");

            migrationBuilder.CreateIndex(
                name: "IX_TblOrderItemDetails_OrderStatusRefId",
                table: "TblOrderItemDetails",
                column: "OrderStatusRefId");

            migrationBuilder.CreateIndex(
                name: "IX_TblOrderItemDetails_ProductRefId",
                table: "TblOrderItemDetails",
                column: "ProductRefId");

            migrationBuilder.CreateIndex(
                name: "IX_TblOrders_UserRefId",
                table: "TblOrders",
                column: "UserRefId");

            migrationBuilder.CreateIndex(
                name: "IX_TblProductImages_ProductRefId",
                table: "TblProductImages",
                column: "ProductRefId");

            migrationBuilder.CreateIndex(
                name: "IX_TblProductMetadatas_ProductRefId",
                table: "TblProductMetadatas",
                column: "ProductRefId");

            migrationBuilder.CreateIndex(
                name: "IX_TblProductOptions_OptionRefId",
                table: "TblProductOptions",
                column: "OptionRefId");

            migrationBuilder.CreateIndex(
                name: "IX_TblProductOptions_ProductRefId",
                table: "TblProductOptions",
                column: "ProductRefId");

            migrationBuilder.CreateIndex(
                name: "IX_TblProductReviewImages_ReviewRefId",
                table: "TblProductReviewImages",
                column: "ReviewRefId");

            migrationBuilder.CreateIndex(
                name: "IX_TblProductReviews_ProductRefId",
                table: "TblProductReviews",
                column: "ProductRefId");

            migrationBuilder.CreateIndex(
                name: "IX_TblProductReviews_UserRefId",
                table: "TblProductReviews",
                column: "UserRefId");

            migrationBuilder.CreateIndex(
                name: "IX_TblProducts_ProdctSubCategoryRefId",
                table: "TblProducts",
                column: "ProdctSubCategoryRefId");

            migrationBuilder.CreateIndex(
                name: "IX_TblSubCategories_ProductCategoryRefId",
                table: "TblSubCategories",
                column: "ProductCategoryRefId");

            migrationBuilder.CreateIndex(
                name: "IX_TblUserAddresses_UserRefId",
                table: "TblUserAddresses",
                column: "UserRefId");

            migrationBuilder.CreateIndex(
                name: "IX_TblUserPaymentMethods_UserPaymentMethodRefCode",
                table: "TblUserPaymentMethods",
                column: "UserPaymentMethodRefCode");

            migrationBuilder.CreateIndex(
                name: "IX_TblUserPaymentMethods_UserRefId",
                table: "TblUserPaymentMethods",
                column: "UserRefId");

            migrationBuilder.CreateIndex(
                name: "IX_TblUsers_RoleId",
                table: "TblUsers",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TblUserShoppingCarts_UserRefId",
                table: "TblUserShoppingCarts",
                column: "UserRefId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TblCartItems");

            migrationBuilder.DropTable(
                name: "TblCategoryImages");

            migrationBuilder.DropTable(
                name: "TblDeliverableLocations");

            migrationBuilder.DropTable(
                name: "TblDeliveryCharges");

            migrationBuilder.DropTable(
                name: "TblOrderItemDetails");

            migrationBuilder.DropTable(
                name: "TblOtpConfirmations");

            migrationBuilder.DropTable(
                name: "TblProductImages");

            migrationBuilder.DropTable(
                name: "TblProductMetadatas");

            migrationBuilder.DropTable(
                name: "TblProductOptions");

            migrationBuilder.DropTable(
                name: "TblProductReviewImages");

            migrationBuilder.DropTable(
                name: "TblUserAddresses");

            migrationBuilder.DropTable(
                name: "TblUserPaymentMethods");

            migrationBuilder.DropTable(
                name: "TblUserShoppingCarts");

            migrationBuilder.DropTable(
                name: "TblOrders");

            migrationBuilder.DropTable(
                name: "TblOrderStatusCodes");

            migrationBuilder.DropTable(
                name: "TblOptions");

            migrationBuilder.DropTable(
                name: "TblProductReviews");

            migrationBuilder.DropTable(
                name: "TblUserPaymentMethodsCodes");

            migrationBuilder.DropTable(
                name: "TblOptionGroups");

            migrationBuilder.DropTable(
                name: "TblProducts");

            migrationBuilder.DropTable(
                name: "TblUsers");

            migrationBuilder.DropTable(
                name: "TblSubCategories");

            migrationBuilder.DropTable(
                name: "TblRoles");

            migrationBuilder.DropTable(
                name: "TblProductCategories");
        }
    }
}
