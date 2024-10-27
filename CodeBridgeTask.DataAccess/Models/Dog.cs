using System.ComponentModel.DataAnnotations;

namespace CodeBridgeTask.DataAccess.Models;

public class Dog
{
    [Key]
    public int Id { get; init; }
    public string Name { get; init; }
    public string Color { get; init; }
    public float TailLength { get; init; }
    public float Weight { get; init; }
}
