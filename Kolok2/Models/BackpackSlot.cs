using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kolok2.Models;


[Table("Backpack_Slots")]
public class BackpackSlot
{
    [Key]
    [Column("PK")]
    public int BackpackSlotId { get; set; }
    
    
    [ForeignKey(nameof(Item))]
    [Column("FK_item")]
    public int ItemId { get; set; }
    public Item Item { get; set; }
    

    [ForeignKey(nameof(Character))]
    [Column("FK_character")]
    public int CharacterId { get; set; }
    public Character Character { get; set; }
}