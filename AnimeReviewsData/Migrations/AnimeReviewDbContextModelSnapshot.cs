﻿// <auto-generated />
using System;
using AnimeReviewsData.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AnimeReviewsData.Migrations
{
    [DbContext(typeof(AnimeReviewDbContext))]
    partial class AnimeReviewDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AnimeReviewsData.Model.Anime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Anime");
                });

            modelBuilder.Entity("AnimeReviewsData.Model.AnimeAuthor", b =>
                {
                    b.Property<int>("AnimeId")
                        .HasColumnType("int");

                    b.Property<int>("AuthorId")
                        .HasColumnType("int");

                    b.HasKey("AnimeId", "AuthorId");

                    b.HasIndex("AuthorId");

                    b.ToTable("AnimeAuthors");
                });

            modelBuilder.Entity("AnimeReviewsData.Model.AnimeCategory", b =>
                {
                    b.Property<int>("AnimeId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.HasKey("AnimeId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("AnimeCategories");
                });

            modelBuilder.Entity("AnimeReviewsData.Model.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("AnimeReviewsData.Model.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("AnimeReviewsData.Model.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AnimeId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<int>("ReviewerId")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AnimeId");

                    b.HasIndex("ReviewerId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("AnimeReviewsData.Model.Reviewer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Reviewers");
                });

            modelBuilder.Entity("AnimeReviewsData.Model.AnimeAuthor", b =>
                {
                    b.HasOne("AnimeReviewsData.Model.Anime", "Anime")
                        .WithMany("AnimeAuthors")
                        .HasForeignKey("AnimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AnimeReviewsData.Model.Author", "Author")
                        .WithMany("AnimeAuthors")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Anime");

                    b.Navigation("Author");
                });

            modelBuilder.Entity("AnimeReviewsData.Model.AnimeCategory", b =>
                {
                    b.HasOne("AnimeReviewsData.Model.Anime", "Anime")
                        .WithMany("AnimeCategories")
                        .HasForeignKey("AnimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AnimeReviewsData.Model.Category", "Category")
                        .WithMany("AnimeCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Anime");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("AnimeReviewsData.Model.Review", b =>
                {
                    b.HasOne("AnimeReviewsData.Model.Anime", "Anime")
                        .WithMany("Reviews")
                        .HasForeignKey("AnimeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AnimeReviewsData.Model.Reviewer", "Reviewer")
                        .WithMany("Reviews")
                        .HasForeignKey("ReviewerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Anime");

                    b.Navigation("Reviewer");
                });

            modelBuilder.Entity("AnimeReviewsData.Model.Anime", b =>
                {
                    b.Navigation("AnimeAuthors");

                    b.Navigation("AnimeCategories");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("AnimeReviewsData.Model.Author", b =>
                {
                    b.Navigation("AnimeAuthors");
                });

            modelBuilder.Entity("AnimeReviewsData.Model.Category", b =>
                {
                    b.Navigation("AnimeCategories");
                });

            modelBuilder.Entity("AnimeReviewsData.Model.Reviewer", b =>
                {
                    b.Navigation("Reviews");
                });
#pragma warning restore 612, 618
        }
    }
}
