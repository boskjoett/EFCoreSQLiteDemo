using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace SqLiteDemo
{
    public class BloggingContext : DbContext
    {
        private readonly string _connectionString;

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public BloggingContext()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                      .SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.json", true, false)
                      .AddEnvironmentVariables()
                      .Build();

            _connectionString = configuration.GetConnectionString("SQLite");

            Console.WriteLine("SQLite connection string: " + _connectionString);
        }

        public BloggingContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(_connectionString);
        }
    }
}
