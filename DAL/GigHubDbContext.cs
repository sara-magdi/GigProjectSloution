using DAL.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL
{
    public partial class GigHubDbContext : IdentityDbContext<User, Role, string,
        UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public GigHubDbContext(DbContextOptions<GigHubDbContext>options)
            :base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Attendance>()
                .HasOne(e => e.Gig)
                .WithMany(e=>e.Attendances)
                .HasForeignKey(e=>e.GigId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Attendance>()
                .HasKey(e => new { e.GigId, e.AttendeeId });

            builder.Entity<User>()
             .HasMany(u => u.Followers).WithOne(tr => tr.Follower)
             .HasForeignKey(e => e.FollowerId)
             .IsRequired(false);
            builder.Entity<User>()
            .HasMany(u => u.Followees).WithOne(tr => tr.Followee)
             .HasForeignKey(e => e.FolloweeId)
            .IsRequired(false);

            builder.Entity<Following>()
               .HasKey(p => new { p.FollowerId, p.FolloweeId });

            base.OnModelCreating(builder);
            builder.Entity<UserNotification>()
                     .HasOne(p => p.User).WithMany(p => p.UserNotifications)
                     .HasForeignKey(p => p.UserId)
                     .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<UserNotification>()
                .HasKey(p => new { p.UserId, p.NotificationId });

        }
        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Following> Followings { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<Notification> Notifications { get; set; }
    }
}
