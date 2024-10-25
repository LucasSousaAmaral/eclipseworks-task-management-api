using EW.TaskManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EW.TaskManagement.Infrastructure.Configurations;

public class ProjectConfiguration : IEntityTypeConfiguration<Project>
{
    public void Configure(EntityTypeBuilder<Project> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(p => p.UserId)
               .IsRequired();

        builder.HasOne(p => p.User)
               .WithMany(u => u.Projects)
               .HasForeignKey(p => p.UserId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(p => p.Tasks)
               .WithOne(t => t.Project)
               .HasForeignKey(t => t.ProjectId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(p => p.UserId);
    }
}