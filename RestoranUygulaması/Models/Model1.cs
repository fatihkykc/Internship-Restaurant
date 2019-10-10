namespace RestoranUygulaması.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=RestaurantContext")
        {
        }

        public virtual DbSet<BasindaBiz> BasindaBiz { get; set; }
        public virtual DbSet<Fiyat> Fiyat { get; set; }
        public virtual DbSet<Hakkimizda> Hakkimizda { get; set; }
        public virtual DbSet<Hazirlanis> Hazirlanis { get; set; }
        public virtual DbSet<Icindekiler> Icindekiler { get; set; }
        public virtual DbSet<Kullanici> Kullanici { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Rezervasyon> Rezervasyons { get; set; }
        public virtual DbSet<Yemek> Yemek { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<BasindaBiz>()
                .HasMany(e => e.Kullanici)
                .WithMany(e => e.BasindaBiz)
                .Map(m => m.ToTable("KulBas").MapLeftKey("basin_id").MapRightKey("kul_id"));
            
               
            modelBuilder.Entity<Fiyat>()
                .HasMany(e => e.Kullanici)
                .WithMany(e => e.Fiyat)
                .Map(m => m.ToTable("KulFiy").MapLeftKey("fiyat_id").MapRightKey("kul_id"));

            modelBuilder.Entity<Fiyat>()
                .HasMany(e => e.Yemek)
                .WithMany(e => e.Fiyat)
                .Map(m => m.ToTable("YemFiy").MapLeftKey("fiyat_id").MapRightKey("yem_id"));

            modelBuilder.Entity<Hakkimizda>()
                .HasMany(e => e.Kullanici)
                .WithMany(e => e.Hakkimizda)
                .Map(m => m.ToTable("KulHak").MapLeftKey("hak_id").MapRightKey("kul_id"));

            modelBuilder.Entity<Hazirlanis>()
                .HasMany(e => e.Kullanici)
                .WithMany(e => e.Hazirlanis)
                .Map(m => m.ToTable("KulHaz").MapLeftKey("hazir_id").MapRightKey("kul_id"));

            modelBuilder.Entity<Hazirlanis>()
                .HasMany(e => e.Yemek)
                .WithMany(e => e.Hazirlanis)
                .Map(m => m.ToTable("YemHaz").MapLeftKey("hazir_id").MapRightKey("yem_id"));

            modelBuilder.Entity<Icindekiler>()
                .HasMany(e => e.Kullanici)
                .WithMany(e => e.Icindekiler)
                .Map(m => m.ToTable("KulIcin").MapLeftKey("icin_id").MapRightKey("kul_id"));

            modelBuilder.Entity<Icindekiler>()
                .HasMany(e => e.Yemek)
                .WithMany(e => e.Icindekiler)
                .Map(m => m.ToTable("YemIcin").MapLeftKey("icin_id").MapRightKey("yem_id"));

            modelBuilder.Entity<Kullanici>()
                .Property(e => e.kul_sifre)
                .IsFixedLength();

            modelBuilder.Entity<Kullanici>()
                .HasMany(e => e.Yemek)
                .WithMany(e => e.Kullanici)
                .Map(m => m.ToTable("KulYem").MapLeftKey("kul_id").MapRightKey("yem_id"));
        }
    }
}
