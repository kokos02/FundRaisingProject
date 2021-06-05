﻿// <auto-generated />
using System;
using FundRaising.Core.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FundRaising.Core.Migrations
{
    [DbContext(typeof(FundRaisingDbContext))]
    [Migration("20210605125750_second")]
    partial class second
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("FundRaising.Core.Models.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<decimal>("CurrentFund")
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProjectCategory")
                        .HasColumnType("int");

                    b.Property<decimal>("TargetFund")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId1")
                        .HasColumnType("int");

                    b.HasKey("ProjectId");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("FundRaising.Core.Models.Reward", b =>
                {
                    b.Property<int>("RewardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RewardId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Rewards");
                });

            modelBuilder.Entity("FundRaising.Core.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FundRaising.Core.Models.UserReward", b =>
                {
                    b.Property<int>("UserRewardId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("ProjectId")
                        .HasColumnType("int");

                    b.Property<int?>("RewardId")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UserRewardId");

                    b.HasIndex("ProjectId");

                    b.HasIndex("RewardId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRewards");
                });

            modelBuilder.Entity("FundRaising.Core.Models.Project", b =>
                {
                    b.HasOne("FundRaising.Core.Models.User", null)
                        .WithMany("CreatedProjects")
                        .HasForeignKey("UserId");

                    b.HasOne("FundRaising.Core.Models.User", null)
                        .WithMany("FundedProjects")
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("FundRaising.Core.Models.Reward", b =>
                {
                    b.HasOne("FundRaising.Core.Models.Project", "Project")
                        .WithMany("AvailableRewards")
                        .HasForeignKey("ProjectId");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("FundRaising.Core.Models.UserReward", b =>
                {
                    b.HasOne("FundRaising.Core.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId");

                    b.HasOne("FundRaising.Core.Models.Reward", "Reward")
                        .WithMany("UserReward")
                        .HasForeignKey("RewardId");

                    b.HasOne("FundRaising.Core.Models.User", "User")
                        .WithMany("PurchasedRewards")
                        .HasForeignKey("UserId");

                    b.Navigation("Project");

                    b.Navigation("Reward");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FundRaising.Core.Models.Project", b =>
                {
                    b.Navigation("AvailableRewards");
                });

            modelBuilder.Entity("FundRaising.Core.Models.Reward", b =>
                {
                    b.Navigation("UserReward");
                });

            modelBuilder.Entity("FundRaising.Core.Models.User", b =>
                {
                    b.Navigation("CreatedProjects");

                    b.Navigation("FundedProjects");

                    b.Navigation("PurchasedRewards");
                });
#pragma warning restore 612, 618
        }
    }
}
