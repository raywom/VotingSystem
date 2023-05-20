using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<AppUser>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        
        public DataContext()
        {
        }
        
        public DbSet<Choice> Choices { get; set; }
        public DbSet<Poll> Polls { get; set; }
        public DbSet<Vote> Voters { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<UserFollowing> UserFollowings { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Vote>(x => x.HasKey(aa => new { aa.AppUserId, ActivityId = aa.PollId }));

            builder.Entity<Vote>()
                .HasOne(u => u.AppUser)
                .WithMany(u => u.Votes)
                .HasForeignKey(aa => aa.AppUserId);

            builder.Entity<Vote>()
                .HasOne(u => u.Poll)
                .WithMany(u => u.Voters)
                .HasForeignKey(aa => aa.PollId);

            builder.Entity<Comment>()
                .HasOne(a => a.Poll)
                .WithMany(c => c.Comments)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<UserFollowing>(b =>
            {
                b.HasKey(k => new { k.ObserverId, k.TargetId });

                b.HasOne(o => o.Observer)
                    .WithMany(f => f.Followings)
                    .HasForeignKey(o => o.ObserverId)
                    .OnDelete(DeleteBehavior.Cascade);
                b.HasOne(t => t.Target)
                    .WithMany(f => f.Followers)
                    .HasForeignKey(t => t.TargetId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
            
            builder.Entity<Choice>()
                .HasOne(a => a.Poll)
                .WithMany(c => c.Choices)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}