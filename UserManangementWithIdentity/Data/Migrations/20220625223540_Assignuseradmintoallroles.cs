using Microsoft.EntityFrameworkCore.Migrations;

namespace UserManangementWithIdentity.Data.Migrations
{
    public partial class Assignuseradmintoallroles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [Security].[UserRoles] (UserId,RoleId) SELECT '902f6571-8f3e-49ed-9623-da9baf1cab9e', Id FROM [Security].[Roles]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [Security].[UserRoles] WHERE UserId='902f6571-8f3e-49ed-9623-da9baf1cab9e'");
        }
    }
}
