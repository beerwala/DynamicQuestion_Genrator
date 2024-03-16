using Domain.Auditable;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options) 
        {
            
        }

        public DbSet<Assessment> Assessments { get; set; }
        public DbSet<AssessmentQuestons> AssessmentQuestons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Assessment>(builder =>
            {
                builder.HasKey(a=>a.Id);
                builder.Property(a=>a.name).IsRequired();
                builder.HasMany(a=>a.assessmentsQuestions)
                        .WithOne(aq=>aq.Assessment)
                        .HasForeignKey(aq=>aq.assessmentId);
            });
            modelBuilder.Entity<AssessmentQuestons>(builder =>
            {
                builder.HasKey(aq=>aq.Id);
                builder.Property(aq=>aq.response_Type)
                        .HasMaxLength(50)
                        .IsRequired();
                builder.HasOne(aq=>aq.Assessment)
                        .WithMany(a=>a.assessmentsQuestions)
                        .HasForeignKey(aq=>aq.assessmentId);   

            });

        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            AddAuditInfo();

            HandleProductDelete();
            return await base.SaveChangesAsync(cancellationToken);
        }

        #region private
        private void AddAuditInfo()
        {
            var entities = ChangeTracker.Entries<IEntity>().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            var utcNow = DateTime.UtcNow;
            //var user = _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? appUser;

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    entity.Entity.CreatedOnUtc = utcNow;
                    entity.Entity.CreatedBy = "sushant";
                    entity.Entity.LastModifiedOnUtc = utcNow;
                    entity.Entity.LastModifiedBy = null;
                }
                else if (entity.State == EntityState.Modified)
                {
                    entity.Entity.LastModifiedOnUtc = utcNow;
                    entity.Entity.LastModifiedBy = "Sushant";
                }
            }
        }
        private void HandleProductDelete()
        {
            var entities = ChangeTracker.Entries()
                         .Where(e => e.State == EntityState.Deleted);

            foreach (var entity in entities)
            {
                entity.State = EntityState.Modified;
                var assessment = entity.Entity as Assessment;
                if (assessment != null)
                {
                    assessment.IsDeleted = true;
                    assessment.LastModifiedOnUtc = DateTime.UtcNow;
                    assessment.LastModifiedBy = "Admin";
                }
                var assessmentQuestions = entity.Entity as AssessmentQuestons;
                if (assessmentQuestions != null)
                {
                    assessment.IsDeleted = true;
                    assessment.LastModifiedOnUtc = DateTime.UtcNow;
                    assessment.LastModifiedBy = "Admin";
                }
            }
            

        }
        #endregion

    }
}

