using MailSystem.Core.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.Repository.Context;

namespace MailSystem.Repository.EntityMapping
{
    public class AddressModelMap : IEntityTypeConfiguration<AddressModel>
    {
        public readonly DbOptions _dbOptions;

        public AddressModelMap(DbOptions dbOptions)
        {
            _dbOptions = dbOptions;
        }

        public void Configure(EntityTypeBuilder<AddressModel> builder)
        {
            builder.ToTable("tb_address");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .IsRequired()
                .HasColumnName("address_id")
                .IsRequired();

            builder.Property(x => x.Street)
                .HasColumnName("street")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.Number)
               .HasColumnName("number")
               .HasMaxLength(5)
               .IsRequired();

            builder.Property(x => x.Neighborhood)
               .HasColumnName("neighborhood")
               .HasMaxLength(50)
               .IsRequired();

            builder.Property(x => x.ZipCode)
               .HasColumnName("zipcode")
               .HasMaxLength(8)
               .HasDefaultValue("43900000")
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
                .WithOne(x => x.Address);
        }
    }
}
