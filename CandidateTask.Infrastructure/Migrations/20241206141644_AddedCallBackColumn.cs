using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CandidateTask.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedCallBackColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CallbackWindow",
                table: "Candidates",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CallbackWindow",
                table: "Candidates");
        }
    }
}
