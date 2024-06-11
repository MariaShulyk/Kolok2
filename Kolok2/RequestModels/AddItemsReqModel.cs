using System.ComponentModel.DataAnnotations;

namespace Kolok2.RequestModels;

public class AddItemsReqModel
{
    [Required]
    public List<int> ItemIds { get; set; }
}