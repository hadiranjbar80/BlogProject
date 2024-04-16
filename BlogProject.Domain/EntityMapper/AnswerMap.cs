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
    public class AnswerMap : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Description)
                .HasMaxLength(800)
                .HasColumnType("nvarchar(800)")
                .IsRequired();
            builder.Property(x => x.DateCreated)
                .HasColumnType("datetime")
                .IsRequired();
        }
    }
}
