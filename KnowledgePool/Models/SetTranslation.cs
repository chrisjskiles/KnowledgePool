using System;
using System.Collections.Generic;

namespace KnowledgePool.Models;

public partial class SetTranslation
{
    public string? Language { get; set; }

    public string? Translation { get; set; }

    public string Uuid { get; set; } = null!;
}
