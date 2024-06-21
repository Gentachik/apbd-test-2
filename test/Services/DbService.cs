using Microsoft.EntityFrameworkCore;
using test.Contex;
using test.DTOs.CharacterDto;
using test.Models;

namespace test.Services;

public class DbService: IDbService
{
    private readonly DatabaseContext _context;
    public DbService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<bool> DoesCharacterExist(int characterId)
    {
        return await _context.Characters.AnyAsync(c => c.Id == characterId);
    }

    public async Task<Character?> GetCharacter(int characterId)
    {
        return await _context.Characters.FirstAsync(c => c.Id == characterId);
    }

    public async Task<List<BackpackItemsDto>> GetItemsForCharacter(int characterId)
    {
        return await _context.Backpacks
            .Where(b => b.CharacterId == characterId)
            .Join(
                _context.Items,
                ba => ba.ItemId,
                i => i.Id,
                (ba, i) => new BackpackItemsDto()
                {
                    ItemName = i.Name,
                    ItemWeight = i.Weight,
                    Amount = ba.Amount
                }
            ).ToListAsync();
    }

    public async Task<List<TitleDto>> GetTitlesForCharacter(int characterId)
    {
        return await _context.CharacterTitles
            .Where(c => c.CharacterId == characterId)
            .Join(
                _context.Titles,
                ct => ct.TitleId,
                t => t.Id,
                (ct, t) => new TitleDto
                {
                    Title = t.Name,
                    AcquiredAt = ct.AcquiredAt
                }
            ).ToListAsync();
    }
    public async Task<bool> DoesItemsExist(List<int> itemIdList)
    {
        return await _context.Items.CountAsync(i => itemIdList.Contains(i.Id)) == itemIdList.Count;
    }

    public async Task<int> GetSumWeight(List<int> itemIdList)
    {
        return await _context.Items
            .Where(i => itemIdList.Contains(i.Id))
            .SumAsync(i => i.Weight);
    }

    public async Task AddItemsToCharacter(int characterId, List<int> itemIdList)
    {
        var character = await _context.Characters.FindAsync(characterId);

        var backpackItems = itemIdList.Select(itemId => new Backpack
        {
            CharacterId = characterId,
            ItemId = itemId,
            Amount = 1
        }).ToList();

        _context.Backpacks.AddRange(backpackItems);

        var sumWeight = await GetSumWeight(itemIdList);
        character.CurrentWeight += sumWeight;

        await _context.SaveChangesAsync();
    }

    public async Task<List<Backpack>> GetItems(List<int> itemIdList)
    {
        return await _context.Backpacks
            .Where(b => itemIdList.Contains(b.ItemId))
            .ToListAsync();
    }
}