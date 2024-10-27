using System.ComponentModel.DataAnnotations;

namespace CodeBridgeTask.DataAccess.Models;

public class Dog
{
    [Key]
    public int Id { get; init; }
    [Required(ErrorMessage = "Please enter the name of the dog")]
    public string Name { get; init; }
    [Required(ErrorMessage = "Please enter the breed of the dog")]
    public string Color { get; init; }
    [Required(ErrorMessage = "Please enter the age of the dog")]
    public float TailLength { get; init; }
    [Required(ErrorMessage = "Please enter the height of the dog")]
    public float Weight { get; init; }
}
