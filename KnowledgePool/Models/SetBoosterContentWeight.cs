using System;
using System.Collections.Generic;

namespace KnowledgePool.Models;

public partial class SetBoosterContentWeight
{
    public long? BoosterIndex { get; set; }

    public string? BoosterName { get; set; }

    public long? BoosterWeight { get; set; }

    public string? SetCode { get; set; }
}
