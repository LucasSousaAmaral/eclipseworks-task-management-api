using EW.TaskManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EW.TaskManagement.Infrastructure.Configurations;
public class TaskHistoryConfiguration : IEntityTypeConfiguration<TaskHistory>
{
    public void Configure(EntityTypeBuilder<TaskHistory> builder)
    {
        builder.HasKey(h => h.Id);

        builder.Property(h => h.Changes)
               .IsRequired()
               .HasMaxLength(1000);

        builder.Property(h => h.ModifiedAt)
               .IsRequired();

        builder.Property(h => h.ModifiedByUserId)
               .IsRequired();

        builder.Property(h => h.TaskItemId)
               .IsRequired();

        builder.HasOne(h => h.TaskItem)
               .WithMany(t => t.Histories)
               .HasForeignKey(h => h.TaskItemId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(h => h.ModifiedByUser)
               .WithMany(u => u.TaskHistories)
               .HasForeignKey(h => h.ModifiedByUserId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(h => h.TaskItemId);
        builder.HasIndex(h => h.ModifiedByUserId);
    }
}