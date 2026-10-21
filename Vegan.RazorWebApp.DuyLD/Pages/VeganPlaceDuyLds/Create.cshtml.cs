using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vegan.Entities.DuyLD.Models;
using Vegan.Services.DuyLD;

namespace Vegan.RazorWebApp.DuyLD.Pages.VeganPlaceDuyLds;

public class CreateModel : PageModel
{
    private readonly IVeganPlaceServiceDuyLd _service;

    public CreateModel(IVeganPlaceServiceDuyLd service)
    {
        _service = service;
    }

    [BindProperty]
    public VeganPlaceDuyLd VeganPlaceDuyLd { get; set; } = new() { IsActive = true };

    public IActionResult OnGet()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        VeganPlaceDuyLd.CreatedAt = DateTime.Now;
        await _service.CreateAsync(VeganPlaceDuyLd);
        return RedirectToPage("./Index");
    }
}

