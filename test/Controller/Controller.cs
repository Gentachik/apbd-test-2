using Microsoft.AspNetCore.Mvc;
using test.DTOs.CharacterDto;
using test.Services;

namespace test.Controller;

[Route("api/characters")]
[ApiController]
public class Controller : ControllerBase
{
    private readonly IDbService _dbService;
    public Controller(IDbService dbService)
    {
        _dbService = dbService;
    }
    
    [HttpPost("/{characterId:int}")]
    public async Task<IActionResult> GetCharacter(int characterId)
    {
        if (await _dbService.DoesCharacterExist(characterId))
        {
            return NotFound($"Character with {characterId} not found");
        }

        var character = await _dbService.GetCharacter(characterId);
        if (character == null)
        {
            return NotFound($"Character with {characterId} not found");
        }
        
        return Ok(new CharacterDto
        {
            FirstName = character.FirstName,
            LastName = character.LastName,
            CurrentWeight = character.CurrentWeight,
            MaxWeight = character.MaxWeight,
            BackpackItems = await _dbService.GetItemsForCharacter(characterId),
            Titles = await _dbService.GetTitlesForCharacter(characterId)
        });
    }

    [HttpPost("/{characterId:int}/backpacks")]
    public async Task<IActionResult> AddItems(int characterId, List<int> itemIdList)
    {
        if (!await _dbService.DoesCharacterExist(characterId))
        {
            return NotFound($"Character with {characterId} not found");
        }

        if (!await _dbService.DoesItemsExist(itemIdList))
        {
            return BadRequest($"Items with this id: {itemIdList} not found");
        }

        var character = await _dbService.GetCharacter(characterId);
        if (character == null)
        {
            return NotFound($"Character with {characterId} not found");
        }

        var total = await _dbService.GetSumWeight(itemIdList);
        if (character.CurrentWeight + total > character.MaxWeight)
        {
            return BadRequest("Weight too big");
        }

        await _dbService.AddItemsToCharacter(characterId, itemIdList);

        return Ok(await _dbService.GetItems(itemIdList));
    }
    
}