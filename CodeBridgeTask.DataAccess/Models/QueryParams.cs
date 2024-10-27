using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace CodeBridgeTask.DataAccess.Models;

public class QueryParams
{
    [FromQuery(Name = "attribute")]
    public string? Attribute { get; set; }
    [FromQuery(Name = "order")]
    public string? Order { get; set; }
    [FromQuery(Name = "pageNumber")]
    public int PageNumber { get; set; } = 1;
    [FromQuery(Name = "pageSize")]
    public int PageSize { get; set; } = 10;
}
