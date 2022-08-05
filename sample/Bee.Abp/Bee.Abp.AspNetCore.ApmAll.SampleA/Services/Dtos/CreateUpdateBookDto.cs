using System.ComponentModel.DataAnnotations;
using Bee.Abp.AspNetCore.ApmAll.SampleA.Entities;

namespace Bee.Abp.AspNetCore.ApmAll.SampleA.Services.Dtos;

public class CreateUpdateBookDto
{
    [Required]
    [StringLength(128)]
    public string Name { get; set; }

    [Required]
    public BookType Type { get; set; } = BookType.Undefined;

    [Required]
    [DataType(DataType.Date)]
    public DateTime PublishDate { get; set; } = DateTime.Now;

    [Required]
    public float Price { get; set; }
}