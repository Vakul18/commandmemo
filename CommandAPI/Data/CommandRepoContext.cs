using CommandAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace CommandAPI.Data
{
    public class CommandRepoContext : IdentityDbContext
    {
        public CommandRepoContext(DbContextOptions<CommandRepoContext> options ): base(options)
        {
            
        }

        internal DbSet<Command> Commands { get; set; }

        internal DbSet<Blog> Blogs { get; set; }

        internal DbSet<Post> Posts { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           /* modelBuilder.Entity<Command>().HasData(
                new Command
                {
                    Id = 169,
                    HowTo = "",
                    Line = "",
                    Platform = ""
                }
            );*/
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Post>().Property<int>("BlogForeignKey");
            modelBuilder.Entity<Blog>().HasMany<Post>(x=> x.Posts).WithOne(x=>x.Blog)
            .HasForeignKey("BlogForeignKey").IsRequired();
            
        }
    }
}