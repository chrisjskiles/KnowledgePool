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
}
