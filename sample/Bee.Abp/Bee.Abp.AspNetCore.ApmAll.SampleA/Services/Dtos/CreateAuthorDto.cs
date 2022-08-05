using System.ComponentModel.DataAnnotations;

namespace Bee.Abp.AspNetCore.ApmAll.SampleA.Services.Dtos;

public class CreateAuthorDto
{
    [Required]
    [StringLength(128)]
    public string Name { get; set; }

    [Required]
    public DateTime BirthDate { get; set; }

    public string ShortBio { get; set; }
}