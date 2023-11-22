using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KnowledgePool.Models;

public partial class Set
{
    public long? BaseSetSize { get; set; }

    public string? Block { get; set; }

    public long? CardsphereSetId { get; set; }

    [Key]
    [Required]
    public string Code { get; set; } = null!;

    public byte[]? IsFoilOnly { get; set; }

    public byte[]? IsForeignOnly { get; set; }

    public byte[]? IsNonFoilOnly { get; set; }

    public byte[]? IsOnlineOnly { get; set; }

    public byte[]? IsPartialPreview { get; set; }

    public string? KeyruneCode { get; set; }

    public string? Languages { get; set; }

    public long? McmId { get; set; }

    public long? McmIdExtras { get; set; }

    public string? McmName { get; set; }

    public string? MtgoCode { get; set; }

    [Required]
    public string Name { get; set; }

    public string? ParentCode { get; set; }

    [Required]
    public string ReleaseDate { get; set; }

    public long? TcgplayerGroupId { get; set; }

    public string? TokenSetCode { get; set; }

    public long? TotalSetSize { get; set; }

    public string? Type { get; set; }
}
