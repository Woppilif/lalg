﻿// <auto-generated />
using System;
using BotAppData;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BotAppData.Migrations
{
    [DbContext(typeof(BotAppContext))]
    [Migration("20200614142953_iscomon")]
    partial class iscomon
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("BotAppData.Models.Age", b =>
                {
                    b.Property<int>("AgeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<bool>("IsShows")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<int>("Order")
                        .HasColumnType("integer");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uuid");

                    b.HasKey("AgeId");

                    b.HasIndex("ProductId");

                    b.ToTable("Ages");
                });

            modelBuilder.Entity("BotAppData.Models.Group", b =>
                {
                    b.Property<Guid>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("AgeId")
                        .HasColumnType("integer");

                    b.Property<Guid>("Creator")
                        .HasColumnType("uuid");

                    b.Property<int>("GroupTypeId")
                        .HasColumnType("integer");

                    b.Property<bool>("IsClosed")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsCommon")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.HasKey("GroupId");

                    b.HasIndex("AgeId");

                    b.HasIndex("GroupTypeId");

                    b.HasIndex("ProductId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("BotAppData.Models.GroupType", b =>
                {
                    b.Property<int>("GroupTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("GroupTypeId");

                    b.ToTable("GroupTypes");
                });

            modelBuilder.Entity("BotAppData.Models.Lesson", b =>
                {
                    b.Property<Guid>("LessonId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("GroupId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsRepeats")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("LessonAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("PatternId")
                        .HasColumnType("uuid");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.HasKey("LessonId");

                    b.HasIndex("GroupId");

                    b.HasIndex("PatternId");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("BotAppData.Models.LessonLog", b =>
                {
                    b.Property<Guid>("LessonLogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("LessonId")
                        .HasColumnType("uuid");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("LessonLogId");

                    b.HasIndex("LessonId");

                    b.HasIndex("UserId");

                    b.ToTable("LessonLogs");
                });

            modelBuilder.Entity("BotAppData.Models.LinkSpyer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("LessonId")
                        .HasColumnType("uuid");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("LessonId");

                    b.HasIndex("UserId");

                    b.ToTable("LinkSpyers");
                });

            modelBuilder.Entity("BotAppData.Models.Pattern", b =>
                {
                    b.Property<Guid>("PatternId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("LessonId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("PatternId");

                    b.HasIndex("LessonId");

                    b.ToTable("Patterns");
                });

            modelBuilder.Entity("BotAppData.Models.PatternMessage", b =>
                {
                    b.Property<Guid>("PatternMessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("AtTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsGreeting")
                        .HasColumnType("boolean");

                    b.Property<string>("Message")
                        .HasColumnType("text");

                    b.Property<Guid>("PatternId")
                        .HasColumnType("uuid");

                    b.Property<bool>("Status")
                        .HasColumnType("boolean");

                    b.HasKey("PatternMessageId");

                    b.HasIndex("PatternId");

                    b.ToTable("PatternMessages");
                });

            modelBuilder.Entity("BotAppData.Models.Payment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("CapturedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsExtends")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPayed")
                        .HasColumnType("boolean");

                    b.Property<string>("PaymentId")
                        .HasColumnType("text");

                    b.Property<Guid>("SubscriptionId")
                        .HasColumnType("uuid");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("SubscriptionId");

                    b.HasIndex("UserId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("BotAppData.Models.Product", b =>
                {
                    b.Property<Guid>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int[]>("AgeId")
                        .HasColumnType("integer[]");

                    b.Property<int>("FreeTimes")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<int>("ProductTypeId")
                        .HasColumnType("integer");

                    b.HasKey("ProductId");

                    b.HasIndex("ProductTypeId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("BotAppData.Models.ProductType", b =>
                {
                    b.Property<int>("ProductTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("ProductTypeId");

                    b.ToTable("ProductTypes");
                });

            modelBuilder.Entity("BotAppData.Models.Subscription", b =>
                {
                    b.Property<Guid>("SubscriptionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Begin")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("End")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uuid");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("SubscriptionId");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("BotAppData.Models.User", b =>
                {
                    b.Property<long>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn);

                    b.Property<int?>("AgeId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Firstname")
                        .HasColumnType("text");

                    b.Property<Guid?>("GroupId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsTeacher")
                        .HasColumnType("boolean");

                    b.Property<string>("Lastname")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<int>("Platform")
                        .HasColumnType("integer");

                    b.Property<bool>("Registered")
                        .HasColumnType("boolean");

                    b.HasKey("UserId");

                    b.HasIndex("AgeId");

                    b.HasIndex("GroupId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BotAppData.Models.Age", b =>
                {
                    b.HasOne("BotAppData.Models.Product", null)
                        .WithMany("Age")
                        .HasForeignKey("ProductId");
                });

            modelBuilder.Entity("BotAppData.Models.Group", b =>
                {
                    b.HasOne("BotAppData.Models.Age", "Age")
                        .WithMany("Groups")
                        .HasForeignKey("AgeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BotAppData.Models.GroupType", "GroupType")
                        .WithMany()
                        .HasForeignKey("GroupTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BotAppData.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BotAppData.Models.Lesson", b =>
                {
                    b.HasOne("BotAppData.Models.Group", "Group")
                        .WithMany("Lessons")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BotAppData.Models.Pattern", "Pattern")
                        .WithMany()
                        .HasForeignKey("PatternId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BotAppData.Models.LessonLog", b =>
                {
                    b.HasOne("BotAppData.Models.Lesson", "Lesson")
                        .WithMany()
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BotAppData.Models.User", "Users")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BotAppData.Models.LinkSpyer", b =>
                {
                    b.HasOne("BotAppData.Models.Lesson", "Lesson")
                        .WithMany()
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BotAppData.Models.User", "Users")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BotAppData.Models.Pattern", b =>
                {
                    b.HasOne("BotAppData.Models.Lesson", null)
                        .WithMany("Patterns")
                        .HasForeignKey("LessonId");
                });

            modelBuilder.Entity("BotAppData.Models.PatternMessage", b =>
                {
                    b.HasOne("BotAppData.Models.Pattern", "Pattern")
                        .WithMany("PatternMessages")
                        .HasForeignKey("PatternId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BotAppData.Models.Payment", b =>
                {
                    b.HasOne("BotAppData.Models.Subscription", "Subscription")
                        .WithMany()
                        .HasForeignKey("SubscriptionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BotAppData.Models.User", "Users")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BotAppData.Models.Product", b =>
                {
                    b.HasOne("BotAppData.Models.ProductType", "ProductType")
                        .WithMany()
                        .HasForeignKey("ProductTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BotAppData.Models.Subscription", b =>
                {
                    b.HasOne("BotAppData.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BotAppData.Models.User", "Users")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BotAppData.Models.User", b =>
                {
                    b.HasOne("BotAppData.Models.Age", "Age")
                        .WithMany()
                        .HasForeignKey("AgeId");

                    b.HasOne("BotAppData.Models.Group", "Group")
                        .WithMany()
                        .HasForeignKey("GroupId");
                });
#pragma warning restore 612, 618
        }
    }
}
