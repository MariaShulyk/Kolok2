using Kolok2.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Kolok2.Services;
using Kolok2.Exceptions;

namespace Kolok2.Controllers;

[ApiController]
[Route("api/characters")]
public class CharactersController(IDbService _dbService) : ControllerBase
{

    [HttpGet("{characterId:int}")]
    public async Task<IActionResult> GetCharacter(int characterId, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _dbService.ShowCharacterByIdAsync(characterId, cancellationToken);
            return Ok(result);
        }
        catch (NotFoundError e)
        {
            return NotFound(e.Message);
        }
    }
    
    [HttpPost("{characterId:int}/backpackslots")]
    public async Task<IActionResult> AddItemsToBackpack(int characterId, AddItemsReqModel reqModel, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _dbService.AddItemsAsync(characterId, reqModel, cancellationToken);
            return Created(string.Empty, result);
        }
        catch (NotFoundError e)
        {
            return NotFound(e.Message);
        }
        catch (BadRequestError e)
        {
            return BadRequest(e.Message);
        }
    }
}