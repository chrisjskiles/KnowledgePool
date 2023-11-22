using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KnowledgePool.Migrations
{
    /// <inheritdoc />
    public partial class NonNullableReleaseDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.CreateTable(
            //    name: "cards",
            //    columns: table => new
            //    {
            //        uuid = table.Column<string>(type: "VARCHAR(36)", nullable: false),
            //        artist = table.Column<string>(type: "TEXT", nullable: true),
            //        artistIds = table.Column<string>(type: "TEXT", nullable: true),
            //        asciiName = table.Column<string>(type: "TEXT", nullable: true),
            //        attractionLights = table.Column<string>(type: "TEXT", nullable: true),
            //        availability = table.Column<string>(type: "TEXT", nullable: true),
            //        boosterTypes = table.Column<string>(type: "TEXT", nullable: true),
            //        borderColor = table.Column<string>(type: "TEXT", nullable: true),
            //        cardParts = table.Column<string>(type: "TEXT", nullable: true),
            //        colorIdentity = table.Column<string>(type: "TEXT", nullable: true),
            //        colorIndicator = table.Column<string>(type: "TEXT", nullable: true),
            //        colors = table.Column<string>(type: "TEXT", nullable: true),
            //        defense = table.Column<string>(type: "TEXT", nullable: true),
            //        duelDeck = table.Column<string>(type: "TEXT", nullable: true),
            //        edhrecRank = table.Column<long>(type: "INTEGER", nullable: true),
            //        edhrecSaltiness = table.Column<double>(type: "FLOAT", nullable: true),
            //        faceConvertedManaCost = table.Column<double>(type: "FLOAT", nullable: true),
            //        faceFlavorName = table.Column<string>(type: "TEXT", nullable: true),
            //        faceManaValue = table.Column<double>(type: "FLOAT", nullable: true),
            //        faceName = table.Column<string>(type: "TEXT", nullable: true),
            //        finishes = table.Column<string>(type: "TEXT", nullable: true),
            //        flavorName = table.Column<string>(type: "TEXT", nullable: true),
            //        flavorText = table.Column<string>(type: "TEXT", nullable: true),
            //        frameEffects = table.Column<string>(type: "TEXT", nullable: true),
            //        frameVersion = table.Column<string>(type: "TEXT", nullable: true),
            //        hand = table.Column<string>(type: "TEXT", nullable: true),
            //        hasAlternativeDeckLimit = table.Column<byte[]>(type: "BOOLEAN", nullable: true),
            //        hasContentWarning = table.Column<byte[]>(type: "BOOLEAN", nullable: true),
            //        hasFoil = table.Column<byte[]>(type: "BOOLEAN", nullable: true),
            //        hasNonFoil = table.Column<byte[]>(type: "BOOLEAN", nullable: true),
            //        isAlternative = table.Column<byte[]>(type: "BOOLEAN", nullable: true),
            //        isFullArt = table.Column<byte[]>(type: "BOOLEAN", nullable: true),
            //        isFunny = table.Column<byte[]>(type: "BOOLEAN", nullable: true),
            //        isOnlineOnly = table.Column<byte[]>(type: "BOOLEAN", nullable: true),
            //        isOversized = table.Column<byte[]>(type: "BOOLEAN", nullable: true),
            //        isPromo = table.Column<byte[]>(type: "BOOLEAN", nullable: true),
            //        isRebalanced = table.Column<byte[]>(type: "BOOLEAN", nullable: true),
            //        isReprint = table.Column<byte[]>(type: "BOOLEAN", nullable: true),
            //        isReserved = table.Column<byte[]>(type: "BOOLEAN", nullable: true),
            //        isStarter = table.Column<byte[]>(type: "BOOLEAN", nullable: true),
            //        isStorySpotlight = table.Column<byte[]>(type: "BOOLEAN", nullable: true),
            //        isTextless = table.Column<byte[]>(type: "BOOLEAN", nullable: true),
            //        isTimeshifted = table.Column<byte[]>(type: "BOOLEAN", nullable: true),
            //        keywords = table.Column<string>(type: "TEXT", nullable: true),
            //        language = table.Column<string>(type: "TEXT", nullable: true),
            //        layout = table.Column<string>(type: "TEXT", nullable: true),
            //        leadershipSkills = table.Column<string>(type: "TEXT", nullable: true),
            //        life = table.Column<string>(type: "TEXT", nullable: true),
            //        loyalty = table.Column<string>(type: "TEXT", nullable: true),
            //        manaCost = table.Column<string>(type: "TEXT", nullable: true),
            //        manaValue = table.Column<double>(type: "FLOAT", nullable: true),
            //        name = table.Column<string>(type: "TEXT", nullable: true),
            //        number = table.Column<string>(type: "TEXT", nullable: true),
            //        originalPrintings = table.Column<string>(type: "TEXT", nullable: true),
            //        originalReleaseDate = table.Column<string>(type: "TEXT", nullable: true),
            //        originalText = table.Column<string>(type: "TEXT", nullable: true),
            //        originalType = table.Column<string>(type: "TEXT", nullable: true),
            //        otherFaceIds = table.Column<string>(type: "TEXT", nullable: true),
            //        power = table.Column<string>(type: "TEXT", nullable: true),
            //        printings = table.Column<string>(type: "TEXT", nullable: true),
            //        promoTypes = table.Column<string>(type: "TEXT", nullable: true),
            //        rarity = table.Column<string>(type: "TEXT", nullable: true),
            //        rebalancedPrintings = table.Column<string>(type: "TEXT", nullable: true),
            //        relatedCards = table.Column<string>(type: "TEXT", nullable: true),
            //        securityStamp = table.Column<string>(type: "TEXT", nullable: true),
            //        setCode = table.Column<string>(type: "TEXT", nullable: true),
            //        side = table.Column<string>(type: "TEXT", nullable: true),
            //        signature = table.Column<string>(type: "TEXT", nullable: true),
            //        sourceProducts = table.Column<string>(type: "TEXT", nullable: true),
            //        subsets = table.Column<string>(type: "TEXT", nullable: true),
            //        subtypes = table.Column<string>(type: "TEXT", nullable: true),
            //        supertypes = table.Column<string>(type: "TEXT", nullable: true),
            //        text = table.Column<string>(type: "TEXT", nullable: true),
            //        toughness = table.Column<string>(type: "TEXT", nullable: true),
            //        type = table.Column<string>(type: "TEXT", nullable: true),
            //        types = table.Column<string>(type: "TEXT", nullable: true),
            //        variations = table.Column<string>(type: "TEXT", nullable: true),
            //        watermark = table.Column<string>(type: "TEXT", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_cards", x => x.uuid);
            //    });

            migrationBuilder.AlterColumn<String>(
                name: "ReleaseDate",
                table: "sets",
                nullable: false);
            //migrationBuilder.CreateTable(
            //    name: "sets",
            //    columns: table => new
            //    {
            //        code = table.Column<string>(type: "VARCHAR(8)", nullable: false),
            //        baseSetSize = table.Column<long>(type: "INTEGER", nullable: true),
            //        block = table.Column<string>(type: "TEXT", nullable: true),
            //        cardsphereSetId = table.Column<long>(type: "INTEGER", nullable: true),
            //        isFoilOnly = table.Column<byte[]>(type: "BOOLEAN", nullable: true),
            //        isForeignOnly = table.Column<byte[]>(type: "BOOLEAN", nullable: true),
            //        isNonFoilOnly = table.Column<byte[]>(type: "BOOLEAN", nullable: true),
            //        isOnlineOnly = table.Column<byte[]>(type: "BOOLEAN", nullable: true),
            //        isPartialPreview = table.Column<byte[]>(type: "BOOLEAN", nullable: true),
            //        keyruneCode = table.Column<string>(type: "TEXT", nullable: true),
            //        languages = table.Column<string>(type: "TEXT", nullable: true),
            //        mcmId = table.Column<long>(type: "INTEGER", nullable: true),
            //        mcmIdExtras = table.Column<long>(type: "INTEGER", nullable: true),
            //        mcmName = table.Column<string>(type: "TEXT", nullable: true),
            //        mtgoCode = table.Column<string>(type: "TEXT", nullable: true),
            //        name = table.Column<string>(type: "TEXT", nullable: true),
            //        parentCode = table.Column<string>(type: "TEXT", nullable: true),
            //        releaseDate = table.Column<string>(type: "TEXT", nullable: false),
            //        tcgplayerGroupId = table.Column<long>(type: "INTEGER", nullable: true),
            //        tokenSetCode = table.Column<string>(type: "TEXT", nullable: true),
            //        totalSetSize = table.Column<long>(type: "INTEGER", nullable: true),
            //        type = table.Column<string>(type: "TEXT", nullable: true)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_sets", x => x.code);
            //    });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cardForeignData");

            migrationBuilder.DropTable(
                name: "cardIdentifiers");

            migrationBuilder.DropTable(
                name: "cardLegalities");

            migrationBuilder.DropTable(
                name: "cardPurchaseUrls");

            migrationBuilder.DropTable(
                name: "cardRulings");

            migrationBuilder.DropTable(
                name: "cards");

            migrationBuilder.DropTable(
                name: "meta");

            migrationBuilder.DropTable(
                name: "setBoosterContents");

            migrationBuilder.DropTable(
                name: "setBoosterContentWeights");

            migrationBuilder.DropTable(
                name: "setBoosterSheetCards");

            migrationBuilder.DropTable(
                name: "setBoosterSheets");

            migrationBuilder.DropTable(
                name: "sets");

            migrationBuilder.DropTable(
                name: "setTranslations");

            migrationBuilder.DropTable(
                name: "tokenIdentifiers");

            migrationBuilder.DropTable(
                name: "tokens");
        }
    }
}
