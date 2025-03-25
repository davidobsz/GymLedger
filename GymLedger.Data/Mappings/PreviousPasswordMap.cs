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
    public class PreviousPasswordMap: EntityTypeConfiguration<PreviousPassword>
    {
        public PreviousPasswordMap()
        {
            // set primary key
            HasKey(t => t.Id);

            Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(t => t.DateAdded).IsRequired();
            Property(t => t.UserId).IsRequired();
            Property(t => t.Password).IsRequired();

            //table name - make plurals
            ToTable("PreviousPasswords");
        }
    }
}
