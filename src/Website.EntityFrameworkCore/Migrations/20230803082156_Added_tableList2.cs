using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Website.Migrations
{
    /// <inheritdoc />
    public partial class AddedtableList2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "TableList",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "ConcurrencyStamp",
                table: "TableList",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "TableList",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "TableList",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "TableList",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "TableList",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExtraProperties",
                table: "TableList",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TableList",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "TableList",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LastModifierId",
                table: "TableList",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TableList",
                table: "TableList",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TableList",
                table: "TableList");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TableList");

            migrationBuilder.DropColumn(
                name: "ConcurrencyStamp",
                table: "TableList");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "TableList");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "TableList");

            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "TableList");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "TableList");

            migrationBuilder.DropColumn(
                name: "ExtraProperties",
                table: "TableList");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TableList");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "TableList");

            migrationBuilder.DropColumn(
                name: "LastModifierId",
                table: "TableList");
        }
    }
}
