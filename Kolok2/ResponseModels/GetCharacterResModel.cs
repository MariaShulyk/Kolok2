namespace Kolok2.ResponseModels;

public class GetCharacterResModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int CurrentWeight { get; set; }
    public int MaxWeight { get; set; }
    public int Money { get; set; }
    public List<BackpackFirstResModel> BackpackSlots { get; set; }
    public List<TitleResModel> Titles { get; set; }
}



