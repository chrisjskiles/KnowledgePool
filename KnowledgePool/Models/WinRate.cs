using System.ComponentModel.DataAnnotations;

namespace KnowledgePool.Models
{
    public class WinRate
    {
        [Key]
        [Required]
        public string Uuid { get; set; } = null!;
        public string? Name { get; set; }
        public string? Set { get; set; }
        public string? Color { get; set; }
        public string? Rarity { get; set; }
        public decimal? OhWr { get; set; }
        public decimal? GdWr { get; set; }
        public decimal? GihWr { get; set; }
        public decimal? Iwd { get; set; }

    }
}
