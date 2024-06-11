using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kolok2.Models;


[Table("Titles")]
public class Title
{
    [Key]
    [Column("PK")]
    public int TitleId { get; set; }
    
    [MaxLength(100)]
    [Column("nam")]
    public string TitleName { get; set; }

    public ICollection<CharacterTitle> CharacterTitles { get; set; }
}
