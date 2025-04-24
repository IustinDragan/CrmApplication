//using CRMRealEstate.DataAccess.Entities;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Builders;

//namespace CRMRealEstate.DataAccess.Configs
//{
//    public class PropertyConfiguration : IEntityTypeConfiguration<Property>
//    {
//        public void Configure(EntityTypeBuilder<Property> builder)
//        {
//            builder.HasOne(p => p.Announcement)
//                .WithOne()
//                .HasForeignKey<Property>(p => p.AnnouncementId);
//        }
//    }
//}
