using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

    public Set() { }
    public Set(JToken jToken)
    {
        BaseSetSize = jToken["baseSetSize"]?.Value<long>();
        Code = jToken["code"]?.Value<string>();
        IsFoilOnly = (bool)jToken["isFoilOnly"] ? new byte[] { 1 } : new byte[] { 0 };
        IsOnlineOnly = (bool)jToken["isOnlineOnly"] ? new byte[] { 1 } : new byte[] { 0 };
        IsPartialPreview = (bool)jToken["isPartialPreview"] ? new byte[] { 1 } : new byte[] { 0 };
        KeyruneCode = jToken["keyruneCode"]?.Value<string>();
        McmId = jToken["mcmId"]?.Value<long>();
        McmIdExtras = jToken["mcmIdExtras"]?.Value<long>();
        McmName = jToken["mcmName"]?.Value<string>();
        MtgoCode = jToken["mtgoCode"]?.Value<string>();
        Name = jToken["name"]?.Value<string>();
        ReleaseDate = jToken["releaseDate"]?.Value<string>();
        TcgplayerGroupId = jToken["tcgPlayerGroupId"]?.Value<long>();
        TokenSetCode = jToken["tokenSetCode"]?.Value<string>();
        TotalSetSize = jToken["totalSetSize"]?.Value<long>();
        Type = jToken["type"]?.Value<string>();

        var languages = jToken["languages"]?.Values<string>().ToList();
        Languages = string.Join(", ", languages);
    }
}
