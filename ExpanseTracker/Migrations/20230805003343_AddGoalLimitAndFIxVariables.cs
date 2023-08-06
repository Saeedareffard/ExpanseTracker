using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpanseTracker.Migrations
{
    /// <inheritdoc />
    public partial class AddGoalLimitAndFIxVariables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "Goals",
                newName: "GoalAmount");

            migrationBuilder.AddColumn<double>(
                name: "CurrentAmount",
                table: "Goals",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentAmount",
                table: "Goals");

            migrationBuilder.RenameColumn(
                name: "GoalAmount",
                table: "Goals",
                newName: "Amount");
        }
    }
}
