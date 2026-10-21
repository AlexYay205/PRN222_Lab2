using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vegan.Entities.DuyLD.Models;
using Vegan.Services.DuyLD;

namespace Vegan.RazorWebApp.DuyLD.Pages.VeganPlaceDuyLds;

public class DeleteModel : PageModel
{
    private readonly IVeganPlaceServiceDuyLd _service;

    public DeleteModel(IVeganPlaceServiceDuyLd service)
    {
        _service = service;
    }

    [BindProperty]
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

    public async Task<IActionResult> OnPostAsync(long? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        await _service.DeleteAsync(id.Value);
        return RedirectToPage("./Index");
    }
}

