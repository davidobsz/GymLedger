using GymLedger.Models;
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
    public class SetMap : EntityTypeConfiguration<Set>
    {
        public SetMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(t => t.UniqueId).IsRequired().HasMaxLength(64).HasColumnType("nvarchar").HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Set") { IsUnique = true }));
            Property(t => t.SetNumber).IsRequired();
            Property(t => t.Weight).IsRequired();
            Property(t => t.Reps).IsRequired();

            // Foreign key configuration (many-to-one relationship)
            HasRequired(t => t.Session).WithMany(s => s.Sets).HasForeignKey(t => t.SessionId);

            // Table name
            ToTable("Sets");
        }
    }
}
