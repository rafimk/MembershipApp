﻿// <auto-generated />
using System;
using Membership.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Membership.Infrastructure.DAL.Migrations
{
    [DbContext(typeof(MembershipDbContext))]
    [Migration("20220706164833_Dispute_Request")]
    partial class Dispute_Request
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Membership.Core.Entities.Commons.FileAttachment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ActualFileName")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("FileExtension")
                        .HasColumnType("text");

                    b.Property<string>("FilePath")
                        .HasColumnType("text");

                    b.Property<long>("FileSize")
                        .HasColumnType("bigint");

                    b.Property<string>("FileType")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("SavedFileName")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("FileAttachments");
                });

            modelBuilder.Entity("Membership.Core.Entities.Commons.OcrResult", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("CardNumber")
                        .HasColumnType("text");

                    b.Property<string>("CardType")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateOnly>("DateofBirth")
                        .HasColumnType("date");

                    b.Property<DateOnly>("ExpiryDate")
                        .HasColumnType("date");

                    b.Property<Guid?>("FrontPageId")
                        .HasColumnType("uuid");

                    b.Property<string>("IdNumber")
                        .HasColumnType("text");

                    b.Property<Guid?>("LastPageId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("OcrResults");
                });

            modelBuilder.Entity("Membership.Core.Entities.Memberships.Disputes.DisputeRequest", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ActionBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("ActionDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("JustificationComment")
                        .HasColumnType("text");

                    b.Property<Guid>("MemberId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProposedAreaId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProposedMandalamId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProposedPanchayatId")
                        .HasColumnType("uuid");

                    b.Property<string>("Reason")
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<Guid>("SubmittedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("SubmittedDate")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("MemberId");

                    b.HasIndex("ProposedAreaId");

                    b.HasIndex("ProposedMandalamId");

                    b.HasIndex("ProposedPanchayatId");

                    b.ToTable("DisputeRequests");
                });

            modelBuilder.Entity("Membership.Core.Entities.Memberships.Members.Member", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("AddressInIndia")
                        .HasColumnType("text");

                    b.Property<string>("AlternativeContactNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<Guid>("AreaId")
                        .HasColumnType("uuid");

                    b.Property<int>("BloodGroup")
                        .HasColumnType("integer");

                    b.Property<string>("CardNumber")
                        .HasColumnType("text");

                    b.Property<double>("CollectedAmount")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("CreatedBy")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("DateOfBirth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<DateTimeOffset>("EmiratesIdExpiry")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("EmiratesIdFrontPage")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("EmiratesIdLastPage")
                        .HasColumnType("uuid");

                    b.Property<string>("EmiratesIdNumber")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("HouseName")
                        .HasColumnType("text");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<Guid>("MandalamId")
                        .HasColumnType("uuid");

                    b.Property<string>("MembershipId")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("character varying(15)");

                    b.Property<Guid>("MembershipPeriodId")
                        .HasColumnType("uuid");

                    b.Property<string>("MobileNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<Guid>("PanchayatId")
                        .HasColumnType("uuid");

                    b.Property<DateTimeOffset>("PassportExpiry")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("PassportFrontPage")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("PassportLastPage")
                        .HasColumnType("uuid");

                    b.Property<string>("PassportNumber")
                        .HasMaxLength(25)
                        .HasColumnType("character varying(25)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<Guid?>("Photo")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProfessionId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("QualificationId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("RegisteredOrganizationId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("RegisteredOrganizationId1")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("VerifiedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid?>("WelfareSchemeId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("WelfareSchemeId1")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AreaId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("MandalamId");

                    b.HasIndex("MembershipPeriodId");

                    b.HasIndex("PanchayatId");

                    b.HasIndex("ProfessionId");

                    b.HasIndex("QualificationId");

                    b.HasIndex("RegisteredOrganizationId1");

                    b.HasIndex("WelfareSchemeId1");

                    b.ToTable("Members");
                });

            modelBuilder.Entity("Membership.Core.Entities.Memberships.MembershipPeriods.MembershipPeriod", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTimeOffset>("End")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsEnrollActive")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("RegistrationEnded")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime?>("RegistrationStarted")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTimeOffset>("Start")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("MembershipPeriods");
                });

            modelBuilder.Entity("Membership.Core.Entities.Memberships.Professions.Profession", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Professions");
                });

            modelBuilder.Entity("Membership.Core.Entities.Memberships.Qualifications.Qualification", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Qualifications");
                });

            modelBuilder.Entity("Membership.Core.Entities.Memberships.RegisteredOrganizations.RegisteredOrganization", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("RegisteredOrganizations");
                });

            modelBuilder.Entity("Membership.Core.Entities.Memberships.WelfareSchemes.WelfareScheme", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("WelfareSchemes");
                });

            modelBuilder.Entity("Membership.Core.Entities.Nationalities.Area", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<Guid>("StateId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("StateId");

                    b.ToTable("Areas");
                });

            modelBuilder.Entity("Membership.Core.Entities.Nationalities.District", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.ToTable("Districts");
                });

            modelBuilder.Entity("Membership.Core.Entities.Nationalities.Mandalam", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<Guid>("DistrictId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("DistrictId");

                    b.ToTable("Mandalams");
                });

            modelBuilder.Entity("Membership.Core.Entities.Nationalities.Panchayat", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<Guid>("MandalamId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.HasKey("Id");

                    b.HasIndex("MandalamId");

                    b.ToTable("Panchayats");
                });

            modelBuilder.Entity("Membership.Core.Entities.Nationalities.State", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("Prefix")
                        .HasMaxLength(5)
                        .HasColumnType("character varying(5)");

                    b.HasKey("Id");

                    b.ToTable("States");
                });

            modelBuilder.Entity("Membership.Core.Entities.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("AlternativeContactNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<Guid?>("CascadeId")
                        .HasColumnType("uuid");

                    b.Property<string>("CascadeName")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Designation")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDisputeCommittee")
                        .HasColumnType("boolean");

                    b.Property<string>("MobileNumber")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("character varying(20)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("Role")
                        .HasColumnType("text");

                    b.Property<Guid?>("StateId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("VerifiedAt")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Membership.Core.Entities.Memberships.Disputes.DisputeRequest", b =>
                {
                    b.HasOne("Membership.Core.Entities.Memberships.Members.Member", "Member")
                        .WithMany()
                        .HasForeignKey("MemberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Membership.Core.Entities.Nationalities.Area", "ProposedArea")
                        .WithMany()
                        .HasForeignKey("ProposedAreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Membership.Core.Entities.Nationalities.Mandalam", "ProposedMandalam")
                        .WithMany()
                        .HasForeignKey("ProposedMandalamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Membership.Core.Entities.Nationalities.Panchayat", "ProposedPanchayat")
                        .WithMany()
                        .HasForeignKey("ProposedPanchayatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Member");

                    b.Navigation("ProposedArea");

                    b.Navigation("ProposedMandalam");

                    b.Navigation("ProposedPanchayat");
                });

            modelBuilder.Entity("Membership.Core.Entities.Memberships.Members.Member", b =>
                {
                    b.HasOne("Membership.Core.Entities.Nationalities.Area", "Area")
                        .WithMany()
                        .HasForeignKey("AreaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Membership.Core.Entities.Nationalities.Mandalam", "Mandalam")
                        .WithMany()
                        .HasForeignKey("MandalamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Membership.Core.Entities.Memberships.MembershipPeriods.MembershipPeriod", "MembershipPeriod")
                        .WithMany()
                        .HasForeignKey("MembershipPeriodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Membership.Core.Entities.Nationalities.Panchayat", "Panchayat")
                        .WithMany()
                        .HasForeignKey("PanchayatId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Membership.Core.Entities.Memberships.Professions.Profession", "Profession")
                        .WithMany()
                        .HasForeignKey("ProfessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Membership.Core.Entities.Memberships.Qualifications.Qualification", "Qualification")
                        .WithMany()
                        .HasForeignKey("QualificationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Membership.Core.Entities.Memberships.RegisteredOrganizations.RegisteredOrganization", "RegisteredOrganization")
                        .WithMany()
                        .HasForeignKey("RegisteredOrganizationId1");

                    b.HasOne("Membership.Core.Entities.Memberships.WelfareSchemes.WelfareScheme", "WelfareScheme")
                        .WithMany()
                        .HasForeignKey("WelfareSchemeId1");

                    b.Navigation("Area");

                    b.Navigation("Mandalam");

                    b.Navigation("MembershipPeriod");

                    b.Navigation("Panchayat");

                    b.Navigation("Profession");

                    b.Navigation("Qualification");

                    b.Navigation("RegisteredOrganization");

                    b.Navigation("WelfareScheme");
                });

            modelBuilder.Entity("Membership.Core.Entities.Nationalities.Area", b =>
                {
                    b.HasOne("Membership.Core.Entities.Nationalities.State", "State")
                        .WithMany()
                        .HasForeignKey("StateId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("State");
                });

            modelBuilder.Entity("Membership.Core.Entities.Nationalities.Mandalam", b =>
                {
                    b.HasOne("Membership.Core.Entities.Nationalities.District", "District")
                        .WithMany()
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("District");
                });

            modelBuilder.Entity("Membership.Core.Entities.Nationalities.Panchayat", b =>
                {
                    b.HasOne("Membership.Core.Entities.Nationalities.Mandalam", "Mandalam")
                        .WithMany()
                        .HasForeignKey("MandalamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mandalam");
                });
#pragma warning restore 612, 618
        }
    }
}
