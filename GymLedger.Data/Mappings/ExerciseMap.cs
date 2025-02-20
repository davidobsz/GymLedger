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
    public class ExerciseMap : EntityTypeConfiguration<Exercise>
    {
        public ExerciseMap()
        {
            HasKey(e => e.Id);
            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.UniqueId).IsRequired().HasMaxLength(64).HasColumnType("nvarchar").HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_Exercise") { IsUnique = true }));
            Property(t => t.UserId).IsRequired();
            Property(t => t.DateAdded).IsRequired();
            Property(t => t.Name).HasMaxLength(64).HasColumnType("nvarchar").IsRequired();

            //table name - make plural
            ToTable("Exercises");
        }
    }
}
