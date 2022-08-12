using Euphorolog.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Euphorolog.Database.ModelConfigurations
{
    public class StoriesConfiguration : IEntityTypeConfiguration<Stories>
    {
        public void Configure(EntityTypeBuilder<Stories> builder)
        {
            //storyId
            builder
                .HasKey("storyId");
            builder
                .Property(u => u.storyId)
                .IsRequired()
                .HasMaxLength(100);

            //storyNo
            builder
                .Property(s => s.storyNo)
                .IsRequired()
                .HasMaxLength(150);

            //storyTitle
            builder
                .Property(s => s.storyTitle)
                .IsRequired()
                .HasMaxLength(150);
            //storyDescription
            builder
                .Property(s => s.storyDescription)
                .IsRequired()
                .HasMaxLength(10005);
            //openingLines
            builder
                .Property(s => s.openingLines)
                .IsRequired()
                .HasMaxLength(150);
            //createdAt
            builder
                .Property(s => s.createdAt)
                .IsRequired();
            //updatedAt
            builder
                .Property(s => s.updatedAt)
                .IsRequired();

            //authorName
            builder
                .HasOne(s=>s.users)
                .WithMany(u => u.stories)
                .HasForeignKey(s=>s.authorName);
        }
    }
}
