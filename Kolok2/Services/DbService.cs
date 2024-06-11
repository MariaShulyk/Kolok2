using Kolok2.Context;
using Kolok2.Models;
using Kolok2.RequestModels;
using Kolok2.ResponseModels;
using Microsoft.EntityFrameworkCore;
using Kolok2.Exceptions;

namespace Kolok2.Services;

public interface IDbService
{
    Task<GetCharacterResModel> ShowCharacterByIdAsync(int id, CancellationToken cancellationToken);
    Task<List<BackpackSecondResModel>> AddItemsAsync(int characterId, AddItemsReqModel reqModel, CancellationToken cancellationToken);
}

public class DbService(ContextDB contextDb) : IDbService
{
    public async Task<GetCharacterResModel> ShowCharacterByIdAsync(int id, CancellationToken cancellationToken)
    {
        var character = await contextDb.Characters
            .Include(c => c.BackpackSlots)
            .ThenInclude(bs => bs.Item)
            .Include(c => c.CharacterTitles)
            .ThenInclude(ct => ct.Title)
            .FirstOrDefaultAsync(c => c.CharacterId == id, cancellationToken);

        //-------------------
        
        
        if (character == null)
        {
            throw new NotFoundError($"Character with id {id} doesn't exist");
        }

        var response = new GetCharacterResModel
        {
            FirstName = character.CharacterFirstName,
            LastName = character.CharacterLastName,
            CurrentWeight = character.CharacterCurrentWeight,
            MaxWeight = character.CharacterMaxWeight,
            Money = character.CharacterMoney,
            BackpackSlots = character.BackpackSlots.Select(bs => new BackpackFirstResModel
            {
                SlotId = bs.ItemId, 
                ItemName = bs.Item.ItemName,
                ItemWeight = bs.Item.ItemWeight
            }).ToList(),
            Titles = character.CharacterTitles.Select(ct => new TitleResModel
            {
                Title = ct.Title.TitleName,
                AcquireAt = ct.AcquireAt
            }).ToList()
        };

        return response;
    }
    
    //-------------------
    
    public async Task<List<BackpackSecondResModel>> AddItemsAsync(int characterId, AddItemsReqModel reqModel, CancellationToken cancellationToken)
    {
        var character = await contextDb.Characters
            .Include(c => c.BackpackSlots)
            .ThenInclude(bs => bs.Item)
            .FirstOrDefaultAsync(c => c.CharacterId == characterId, cancellationToken);

        if (character == null)
        {
            throw new NotFoundError($"Character with id {characterId} doesn't exist");
        }

        var items = await contextDb.Items
            .Where(i => reqModel.ItemIds.Contains(i.ItemId))
            .ToListAsync(cancellationToken);

        if (items.Count != reqModel.ItemIds.Count)
        {
            throw new BadRequestError("Items don't exist");
        }

        var totalWeight = items.Sum(i => i.ItemWeight);
        if (character.CharacterCurrentWeight + totalWeight > character.CharacterMaxWeight)
        {
            throw new BadRequestError("Character doesn't have enough capacity");
        }

        var newBackpackSlots = new List<BackpackSlot>();
        
        foreach (var item in items)
        {
            var newSlot = new BackpackSlot
            {
                ItemId = item.ItemId,
                CharacterId = character.CharacterId
            };
            
            contextDb.BackpackSlots.Add(newSlot);
            newBackpackSlots.Add(newSlot);
        }

        character.CharacterCurrentWeight += totalWeight;
        await contextDb.SaveChangesAsync(cancellationToken);

        return newBackpackSlots.Select(bs => new BackpackSecondResModel
        {
            SlotId = bs.BackpackSlotId,
            ItemId = bs.ItemId,
            CharacterId = bs.CharacterId
        }).ToList();
    }
}