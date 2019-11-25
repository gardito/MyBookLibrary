using Microsoft.EntityFrameworkCore.Migrations;

namespace MyBookLibrary.Data.Migrations
{
    public partial class SeedBooksAndAuthorsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .Sql("INSERT INTO Authors (Name) Values ('Uncle Bob')");

            migrationBuilder
                .Sql("INSERT INTO Authors (Name) Values ('Eric Evans')");

            migrationBuilder
                .Sql("INSERT INTO Authors (Name) Values ('Scott Wlaschin')");

            migrationBuilder
                .Sql("INSERT INTO Books (Title, AuthorId) Values ('Clean Code', (SELECT Id FROM Authors WHERE Name = 'Uncle Bob'))");
            
            migrationBuilder
                .Sql("INSERT INTO Books (Title, AuthorId) Values ('Clean Architecture', (SELECT Id FROM Authors WHERE Name = 'Uncle Bob'))");

            migrationBuilder
                .Sql("INSERT INTO Books (Title, AuthorId) Values ('Domain Driven Design', (SELECT Id FROM Authors WHERE Name = 'Eric Evans'))");

            migrationBuilder
                .Sql("INSERT INTO Books (Title, AuthorId) Values ('Domain Modeling Made Functional', (SELECT Id FROM Authors WHERE Name = 'Scott Wlaschin'))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder
                .Sql("DELETE FROM Books");
            
            migrationBuilder
                .Sql("DELETE FROM Authors");
        }
    }
}
