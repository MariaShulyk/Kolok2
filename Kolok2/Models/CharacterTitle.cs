using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kolok2.Models;


[Table("Characters_Titles")]
[PrimaryKey(nameof(CharacterId), nameof(TitleId))]
public class CharacterTitle
{
    [ForeignKey(nameof(Character))]
    [Column("FK_charact")]
    public int CharacterId { get; set; }
    public Character Character { get; set; }
    

    [ForeignKey(nameof(Title))]
    [Column("FK_title")]
    public int TitleId { get; set; }
    public Title Title { get; set; }
    
    
    [Column("aquire_at")]
    public DateTime AcquireAt { get; set; }
}