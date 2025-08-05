using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

// Based on this article: https://docs.microsoft.com/en-us/ef/core/get-started/?tabs=netcore-cli

namespace SqLiteDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SQLite Demo");

            using (var db = new BloggingContext())
            {
                // Show existing blogs
                var blogs = db.Blogs
                    .OrderBy(b => b.BlogId)
                    .Include(blog => blog.Posts)
                    .ToList(); // Fix: Enumerate the query immediately to avoid deferred execution issues

                foreach (var b in blogs)
                {
                    Console.WriteLine($"Blog ID: {b.BlogId}, URL: {b.Url}");

                    foreach (var p in b.Posts)
                    {
                        Console.WriteLine($"\n   Post ID: {p.PostId}, Created: {p.Created}, Title: {p.Title}, Content: {p.Content}");
                    }
                }

                // Create
                Console.WriteLine("\n---------------------");
                Console.WriteLine("Inserting a new blog");

                Console.Write("Enter URL: ");
                string url = Console.ReadLine();
                
                // Fix: Add null and empty validation for URL
                if (string.IsNullOrWhiteSpace(url))
                {
                    Console.WriteLine("Error: URL cannot be null or empty.");
                    return;
                }

                var newBlog = db.Add(new Blog { Url = url });

                Console.WriteLine("\nInserting a blog post");

                Console.Write("Enter title: ");
                string title = Console.ReadLine();
                
                // Fix: Add null and empty validation for title
                if (string.IsNullOrWhiteSpace(title))
                {
                    Console.WriteLine("Error: Title cannot be null or empty.");
                    return;
                }
                
                Console.Write("Enter content: ");
                string content = Console.ReadLine();
                
                // Fix: Add null and empty validation for content
                if (string.IsNullOrWhiteSpace(content))
                {
                    Console.WriteLine("Error: Content cannot be null or empty.");
                    return;
                }

                newBlog.Entity.Posts.Add(
                    new Post
                    {
                        Created = DateTime.Now,
                        Title = title,
                        Content = content
                    });

                db.SaveChanges();

                // Delete
                //Console.WriteLine("Delete the blog");
                //db.Remove(blog);
                //db.SaveChanges();
            }
        }
    }
}
