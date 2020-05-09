using Microsoft.EntityFrameworkCore;
using Podler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Podler.WebApi
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Comic> Comics { get; set; }
        public DbSet<Chapter> Chapters { get; set; }
        public DbSet<Designer> Designers { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CoverImage> CoverImages { get; set; }
        public DbSet<PageImage> PageImages { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Comic>().HasKey(c => c.Id);
            modelBuilder.Entity<Comic>().HasOne(c => c.Publisher).WithMany(p => p.Comics);
            modelBuilder.Entity<Comic>().HasMany(c => c.Chapters).WithOne(c => c.Comic);
            modelBuilder.Entity<Comic>().HasOne(c => c.CoverImage)
                                        .WithOne(c => c.Comic)
                                        .HasForeignKey<CoverImage>(c => c.ComicId);

            modelBuilder.Entity<Chapter>().HasKey(c => c.Id);
            modelBuilder.Entity<Chapter>().HasOne(c => c.Comic);
            modelBuilder.Entity<Chapter>().HasMany(c => c.PageImages).WithOne(p => p.Chapter);

            modelBuilder.Entity<Publisher>().HasKey(p => p.Id);
            modelBuilder.Entity<Publisher>().HasMany(p => p.Comics);

            modelBuilder.Entity<CoverImage>().HasKey(c => c.Id);
            modelBuilder.Entity<CoverImage>().HasOne(c => c.Comic);

            modelBuilder.Entity<PageImage>().HasKey(p => p.Id);
            modelBuilder.Entity<PageImage>().HasOne(p => p.Chapter);

            #region Many To Many Relationships

            modelBuilder.Entity<ComicCategory>().HasKey(cc => new { cc.ComicId, cc.CategoryId });

            modelBuilder.Entity<ComicCategory>().HasOne(cc => cc.Comic)
                                                .WithMany(c => c.ComicCategories)
                                                .HasForeignKey(cc => cc.ComicId);

            modelBuilder.Entity<ComicCategory>().HasOne(cc => cc.Category)
                                                .WithMany(cc => cc.ComicCategories)
                                                .HasForeignKey(cc => cc.CategoryId);

            modelBuilder.Entity<ComicAuthor>().HasKey(ca => new { ca.ComicId, ca.AuthorId });

            modelBuilder.Entity<ComicAuthor>().HasOne(ca => ca.Comic)
                                                .WithMany(c => c.ComicAuthors)
                                                .HasForeignKey(cc => cc.ComicId);

            modelBuilder.Entity<ComicAuthor>().HasOne(ca => ca.Author)
                                                .WithMany(ca => ca.ComicAuthors)
                                                .HasForeignKey(ca => ca.AuthorId);

            modelBuilder.Entity<ComicDesigner>().HasKey(cd => new { cd.ComicId, cd.DesignerId });

            modelBuilder.Entity<ComicDesigner>().HasOne(cd => cd.Comic)
                                                .WithMany(c => c.ComicDesigners)
                                                .HasForeignKey(cd => cd.ComicId);

            modelBuilder.Entity<ComicDesigner>().HasOne(cd => cd.Designer)
                                                .WithMany(cd => cd.ComicDesigners)
                                                .HasForeignKey(cd => cd.DesignerId);

            #endregion
        }
    }
}
