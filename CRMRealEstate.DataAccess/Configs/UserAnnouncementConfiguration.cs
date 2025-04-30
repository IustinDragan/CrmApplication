using CRMRealEstate.DataAccess.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

class UserAnnouncementConfiguration : IEntityTypeConfiguration<UserAnnouncement>
{
    public void Configure(EntityTypeBuilder<UserAnnouncement> builder)
    {
        builder.HasKey(ua => ua.Id);

        builder.HasOne(ua => ua.Announcement)
            .WithMany(a => a.UserAnnouncements)
            .HasForeignKey(ua => ua.AnnouncementId);

        builder.HasOne(ua => ua.User)
            .WithMany(u => u.FavoritesAnnouncements)
            .HasForeignKey(ua => ua.UserId);
    }
}
