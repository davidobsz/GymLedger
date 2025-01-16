using GymLedger.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLedger.Data.Mappings
{
    public class SessionMap : EntityTypeConfiguration<Session>
    {
        public SessionMap()
        {
            // set primary key
            HasKey(t => t.Id);

            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.UniqueId).IsRequired().HasMaxLength(64).HasColumnType("nvarchar").HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Session") { IsUnique = true }));
            Property(t => t.DateAdded).IsRequired();
            Property(t => t.UserId).IsRequired();
            Property(t => t.Date).IsRequired();

            // one to many (1 session to many Sets) 
            HasMany(t => t.Sets)
            .WithRequired(s => s.Session) // Required navigation
            .HasForeignKey(s => s.SessionId) // FK in Sets
            .WillCascadeOnDelete(true);

            //table name - make plurals
            ToTable("Sessions");
        }
    }
}
