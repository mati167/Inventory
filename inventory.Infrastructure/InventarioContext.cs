using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Core.DAO;

public partial class InventarioContext : DbContext
{
    public InventarioContext()
    {
    }

    public InventarioContext(DbContextOptions<InventarioContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Comic> Comics { get; set; }

    public virtual DbSet<Continent> Continents { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Demography> Demographies { get; set; }

    public virtual DbSet<Film> Films { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Literature> Literatures { get; set; }

    public virtual DbSet<Manga> Mangas { get; set; }

    public virtual DbSet<Person> People { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<Size> Sizes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=MATIAS;Database=inventario;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Idbook);

            entity.ToTable("Book");

            entity.Property(e => e.Idbook).HasColumnName("IDBook");
            entity.Property(e => e.BookName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.OriginalName)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.PublisherNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.Publisher)
                .HasConstraintName("FK_Book_Publisher");
        });

        modelBuilder.Entity<Comic>(entity =>
        {
            entity.HasKey(e => e.Idcomic);

            entity.ToTable("Comic");

            entity.Property(e => e.Idcomic).HasColumnName("IDComic");
            entity.Property(e => e.Idbook).HasColumnName("IDBook");
            entity.Property(e => e.Isbn).HasColumnName("ISBN");

            entity.HasOne(d => d.ArtistNavigation).WithMany(p => p.ComicArtistNavigations)
                .HasForeignKey(d => d.Artist)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comic_PersonArtist");

            entity.HasOne(d => d.AuthorNavigation).WithMany(p => p.ComicAuthorNavigations)
                .HasForeignKey(d => d.Author)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comic_PersonAuthor");

            entity.HasOne(d => d.IdbookNavigation).WithMany(p => p.Comics)
                .HasForeignKey(d => d.Idbook)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comic_Book");
        });

