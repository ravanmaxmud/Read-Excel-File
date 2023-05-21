using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Read_Excel_File.Database.Models;

namespace Read_Excel_File.Database.Configuration
{
    public class BasketProductConfiguration : IEntityTypeConfiguration<BasketProduct>
    {
        public void Configure(EntityTypeBuilder<BasketProduct> builder)
        {
            builder
                .ToTable("basket-products");

            builder
             .HasOne(bp => bp.Basket)
             .WithMany(basket => basket.BasketProducts)
             .HasForeignKey(bp => bp.BasketId);
        }
    }
}
