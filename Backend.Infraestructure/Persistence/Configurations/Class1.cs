
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Backend.Infraestructure.Persistence.Configurations
{
   public class POCOTestClassConfiguration : IEntityTypeConfiguration<POCOTestClass>
    {
        public void Configure(EntityTypeBuilder<POCOTestClass> builder)
        {
            builder.HasKey(k => k.PocoID).Metadata.IsPrimaryKey();
            builder.Property(e => e.PocoID).ValueGeneratedOnAdd();
            builder.ToTable("POCOTestClass");
        }
    }
}
