using System.Reflection;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;

using MyBlazorApp.Models.MyBlazorAppDb;

namespace MyBlazorApp.Data
{
  public partial class MyBlazorAppDbContext : Microsoft.EntityFrameworkCore.DbContext
  {
    public MyBlazorAppDbContext(DbContextOptions<MyBlazorAppDbContext> options):base(options)
    {
    }

    public MyBlazorAppDbContext()
    {
    }

    partial void OnModelBuilding(ModelBuilder builder);

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);



        builder.Entity<MyBlazorApp.Models.MyBlazorAppDb.ToDoList>()
              .Property(p => p.DATE_CREATED)
              .HasColumnType("datetime2");

        builder.Entity<MyBlazorApp.Models.MyBlazorAppDb.Item>()
              .Property(p => p.ITEM_ID)
              .HasPrecision(10, 0);

        builder.Entity<MyBlazorApp.Models.MyBlazorAppDb.Item>()
              .Property(p => p.LIST_ID)
              .HasPrecision(10, 0);

        builder.Entity<MyBlazorApp.Models.MyBlazorAppDb.ToDoList>()
              .Property(p => p.LIST_ID)
              .HasPrecision(10, 0);
        this.OnModelBuilding(builder);
    }


    public DbSet<MyBlazorApp.Models.MyBlazorAppDb.Item> Items
    {
      get;
      set;
    }

    public DbSet<MyBlazorApp.Models.MyBlazorAppDb.ToDoList> ToDoLists
    {
      get;
      set;
    }
  }
}
