using System.ComponentModel.DataAnnotations.Schema;

namespace test.Models;

[Table("backpacks")]
public class Backpack
{
    public int CharacterId { get; set; }
    public int ItemId { get; set; }
    public int Amount { get; set; }
}