using MailSystem.Core.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace MailSystem.Repository.EntityMapping
{
    public class DocumentModelMap : IEntityTypeConfiguration<DocumentModel>
    {
        public void Configure(EntityTypeBuilder<DocumentModel> builder)
        {
            builder.ToTable("tb_document");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasColumnName("document_id")
                .IsRequired();

            builder.Property(x => x.ReceiverId)
                .HasColumnName("receiver_id")
                .IsRequired();

            builder.Property(x => x.Value)
               .HasColumnName("value")
               .HasMaxLength(64)
               .IsRequired();

            builder.Property(x => x.Type)
               .HasColumnName("type")
               .HasMaxLength(20)
               .IsRequired();

            builder.Property(x => x.CreatedAt)
                .HasColumnName("created_at")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.UpdatedAt)
               .HasColumnName("updated_at")
               .ValueGeneratedOnUpdate();

            builder.Property(x => x.DeletedAt)
               .HasColumnName("deleted_at");

            builder.HasOne(x => x.Receiver)
                .WithMany(x => x.Documents);
        }
    }
}
