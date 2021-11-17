using MailSystem.Core.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MailSystem.Repository.EntityMapping
{
    public class ReceiverModelMap : IEntityTypeConfiguration<ReceiverModel>
    {
        public void Configure(EntityTypeBuilder<ReceiverModel> builder)
        {
            builder.ToTable("tb_receiver");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasColumnName("receiver_id")
                .IsRequired();

            builder.Property(x => x.AddressId)
                .HasColumnName("address_id")
                .IsRequired();

            builder.Property(x => x.Name)
               .HasColumnName("name")
               .HasMaxLength(100)
               .IsRequired();

            builder.Property(x => x.Document)
               .HasColumnName("document")
               .HasMaxLength(64)
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

            builder.HasOne(x => x.Address)
                .WithOne(x => x.Receiver);

            builder.HasMany(x => x.Deliveries)
                .WithOne(x => x.Receiver);
        }
    }
}
