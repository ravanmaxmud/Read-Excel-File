using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Read_Excel_File.Contracts.Identity;
using Read_Excel_File.Database.Models;

namespace Read_Excel_File.Database.Configuration
{
    public class RoleConfigurations : IEntityTypeConfiguration<Role>
    {

        private int _idCounter = 1;

        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder
               .ToTable("Roles");


            builder
                .HasData(
                    new Role
                    {
                        Id = _idCounter++,
                        Name = RoleNames.ADMIN,
                     
                    },
                    new Role
                    {
                        Id = _idCounter++,
                        Name = RoleNames.CLIENT,
                    }
          );
        }
    }
}
