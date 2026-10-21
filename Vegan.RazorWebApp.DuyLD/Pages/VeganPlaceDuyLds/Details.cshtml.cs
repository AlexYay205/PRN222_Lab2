using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vegan.Entities.DuyLD.Models;
using Vegan.Services.DuyLD;

namespace Vegan.RazorWebApp.DuyLD.Pages.VeganPlaceDuyLds;

public class DetailsModel : PageModel
{
    private readonly IVeganPlaceServiceDuyLd _service;

    public DetailsModel(IVeganPlaceServiceDuyLd service)
    {
        _service = service;
    }

    public VeganPlaceDuyLd VeganPlaceDuyLd { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var place = await _service.GetByIdAsync(id.Value);
        if (place == null)
        {
            return NotFound();
        }

        VeganPlaceDuyLd = place;
        return Page();
    }
}

