namespace Data
{
    using System.Data.Entity;

    using Migrations;

    using Microsoft.AspNet.Identity.EntityFramework;

    using Models;
    using Models.Images;
    using Models.Videos;

    public class ESportsEventsContext : IdentityDbContext<RegisteredUser>
    {
        public ESportsEventsContext()
            : base("ESportsEventsContext", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ESportsEventsContext, Configuration>());
        }

        //public DbSet<RegisteredUser> RegisteredUsers { get; set; }

        public DbSet<Player> Players { get; set; }

        //public DbSet<Sponsor> Sponsors { get; set; }

        public DbSet<MatchImage> MatchImages { get; set; }

        public DbSet<MapImage> MapImages { get; set; }

        public DbSet<PlayerImage> PlayerImages { get; set; }

        public DbSet<Logo> Logos { get; set; }

        public DbSet<ArticleImage> ArticleImages { get; set; }

        public DbSet<EventImage> EventImages { get; set; }

        public DbSet<TeamLogo> TeamLogos { get; set; }

        public DbSet<Flag> Flags { get; set; }

        public DbSet<ArticleVideo> ArticleVideos { get; set; }

        public DbSet<MatchVideo> MatchVideos { get; set; }

        public DbSet<EventVideo> EventVideos { get; set; }

        public DbSet<Event> Events { get; set; }

        public DbSet<Match> Matches { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Map> Maps { get; set; }

        public DbSet<Article> Articles { get; set; }

        public DbSet<Team> Teams { get; set; }

        public DbSet<Country> Countries { get; set; }

        public DbSet<PlayerEventRanking> PlayerEventRankings { get; set; }

        public DbSet<Season> Seasons { get; set; }

        public DbSet<City> Cities { get; set; }

        public DbSet<Venue> Venues { get; set; }

        public DbSet<Round> Rounds { get; set; }

        public static ESportsEventsContext Create()
        {
            return new ESportsEventsContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>().HasOptional(e => e.Logo).WithRequired(l => l.Event).WillCascadeOnDelete(true);
            modelBuilder.Entity<Player>().HasOptional(e => e.PlayerImage).WithRequired(l => l.Player);
            modelBuilder.Entity<Match>().HasRequired(p => p.FirstPlayer).WithMany(e => e.MatchesAsFirstPlayer).WillCascadeOnDelete(false);
            modelBuilder.Entity<Match>().HasRequired(p => p.FirstPlayer).WithMany(e => e.MatchesAsSecondPlayer).WillCascadeOnDelete(false);
            modelBuilder.Entity<Map>().HasOptional(p => p.MapImage).WithRequired(e => e.Map);

            modelBuilder.Entity<RegisteredUser>().HasMany(ru => ru.Events).WithMany(e => e.Users).Map(
                e =>
                    {
                        e.MapLeftKey("UserId");
                        e.MapRightKey("EventId");
                        e.ToTable("UsersEvents");
                    });
            modelBuilder.Entity<Player>().HasMany(p => p.Events).WithMany(e => e.Players).Map(
                e =>
                    {
                        e.MapLeftKey("PlayerId");
                        e.MapRightKey("EventId");
                        e.ToTable("PlayersEvents");
                    });
            modelBuilder.Entity<Match>().HasRequired(e => e.Event).WithMany(l => l.Matches);
            modelBuilder.Entity<Match>().HasMany(p => p.Games).WithRequired(e => e.Match);
            modelBuilder.Entity<Game>().HasRequired(p => p.Map).WithMany(e => e.Games);
            modelBuilder.Entity<Event>().HasMany(p => p.MapPool).WithMany(e => e.Events).Map(
                e =>
                    {
                        e.MapLeftKey("EventId");
                        e.MapRightKey("MapId");
                        e.ToTable("EventsMapPool");
                    });
            modelBuilder.Entity<Sponsor>().HasRequired(p => p.User).WithRequiredDependent(e => e.Sponsor);
            modelBuilder.Entity<Article>().HasRequired(p => p.Author).WithMany(e => e.Articles);
            modelBuilder.Entity<Article>().HasMany(p => p.Images).WithRequired(e => e.Article);
            modelBuilder.Entity<Article>().HasMany(p => p.Videos).WithRequired(e => e.Article);
            modelBuilder.Entity<Match>().HasMany(p => p.Images).WithRequired(e => e.Match);
            modelBuilder.Entity<Match>().HasMany(p => p.Videos).WithRequired(e => e.Match);
            modelBuilder.Entity<Event>().HasMany(p => p.EventAdmins).WithMany(e => e.AdministratedEvents).Map(
               e =>
               {
                   e.MapLeftKey("AdministratedEventId");
                   e.MapRightKey("EventAdminId");
                   e.ToTable("AdministratedEventsEventAdmins");
               });
            modelBuilder.Entity<Event>().HasMany(p => p.EventImages).WithRequired(e => e.Event).WillCascadeOnDelete(false);
            modelBuilder.Entity<Player>().HasOptional(e => e.PlayerImage).WithRequired(l => l.Player).WillCascadeOnDelete(true);
            modelBuilder.Entity<Team>().HasOptional(e => e.TeamLogo).WithRequired(l => l.Team).WillCascadeOnDelete(true);
            modelBuilder.Entity<Team>().HasMany(e => e.Players).WithOptional(l => l.Team).WillCascadeOnDelete(true);
            modelBuilder.Entity<Country>().HasMany(e => e.Players).WithOptional(l => l.Country).WillCascadeOnDelete(true);
            modelBuilder.Entity<Country>().HasOptional(e => e.Flag).WithRequired(l => l.Country).WillCascadeOnDelete(true);
            modelBuilder.Entity<Event>().HasMany(e => e.PlayerEventRankings).WithRequired(l => l.Event).WillCascadeOnDelete(false);
            modelBuilder.Entity<Player>().HasMany(e => e.PlayerEventRankings).WithRequired(l => l.Player).WillCascadeOnDelete(false);
            modelBuilder.Entity<Event>().HasOptional(e => e.Season).WithMany(l => l.Events).WillCascadeOnDelete(false);
            modelBuilder.Entity<City>().HasRequired(e => e.Country).WithMany(c => c.Cities).WillCascadeOnDelete(true);
            modelBuilder.Entity<Team>().HasOptional(e => e.Country).WithMany(c => c.Teams).WillCascadeOnDelete(false);
            modelBuilder.Entity<Team>().HasOptional(e => e.City).WithMany(c => c.Teams).WillCascadeOnDelete(false);
            modelBuilder.Entity<Player>().HasOptional(e => e.City).WithMany(c => c.Players).WillCascadeOnDelete(false);
            modelBuilder.Entity<Match>().HasRequired(e => e.Round).WithMany(c => c.Matches).WillCascadeOnDelete(false);
            modelBuilder.Entity<Event>().HasOptional(e => e.Venue).WithMany(c => c.Events).WillCascadeOnDelete(false);
            modelBuilder.Entity<Round>().HasOptional(e => e.Venue).WithMany(c => c.Rounds).WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}