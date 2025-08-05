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

            // Fix: Remove connection string logging to prevent exposure of sensitive data
            // Console.WriteLine("SQLite connection string: " + _connectionString);
            
            // Fix: Add validation for connection string
            if (string.IsNullOrEmpty(_connectionString))
            {
                throw new InvalidOperationException("SQLite connection string not found in configuration.");
            }
        }

        public BloggingContext(string connectionString)
        {
            // Fix: Add validation for connection string parameter
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("Connection string cannot be null or empty.", nameof(connectionString));
            }
            
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite(_connectionString);
        }
    }
}
