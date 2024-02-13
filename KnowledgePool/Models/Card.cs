using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KnowledgePool.Models;

public partial class Card
{
    public string? Artist { get; set; }

    public string? ArtistIds { get; set; }

    public string? AsciiName { get; set; }

    public string? AttractionLights { get; set; }

    public string? Availability { get; set; }

    public string? BoosterTypes { get; set; }

    public string? BorderColor { get; set; }

    public string? CardParts { get; set; }

    public string? ColorIdentity { get; set; }

    public string? ColorIndicator { get; set; }

    public string? Colors { get; set; }

    public string? Defense { get; set; }

    public string? DuelDeck { get; set; }

    public long? EdhrecRank { get; set; }

    public double? EdhrecSaltiness { get; set; }

    public double? FaceConvertedManaCost { get; set; }

    public string? FaceFlavorName { get; set; }

    public double? FaceManaValue { get; set; }

    public string? FaceName { get; set; }

    public string? Finishes { get; set; }

    public string? FlavorName { get; set; }

    public string? FlavorText { get; set; }

    public string? FrameEffects { get; set; }

    public string? FrameVersion { get; set; }

    public string? Hand { get; set; }

    public byte[]? HasAlternativeDeckLimit { get; set; }

    public byte[]? HasContentWarning { get; set; }

    public byte[]? HasFoil { get; set; }

    public byte[]? HasNonFoil { get; set; }

    public byte[]? IsAlternative { get; set; }

    public byte[]? IsFullArt { get; set; }

    public byte[]? IsFunny { get; set; }

    public byte[]? IsOnlineOnly { get; set; }

    public byte[]? IsOversized { get; set; }

    public byte[]? IsPromo { get; set; }

    public byte[]? IsRebalanced { get; set; }

    public byte[]? IsReprint { get; set; }

    public byte[]? IsReserved { get; set; }

    public byte[]? IsStarter { get; set; }

    public byte[]? IsStorySpotlight { get; set; }

    public byte[]? IsTextless { get; set; }

    public byte[]? IsTimeshifted { get; set; }

    public string? Keywords { get; set; }

    public string? Language { get; set; }

    public string? Layout { get; set; }

    public string? LeadershipSkills { get; set; }

    public string? Life { get; set; }

    public string? Loyalty { get; set; }

    public string? ManaCost { get; set; }

    public double? ManaValue { get; set; }

    public string? Name { get; set; }

    public string? Number { get; set; }

    public string? OriginalPrintings { get; set; }

    public string? OriginalReleaseDate { get; set; }

    public string? OriginalText { get; set; }

    public string? OriginalType { get; set; }

    public string? OtherFaceIds { get; set; }

    public string? Power { get; set; }

    public string? Printings { get; set; }

    public string? PromoTypes { get; set; }

    public string? Rarity { get; set; }

    public string? RebalancedPrintings { get; set; }

    public string? RelatedCards { get; set; }

    public string? SecurityStamp { get; set; }

    public string? SetCode { get; set; }

    public string? Side { get; set; }

    public string? Signature { get; set; }

    public string? SourceProducts { get; set; }

    public string? Subsets { get; set; }

    public string? Subtypes { get; set; }

    public string? Supertypes { get; set; }

    public string? Text { get; set; }

    public string? Toughness { get; set; }

    [Required]
    public string Type { get; set; }

    public string? Types { get; set; }

    [Key]
    [Required]
    public string Uuid { get; set; } = null!;

    public string? Variations { get; set; }

    public string? Watermark { get; set; }

    public Card() { }

