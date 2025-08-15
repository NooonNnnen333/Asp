using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace MoviesAPI.Entities;

public class Move
{
    public int Id { get; set; }

    [Required] [StringLength(55)] 
    public required string Name { get; set; }
    
    [Required] [Range(0, 100)]
    public decimal Price { get; set; }
    
    [ValidateNever]
    public Genre? Genre { get; set; }

    public int GenereId { get; set; }
    
    public DateOnly RealisDate { get; set; }



}