using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebApplication1.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PriceColumnPropTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceColumnPropTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceLists",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PriceColumns",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropTypeId = table.Column<long>(type: "bigint", nullable: false),
                    PropName = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceColumns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceColumns_PriceColumnPropTypes_PropTypeId",
                        column: x => x.PropTypeId,
                        principalTable: "PriceColumnPropTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Articul = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PriceListId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_PriceLists_PriceListId",
                        column: x => x.PriceListId,
                        principalTable: "PriceLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PriceColumnModelPriceListModel",
                columns: table => new
                {
                    ColumnsId = table.Column<long>(type: "bigint", nullable: false),
                    PriceListsId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceColumnModelPriceListModel", x => new { x.ColumnsId, x.PriceListsId });
                    table.ForeignKey(
                        name: "FK_PriceColumnModelPriceListModel_PriceColumns_ColumnsId",
                        column: x => x.ColumnsId,
                        principalTable: "PriceColumns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PriceColumnModelPriceListModel_PriceLists_PriceListsId",
                        column: x => x.PriceListsId,
                        principalTable: "PriceLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductColumnValues",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColumnId = table.Column<long>(type: "bigint", nullable: false),
                    ColumnValue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductModelId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductColumnValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductColumnValues_PriceColumns_ColumnId",
                        column: x => x.ColumnId,
                        principalTable: "PriceColumns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductColumnValues_Products_ProductModelId",
                        column: x => x.ProductModelId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "PriceColumnPropTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "Число" },
                    { 2L, "Строка" },
                    { 3L, "Текст" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriceColumnModelPriceListModel_PriceListsId",
                table: "PriceColumnModelPriceListModel",
                column: "PriceListsId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceColumns_PropName_PropTypeId",
                table: "PriceColumns",
                columns: new[] { "PropName", "PropTypeId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PriceColumns_PropTypeId",
                table: "PriceColumns",
                column: "PropTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductColumnValues_ColumnId",
                table: "ProductColumnValues",
                column: "ColumnId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductColumnValues_ProductModelId",
                table: "ProductColumnValues",
                column: "ProductModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_PriceListId",
                table: "Products",
                column: "PriceListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceColumnModelPriceListModel");

            migrationBuilder.DropTable(
                name: "ProductColumnValues");

            migrationBuilder.DropTable(
                name: "PriceColumns");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "PriceColumnPropTypes");

            migrationBuilder.DropTable(
                name: "PriceLists");
        }
    }
}
