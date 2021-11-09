using Microsoft.EntityFrameworkCore.Migrations;

namespace vehicle_tracking.Migrations
{
    public partial class NewMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_VehicleDevices_DeviceID",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersDb_UserRolesDb_RoleID",
                table: "UsersDb");

            migrationBuilder.DropIndex(
                name: "IX_UsersDb_RoleID",
                table: "UsersDb");

            migrationBuilder.DropIndex(
                name: "IX_Locations_DeviceID",
                table: "Locations");

            migrationBuilder.AddColumn<int>(
                name: "UserRoleRoleID",
                table: "UsersDb",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "VehicleDeviceDeviceID",
                table: "Locations",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UsersDb_UserRoleRoleID",
                table: "UsersDb",
                column: "UserRoleRoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_VehicleDeviceDeviceID",
                table: "Locations",
                column: "VehicleDeviceDeviceID");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_VehicleDevices_VehicleDeviceDeviceID",
                table: "Locations",
                column: "VehicleDeviceDeviceID",
                principalTable: "VehicleDevices",
                principalColumn: "DeviceID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersDb_UserRolesDb_UserRoleRoleID",
                table: "UsersDb",
                column: "UserRoleRoleID",
                principalTable: "UserRolesDb",
                principalColumn: "RoleID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_VehicleDevices_VehicleDeviceDeviceID",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_UsersDb_UserRolesDb_UserRoleRoleID",
                table: "UsersDb");

            migrationBuilder.DropIndex(
                name: "IX_UsersDb_UserRoleRoleID",
                table: "UsersDb");

            migrationBuilder.DropIndex(
                name: "IX_Locations_VehicleDeviceDeviceID",
                table: "Locations");

            migrationBuilder.DropColumn(
                name: "UserRoleRoleID",
                table: "UsersDb");

            migrationBuilder.DropColumn(
                name: "VehicleDeviceDeviceID",
                table: "Locations");

            migrationBuilder.CreateIndex(
                name: "IX_UsersDb_RoleID",
                table: "UsersDb",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Locations_DeviceID",
                table: "Locations",
                column: "DeviceID");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_VehicleDevices_DeviceID",
                table: "Locations",
                column: "DeviceID",
                principalTable: "VehicleDevices",
                principalColumn: "DeviceID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UsersDb_UserRolesDb_RoleID",
                table: "UsersDb",
                column: "RoleID",
                principalTable: "UserRolesDb",
                principalColumn: "RoleID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
