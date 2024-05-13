using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "address_status",
                columns: table => new
                {
                    status_id = table.Column<int>(type: "integer", nullable: false),
                    address_status = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_addr_status", x => x.status_id);
                });

            migrationBuilder.CreateTable(
                name: "author",
                columns: table => new
                {
                    author_id = table.Column<int>(type: "integer", nullable: false),
                    author_name = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_author", x => x.author_id);
                });

            migrationBuilder.CreateTable(
                name: "book_language",
                columns: table => new
                {
                    language_id = table.Column<int>(type: "integer", nullable: false),
                    language_code = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: true),
                    language_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_language", x => x.language_id);
                });

            migrationBuilder.CreateTable(
                name: "country",
                columns: table => new
                {
                    country_id = table.Column<int>(type: "integer", nullable: false),
                    country_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_country", x => x.country_id);
                });

            migrationBuilder.CreateTable(
                name: "customer",
                columns: table => new
                {
                    customer_id = table.Column<int>(type: "integer", nullable: false),
                    first_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    last_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    email = table.Column<string>(type: "character varying(350)", maxLength: 350, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_customer", x => x.customer_id);
                });

            migrationBuilder.CreateTable(
                name: "order_status",
                columns: table => new
                {
                    status_id = table.Column<int>(type: "integer", nullable: false),
                    status_value = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orderstatus", x => x.status_id);
                });

            migrationBuilder.CreateTable(
                name: "publisher",
                columns: table => new
                {
                    publisher_id = table.Column<int>(type: "integer", nullable: false),
                    publisher_name = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_publisher", x => x.publisher_id);
                });

            migrationBuilder.CreateTable(
                name: "shipping_method",
                columns: table => new
                {
                    method_id = table.Column<int>(type: "integer", nullable: false),
                    method_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    cost = table.Column<decimal>(type: "numeric(6,2)", precision: 6, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_shipmethod", x => x.method_id);
                });

            migrationBuilder.CreateTable(
                name: "address",
                columns: table => new
                {
                    address_id = table.Column<int>(type: "integer", nullable: false),
                    street_number = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    street_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    city = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    country_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_address", x => x.address_id);
                    table.ForeignKey(
                        name: "fk_addr_ctry",
                        column: x => x.country_id,
                        principalTable: "country",
                        principalColumn: "country_id");
                });

            migrationBuilder.CreateTable(
                name: "book",
                columns: table => new
                {
                    book_id = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: true),
                    isbn13 = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: true),
                    language_id = table.Column<int>(type: "integer", nullable: true),
                    num_pages = table.Column<int>(type: "integer", nullable: true),
                    publication_date = table.Column<DateOnly>(type: "date", nullable: true),
                    publisher_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_book", x => x.book_id);
                    table.ForeignKey(
                        name: "fk_book_lang",
                        column: x => x.language_id,
                        principalTable: "book_language",
                        principalColumn: "language_id");
                    table.ForeignKey(
                        name: "fk_book_pub",
                        column: x => x.publisher_id,
                        principalTable: "publisher",
                        principalColumn: "publisher_id");
                });

            migrationBuilder.CreateTable(
                name: "cust_order",
                columns: table => new
                {
                    order_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    customer_id = table.Column<int>(type: "integer", nullable: true),
                    shipping_method_id = table.Column<int>(type: "integer", nullable: true),
                    dest_address_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_custorder", x => x.order_id);
                    table.ForeignKey(
                        name: "fk_order_addr",
                        column: x => x.dest_address_id,
                        principalTable: "address",
                        principalColumn: "address_id");
                    table.ForeignKey(
                        name: "fk_order_cust",
                        column: x => x.customer_id,
                        principalTable: "customer",
                        principalColumn: "customer_id");
                    table.ForeignKey(
                        name: "fk_order_ship",
                        column: x => x.shipping_method_id,
                        principalTable: "shipping_method",
                        principalColumn: "method_id");
                });

            migrationBuilder.CreateTable(
                name: "customer_address",
                columns: table => new
                {
                    customer_id = table.Column<int>(type: "integer", nullable: false),
                    address_id = table.Column<int>(type: "integer", nullable: false),
                    status_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_custaddr", x => new { x.customer_id, x.address_id });
                    table.ForeignKey(
                        name: "FK_customer_address_address_status_status_id",
                        column: x => x.status_id,
                        principalTable: "address_status",
                        principalColumn: "status_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_ca_addr",
                        column: x => x.address_id,
                        principalTable: "address",
                        principalColumn: "address_id");
                    table.ForeignKey(
                        name: "fk_ca_cust",
                        column: x => x.customer_id,
                        principalTable: "customer",
                        principalColumn: "customer_id");
                });

            migrationBuilder.CreateTable(
                name: "book_author",
                columns: table => new
                {
                    book_id = table.Column<int>(type: "integer", nullable: false),
                    author_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_book_author", x => new { x.book_id, x.author_id });
                    table.ForeignKey(
                        name: "FK_book_author_author_author_id",
                        column: x => x.author_id,
                        principalTable: "author",
                        principalColumn: "author_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_book_author_book_book_id",
                        column: x => x.book_id,
                        principalTable: "book",
                        principalColumn: "book_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_history",
                columns: table => new
                {
                    history_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_id = table.Column<int>(type: "integer", nullable: true),
                    status_id = table.Column<int>(type: "integer", nullable: true),
                    status_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orderhist", x => x.history_id);
                    table.ForeignKey(
                        name: "fk_oh_order",
                        column: x => x.order_id,
                        principalTable: "cust_order",
                        principalColumn: "order_id");
                    table.ForeignKey(
                        name: "fk_oh_status",
                        column: x => x.status_id,
                        principalTable: "order_status",
                        principalColumn: "status_id");
                });

            migrationBuilder.CreateTable(
                name: "order_line",
                columns: table => new
                {
                    line_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_id = table.Column<int>(type: "integer", nullable: true),
                    book_id = table.Column<int>(type: "integer", nullable: true),
                    price = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orderline", x => x.line_id);
                    table.ForeignKey(
                        name: "fk_ol_book",
                        column: x => x.book_id,
                        principalTable: "book",
                        principalColumn: "book_id");
                    table.ForeignKey(
                        name: "fk_ol_order",
                        column: x => x.order_id,
                        principalTable: "cust_order",
                        principalColumn: "order_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_address_country_id",
                table: "address",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_book_language_id",
                table: "book",
                column: "language_id");

            migrationBuilder.CreateIndex(
                name: "IX_book_publisher_id",
                table: "book",
                column: "publisher_id");

            migrationBuilder.CreateIndex(
                name: "IX_book_author_author_id",
                table: "book_author",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "IX_cust_order_customer_id",
                table: "cust_order",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_cust_order_dest_address_id",
                table: "cust_order",
                column: "dest_address_id");

            migrationBuilder.CreateIndex(
                name: "IX_cust_order_shipping_method_id",
                table: "cust_order",
                column: "shipping_method_id");

            migrationBuilder.CreateIndex(
                name: "IX_customer_address_address_id",
                table: "customer_address",
                column: "address_id");

            migrationBuilder.CreateIndex(
                name: "IX_customer_address_status_id",
                table: "customer_address",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_history_order_id",
                table: "order_history",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_history_status_id",
                table: "order_history",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_line_book_id",
                table: "order_line",
                column: "book_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_line_order_id",
                table: "order_line",
                column: "order_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "book_author");

            migrationBuilder.DropTable(
                name: "customer_address");

            migrationBuilder.DropTable(
                name: "order_history");

            migrationBuilder.DropTable(
                name: "order_line");

            migrationBuilder.DropTable(
                name: "author");

            migrationBuilder.DropTable(
                name: "address_status");

            migrationBuilder.DropTable(
                name: "order_status");

            migrationBuilder.DropTable(
                name: "book");

            migrationBuilder.DropTable(
                name: "cust_order");

            migrationBuilder.DropTable(
                name: "book_language");

            migrationBuilder.DropTable(
                name: "publisher");

            migrationBuilder.DropTable(
                name: "address");

            migrationBuilder.DropTable(
                name: "customer");

            migrationBuilder.DropTable(
                name: "shipping_method");

            migrationBuilder.DropTable(
                name: "country");
        }
    }
}
