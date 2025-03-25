using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.Infrastructure.Annotations;
using GymLedger.Models.Models;

namespace GymLedger.Models.Mappings
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary key
            HasKey(u => u.Id);

            // Properties
            Property(u => u.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(64)
                .HasColumnType("nvarchar");

            Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(128)
                .HasColumnType("nvarchar");

            Property(u => u.UniqueId)
                .IsRequired()
                .HasMaxLength(64)
                .HasColumnType("nvarchar")
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_User") { IsUnique = true }));

            Property(u => u.LastLogin)
                .IsOptional();

            Property(u => u.UserRole).IsRequired();

            Property(u => u.IsLockedOut).IsOptional();
            Property(u => u.LockedOutDate).IsOptional();
            Property(u => u.FailedLoginAttempts).IsOptional();

            // Relationships
            HasMany(u => u.Sessions)
                .WithRequired(s => s.User) 
                .HasForeignKey(s => s.UserId)
                .WillCascadeOnDelete(false); 

            HasMany(u => u.PreviousLogins)
                .WithRequired(u => u.User)
                .HasForeignKey(u => u.UserId)
                .WillCascadeOnDelete(true);

            HasMany(u => u.PreviousPasswords)
                .WithRequired(u => u.User)
                .HasForeignKey(u => u.UserId)
                .WillCascadeOnDelete(true);

            // Table name
            ToTable("Users");
        }
    }
}
