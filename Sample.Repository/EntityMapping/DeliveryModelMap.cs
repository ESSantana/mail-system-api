using MailSystem.Core.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MailSystem.Repository.EntityMapping
{
    class DeliveryModelMap : IEntityTypeConfiguration<DeliveryModel>
    {
        public void Configure(EntityTypeBuilder<DeliveryModel> builder)
        {

            builder.ToTable("tb_delivery");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasColumnName("delivery_id")
                .IsRequired();

            builder.Property(x => x.ReceiverId)
                .IsRequired()
                .HasColumnName("receiver_id")
                .IsRequired();

            builder.Property(x => x.Description)
                .HasColumnName("description")
                .HasMaxLength(100);

            builder.Property(x => x.Type)
               .HasColumnName("type")
               .IsRequired();

            builder.Property(x => x.TrackCode)
               .HasColumnName("track_code")
               .HasMaxLength(45);

            builder.Property(x => x.DeliveredTo)
               .HasColumnName("delivered_to")
               .HasMaxLength(100);

            builder.Property(x => x.ArrivedAt)
                .HasColumnName("arrived_at")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(x => x.DeliveredAt)
               .HasColumnName("delivered_at")
               .ValueGeneratedOnUpdate();

            builder.Property(x => x.DeletedAt)
               .HasColumnName("deleted_at");

            builder.HasOne(x => x.Receiver)
                .WithMany(x => x.Deliveries);
        }
    }
}
