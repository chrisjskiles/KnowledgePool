using System;
using System.Collections.Generic;

namespace KnowledgePool.Models;

public partial class SetBoosterContent
{
    public long? BoosterIndex { get; set; }

    public string? BoosterName { get; set; }

    public string? SetCode { get; set; }

    public string? SheetName { get; set; }

    public long? SheetPicks { get; set; }
}
