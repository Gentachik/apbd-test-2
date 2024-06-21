using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace test.Models;

[Table("titles")]
public class Title
{
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    public string Name { get; set; }

    public ICollection<Character> Characters { get; set; } =  new HashSet<Character>();
}