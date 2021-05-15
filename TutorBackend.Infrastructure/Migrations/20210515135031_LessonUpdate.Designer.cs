﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TutorBackend.Infrastructure.SqlServerContext;

namespace TutorBackend.Infrastructure.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20210515135031_LessonUpdate")]
    partial class LessonUpdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.5")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TopicTutor", b =>
                {
                    b.Property<string>("TopicsName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<Guid>("TutorsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TopicsName", "TutorsId");

                    b.HasIndex("TutorsId");

                    b.ToTable("TutorTopics");
                });

            modelBuilder.Entity("TutorBackend.Core.Entities.Lesson", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsAccepted")
                        .HasColumnType("bit");

                    b.Property<Guid>("ScheduleDayId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("StartAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ScheduleDayId");

                    b.HasIndex("UserId");

                    b.ToTable("Lessons");
                });

            modelBuilder.Entity("TutorBackend.Core.Entities.ScheduleDay", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartAt")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("TutorId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("TutorId");

                    b.ToTable("ScheduleDays");
                });

            modelBuilder.Entity("TutorBackend.Core.Entities.Topic", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Name");

                    b.ToTable("Topics");
                });

            modelBuilder.Entity("TutorBackend.Core.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasDiscriminator<string>("UserType").HasValue("User");
                });

            modelBuilder.Entity("TutorBackend.Core.Entities.Tutor", b =>
                {
                    b.HasBaseType("TutorBackend.Core.Entities.User");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("HasLocalLessons")
                        .HasColumnType("bit");

                    b.Property<bool>("HasRemoteLessons")
                        .HasColumnType("bit");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Tutor");
                });

            modelBuilder.Entity("TopicTutor", b =>
                {
                    b.HasOne("TutorBackend.Core.Entities.Topic", null)
                        .WithMany()
                        .HasForeignKey("TopicsName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TutorBackend.Core.Entities.Tutor", null)
                        .WithMany()
                        .HasForeignKey("TutorsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("TutorBackend.Core.Entities.Lesson", b =>
                {
                    b.HasOne("TutorBackend.Core.Entities.ScheduleDay", "ScheduleDay")
                        .WithMany("Lessons")
                        .HasForeignKey("ScheduleDayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TutorBackend.Core.Entities.User", "User")
                        .WithMany("Lessons")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("ScheduleDay");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TutorBackend.Core.Entities.ScheduleDay", b =>
                {
                    b.HasOne("TutorBackend.Core.Entities.Tutor", "Tutor")
                        .WithMany("ScheduleDays")
                        .HasForeignKey("TutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tutor");
                });

            modelBuilder.Entity("TutorBackend.Core.Entities.ScheduleDay", b =>
                {
                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("TutorBackend.Core.Entities.User", b =>
                {
                    b.Navigation("Lessons");
                });

            modelBuilder.Entity("TutorBackend.Core.Entities.Tutor", b =>
                {
                    b.Navigation("ScheduleDays");
                });
#pragma warning restore 612, 618
        }
    }
}
