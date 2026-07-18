using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Phase03.Entities;

namespace Phase03.Context;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Assignment> Assignments { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<Lmsuser> Lmsusers { get; set; }

    public virtual DbSet<Professor> Professors { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Submission> Submissions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=atr.eng.utah.edu;Username=u0793491;Database=LMS3");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("admin");

            entity.Property(e => e.Dob).HasColumnName("dob");
            entity.Property(e => e.Firstname)
                .HasMaxLength(100)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(100)
                .HasColumnName("lastname");
            entity.Property(e => e.Userid)
                .HasMaxLength(8)
                .HasColumnName("userid");
        });

        modelBuilder.Entity<Assignment>(entity =>
        {
            entity.HasKey(e => e.Assignmentid).HasName("assignment_pkey");

            entity.ToTable("assignment");

            entity.Property(e => e.Assignmentid).HasColumnName("assignmentid");
            entity.Property(e => e.Assignmentname)
                .HasMaxLength(100)
                .HasColumnName("assignmentname");
            entity.Property(e => e.Catid).HasColumnName("catid");
            entity.Property(e => e.Duedate)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("duedate");
            entity.Property(e => e.Pointval).HasColumnName("pointval");

            entity.HasOne(d => d.Cat).WithMany(p => p.Assignments)
                .HasForeignKey(d => d.Catid)
                .HasConstraintName("assignment_catid_fkey");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Catid).HasName("category_pkey");

            entity.ToTable("category");

            entity.HasIndex(e => new { e.Catname, e.Classid }, "category_catname_classid_key").IsUnique();

            entity.Property(e => e.Catid).HasColumnName("catid");
            entity.Property(e => e.Catname)
                .HasMaxLength(100)
                .HasColumnName("catname");
            entity.Property(e => e.Classid).HasColumnName("classid");
            entity.Property(e => e.Gradingweight).HasColumnName("gradingweight");

            entity.HasOne(d => d.Class).WithMany(p => p.Categories)
                .HasForeignKey(d => d.Classid)
                .HasConstraintName("category_classid_fkey");
        });

        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Classid).HasName("class_pkey");

            entity.ToTable("class");

            entity.HasIndex(e => new { e.Year, e.Season, e.Catalogid }, "class_year_season_catalogid_key").IsUnique();

            entity.Property(e => e.Classid).HasColumnName("classid");
            entity.Property(e => e.Catalogid)
                .HasMaxLength(5)
                .HasColumnName("catalogid");
            entity.Property(e => e.Endtime).HasColumnName("endtime");
            entity.Property(e => e.Location)
                .HasMaxLength(100)
                .HasColumnName("location");
            entity.Property(e => e.Professorid)
                .HasMaxLength(8)
                .HasColumnName("professorid");
            entity.Property(e => e.Season)
                .HasMaxLength(6)
                .HasColumnName("season");
            entity.Property(e => e.Starttime).HasColumnName("starttime");
            entity.Property(e => e.Year).HasColumnName("year");

            entity.HasOne(d => d.Catalog).WithMany(p => p.Classes)
                .HasPrincipalKey(p => p.Catalogid)
                .HasForeignKey(d => d.Catalogid)
                .HasConstraintName("class_catalogid_fkey");

            entity.HasOne(d => d.Professor).WithMany(p => p.Classes)
                .HasForeignKey(d => d.Professorid)
                .HasConstraintName("class_professorid_fkey");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => new { e.Catalogid, e.Coursenum }).HasName("course_pkey");

            entity.ToTable("course");

            entity.HasIndex(e => e.Catalogid, "course_catalogid_key").IsUnique();

            entity.Property(e => e.Catalogid)
                .HasMaxLength(5)
                .HasColumnName("catalogid");
            entity.Property(e => e.Coursenum)
                .HasMaxLength(4)
                .HasColumnName("coursenum");
            entity.Property(e => e.Coursename)
                .HasMaxLength(100)
                .HasColumnName("coursename");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => new { e.Subject, e.Depname }).HasName("department_pkey");

            entity.ToTable("department");

            entity.HasIndex(e => e.Subject, "department_subject_key").IsUnique();

            entity.Property(e => e.Subject)
                .HasMaxLength(4)
                .HasColumnName("subject");
            entity.Property(e => e.Depname)
                .HasMaxLength(100)
                .HasColumnName("depname");
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => new { e.Userid, e.Classid }).HasName("enrollment_pkey");

            entity.ToTable("enrollment");

            entity.Property(e => e.Userid)
                .HasMaxLength(8)
                .HasColumnName("userid");
            entity.Property(e => e.Classid)
                .ValueGeneratedOnAdd()
                .HasColumnName("classid");
            entity.Property(e => e.Grade)
                .HasMaxLength(2)
                .HasColumnName("grade");

            entity.HasOne(d => d.Class).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.Classid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("enrollment_classid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("enrollment_userid_fkey");
        });

        modelBuilder.Entity<Lmsuser>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("lmsuser_pkey");

            entity.ToTable("lmsuser");

            entity.Property(e => e.Userid)
                .HasMaxLength(8)
                .HasColumnName("userid");
            entity.Property(e => e.Dob).HasColumnName("dob");
            entity.Property(e => e.Firstname)
                .HasMaxLength(100)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(100)
                .HasColumnName("lastname");
        });

        modelBuilder.Entity<Professor>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("professor_pkey");

            entity.ToTable("professor");

            entity.Property(e => e.Userid)
                .HasMaxLength(8)
                .HasColumnName("userid");
            entity.Property(e => e.Dob).HasColumnName("dob");
            entity.Property(e => e.Employer)
                .HasMaxLength(4)
                .HasColumnName("employer");
            entity.Property(e => e.Firstname)
                .HasMaxLength(100)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(100)
                .HasColumnName("lastname");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Userid).HasName("student_pkey");

            entity.ToTable("student");

            entity.Property(e => e.Userid)
                .HasMaxLength(8)
                .HasColumnName("userid");
            entity.Property(e => e.Dob).HasColumnName("dob");
            entity.Property(e => e.Firstname)
                .HasMaxLength(100)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(100)
                .HasColumnName("lastname");
            entity.Property(e => e.Major).HasColumnName("major");
        });

        modelBuilder.Entity<Submission>(entity =>
        {
            entity.HasKey(e => new { e.Submittedat, e.Userid }).HasName("submissions_pkey");

            entity.ToTable("submissions");

            entity.Property(e => e.Submittedat)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("submittedat");
            entity.Property(e => e.Userid)
                .HasMaxLength(8)
                .HasColumnName("userid");
            entity.Property(e => e.Assignmentid).HasColumnName("assignmentid");
            entity.Property(e => e.Contents)
                .HasMaxLength(8192)
                .HasColumnName("contents");
            entity.Property(e => e.Score).HasColumnName("score");

            entity.HasOne(d => d.Assignment).WithMany(p => p.Submissions)
                .HasForeignKey(d => d.Assignmentid)
                .HasConstraintName("submissions_assignmentid_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Submissions)
                .HasForeignKey(d => d.Userid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("submissions_userid_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
