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
    public class ArticleMap : IEntityTypeConfiguration<Article>
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=>x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("nvarchar(100)");

            builder.Property(x => x.ShortDescription)
                .HasMaxLength(100)
                .HasColumnType("nvarchar(100)")
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnType("nvarchar(1000)")
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(x => x.Tags)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnType("nvarchar(200)");

            builder.Property(x => x.ImageName)
                .HasMaxLength(50)
                .HasColumnType("nvarchar(50)")
                .IsRequired();

            builder.Property(x => x.DateCreated)
                .HasColumnType("datetime")
                .IsRequired();
        }
    }
}
