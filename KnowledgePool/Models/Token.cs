using System;
using System.Collections.Generic;

namespace KnowledgePool.Models;

public partial class Token
{
    public string? Artist { get; set; }

    public string? ArtistIds { get; set; }

    public string? AsciiName { get; set; }

    public string? Availability { get; set; }

    public string? BoosterTypes { get; set; }

    public string? BorderColor { get; set; }

    public string? ColorIdentity { get; set; }

    public string? Colors { get; set; }

    public double? EdhrecSaltiness { get; set; }

    public string? FaceName { get; set; }

    public string? Finishes { get; set; }

    public string? FlavorText { get; set; }

    public string? FrameEffects { get; set; }

    public string? FrameVersion { get; set; }

    public byte[]? HasFoil { get; set; }

    public byte[]? HasNonFoil { get; set; }

    public byte[]? IsFullArt { get; set; }

    public byte[]? IsFunny { get; set; }

    public byte[]? IsPromo { get; set; }

    public byte[]? IsReprint { get; set; }

    public byte[]? IsTextless { get; set; }

    public string? Keywords { get; set; }

    public string? Language { get; set; }

    public string? Layout { get; set; }

    public string? ManaCost { get; set; }

    public string? Name { get; set; }

    public string? Number { get; set; }

    public string? Orientation { get; set; }

    public string? OriginalText { get; set; }

    public string? OriginalType { get; set; }

    public string? OtherFaceIds { get; set; }

    public string? Power { get; set; }

    public string? PromoTypes { get; set; }

    public string? RelatedCards { get; set; }

    public string? ReverseRelated { get; set; }

    public string? SecurityStamp { get; set; }

    public string? SetCode { get; set; }

    public string? Side { get; set; }

    public string? Signature { get; set; }

    public string? Subtypes { get; set; }

    public string? Supertypes { get; set; }

    public string? Text { get; set; }

    public string? Toughness { get; set; }

    public string? Type { get; set; }

    public string? Types { get; set; }

    public string Uuid { get; set; } = null!;

    public string? Watermark { get; set; }
}
