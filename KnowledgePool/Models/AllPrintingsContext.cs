using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace KnowledgePool.Models;

public partial class AllPrintingsContext : DbContext
{
    public AllPrintingsContext()
    {
        
    }

    public AllPrintingsContext(DbContextOptions<AllPrintingsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Card> Cards { get; set; }

    public virtual DbSet<CardForeignDatum> CardForeignData { get; set; }

    public virtual DbSet<CardIdentifier> CardIdentifiers { get; set; }

    public virtual DbSet<CardLegality> CardLegalities { get; set; }

    public virtual DbSet<CardPurchaseUrl> CardPurchaseUrls { get; set; }

    public virtual DbSet<CardRuling> CardRulings { get; set; }

    public virtual DbSet<Metum> Meta { get; set; }

    public virtual DbSet<Set> Sets { get; set; }

    public virtual DbSet<SetBoosterContent> SetBoosterContents { get; set; }

    public virtual DbSet<SetBoosterContentWeight> SetBoosterContentWeights { get; set; }

    public virtual DbSet<SetBoosterSheet> SetBoosterSheets { get; set; }

    public virtual DbSet<SetBoosterSheetCard> SetBoosterSheetCards { get; set; }

    public virtual DbSet<SetTranslation> SetTranslations { get; set; }

    public virtual DbSet<Token> Tokens { get; set; }

    public virtual DbSet<TokenIdentifier> TokenIdentifiers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("DataSource=C:\\Users\\Chris\\source\\repos\\KnowledgePool\\KnowledgePool\\AllPrintings.sqlite");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Card>(entity =>
        {
            entity.HasKey(e => e.Uuid);

            entity.ToTable("cards");

            entity.HasIndex(e => e.Uuid, "cards_uuid");

            entity.Property(e => e.Uuid)
                .HasColumnType("VARCHAR(36)")
                .HasColumnName("uuid");
            entity.Property(e => e.Artist).HasColumnName("artist");
            entity.Property(e => e.ArtistIds).HasColumnName("artistIds");
            entity.Property(e => e.AsciiName).HasColumnName("asciiName");
            entity.Property(e => e.AttractionLights).HasColumnName("attractionLights");
            entity.Property(e => e.Availability).HasColumnName("availability");
            entity.Property(e => e.BoosterTypes).HasColumnName("boosterTypes");
            entity.Property(e => e.BorderColor).HasColumnName("borderColor");
            entity.Property(e => e.CardParts).HasColumnName("cardParts");
            entity.Property(e => e.ColorIdentity).HasColumnName("colorIdentity");
            entity.Property(e => e.ColorIndicator).HasColumnName("colorIndicator");
            entity.Property(e => e.Colors).HasColumnName("colors");
            entity.Property(e => e.Defense).HasColumnName("defense");
            entity.Property(e => e.DuelDeck).HasColumnName("duelDeck");
            entity.Property(e => e.EdhrecRank).HasColumnName("edhrecRank");
            entity.Property(e => e.EdhrecSaltiness)
                .HasColumnType("FLOAT")
                .HasColumnName("edhrecSaltiness");
            entity.Property(e => e.FaceConvertedManaCost)
                .HasColumnType("FLOAT")
                .HasColumnName("faceConvertedManaCost");
            entity.Property(e => e.FaceFlavorName).HasColumnName("faceFlavorName");
            entity.Property(e => e.FaceManaValue)
                .HasColumnType("FLOAT")
                .HasColumnName("faceManaValue");
            entity.Property(e => e.FaceName).HasColumnName("faceName");
            entity.Property(e => e.Finishes).HasColumnName("finishes");
            entity.Property(e => e.FlavorName).HasColumnName("flavorName");
            entity.Property(e => e.FlavorText).HasColumnName("flavorText");
            entity.Property(e => e.FrameEffects).HasColumnName("frameEffects");
            entity.Property(e => e.FrameVersion).HasColumnName("frameVersion");
            entity.Property(e => e.Hand).HasColumnName("hand");
            entity.Property(e => e.HasAlternativeDeckLimit)
                .HasColumnType("BOOLEAN")
                .HasColumnName("hasAlternativeDeckLimit");
            entity.Property(e => e.HasContentWarning)
                .HasColumnType("BOOLEAN")
                .HasColumnName("hasContentWarning");
            entity.Property(e => e.HasFoil)
                .HasColumnType("BOOLEAN")
                .HasColumnName("hasFoil");
            entity.Property(e => e.HasNonFoil)
                .HasColumnType("BOOLEAN")
                .HasColumnName("hasNonFoil");
            entity.Property(e => e.IsAlternative)
                .HasColumnType("BOOLEAN")
                .HasColumnName("isAlternative");
            entity.Property(e => e.IsFullArt)
                .HasColumnType("BOOLEAN")
                .HasColumnName("isFullArt");
            entity.Property(e => e.IsFunny)
                .HasColumnType("BOOLEAN")
                .HasColumnName("isFunny");
            entity.Property(e => e.IsOnlineOnly)
                .HasColumnType("BOOLEAN")
                .HasColumnName("isOnlineOnly");
            entity.Property(e => e.IsOversized)
                .HasColumnType("BOOLEAN")
                .HasColumnName("isOversized");
            entity.Property(e => e.IsPromo)
                .HasColumnType("BOOLEAN")
                .HasColumnName("isPromo");
            entity.Property(e => e.IsRebalanced)
                .HasColumnType("BOOLEAN")
                .HasColumnName("isRebalanced");
            entity.Property(e => e.IsReprint)
                .HasColumnType("BOOLEAN")
                .HasColumnName("isReprint");
            entity.Property(e => e.IsReserved)
                .HasColumnType("BOOLEAN")
                .HasColumnName("isReserved");
            entity.Property(e => e.IsStarter)
                .HasColumnType("BOOLEAN")
                .HasColumnName("isStarter");
            entity.Property(e => e.IsStorySpotlight)
                .HasColumnType("BOOLEAN")
                .HasColumnName("isStorySpotlight");
            entity.Property(e => e.IsTextless)
                .HasColumnType("BOOLEAN")
                .HasColumnName("isTextless");
            entity.Property(e => e.IsTimeshifted)
                .HasColumnType("BOOLEAN")
                .HasColumnName("isTimeshifted");
            entity.Property(e => e.Keywords).HasColumnName("keywords");
            entity.Property(e => e.Language).HasColumnName("language");
            entity.Property(e => e.Layout).HasColumnName("layout");
            entity.Property(e => e.LeadershipSkills).HasColumnName("leadershipSkills");
            entity.Property(e => e.Life).HasColumnName("life");
            entity.Property(e => e.Loyalty).HasColumnName("loyalty");
            entity.Property(e => e.ManaCost).HasColumnName("manaCost");
            entity.Property(e => e.ManaValue)
                .HasColumnType("FLOAT")
                .HasColumnName("manaValue");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.OriginalPrintings).HasColumnName("originalPrintings");
            entity.Property(e => e.OriginalReleaseDate).HasColumnName("originalReleaseDate");
            entity.Property(e => e.OriginalText).HasColumnName("originalText");
            entity.Property(e => e.OriginalType).HasColumnName("originalType");
            entity.Property(e => e.OtherFaceIds).HasColumnName("otherFaceIds");
            entity.Property(e => e.Power).HasColumnName("power");
            entity.Property(e => e.Printings).HasColumnName("printings");
            entity.Property(e => e.PromoTypes).HasColumnName("promoTypes");
            entity.Property(e => e.Rarity).HasColumnName("rarity");
            entity.Property(e => e.RebalancedPrintings).HasColumnName("rebalancedPrintings");
            entity.Property(e => e.RelatedCards).HasColumnName("relatedCards");
            entity.Property(e => e.SecurityStamp).HasColumnName("securityStamp");
            entity.Property(e => e.SetCode).HasColumnName("setCode");
            entity.Property(e => e.Side).HasColumnName("side");
            entity.Property(e => e.Signature).HasColumnName("signature");
            entity.Property(e => e.SourceProducts).HasColumnName("sourceProducts");
            entity.Property(e => e.Subsets).HasColumnName("subsets");
            entity.Property(e => e.Subtypes).HasColumnName("subtypes");
            entity.Property(e => e.Supertypes).HasColumnName("supertypes");
            entity.Property(e => e.Text).HasColumnName("text");
            entity.Property(e => e.Toughness).HasColumnName("toughness");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Types).HasColumnName("types");
            entity.Property(e => e.Variations).HasColumnName("variations");
            entity.Property(e => e.Watermark).HasColumnName("watermark");
        });

        modelBuilder.Entity<CardForeignDatum>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("cardForeignData");

            entity.HasIndex(e => e.Uuid, "cardForeignData_uuid");

            entity.Property(e => e.FaceName).HasColumnName("faceName");
            entity.Property(e => e.FlavorText).HasColumnName("flavorText");
            entity.Property(e => e.Language).HasColumnName("language");
            entity.Property(e => e.MultiverseId).HasColumnName("multiverseId");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Text).HasColumnName("text");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Uuid).HasColumnName("uuid");
        });

        modelBuilder.Entity<CardIdentifier>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("cardIdentifiers");

            entity.HasIndex(e => e.Uuid, "cardIdentifiers_uuid");

            entity.Property(e => e.CardKingdomEtchedId).HasColumnName("cardKingdomEtchedId");
            entity.Property(e => e.CardKingdomFoilId).HasColumnName("cardKingdomFoilId");
            entity.Property(e => e.CardKingdomId).HasColumnName("cardKingdomId");
            entity.Property(e => e.CardsphereId).HasColumnName("cardsphereId");
            entity.Property(e => e.McmId).HasColumnName("mcmId");
            entity.Property(e => e.McmMetaId).HasColumnName("mcmMetaId");
            entity.Property(e => e.MtgArenaId).HasColumnName("mtgArenaId");
            entity.Property(e => e.MtgjsonFoilVersionId).HasColumnName("mtgjsonFoilVersionId");
            entity.Property(e => e.MtgjsonNonFoilVersionId).HasColumnName("mtgjsonNonFoilVersionId");
            entity.Property(e => e.MtgjsonV4id).HasColumnName("mtgjsonV4Id");
            entity.Property(e => e.MtgoFoilId).HasColumnName("mtgoFoilId");
            entity.Property(e => e.MtgoId).HasColumnName("mtgoId");
            entity.Property(e => e.MultiverseId).HasColumnName("multiverseId");
            entity.Property(e => e.ScryfallId).HasColumnName("scryfallId");
            entity.Property(e => e.ScryfallIllustrationId).HasColumnName("scryfallIllustrationId");
            entity.Property(e => e.ScryfallOracleId).HasColumnName("scryfallOracleId");
            entity.Property(e => e.TcgplayerEtchedProductId).HasColumnName("tcgplayerEtchedProductId");
            entity.Property(e => e.TcgplayerProductId).HasColumnName("tcgplayerProductId");
            entity.Property(e => e.Uuid).HasColumnName("uuid");
        });

        modelBuilder.Entity<CardLegality>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("cardLegalities");

            entity.HasIndex(e => e.Uuid, "cardLegalities_uuid");

            entity.Property(e => e.Alchemy).HasColumnName("alchemy");
            entity.Property(e => e.Brawl).HasColumnName("brawl");
            entity.Property(e => e.Commander).HasColumnName("commander");
            entity.Property(e => e.Duel).HasColumnName("duel");
            entity.Property(e => e.Explorer).HasColumnName("explorer");
            entity.Property(e => e.Future).HasColumnName("future");
            entity.Property(e => e.Gladiator).HasColumnName("gladiator");
            entity.Property(e => e.Historic).HasColumnName("historic");
            entity.Property(e => e.Historicbrawl).HasColumnName("historicbrawl");
            entity.Property(e => e.Legacy).HasColumnName("legacy");
            entity.Property(e => e.Modern).HasColumnName("modern");
            entity.Property(e => e.Oathbreaker).HasColumnName("oathbreaker");
            entity.Property(e => e.Oldschool).HasColumnName("oldschool");
            entity.Property(e => e.Pauper).HasColumnName("pauper");
            entity.Property(e => e.Paupercommander).HasColumnName("paupercommander");
            entity.Property(e => e.Penny).HasColumnName("penny");
            entity.Property(e => e.Pioneer).HasColumnName("pioneer");
            entity.Property(e => e.Predh).HasColumnName("predh");
            entity.Property(e => e.Premodern).HasColumnName("premodern");
            entity.Property(e => e.Standard).HasColumnName("standard");
            entity.Property(e => e.Uuid).HasColumnName("uuid");
            entity.Property(e => e.Vintage).HasColumnName("vintage");
        });

        modelBuilder.Entity<CardPurchaseUrl>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("cardPurchaseUrls");

            entity.HasIndex(e => e.Uuid, "cardPurchaseUrls_uuid");

            entity.Property(e => e.CardKingdom).HasColumnName("cardKingdom");
            entity.Property(e => e.CardKingdomEtched).HasColumnName("cardKingdomEtched");
            entity.Property(e => e.CardKingdomFoil).HasColumnName("cardKingdomFoil");
            entity.Property(e => e.Cardmarket).HasColumnName("cardmarket");
            entity.Property(e => e.Tcgplayer).HasColumnName("tcgplayer");
            entity.Property(e => e.TcgplayerEtched).HasColumnName("tcgplayerEtched");
            entity.Property(e => e.Uuid).HasColumnName("uuid");
        });

        modelBuilder.Entity<CardRuling>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("cardRulings");

            entity.HasIndex(e => e.Uuid, "cardRulings_uuid");

            entity.Property(e => e.Date)
                .HasColumnType("DATE")
                .HasColumnName("date");
            entity.Property(e => e.Text).HasColumnName("text");
            entity.Property(e => e.Uuid)
                .HasColumnType("VARCHAR(36)")
                .HasColumnName("uuid");
        });

        modelBuilder.Entity<Metum>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("meta");

            entity.Property(e => e.Date)
                .HasColumnType("DATE")
                .HasColumnName("date");
            entity.Property(e => e.Version).HasColumnName("version");
        });

        modelBuilder.Entity<Set>(entity =>
        {
            entity.HasKey(e => e.Code);

            entity.ToTable("sets");

            entity.Property(e => e.Code)
                .HasColumnType("VARCHAR(8)")
                .HasColumnName("code");
            entity.Property(e => e.BaseSetSize).HasColumnName("baseSetSize");
            entity.Property(e => e.Block).HasColumnName("block");
            entity.Property(e => e.CardsphereSetId).HasColumnName("cardsphereSetId");
            entity.Property(e => e.IsFoilOnly)
                .HasColumnType("BOOLEAN")
                .HasColumnName("isFoilOnly");
            entity.Property(e => e.IsForeignOnly)
                .HasColumnType("BOOLEAN")
                .HasColumnName("isForeignOnly");
            entity.Property(e => e.IsNonFoilOnly)
                .HasColumnType("BOOLEAN")
                .HasColumnName("isNonFoilOnly");
            entity.Property(e => e.IsOnlineOnly)
                .HasColumnType("BOOLEAN")
                .HasColumnName("isOnlineOnly");
            entity.Property(e => e.IsPartialPreview)
                .HasColumnType("BOOLEAN")
                .HasColumnName("isPartialPreview");
            entity.Property(e => e.KeyruneCode).HasColumnName("keyruneCode");
            entity.Property(e => e.Languages).HasColumnName("languages");
            entity.Property(e => e.McmId).HasColumnName("mcmId");
            entity.Property(e => e.McmIdExtras).HasColumnName("mcmIdExtras");
            entity.Property(e => e.McmName).HasColumnName("mcmName");
            entity.Property(e => e.MtgoCode).HasColumnName("mtgoCode");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.ParentCode).HasColumnName("parentCode");
            entity.Property(e => e.ReleaseDate).HasColumnName("releaseDate");
            entity.Property(e => e.TcgplayerGroupId).HasColumnName("tcgplayerGroupId");
            entity.Property(e => e.TokenSetCode).HasColumnName("tokenSetCode");
            entity.Property(e => e.TotalSetSize).HasColumnName("totalSetSize");
            entity.Property(e => e.Type).HasColumnName("type");
        });

        modelBuilder.Entity<SetBoosterContent>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("setBoosterContents");

            entity.HasIndex(e => new { e.SetCode, e.SheetName, e.BoosterName, e.BoosterIndex }, "IX_setBoosterContents_setCode_sheetName_boosterName_boosterIndex").IsUnique();

            entity.Property(e => e.BoosterIndex).HasColumnName("boosterIndex");
            entity.Property(e => e.BoosterName)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("boosterName");
            entity.Property(e => e.SetCode)
                .HasColumnType("VARCHAR(20)")
                .HasColumnName("setCode");
            entity.Property(e => e.SheetName)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("sheetName");
            entity.Property(e => e.SheetPicks).HasColumnName("sheetPicks");
        });

        modelBuilder.Entity<SetBoosterContentWeight>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("setBoosterContentWeights");

            entity.Property(e => e.BoosterIndex).HasColumnName("boosterIndex");
            entity.Property(e => e.BoosterName)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("boosterName");
            entity.Property(e => e.BoosterWeight).HasColumnName("boosterWeight");
            entity.Property(e => e.SetCode)
                .HasColumnType("VARCHAR(20)")
                .HasColumnName("setCode");
        });

        modelBuilder.Entity<SetBoosterSheet>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("setBoosterSheets");

            entity.HasIndex(e => new { e.SetCode, e.SheetName, e.BoosterName }, "IX_setBoosterSheets_setCode_sheetName_boosterName").IsUnique();

            entity.Property(e => e.BoosterName)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("boosterName");
            entity.Property(e => e.SetCode)
                .HasColumnType("VARCHAR(20)")
                .HasColumnName("setCode");
            entity.Property(e => e.SheetHasBalanceColors)
                .HasColumnType("BOOLEAN")
                .HasColumnName("sheetHasBalanceColors");
            entity.Property(e => e.SheetIsFoil)
                .HasColumnType("BOOLEAN")
                .HasColumnName("sheetIsFoil");
            entity.Property(e => e.SheetName)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("sheetName");
        });

        modelBuilder.Entity<SetBoosterSheetCard>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("setBoosterSheetCards");

            entity.HasIndex(e => new { e.SetCode, e.SheetName, e.BoosterName, e.CardUuid }, "IX_setBoosterSheetCards_setCode_sheetName_boosterName_cardUuid").IsUnique();

            entity.Property(e => e.BoosterName)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("boosterName");
            entity.Property(e => e.CardUuid)
                .HasColumnType("VARCHAR(36)")
                .HasColumnName("cardUuid");
            entity.Property(e => e.CardWeight)
                .HasColumnType("BIGINT")
                .HasColumnName("cardWeight");
            entity.Property(e => e.SetCode)
                .HasColumnType("VARCHAR(20)")
                .HasColumnName("setCode");
            entity.Property(e => e.SheetName)
                .HasColumnType("VARCHAR(255)")
                .HasColumnName("sheetName");
        });

        modelBuilder.Entity<SetTranslation>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("setTranslations");

            entity.HasIndex(e => e.Uuid, "setTranslations_uuid");

            entity.Property(e => e.Language).HasColumnName("language");
            entity.Property(e => e.Translation).HasColumnName("translation");
            entity.Property(e => e.Uuid)
                .HasColumnType("VARCHAR(36)")
                .HasColumnName("uuid");
        });

        modelBuilder.Entity<Token>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tokens");

            entity.HasIndex(e => e.Uuid, "tokens_uuid");

            entity.Property(e => e.Artist).HasColumnName("artist");
            entity.Property(e => e.ArtistIds).HasColumnName("artistIds");
            entity.Property(e => e.AsciiName).HasColumnName("asciiName");
            entity.Property(e => e.Availability).HasColumnName("availability");
            entity.Property(e => e.BoosterTypes).HasColumnName("boosterTypes");
            entity.Property(e => e.BorderColor).HasColumnName("borderColor");
            entity.Property(e => e.ColorIdentity).HasColumnName("colorIdentity");
            entity.Property(e => e.Colors).HasColumnName("colors");
            entity.Property(e => e.EdhrecSaltiness)
                .HasColumnType("FLOAT")
                .HasColumnName("edhrecSaltiness");
            entity.Property(e => e.FaceName).HasColumnName("faceName");
            entity.Property(e => e.Finishes).HasColumnName("finishes");
            entity.Property(e => e.FlavorText).HasColumnName("flavorText");
            entity.Property(e => e.FrameEffects).HasColumnName("frameEffects");
            entity.Property(e => e.FrameVersion).HasColumnName("frameVersion");
            entity.Property(e => e.HasFoil)
                .HasColumnType("BOOLEAN")
                .HasColumnName("hasFoil");
            entity.Property(e => e.HasNonFoil)
                .HasColumnType("BOOLEAN")
                .HasColumnName("hasNonFoil");
            entity.Property(e => e.IsFullArt)
                .HasColumnType("BOOLEAN")
                .HasColumnName("isFullArt");
            entity.Property(e => e.IsFunny)
                .HasColumnType("BOOLEAN")
                .HasColumnName("isFunny");
            entity.Property(e => e.IsPromo)
                .HasColumnType("BOOLEAN")
                .HasColumnName("isPromo");
            entity.Property(e => e.IsReprint)
                .HasColumnType("BOOLEAN")
                .HasColumnName("isReprint");
            entity.Property(e => e.IsTextless)
                .HasColumnType("BOOLEAN")
                .HasColumnName("isTextless");
            entity.Property(e => e.Keywords).HasColumnName("keywords");
            entity.Property(e => e.Language).HasColumnName("language");
            entity.Property(e => e.Layout).HasColumnName("layout");
            entity.Property(e => e.ManaCost).HasColumnName("manaCost");
            entity.Property(e => e.Name).HasColumnName("name");
            entity.Property(e => e.Number).HasColumnName("number");
            entity.Property(e => e.Orientation).HasColumnName("orientation");
            entity.Property(e => e.OriginalText).HasColumnName("originalText");
            entity.Property(e => e.OriginalType).HasColumnName("originalType");
            entity.Property(e => e.OtherFaceIds).HasColumnName("otherFaceIds");
            entity.Property(e => e.Power).HasColumnName("power");
            entity.Property(e => e.PromoTypes).HasColumnName("promoTypes");
            entity.Property(e => e.RelatedCards).HasColumnName("relatedCards");
            entity.Property(e => e.ReverseRelated).HasColumnName("reverseRelated");
            entity.Property(e => e.SecurityStamp).HasColumnName("securityStamp");
            entity.Property(e => e.SetCode).HasColumnName("setCode");
            entity.Property(e => e.Side).HasColumnName("side");
            entity.Property(e => e.Signature).HasColumnName("signature");
            entity.Property(e => e.Subtypes).HasColumnName("subtypes");
            entity.Property(e => e.Supertypes).HasColumnName("supertypes");
            entity.Property(e => e.Text).HasColumnName("text");
            entity.Property(e => e.Toughness).HasColumnName("toughness");
            entity.Property(e => e.Type).HasColumnName("type");
            entity.Property(e => e.Types).HasColumnName("types");
            entity.Property(e => e.Uuid)
                .HasColumnType("VARCHAR(36)")
                .HasColumnName("uuid");
            entity.Property(e => e.Watermark).HasColumnName("watermark");
        });

        modelBuilder.Entity<TokenIdentifier>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tokenIdentifiers");

            entity.HasIndex(e => e.Uuid, "tokenIdentifiers_uuid");

            entity.Property(e => e.CardKingdomEtchedId).HasColumnName("cardKingdomEtchedId");
            entity.Property(e => e.CardKingdomFoilId).HasColumnName("cardKingdomFoilId");
            entity.Property(e => e.CardKingdomId).HasColumnName("cardKingdomId");
            entity.Property(e => e.CardsphereId).HasColumnName("cardsphereId");
            entity.Property(e => e.McmId).HasColumnName("mcmId");
            entity.Property(e => e.McmMetaId).HasColumnName("mcmMetaId");
            entity.Property(e => e.MtgArenaId).HasColumnName("mtgArenaId");
            entity.Property(e => e.MtgjsonFoilVersionId).HasColumnName("mtgjsonFoilVersionId");
            entity.Property(e => e.MtgjsonNonFoilVersionId).HasColumnName("mtgjsonNonFoilVersionId");
            entity.Property(e => e.MtgjsonV4id).HasColumnName("mtgjsonV4Id");
            entity.Property(e => e.MtgoFoilId).HasColumnName("mtgoFoilId");
            entity.Property(e => e.MtgoId).HasColumnName("mtgoId");
            entity.Property(e => e.MultiverseId).HasColumnName("multiverseId");
            entity.Property(e => e.ScryfallId).HasColumnName("scryfallId");
            entity.Property(e => e.ScryfallIllustrationId).HasColumnName("scryfallIllustrationId");
            entity.Property(e => e.ScryfallOracleId).HasColumnName("scryfallOracleId");
            entity.Property(e => e.TcgplayerEtchedProductId).HasColumnName("tcgplayerEtchedProductId");
            entity.Property(e => e.TcgplayerProductId).HasColumnName("tcgplayerProductId");
            entity.Property(e => e.Uuid).HasColumnName("uuid");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
