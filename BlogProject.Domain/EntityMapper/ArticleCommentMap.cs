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
    public class ArticleCommentMap : IEntityTypeConfiguration<ArticleComment>
    {
        public void Configure(EntityTypeBuilder<ArticleComment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x=> x.CommentText)
                .HasMaxLength(500)
                .HasColumnType("nvarchar(500)")
                .IsRequired();

            builder.Property(x => x.DateCreated)
               .HasColumnType("datetime")
               .IsRequired();
        }
    }
}
