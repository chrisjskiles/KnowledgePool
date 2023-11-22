using System;
using System.Collections.Generic;

namespace KnowledgePool.Models;

public partial class SetBoosterSheet
{
    public string? BoosterName { get; set; }

    public string? SetCode { get; set; }

    public byte[]? SheetHasBalanceColors { get; set; }

    public byte[]? SheetIsFoil { get; set; }

    public string? SheetName { get; set; }
}
