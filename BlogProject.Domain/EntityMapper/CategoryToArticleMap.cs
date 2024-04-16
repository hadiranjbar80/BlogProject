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
    public class CategoryToArticleMap : IEntityTypeConfiguration<CategoryToArticle>
    {
        public void Configure(EntityTypeBuilder<CategoryToArticle> builder)
        {
            builder.HasKey(cm => new { cm.CategoryId, cm.ArticleId });
            
        }
    }
}
