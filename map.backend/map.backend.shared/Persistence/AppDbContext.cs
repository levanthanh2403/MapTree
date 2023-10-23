using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using map.backend.shared.Entities.Auth;
using Microsoft.AspNetCore.Http;
using map.backend.shared.Entities.Map;

namespace map.backend.shared.Persistence
{
    public class AppDbContext : DbContext
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor contextAccessor) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            _contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var mapper = new Npgsql.NameTranslation.NpgsqlSnakeCaseNameTranslator();
            var types = modelBuilder.Model.GetEntityTypes().ToList();

            modelBuilder.HasSequence<int>("seq_location_id")
                .StartsAt(0)
                .HasMin(0)
                .HasMax(9999)
                .IncrementsBy(1)
                .IsCyclic(true);

            //modelBuilder.Entity<extb_data_import>()
            //            .Property(e => e.description)
            //            .HasColumnType("text");
            //modelBuilder.Entity<extb_log>()
            //            .Property(e => e.description)
            //            .HasColumnType("text");

            modelBuilder.Entity<tb_user>()
                .Property(e => e.limit)
                .HasDefaultValue(5);
            modelBuilder.Entity<tb_user>()
                .Property(e => e.status)
                .HasDefaultValue("O");
            modelBuilder.Entity<tb_user>()
                .Property(e => e.img)
                .HasColumnType("text");

            modelBuilder.Entity<tb_projects>()
                .Property(e => e.projectdetail)
                .HasColumnType("text");
            modelBuilder.Entity<tb_projects>()
                .Property(e => e.img)
                .HasColumnType("text");

            modelBuilder.Entity<tb_locations>()
                .Property(e => e.locationinfo)
                .HasColumnType("text");
            modelBuilder.Entity<tb_locations>()
                .Property(e => e.treeinfor)
                .HasColumnType("text");

            modelBuilder.Entity<tb_locations_history>()
                .Property(e => e.locationinfo)
                .HasColumnType("text");
            modelBuilder.Entity<tb_locations_history>()
                .Property(e => e.treeinfor)
                .HasColumnType("text");
        }
        public DbSet<tb_user> tb_user { get; set; }
        public DbSet<tb_screens> tb_screens { get; set; }
        public DbSet<tb_roles> tb_roles { get; set; }
        public DbSet<tb_role_mapping> tb_role_mapping { get; set; }
        public DbSet<tb_projects> tb_projects { get; set; }
        public DbSet<tb_locations> tb_locations { get; set; }
        public DbSet<tb_locations_history> tb_locations_history { get; set; }

        public string CurrentUser
        {
            get
            {
                var httpContext = _contextAccessor.HttpContext;
                var _key = "userid";
                if (httpContext.User.HasClaim(a => a.Type == _key))
                {
                    var claim = httpContext.User.FindFirst(_key);
                    return claim.Value.ToString();
                }
                throw new Exception("The request not exist userid info.");
            }
        }
    }
}
