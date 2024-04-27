using System.ComponentModel.DataAnnotations;

namespace Application.DTOs;
public class CurrencyDto
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Currency Code is required")]
    public string Code { get; set; }

    [Required(ErrorMessage = "Currency Exchange Rate is required")]
    public decimal Exchange { get; set; }
}
