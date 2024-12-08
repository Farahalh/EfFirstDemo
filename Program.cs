namespace EfFirstDemo;

using Microsoft.EntityFrameworkCore;
using System;
using System.Diagnostics;
using System.Linq;

class Program
{
    private static string GetConnectionString()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        return System.IO.Path.Join(path, "blogging.db");
    }
    static void Main(string[] args)
    {
        
        var connectionString = Program.GetConnectionString();

        //Create DbContext instance
        using var db = new BloggingContext(connectionString);

        //Delete the database everytime
        db.Database.EnsureDeleted();
        db.Database.EnsureCreated();

        //Recreate he database from the model
        //this means you can change your model and the database will map your entities
        var sqlenDb = db.Database.GenerateCreateScript();
        Debug.WriteLine(sqlenDb);

        //Just for fun
        var dbGenerator = new DbDataGenerator();
        dbGenerator.PopulateDb(db);
    }
}
 
/// <summary>
/// Responsible for populating the database with data
/// </summary>
public class DbDataGenerator
{
    public DbDataGenerator()
    {

    }

    public void PopulateDb(BloggingContext db)
    {
        // Note: This sample requires the database to be created before running.
        Console.WriteLine($"Database path: {db.DbPath}.");


        // Create
        Console.WriteLine("Inserting a new blog");
        db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
        db.Add(new Blog { Url = "http://microsoft.com" });
        db.Add(new Blog { Url = "http://helloworld.com" });
        db.SaveChanges();

        // Read
        Console.WriteLine("Querying for a blog");
        var blog = db.Blogs
            .OrderBy(b => b.BlogId)
            .First();

        // Update
        Console.WriteLine("Updating the blog and adding a post");
        blog.Url = "https://devblogs.microsoft.com/dotnet";
        blog.Posts.Add(
            new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });
        db.SaveChanges();

        // Delete
        Console.WriteLine("Delete the blog");
        db.Remove(blog);
        db.SaveChanges();
    }
}