    //constructor to parse JSON into card object
    public Card(JToken jToken)
    {
        Artist = jToken["artist"]?.ToString();
        AsciiName = jToken["asciiName"]?.ToString();
        AttractionLights = jToken["attractionLights"]?.ToString();
        BorderColor = jToken["borderColor"]?.ToString();
        CardParts = jToken["cardParts"]?.ToString();
        ColorIndicator = jToken["colorIndicator"]?.ToString();
        Defense = jToken["defense"]?.ToString();
        DuelDeck = jToken["duelDeck"]?.ToString();
        EdhrecRank = jToken["edhrecRank"]?.Value<long?>();
        EdhrecSaltiness = jToken["edhrecSaltiness"]?.Value<double?>();
        FaceConvertedManaCost = jToken["faceConvertedManaCost"]?.Value<double?>();
        FaceFlavorName = jToken["faceFlavorName"]?.ToString();
        FaceManaValue = jToken["faceManaValue"]?.Value<double?>();
        FaceName = jToken["faceName"]?.ToString();
        FlavorName = jToken["flavorName"]?.ToString();
        FlavorText = jToken["flavorText"]?.ToString();
        FrameVersion = jToken["frameVersion"]?.ToString();
        Hand = jToken["hand"]?.ToString();
        HasAlternativeDeckLimit = jToken?["hasAlternativeDeckLimit"]?.Value<bool>() ?? false ? new byte[] { 1 } : new byte[] { 0 };
        HasContentWarning = jToken?["hasContentWarning"]?.Value<bool>() ?? false ? new byte[] { 1 } : new byte[] { 0 };
        HasFoil = jToken?["hasFoil"]?.Value<bool>() ?? false ? new byte[] { 1 } : new byte[] { 0 };
        HasNonFoil = jToken?["hasNonFoil"]?.Value<bool>() ?? false ? new byte[] { 1 } : new byte[] { 0 };
        IsAlternative = jToken?["isAlternative"]?.Value<bool>() ?? false ? new byte[] { 1 } : new byte[] { 0 };
        IsFullArt = jToken?["isFullArt"]?.Value<bool>() ?? false ? new byte[] { 1 } : new byte[] { 0 };
        IsFunny = jToken?["isFunny"]?.Value<bool>() ?? false ? new byte[] { 1 } : new byte[] { 0 };
        IsOnlineOnly = jToken?["isOnlineOnly"]?.Value<bool>() ?? false ? new byte[] { 1 } : new byte[] { 0 };
        IsOversized = jToken?["isOversized"]?.Value<bool>() ?? false ? new byte[] { 1 } : new byte[] { 0 };
        IsPromo = jToken?["isPromo"]?.Value<bool>() ?? false ? new byte[] { 1 } : new byte[] { 0 };
        IsRebalanced = jToken?["isRebalanced"]?.Value<bool>() ?? false ? new byte[] { 1 } : new byte[] { 0 };
        IsReprint = jToken?["isReprint"]?.Value<bool>() ?? false ? new byte[] { 1 } : new byte[] { 0 };
        IsReserved = jToken?["isReserved"]?.Value<bool>() ?? false ? new byte[] { 1 } : new byte[] { 0 };
        IsStarter = jToken?["isStarter"]?.Value<bool>() ?? false ? new byte[] { 1 } : new byte[] { 0 };
        IsStorySpotlight = jToken?["isStorySpotlight"]?.Value<bool>() ?? false ? new byte[] { 1 } : new byte[] { 0 };
        IsTextless = jToken?["isTextless"]?.Value<bool>() ?? false ? new byte[] { 1 } : new byte[] { 0 };
        IsTimeshifted = jToken?["isTimeshifted"]?.Value<bool>() ?? false ? new byte[] { 1 } : new byte[] { 0 };
        Language = jToken["language"]?.ToString();
        Layout = jToken["layout"]?.ToString();
        Life = jToken["life"]?.ToString();
        Loyalty = jToken["loyalty"]?.ToString();
        ManaCost = jToken["manaCost"]?.ToString();
        ManaValue = jToken["manaValue"]?.Value<double?>();
        Name = jToken["name"]?.ToString();
        Number = jToken["number"]?.ToString();
        OriginalPrintings = jToken["originalPrintings"]?.ToString();
        OriginalReleaseDate = jToken["originalReleaseDate"]?.ToString();
        OriginalText = jToken["originalText"]?.ToString();
        OriginalType = jToken["originalType"]?.ToString();
        OtherFaceIds = jToken["otherFaceIds"]?.ToString();
        Power = jToken["power"]?.ToString();
        Rarity = jToken["rarity"]?.ToString();
        RebalancedPrintings = jToken["rebalancedPrintings"]?.ToString();
        RelatedCards = jToken["relatedCards"]?.ToString();
        SecurityStamp = jToken["securityStamp"]?.ToString();
        SetCode = jToken["setCode"]?.ToString();
        Side = jToken["side"]?.ToString();
        Signature = jToken["signature"]?.ToString();
        SourceProducts = jToken["sourceProducts"]?.ToString();
        Subsets = jToken["subsets"]?.ToString();
        Text = jToken["text"]?.ToString();
        Toughness = jToken["toughness"]?.ToString();
        Type = jToken["type"]?.ToString() ?? throw new ArgumentException("Type is required", nameof(jToken));
        Uuid = jToken["uuid"]?.ToString() ?? throw new ArgumentException("Uuid is required", nameof(jToken));
        Watermark = jToken["watermark"]?.ToString();

        var artistIds = jToken["artistIds"]?.Values<string>().ToList();
        if (artistIds is not null) ArtistIds = string.Join(", ", artistIds);

        var availability = jToken["availability"]?.Values<string>().ToList();
        if (availability is not null) Availability = string.Join(", ", availability);

        var boosterTypes = jToken["boosterTypes"]?.Values<string>().ToList();
        if (boosterTypes is not null) BoosterTypes = string.Join(", ", boosterTypes);

        var finishes = jToken["finishes"]?.Values<string>().ToList();
        if (finishes is not null) Finishes = string.Join(", ", finishes);

        var colorIdentity = jToken["colorIdentity"]?.Values<string>().ToList();
        if (colorIdentity is not null) ColorIdentity = string.Join(string.Empty, colorIdentity);

        var colors = jToken["colors"]?.Values<string>().ToList();
        if (colors is not null) Colors = string.Join(string.Empty, colors);

        var frameEffects = jToken["frameEffects"]?.Values<string>().ToList();
        if (frameEffects is not null) FrameEffects = string.Join(", ", frameEffects);

        var keywords = jToken["keywords"]?.Values<string>().ToList();
        if (keywords is not null) Keywords = string.Join(", ", keywords);

        var printings = jToken["printings"]?.Values<string>().ToList();
        if (printings is not null) Printings = string.Join(", ", printings);

        var promoTypes = jToken["promoTypes"]?.Values<string>().ToList();
        if (promoTypes is not null) PromoTypes = string.Join(", ", promoTypes);

        var subtypes = jToken["subtypes"]?.Values<string>().ToList();
        if (subtypes is not null) Subtypes = string.Join(", ", subtypes);

        var supertypes = jToken["supertypes"]?.Values<string>().ToList();
        if (supertypes is not null) Supertypes = string.Join(", ", supertypes);

        var types = jToken["types"]?.Values<string>().ToList();
        if (types is not null) Types = string.Join(", ", types);

        var variations = jToken["variations"]?.Values<string>().ToList();
        if (variations is not null) Variations = string.Join(", ", variations);
    }
}
