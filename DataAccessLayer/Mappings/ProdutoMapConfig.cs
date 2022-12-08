using Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Mappings
{
    internal class ProdutoMapConfig : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("PRODUTOS");
            //string -> DESCRICAO NVARCHAR(MAX) NULL
            //DESCRICAO VARCHAR(100) NOT NULL
            builder.Property(p => p.Descricao).IsRequired().HasMaxLength(100).IsUnicode(false);
            builder.Property(p => p.Preco).HasPrecision(10, 2);
            builder.Property(p => p.Estoque).HasPrecision(10, 2);
        }
    }
}
