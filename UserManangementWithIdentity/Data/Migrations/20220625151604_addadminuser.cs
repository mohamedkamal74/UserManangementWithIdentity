using Microsoft.EntityFrameworkCore.Migrations;

namespace UserManangementWithIdentity.Data.Migrations
{
    public partial class addadminuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [Security].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [ProfilePicture]) VALUES (N'902f6571-8f3e-49ed-9623-da9baf1cab9e', N'admin', N'ADMIN', N'admin@admin.test', N'ADMIN@ADMIN.TEST', 0, N'AQAAAAEAACcQAAAAEKXDDuxEox8dPKA38niWKPbCnBc6r8CTRD9q1fgnfYRpr71GoQw3JMZjYzBGi0WaFw==', N'4GSNNOOYPV4QWR4XOON2FW63WGBJIXOX', N'54a86cf4-bd39-4c9c-92e7-48c0677a00ac', NULL, 0, 0, NULL, 1, 0, N'mohamed', N'kamal attiya', NULL)");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [Security].[Users] WHERE Id = '902f6571-8f3e-49ed-9623-da9baf1cab9e'");

        }
    }
}
