using EW.TaskManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EW.TaskManagement.Infrastructure.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.Content)
               .IsRequired()
               .HasMaxLength(1000);

        builder.Property(c => c.CreatedAt)
               .IsRequired();

        builder.Property(c => c.TaskItemId)
               .IsRequired();

        builder.Property(c => c.UserId)
               .IsRequired();

        builder.HasOne(c => c.TaskItem)
               .WithMany(t => t.Comments)
               .HasForeignKey(c => c.TaskItemId)
               .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(c => c.User)
               .WithMany(u => u.Comments)
               .HasForeignKey(c => c.UserId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(c => c.TaskItemId);
        builder.HasIndex(c => c.UserId);
    }
}