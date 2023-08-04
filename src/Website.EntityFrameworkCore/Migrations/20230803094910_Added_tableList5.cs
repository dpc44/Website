using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Website.Migrations
{
    /// <inheritdoc />
    public partial class AddedtableList5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Breakdown",
                table: "TableList");

            migrationBuilder.DropColumn(
                name: "BreakdownCategory",
                table: "TableList");

            migrationBuilder.DropColumn(
                name: "Footnotes",
                table: "TableList");

            migrationBuilder.DropColumn(
                name: "RdValue",
                table: "TableList");

            migrationBuilder.DropColumn(
                name: "RelativeSamplingError",
                table: "TableList");

            migrationBuilder.RenameColumn(
                name: "Year",
                table: "TableList",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "Variable",
                table: "TableList",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Unit",
                table: "TableList",
                newName: "Identifier");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "TableList",
                newName: "FirstName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Username",
                table: "TableList",
                newName: "Year");

            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "TableList",
                newName: "Variable");

            migrationBuilder.RenameColumn(
                name: "Identifier",
                table: "TableList",
                newName: "Unit");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "TableList",
                newName: "Status");

            migrationBuilder.AddColumn<string>(
                name: "Breakdown",
                table: "TableList",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "BreakdownCategory",
                table: "TableList",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Footnotes",
                table: "TableList",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RdValue",
                table: "TableList",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RelativeSamplingError",
                table: "TableList",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
