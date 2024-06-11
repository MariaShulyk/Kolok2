using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kolok2.Models;


[Table("Characters")]
public class Character
{
    [Key]
    [Column("PK")]
    public int CharacterId { get; set; }
    
    [MaxLength(50)]
    [Column("first_name")]
    public string CharacterFirstName { get; set; }
    
    [MaxLength(50)]
    [Column("last_name")]
    public string CharacterLastName { get; set; }
    
    [Column("current_weig")]
    public int CharacterCurrentWeight { get; set; }
    
    [Column("max_weight")]
    public int CharacterMaxWeight { get; set; }
    
    [Column("money")]
    public int CharacterMoney { get; set; }

    public ICollection<BackpackSlot> BackpackSlots { get; set; }
    public ICollection<CharacterTitle> CharacterTitles { get; set; }
}    