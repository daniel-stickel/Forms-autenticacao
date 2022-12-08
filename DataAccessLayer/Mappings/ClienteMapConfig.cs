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
    internal class ClienteMapConfig : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("CLIENTES");
            builder.Property(c => c.Nome).HasMaxLength(70).IsRequired().IsUnicode(false);
            builder.Property(c => c.CPF).IsFixedLength().HasMaxLength(11).IsUnicode(false).IsRequired();
            builder.HasIndex(c => c.CPF).IsUnique().HasDatabaseName("UQ_CLIENTE_CPF");
            builder.HasIndex(c => c.Nome);
            builder.Property(c => c.Telefone).IsRequired().HasMaxLength(15).IsUnicode(false);
            builder.Property(c => c.Email).IsRequired().HasMaxLength(100).IsUnicode(false);
            builder.HasIndex(c => c.Email).IsUnique().HasDatabaseName("UQ_CLIENTE_EMAIL");
            builder.Property(c => c.DataNascimento).HasColumnType("date");
            builder.Property(c => c.Ativo).HasDefaultValue(true);
        }
    }
}