        modelBuilder.Entity<Continent>(entity =>
        {
            entity.HasKey(e => e.Idcontinent).HasName("PK_continente");

            entity.ToTable("Continent");

            entity.Property(e => e.Idcontinent).HasColumnName("IDContinent");
            entity.Property(e => e.ContinentName)
                .HasMaxLength(45)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Idcountry).HasName("PK_pais");

            entity.ToTable("Country");

            entity.Property(e => e.Idcountry).HasColumnName("IDCountry");
            entity.Property(e => e.CountryName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Idcontinent).HasColumnName("IDContinent");

            entity.HasOne(d => d.IdcontinentNavigation).WithMany(p => p.Countries)
                .HasForeignKey(d => d.Idcontinent)
                .HasConstraintName("PK_pais_continente");
        });

        modelBuilder.Entity<Demography>(entity =>
        {
            entity.HasKey(e => e.Iddemography);

            entity.ToTable("Demography");

            entity.Property(e => e.Iddemography).HasColumnName("IDDemography");
            entity.Property(e => e.DemographyDescription).IsUnicode(false);
        });

        modelBuilder.Entity<Film>(entity =>
        {
            entity.HasKey(e => e.Idfilm).HasName("PK_Pelicula");

            entity.ToTable("Film");

            entity.Property(e => e.Idfilm).HasColumnName("IDFilm");
            entity.Property(e => e.FilmName)
                .HasMaxLength(45)
                .IsUnicode(false);

            entity.HasMany(d => d.Idcountries).WithMany(p => p.Idfilms)
                .UsingEntity<Dictionary<string, object>>(
                    "FilmCountry",
                    r => r.HasOne<Country>().WithMany()
                        .HasForeignKey("Idcountry")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_se_filmo_en_pais"),
                    l => l.HasOne<Film>().WithMany()
                        .HasForeignKey("Idfilm")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_se_filmo_en_pelicula"),
                    j =>
                    {
                        j.HasKey("Idfilm", "Idcountry").HasName("PK_se_filmo_en");
                        j.ToTable("Film_Country");
                        j.IndexerProperty<int>("Idfilm").HasColumnName("IDFilm");
                        j.IndexerProperty<int>("Idcountry").HasColumnName("IDCountry");
                    });

            entity.HasMany(d => d.Idgenres).WithMany(p => p.Idfilms)
                .UsingEntity<Dictionary<string, object>>(
                    "FilmGenre",
                    r => r.HasOne<Genre>().WithMany()
                        .HasForeignKey("Idgenre")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_pertenece_genero"),
                    l => l.HasOne<Film>().WithMany()
                        .HasForeignKey("Idfilm")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_pertenece_pelicula"),
                    j =>
                    {
                        j.HasKey("Idfilm", "Idgenre").HasName("PK_pertenece");
                        j.ToTable("Film_Genre");
                        j.IndexerProperty<int>("Idfilm").HasColumnName("IDFilm");
                        j.IndexerProperty<int>("Idgenre").HasColumnName("IDGenre");
                    });

            entity.HasMany(d => d.Idpeople).WithMany(p => p.Idfilms)
                .UsingEntity<Dictionary<string, object>>(
                    "Directed",
                    r => r.HasOne<Person>().WithMany()
                        .HasForeignKey("Idperson")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_es_dirigida_persona"),
                    l => l.HasOne<Film>().WithMany()
                        .HasForeignKey("Idfilm")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_es_dirigida_pelicula"),
                    j =>
                    {
                        j.HasKey("Idfilm", "Idperson").HasName("PK_es_dirigida");
                        j.ToTable("Directed");
                        j.IndexerProperty<int>("Idfilm").HasColumnName("IDFilm");
                        j.IndexerProperty<int>("Idperson").HasColumnName("IDPerson");
                    });

            entity.HasMany(d => d.IdpeopleNavigation).WithMany(p => p.IdfilmsNavigation)
                .UsingEntity<Dictionary<string, object>>(
                    "Played",
                    r => r.HasOne<Person>().WithMany()
                        .HasForeignKey("Idperson")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_es_actuada_persona"),
                    l => l.HasOne<Film>().WithMany()
                        .HasForeignKey("Idfilm")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_es_actuada_pelicula"),
                    j =>
                    {
                        j.HasKey("Idfilm", "Idperson").HasName("PK_es_actuada");
                        j.ToTable("Played");
                        j.IndexerProperty<int>("Idfilm").HasColumnName("IDFilm");
                        j.IndexerProperty<int>("Idperson").HasColumnName("IDPerson");
                    });
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Idgenre).HasName("PK_genero");

            entity.ToTable("Genre");

            entity.Property(e => e.Idgenre).HasColumnName("IDGenre");
            entity.Property(e => e.Description)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Literature>(entity =>
        {
            entity.HasKey(e => e.Idliterature);

            entity.ToTable("Literature");

            entity.Property(e => e.Idliterature).HasColumnName("IDLiterature");
            entity.Property(e => e.Idbook).HasColumnName("IDBook");
            entity.Property(e => e.Isbn).HasColumnName("ISBN");

            entity.HasOne(d => d.AuthorNavigation).WithMany(p => p.Literatures)
                .HasForeignKey(d => d.Author)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Literature_Person");

            entity.HasOne(d => d.IdbookNavigation).WithMany(p => p.Literatures)
                .HasForeignKey(d => d.Idbook)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Literature_Book");
        });

        modelBuilder.Entity<Manga>(entity =>
        {
            entity.HasKey(e => e.Idmanga);

            entity.ToTable("Manga");

            entity.Property(e => e.Idmanga).HasColumnName("IDManga");
            entity.Property(e => e.Idbook).HasColumnName("IDBook");
            entity.Property(e => e.Isbn).HasColumnName("ISBN");

            entity.HasOne(d => d.ArtistNavigation).WithMany(p => p.MangaArtistNavigations)
                .HasForeignKey(d => d.Artist)
                .HasConstraintName("FK_Manga_PersonArtist");

            entity.HasOne(d => d.AuthorNavigation).WithMany(p => p.MangaAuthorNavigations)
                .HasForeignKey(d => d.Author)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Manga_PersonAuthor");

            entity.HasOne(d => d.DemographyNavigation).WithMany(p => p.Mangas)
                .HasForeignKey(d => d.Demography)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Manga_Demography");

            entity.HasOne(d => d.IdbookNavigation).WithMany(p => p.Mangas)
                .HasForeignKey(d => d.Idbook)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Manga_Book");

            entity.HasOne(d => d.SizeNavigation).WithMany(p => p.Mangas)
                .HasForeignKey(d => d.Size)
                .HasConstraintName("FK_Manga_Size");
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.HasKey(e => e.Idpersona).HasName("PK_persona");

            entity.ToTable("Person");

            entity.Property(e => e.Idpersona)
                .ValueGeneratedNever()
                .HasColumnName("IDPersona");
            entity.Property(e => e.LastName)
                .HasMaxLength(45)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .IsUnicode(false);

            entity.HasMany(d => d.Idcountries).WithMany(p => p.Idpeople)
                .UsingEntity<Dictionary<string, object>>(
                    "Nacionality",
                    r => r.HasOne<Country>().WithMany()
                        .HasForeignKey("Idcountry")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_nacionalidad_pais"),
                    l => l.HasOne<Person>().WithMany()
                        .HasForeignKey("Idperson")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_nacionalidad_persona"),
                    j =>
                    {
                        j.HasKey("Idperson", "Idcountry").HasName("PK_nacionalidad");
                        j.ToTable("Nacionality");
                        j.IndexerProperty<int>("Idperson").HasColumnName("IDPerson");
                        j.IndexerProperty<int>("Idcountry").HasColumnName("IDCountry");
                    });
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.Idpublisher);

            entity.ToTable("Publisher");

            entity.Property(e => e.Idpublisher).HasColumnName("IDPublisher");
            entity.Property(e => e.PublisherName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Size>(entity =>
        {
            entity.HasKey(e => e.Idsize);

            entity.ToTable("Size");

            entity.Property(e => e.Idsize).HasColumnName("IDSize");
            entity.Property(e => e.SizeName)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
