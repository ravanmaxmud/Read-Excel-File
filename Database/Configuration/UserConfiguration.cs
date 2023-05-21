using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Read_Excel_File.Database.Models;
using Read_Excel_File.Contracts.Identity;

namespace Read_Excel_File.Database.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        private int _idCounter = 1;
        public void Configure(EntityTypeBuilder<User> builder)
        {

            builder
            .ToTable("Users");

            builder
                .HasData(
                    new User
                    {
                        Id = _idCounter++,
                        Email = "admin@gmail.com",
                        Password = "123",
                        RoleId = 1,


                    },
                     new User
                     {
                         Id = _idCounter++,
                         Email = "client@gmail.com",
                         Password = "123",
                         RoleId = 2,


                     });

        }
    }
}
