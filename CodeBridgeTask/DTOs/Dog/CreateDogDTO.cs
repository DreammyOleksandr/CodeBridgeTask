using System.ComponentModel.DataAnnotations;

namespace CodeBridgeTask.DTOs;

public class CreateDogDTO
{
    [Required(ErrorMessage = "Please enter the name of the dog")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Please enter the color of the dog")]
    public string Color { get; set; }
    [Required(ErrorMessage = "Please enter the tail length of the dog")]
    public float TailLength { get; set; }
    [Required(ErrorMessage = "Please enter the weight of the dog")]
    public float Weight { get; set; }
}
