﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Podler.WebApi;

namespace Podler.WebApi.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20200515181801_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3");

            modelBuilder.Entity("Podler.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(40);

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("Podler.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(40);

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Podler.Models.Chapter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CapterNumber")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ComicId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(80);

                    b.HasKey("Id");

                    b.HasIndex("ComicId");

                    b.ToTable("Chapters");
                });

            modelBuilder.Entity("Podler.Models.Comic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(900);

                    b.Property<int>("NumberChapters")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PublisherId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Rank")
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("Score")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(40);

                    b.HasKey("Id");

                    b.HasIndex("PublisherId");

                    b.ToTable("Comics");
                });

            modelBuilder.Entity("Podler.Models.ComicAuthor", b =>
                {
                    b.Property<int>("ComicId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("AuthorId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ComicId", "AuthorId");

                    b.HasIndex("AuthorId");

                    b.ToTable("ComicAuthor");
                });

            modelBuilder.Entity("Podler.Models.ComicCategory", b =>
                {
                    b.Property<int>("ComicId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ComicId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("ComicCategory");
                });

            modelBuilder.Entity("Podler.Models.ComicDesigner", b =>
                {
                    b.Property<int>("ComicId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DesignerId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ComicId", "DesignerId");

                    b.HasIndex("DesignerId");

                    b.ToTable("ComicDesigner");
                });

            modelBuilder.Entity("Podler.Models.Cover", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("ComicId")
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.HasKey("Id");

                    b.HasIndex("ComicId")
                        .IsUnique();

                    b.ToTable("Covers");
                });

            modelBuilder.Entity("Podler.Models.Designer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(40);

                    b.HasKey("Id");

                    b.ToTable("Designers");
                });

            modelBuilder.Entity("Podler.Models.Page", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ChapterId")
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("Image")
                        .IsRequired()
                        .HasColumnType("BLOB");

                    b.HasKey("Id");

                    b.HasIndex("ChapterId");

                    b.ToTable("Pages");
                });

            modelBuilder.Entity("Podler.Models.Publisher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(40);

                    b.HasKey("Id");

                    b.ToTable("Publishers");
                });

            modelBuilder.Entity("Podler.Models.Chapter", b =>
                {
                    b.HasOne("Podler.Models.Comic", "Comic")
                        .WithMany("Chapters")
                        .HasForeignKey("ComicId");
                });

            modelBuilder.Entity("Podler.Models.Comic", b =>
                {
                    b.HasOne("Podler.Models.Publisher", "Publisher")
                        .WithMany("Comics")
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Podler.Models.ComicAuthor", b =>
                {
                    b.HasOne("Podler.Models.Author", "Author")
                        .WithMany("Comics")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Podler.Models.Comic", "Comic")
                        .WithMany("Authors")
                        .HasForeignKey("ComicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Podler.Models.ComicCategory", b =>
                {
                    b.HasOne("Podler.Models.Category", "Category")
                        .WithMany("Comics")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Podler.Models.Comic", "Comic")
                        .WithMany("Categories")
                        .HasForeignKey("ComicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Podler.Models.ComicDesigner", b =>
                {
                    b.HasOne("Podler.Models.Comic", "Comic")
                        .WithMany("Designers")
                        .HasForeignKey("ComicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Podler.Models.Designer", "Designer")
                        .WithMany("Comics")
                        .HasForeignKey("DesignerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Podler.Models.Cover", b =>
                {
                    b.HasOne("Podler.Models.Comic", "Comic")
                        .WithOne("Cover")
                        .HasForeignKey("Podler.Models.Cover", "ComicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Podler.Models.Page", b =>
                {
                    b.HasOne("Podler.Models.Chapter", "Chapter")
                        .WithMany("Pages")
                        .HasForeignKey("ChapterId");
                });
#pragma warning restore 612, 618
        }
    }
}
