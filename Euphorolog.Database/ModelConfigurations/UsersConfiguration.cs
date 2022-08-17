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
    public class UsersConfiguration : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            //userName
            builder
                .HasKey("userName");
            builder
                .Property(u => u.userName)
                .IsRequired()
                .HasMaxLength(55);
            builder
                .HasIndex(u => u.userName)
                .IsUnique();
            //fullName
            builder
                .Property(u => u.fullName)
                .IsRequired()
                .HasMaxLength(255);
            //eMail
            builder
                .Property(u => u.eMail)
                .IsRequired()
                .HasMaxLength(400);
            builder
                .HasIndex(u => u.eMail)
                .IsUnique();
            //passwordHash
            builder
                .Property(u => u.passwordHash)
                .IsRequired();
            //passwordSalt
            builder
                .Property(u => u.passwordSalt)
                .IsRequired();
            //passChangedAt
            builder
                .Property(u => u.passChangedAt)
                .IsRequired();
        }
    }
}
