using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StudyPractice.Models;

namespace StudyPractice.Context;

public partial class TesterContext : DbContext
{
    public TesterContext()
    {
    }

    public TesterContext(DbContextOptions<TesterContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AbsenceCalendar> AbsenceCalendars { get; set; }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<Office> Offices { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<ResumeLibrary> ResumeLibraries { get; set; }

    public virtual DbSet<Status> Statuses { get; set; }

    public virtual DbSet<TraningCalendar> TraningCalendars { get; set; }

    public virtual DbSet<TypesMaterial> TypesMaterials { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=195.161.68.85;Database=tester;Username=gamov;password=1423");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AbsenceCalendar>(entity =>
        {
            entity.HasKey(e => e.AbsenceCalendarId).HasName("absence_calendar_pk");

            entity.ToTable("absence_calendar", "Ogromnay");

            entity.Property(e => e.AbsenceCalendarId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("absence_calendar_id");
            entity.Property(e => e.AbsenceCalendarResone)
                .HasColumnType("character varying")
                .HasColumnName("absence_calendar_resone");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.EndDate).HasColumnName("end_date");
            entity.Property(e => e.ReplacementId).HasColumnName("replacement_id");
            entity.Property(e => e.StartDate).HasColumnName("start_date");

            entity.HasOne(d => d.Employee).WithMany(p => p.AbsenceCalendarEmployees)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("absence_calendar_employees_fk");

            entity.HasOne(d => d.Replacement).WithMany(p => p.AbsenceCalendarReplacements)
                .HasForeignKey(d => d.ReplacementId)
                .HasConstraintName("alendar_employees_fk");
        });

        modelBuilder.Entity<Area>(entity =>
        {
            entity.HasKey(e => e.AreaId).HasName("areas_pk");

            entity.ToTable("areas", "Ogromnay");

            entity.Property(e => e.AreaId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("area_id");
            entity.Property(e => e.AreaName)
                .HasColumnType("character varying")
                .HasColumnName("area_name");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("departments_pk");

            entity.ToTable("departments", "Ogromnay");

            entity.Property(e => e.DepartmentId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("department_id");
            entity.Property(e => e.DepartmentDescription)
                .HasColumnType("character varying")
                .HasColumnName("department_description");
            entity.Property(e => e.DepartmentHeadId).HasColumnName("department_head_id");
            entity.Property(e => e.DepartmentName)
                .HasColumnType("character varying")
                .HasColumnName("department_name");

            entity.HasOne(d => d.DepartmentHead).WithMany(p => p.InverseDepartmentHead)
                .HasForeignKey(d => d.DepartmentHeadId)
                .HasConstraintName("departments_departments_fk");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeId).HasName("employees_pk");

            entity.ToTable("employees", "Ogromnay");

            entity.Property(e => e.EmployeId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("employe_id");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.EmployeAssistentId).HasColumnName("employe_assistent_id");
            entity.Property(e => e.EmployeBirth).HasColumnName("employe_birth");
            entity.Property(e => e.EmployeChiefId).HasColumnName("employe_chief_id");
            entity.Property(e => e.EmployeEmail)
                .HasColumnType("character varying")
                .HasColumnName("employe_email");
            entity.Property(e => e.EmployeFio)
                .HasColumnType("character varying")
                .HasColumnName("employe_fio");
            entity.Property(e => e.EmployePhone)
                .HasColumnType("character varying")
                .HasColumnName("employe_phone");
            entity.Property(e => e.OfficeId).HasColumnName("office_id");
            entity.Property(e => e.PositionId).HasColumnName("position_id");

            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("employees_departments_fk");

            entity.HasOne(d => d.EmployeAssistent).WithMany(p => p.InverseEmployeAssistent)
                .HasForeignKey(d => d.EmployeAssistentId)
                .HasConstraintName("employees_employees_fk");

            entity.HasOne(d => d.EmployeChief).WithMany(p => p.InverseEmployeChief)
                .HasForeignKey(d => d.EmployeChiefId)
                .HasConstraintName("employees_employees_fk_1");

            entity.HasOne(d => d.Office).WithMany(p => p.Employees)
                .HasForeignKey(d => d.OfficeId)
                .HasConstraintName("employees_offices_fk");

            entity.HasOne(d => d.Position).WithMany(p => p.Employees)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("employees_positions_fk");
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("events_pk");

            entity.ToTable("events", "Ogromnay");

            entity.Property(e => e.EventId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("event_id");
            entity.Property(e => e.DateTime)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("date_time");
            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.EventDescription)
                .HasColumnType("character varying")
                .HasColumnName("event_description");
            entity.Property(e => e.EventName)
                .HasColumnType("character varying")
                .HasColumnName("event_name");
            entity.Property(e => e.EventResponsible).HasColumnName("event_responsible");
            entity.Property(e => e.StatusEventsId).HasColumnName("status_events_id");
            entity.Property(e => e.TypeEventId).HasColumnName("type_event_id");

            entity.HasOne(d => d.Department).WithMany(p => p.Events)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("events_departments_fk");

            entity.HasOne(d => d.EventResponsibleNavigation).WithMany(p => p.Events)
                .HasForeignKey(d => d.EventResponsible)
                .HasConstraintName("events_employees_fk");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.MaterialId).HasName("materials_pk");

            entity.ToTable("materials", "Ogromnay");

            entity.Property(e => e.MaterialId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("material_id");
            entity.Property(e => e.ApprovalDate).HasColumnName("approval_date");
            entity.Property(e => e.AreaId).HasColumnName("area_id");
            entity.Property(e => e.Author).HasColumnName("author");
            entity.Property(e => e.MaterialName)
                .HasColumnType("character varying")
                .HasColumnName("material_name");
            entity.Property(e => e.ModificationDate).HasColumnName("modification_date");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.TypeMaterialId).HasColumnName("type_material_id");

            entity.HasOne(d => d.Area).WithMany(p => p.Materials)
                .HasForeignKey(d => d.AreaId)
                .HasConstraintName("materials_areas_fk");

            entity.HasOne(d => d.AuthorNavigation).WithMany(p => p.Materials)
                .HasForeignKey(d => d.Author)
                .HasConstraintName("materials_employees_fk");

            entity.HasOne(d => d.Status).WithMany(p => p.Materials)
                .HasForeignKey(d => d.StatusId)
                .HasConstraintName("materials_statuses_fk");

            entity.HasOne(d => d.TypeMaterial).WithMany(p => p.Materials)
                .HasForeignKey(d => d.TypeMaterialId)
                .HasConstraintName("materials_types_material_fk");
        });

        modelBuilder.Entity<Office>(entity =>
        {
            entity.HasKey(e => e.OfficeId).HasName("cabinet_pk");

            entity.ToTable("offices", "Ogromnay");

            entity.Property(e => e.OfficeId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("office_id");
            entity.Property(e => e.OfficeNumder)
                .HasColumnType("character varying")
                .HasColumnName("office_numder");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.PositionId).HasName("positions_pk");

            entity.ToTable("positions", "Ogromnay");

            entity.Property(e => e.PositionId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("position_id");
            entity.Property(e => e.PositionName)
                .HasColumnType("character varying")
                .HasColumnName("position_name");
        });

        modelBuilder.Entity<ResumeLibrary>(entity =>
        {
            entity.HasKey(e => e.ResumeLibraryId).HasName("resume_library_pk");

            entity.ToTable("resume_library", "Ogromnay");

            entity.Property(e => e.ResumeLibraryId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("resume_library_id");
            entity.Property(e => e.CandidateName)
                .HasColumnType("character varying")
                .HasColumnName("candidate_name");
            entity.Property(e => e.PositionId).HasColumnName("position_id");
            entity.Property(e => e.ReceivedDate).HasColumnName("received_date");
            entity.Property(e => e.Resume).HasColumnName("resume");

            entity.HasOne(d => d.Position).WithMany(p => p.ResumeLibraries)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("resume_library_positions_fk");
        });

        modelBuilder.Entity<Status>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("statuses_pk");

            entity.ToTable("statuses", "Ogromnay");

            entity.Property(e => e.StatusId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("status_id");
            entity.Property(e => e.StatusName)
                .HasColumnType("character varying")
                .HasColumnName("status_name");
        });

        modelBuilder.Entity<TraningCalendar>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("traning_calendar_pk");

            entity.ToTable("traning_calendar", "Ogromnay");

            entity.Property(e => e.Id)
                .UseIdentityAlwaysColumn()
                .HasColumnName("id");
            entity.Property(e => e.Classification)
                .HasColumnType("character varying")
                .HasColumnName("classification");
            entity.Property(e => e.DateFinish).HasColumnName("date_finish");
            entity.Property(e => e.DateStart).HasColumnName("date_start");
            entity.Property(e => e.EmployeId).HasColumnName("employe_id");
            entity.Property(e => e.MaterialId).HasColumnName("material_id");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");

            entity.HasOne(d => d.Employe).WithMany(p => p.TraningCalendars)
                .HasForeignKey(d => d.EmployeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("traning_calendar_employees_fk");

            entity.HasOne(d => d.Material).WithMany(p => p.TraningCalendars)
                .HasForeignKey(d => d.MaterialId)
                .HasConstraintName("traning_calendar_materials_fk");
        });

        modelBuilder.Entity<TypesMaterial>(entity =>
        {
            entity.HasKey(e => e.TypeMaterialId).HasName("newtable_pk");

            entity.ToTable("types_material", "Ogromnay");

            entity.Property(e => e.TypeMaterialId)
                .UseIdentityAlwaysColumn()
                .HasColumnName("type_material_id");
            entity.Property(e => e.TypeMaterialName)
                .HasColumnType("character varying")
                .HasColumnName("type_material_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
