using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

/// <summary>
/// Represents a session with the Database.
/// </summary>

public class BloggingContext : DbContext
{
    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }

    public BloggingContext()
    {
        
    }

    // A method that returns a new instance of BloggingContext, same as row 17.
    //public static BloggingContext CreateBlogginsContext()
    //{
    //    return new BloggingContext();
    //}

    //// The following configures EF to create a Sqlite database file in the
    //// special "local" folder for your platform.
    ///
    //protected override void OnConfiguring(DbContextOptionsBuilder options)
    //    => options.UseSqlite($"Data Source={DbPath}")
    //    .LogTo(message => Debug.WriteLine(message))
    //    .EnableSensitiveDataLogging();



    // The following configures EF to create a SqlServer database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer(@"Server=ITHS-HP-EB-059\SQLEXPRESS;Database=BloggingDb;Trusted_Connection=True;TrustServerCertificate=True;")
                    .LogTo(message => Debug.WriteLine(message))
                    .EnableSensitiveDataLogging();
    }
}

[Table("Moo")]
public class Blog
{
    public int BlogId { get; set; }
    public string Url { get; set; } = null!;

    public List<Post> Posts { get; } = new();
}


[Table("Raawr")]
public class Post
{
    public int PostId { get; set; }
    public string? Title { get; set; }
    public string? Content { get; set; }

    public int BlogId { get; set; }
    public Blog? Blog { get; set; } //required
    public List<Writer> Writers { get; }
}

public class Writer
{
    public int WriterId { get; set; }
    public string Name { get; set; } = null!;

    public List<Post> Posts { get; } 
}

