using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kolok2.Models;


[Table("Items")]
public class Item
{
    [Key]
    [Column("PK")]
    public int ItemId { get; set; }
    
    [MaxLength(50)]
    [Column("name")]
    public string ItemName { get; set; }
    
    [Column("weig")]
    public int ItemWeight { get; set; }
    

    public ICollection<BackpackSlot> BackpackSlots { get; set; }
}

