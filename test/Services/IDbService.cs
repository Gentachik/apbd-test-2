using test.DTOs.CharacterDto;
using test.Models;

namespace test.Services;

public interface IDbService
{
    Task<bool> DoesCharacterExist(int characterId);
    Task<Character?> GetCharacter(int characterId);
    Task<List<BackpackItemsDto>> GetItemsForCharacter(int characterId);
    Task<List<TitleDto>> GetTitlesForCharacter(int characterId);
    Task<bool> DoesItemsExist(List<int> itemIdList);
    Task<int> GetSumWeight(List<int> itemIdList);
    Task AddItemsToCharacter(int characterId, List<int> itemIdList);
    Task<List<Backpack>> GetItems(List<int> itemIdList);
}