using DevIO.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Collections;

namespace DevIO.Data.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Descricao)
                .IsRequired()
                .HasColumnType("varchar(1000)");

            builder.Property(p => p.Ativo)
           .IsRequired()
           .HasColumnType("bit(1)")
           .HasConversion(ConverterProvider.GetBoolToBitArrayConverter());

            builder.ToTable("Produtos");
        }
    }

    public static class ConverterProvider
    {
        public static ValueConverter<bool, BitArray> GetBoolToBitArrayConverter()
        {
            return new ValueConverter<bool, BitArray>(
                value => new BitArray(new[] { value }),
                value => value.Get(0));
        }
    }


}
