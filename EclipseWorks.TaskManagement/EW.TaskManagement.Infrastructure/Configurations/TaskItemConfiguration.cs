using EW.TaskManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EW.TaskManagement.Infrastructure.Configurations;

public class TaskItemConfiguration : IEntityTypeConfiguration<TaskItem>
{
    public void Configure(EntityTypeBuilder<TaskItem> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Title)
               .IsRequired()
               .HasMaxLength(200);

        builder.Property(t => t.Description)
               .HasMaxLength(1000);

        builder.Property(t => t.DueDate)
               .IsRequired();

        builder.Property(t => t.Status)
               .IsRequired()
               .HasConversion<string>();

        builder.Property(t => t.Priority)
               .IsRequired()
               .HasConversion<string>();

        builder.Property(t => t.CompletedAt)
               .IsRequired(false);

        builder.Property(t => t.ProjectId)
               .IsRequired();

        builder.HasOne(t => t.Project)
               .WithMany(p => p.Tasks)
               .HasForeignKey(t => t.ProjectId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.Comments)
               .WithOne(c => c.TaskItem)
               .HasForeignKey(c => c.TaskItemId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(t => t.Histories)
               .WithOne(h => h.TaskItem)
               .HasForeignKey(h => h.TaskItemId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(t => t.ProjectId);
        builder.HasIndex(t => t.Status);
    }
}