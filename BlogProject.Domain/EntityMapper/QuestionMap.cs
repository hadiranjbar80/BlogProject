using BlogProject.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Domain.EntityMapper
{
    public class QuestionMap : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Description)
                .HasMaxLength(1000)
                .HasColumnType("nvarchar(1000)")
                .IsRequired();
            builder.Property(x => x.DateCreated)
                .HasColumnType("datetime")
                .IsRequired();
            builder.Property(x=>x.Title)
                .HasMaxLength(50)
                .HasColumnType("nvarchar(50)")
                .IsRequired();
        }
    }
}
