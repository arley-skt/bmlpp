﻿using DevIO.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Collections;

namespace DevIO.Data.Mappings
{
    public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Email)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(p => p.Senha)
                .IsRequired()
                .HasColumnType("varchar(14)");

            builder.Property(p => p.Ativo)
               .IsRequired()
               .HasColumnType("bit(1)")
               .HasConversion(ConverterProvider.GetBoolToBitArrayConverter());

            //// 1 : 1 => Fornecedor : Endereco
            //builder.HasOne(p => p.Endereco)
            //    .WithOne(e => e.Fornecedor);

            //// 1 : N => Fornecedor : Produtos
            //builder.HasMany(p => p.Produtos)
            //    .WithOne(p => p.Fornecedor)
            //    .HasForeignKey(p => p.FornecedorId);

            builder.ToTable("Usuarios");
        }
    }

  
}
