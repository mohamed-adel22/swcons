using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Engine
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Ensures auto-increment behavior
    public int Id { get; set; }
    public   string? Name { get; set; }
    public  string? CarModel { get; set; }
    public  string? Code { get; set; }
    public int? Price { get; set; }

}
