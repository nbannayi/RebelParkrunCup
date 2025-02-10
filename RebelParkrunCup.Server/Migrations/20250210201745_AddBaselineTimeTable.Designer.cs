﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RebelParkrunCup.Server.Data;

#nullable disable

namespace RebelParkrunCup.Server.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250210201745_AddBaselineTimeTable")]
    partial class AddBaselineTimeTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.1");

            modelBuilder.Entity("RebelParkrunCup.Shared.BaselineTime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BaselineTimeMins")
                        .HasColumnType("INTEGER");

                    b.Property<int>("BaselineTimeSecs")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RunnerId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TournamentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RunnerId");

                    b.HasIndex("TournamentId");

                    b.ToTable("BaselineTimes");
                });

            modelBuilder.Entity("RebelParkrunCup.Shared.Runner", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BaselineTimeMins")
                        .HasColumnType("INTEGER");

                    b.Property<int>("BaselineTimeSecs")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Runners");
                });

            modelBuilder.Entity("RebelParkrunCup.Shared.Tournament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("InProgress")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Tournaments");
                });

            modelBuilder.Entity("RebelParkrunCup.Shared.BaselineTime", b =>
                {
                    b.HasOne("RebelParkrunCup.Shared.Runner", "Runner")
                        .WithMany("BaselineTimes")
                        .HasForeignKey("RunnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RebelParkrunCup.Shared.Tournament", "Tournament")
                        .WithMany("BaselineTimes")
                        .HasForeignKey("TournamentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Runner");

                    b.Navigation("Tournament");
                });

            modelBuilder.Entity("RebelParkrunCup.Shared.Runner", b =>
                {
                    b.Navigation("BaselineTimes");
                });

            modelBuilder.Entity("RebelParkrunCup.Shared.Tournament", b =>
                {
                    b.Navigation("BaselineTimes");
                });
#pragma warning restore 612, 618
        }
    }
}
