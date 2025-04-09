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
    public class OneRepMaxMap : EntityTypeConfiguration<OneRepMax>
    {
        public OneRepMaxMap()
        {
            // set primary key
            HasKey(t => t.Id);

            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.UniqueId).IsRequired().HasMaxLength(64).HasColumnType("nvarchar").HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("IX_OneRepMax") { IsUnique = true }));
            Property(t => t.DateAdded).IsRequired();
            Property(t => t.UserId).IsRequired();
            Property(t => t.Date).IsRequired();
            Property(t => t.Weight).IsRequired();
            Property(t => t.ExerciseId).IsRequired();

            //table name - make plurals
            ToTable("OneRepMaxes");
        }
    }
}
