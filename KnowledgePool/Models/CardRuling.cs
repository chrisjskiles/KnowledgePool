using System;
using System.Collections.Generic;

namespace KnowledgePool.Models;

public partial class CardRuling
{
    public byte[]? Date { get; set; }

    public string? Text { get; set; }

    public string Uuid { get; set; } = null!;
}
