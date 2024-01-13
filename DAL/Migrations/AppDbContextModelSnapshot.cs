﻿// <auto-generated />
using System;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CategoryDeal", b =>
                {
                    b.Property<int>("CategoriesId")
                        .HasColumnType("integer");

                    b.Property<int>("DealsId")
                        .HasColumnType("integer");

                    b.HasKey("CategoriesId", "DealsId");

                    b.HasIndex("DealsId");

                    b.ToTable("CategoryDeal");
                });

            modelBuilder.Entity("CategoryUser", b =>
                {
                    b.Property<int>("CategoriesId")
                        .HasColumnType("integer");

                    b.Property<int>("UsersId")
                        .HasColumnType("integer");

                    b.HasKey("CategoriesId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("CategoryUser");
                });

            modelBuilder.Entity("Domain.Entity.Avatar", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<Guid>("Key")
                        .HasColumnType("uuid");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Avatars");
                });

            modelBuilder.Entity("Domain.Entity.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Разработка"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Дизайн"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Базы данных"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Репетиторство"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Литература"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Музыка"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Видео мантаж"
                        },
                        new
                        {
                            Id = 8,
                            Name = "Фото"
                        },
                        new
                        {
                            Id = 9,
                            Name = "3D"
                        },
                        new
                        {
                            Id = 10,
                            Name = "Переводы"
                        });
                });

            modelBuilder.Entity("Domain.Entity.CommentDeals", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatorUserId")
                        .HasColumnType("integer");

                    b.Property<int>("DealId")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("TimeCreated")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("CreatorUserId");

                    b.HasIndex("DealId");

                    b.ToTable("CommentDeals");
                });

            modelBuilder.Entity("Domain.Entity.CommentUsers", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("CreatorUserId")
                        .HasColumnType("integer");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("TimeCreated")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId1")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("CommentUsers");
                });

            modelBuilder.Entity("Domain.Entity.Deal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("ApproximateDate")
                        .HasColumnType("date");

                    b.Property<int>("CreatorUserId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("DatePublication")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int?>("ExecutorUserId")
                        .HasColumnType("integer");

                    b.Property<string>("Localtion")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("MaxPrice")
                        .HasColumnType("integer");

                    b.Property<int>("MinPrice")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CreatorUserId");

                    b.HasIndex("ExecutorUserId");

                    b.ToTable("Deals");
                });

            modelBuilder.Entity("Domain.Entity.Proposal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DatePublish")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("DealId")
                        .HasColumnType("integer");

                    b.Property<string>("Descripton")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Price")
                        .HasColumnType("integer");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DealId");

                    b.HasIndex("UserId");

                    b.ToTable("Proposals");
                });

            modelBuilder.Entity("Domain.Entity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Balance")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<bool>("IsVIP")
                        .HasColumnType("boolean");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<string>("SecondName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CategoryDeal", b =>
                {
                    b.HasOne("Domain.Entity.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entity.Deal", null)
                        .WithMany()
                        .HasForeignKey("DealsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CategoryUser", b =>
                {
                    b.HasOne("Domain.Entity.Category", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entity.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Entity.Avatar", b =>
                {
                    b.HasOne("Domain.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entity.CommentDeals", b =>
                {
                    b.HasOne("Domain.Entity.User", "CreatorUser")
                        .WithMany("CommentDeals")
                        .HasForeignKey("CreatorUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entity.Deal", "Deal")
                        .WithMany()
                        .HasForeignKey("DealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatorUser");

                    b.Navigation("Deal");
                });

            modelBuilder.Entity("Domain.Entity.CommentUsers", b =>
                {
                    b.HasOne("Domain.Entity.User", "CreatorUser")
                        .WithMany("CommentUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entity.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatorUser");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entity.Deal", b =>
                {
                    b.HasOne("Domain.Entity.User", "CreatorUser")
                        .WithMany("CreatedDeals")
                        .HasForeignKey("CreatorUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entity.User", "ExecutorUser")
                        .WithMany("AcceptedDeals")
                        .HasForeignKey("ExecutorUserId");

                    b.Navigation("CreatorUser");

                    b.Navigation("ExecutorUser");
                });

            modelBuilder.Entity("Domain.Entity.Proposal", b =>
                {
                    b.HasOne("Domain.Entity.Deal", "Deal")
                        .WithMany("Proposals")
                        .HasForeignKey("DealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entity.User", "User")
                        .WithMany("Proposals")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Deal");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Domain.Entity.Deal", b =>
                {
                    b.Navigation("Proposals");
                });

            modelBuilder.Entity("Domain.Entity.User", b =>
                {
                    b.Navigation("AcceptedDeals");

                    b.Navigation("CommentDeals");

                    b.Navigation("CommentUsers");

                    b.Navigation("CreatedDeals");

                    b.Navigation("Proposals");
                });
#pragma warning restore 612, 618
        }
    }
}
