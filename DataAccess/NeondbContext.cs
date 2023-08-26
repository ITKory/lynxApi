using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace DataAccess;

public partial class NeondbContext : DbContext
{
    private readonly IConfiguration _config;
    public NeondbContext(IConfiguration configuration)
    {
        _config = configuration;
    }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Crew> Crews { get; set; }

    public virtual DbSet<CrewMate> CrewMates { get; set; }

    public virtual DbSet<FoundStat> FoundStats { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Profile> Profiles { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<SearchDeparture> SearchDepartures { get; set; }

    public virtual DbSet<SearchRequest> SearchRequests { get; set; }

    public virtual DbSet<SeekerRegistration> SeekerRegistrations { get; set; }

    public virtual DbSet<Tag> Tags { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         => optionsBuilder.UseNpgsql(_config.GetConnectionString("DbConnectionString"));
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("city_pkey");

            entity.ToTable("city");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Title).HasColumnName("title");
        });

        modelBuilder.Entity<Crew>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("crew_pkey");

            entity.ToTable("crew");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.IsArrived).HasColumnName("is_arrived");
            entity.Property(e => e.SearchDepartureId).HasColumnName("search_departure_id");
            entity.Property(e => e.Title).HasColumnName("title");

            entity.HasOne(d => d.SearchDeparture).WithMany(p => p.Crews)
                .HasForeignKey(d => d.SearchDepartureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("crew_search_departure_id_foreign");
        });

        modelBuilder.Entity<CrewMate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("crew_mates_pkey");

            entity.ToTable("crew_mates");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CrewId).HasColumnName("crew_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Crew).WithMany(p => p.CrewMates)
                .HasForeignKey(d => d.CrewId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("crew_mates_crew_id_foreign");

            entity.HasOne(d => d.User).WithMany(p => p.CrewMates)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("crew_mates_user_id_foreign");
        });

        modelBuilder.Entity<FoundStat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("found_stat_pkey");

            entity.ToTable("found_stat");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.SearchRequestId).HasColumnName("search_request_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.SearchRequest).WithMany(p => p.FoundStats)
                .HasForeignKey(d => d.SearchRequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("found_stat_search_request_id_foreign");

            entity.HasOne(d => d.User).WithMany(p => p.FoundStats)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("found_stat_user_id_foreign");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("location_pkey");

            entity.ToTable("location");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Latitude).HasColumnName("latitude");
            entity.Property(e => e.Longitude).HasColumnName("longitude");
        });

        modelBuilder.Entity<Profile>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("profile_pkey");

            entity.ToTable("profile");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.Call).HasColumnName("call");
            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Phone).HasColumnName("phone");
            entity.Property(e => e.RelativesPhone).HasColumnName("relatives_phone");

            entity.HasOne(d => d.City).WithMany(p => p.Profiles)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("profile_city_id_foreign");

            entity.HasOne(d => d.Location).WithMany(p => p.Profiles)
                .HasForeignKey(d => d.LocationId)
                .HasConstraintName("profile_location_id_foreign");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("role_pkey");

            entity.ToTable("role");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.TagId).HasColumnName("tag_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Tag).WithMany(p => p.Roles)
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("role_tag_id_foreign");

            entity.HasOne(d => d.User).WithMany(p => p.Roles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("role_user_id_foreign");
        });

        modelBuilder.Entity<SearchDeparture>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("search_departure_pkey");

            entity.ToTable("search_departure");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CartographerId).HasColumnName("cartographer_id");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.IsUrgent).HasColumnName("is_urgent");
            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.SearchAdministratorId).HasColumnName("search_administrator_id");
            entity.Property(e => e.SearchRequestId).HasColumnName("search_request_id");

            entity.HasOne(d => d.Cartographer).WithMany(p => p.SearchDepartureCartographers)
                .HasForeignKey(d => d.CartographerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("search_departure_cartographer_id_foreign");

            entity.HasOne(d => d.Location).WithMany(p => p.SearchDepartures)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("search_departure_location_id_foreign");

            entity.HasOne(d => d.SearchAdministrator).WithMany(p => p.SearchDepartureSearchAdministrators)
                .HasForeignKey(d => d.SearchAdministratorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("search_departure_search_administrator_id_foreign");

            entity.HasOne(d => d.SearchRequest).WithMany(p => p.SearchDepartures)
                .HasForeignKey(d => d.SearchRequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("search_departure_search_request_id_foreign");
        });

        modelBuilder.Entity<SearchRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("search_request_pkey");

            entity.ToTable("search_request");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Comment).HasColumnName("comment");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.DateOfFound).HasColumnName("date_of_found");
            entity.Property(e => e.DateOfLosee).HasColumnName("date_of_losee");
            entity.Property(e => e.Face).HasColumnName("face");
            entity.Property(e => e.IsActive).HasColumnName("is_active");
            entity.Property(e => e.IsFound).HasColumnName("is_found");
            entity.Property(e => e.IsDied).HasColumnName("is_died");
            entity.Property(e => e.LocationId).HasColumnName("location_id");
            entity.Property(e => e.LostId).HasColumnName("lost_id");
            entity.Property(e => e.MissingInformerId).HasColumnName("missing_informer_id");
            entity.Property(e => e.RequestAdministratorId).HasColumnName("request_administrator_id");

            entity.HasOne(d => d.Location).WithMany(p => p.SearchRequests)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("search_request_location_id_foreign");

            entity.HasOne(d => d.Lost).WithMany(p => p.SearchRequestLosts)
                .HasForeignKey(d => d.LostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("search_request_lost_id_foreign");

            entity.HasOne(d => d.MissingInformer).WithMany(p => p.SearchRequestMissingInformers)
                .HasForeignKey(d => d.MissingInformerId)
                .HasConstraintName("search_request_missing_informer_id_foreign");

            entity.HasOne(d => d.RequestAdministrator).WithMany(p => p.SearchRequests)
                .HasForeignKey(d => d.RequestAdministratorId)
                .HasConstraintName("search_request_request_administrator_id_foreign");
        });

        modelBuilder.Entity<SeekerRegistration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("seeker_registration_pkey");

            entity.ToTable("seeker_registration");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EndAt)
                .HasPrecision(0)
                .HasColumnName("end_at");
            entity.Property(e => e.SearchDepartureId).HasColumnName("search_departure_id");
            entity.Property(e => e.StartAt)
                .HasPrecision(0)
                .HasColumnName("start_at");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.SearchDeparture).WithMany(p => p.SeekerRegistrations)
                .HasForeignKey(d => d.SearchDepartureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("seeker_registration_search_departure_id_foreign");

            entity.HasOne(d => d.User).WithMany(p => p.SeekerRegistrations)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("seeker_registration_user_id_foreign");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("tag_pkey");

            entity.ToTable("tag");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Title).HasColumnName("title");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("user_pkey");

            entity.ToTable("user");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.Login).HasColumnName("login");
            entity.Property(e => e.Password).HasColumnName("password");
            entity.Property(e => e.ProfileId).HasColumnName("profile_id");

            entity.HasOne(d => d.Profile).WithMany(p => p.Users)
                .HasForeignKey(d => d.ProfileId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_profile_id_foreign");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
