using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vegan.Entities.DuyLD.Models;
using Vegan.Services.DuyLD;

namespace Vegan.RazorWebApp.DuyLD.Pages.VeganPlaceDuyLds;

public class IndexModel : PageModel
{
    private const int PageSize = 5;
    private readonly IVeganPlaceServiceDuyLd _veganPlaceService;

    public IndexModel(IVeganPlaceServiceDuyLd veganPlaceService)
    {
        _veganPlaceService = veganPlaceService;
    }

    public IList<VeganPlaceDuyLd> VeganPlaceDuyLd { get; set; } = [];

    [BindProperty(SupportsGet = true)]
    public string? PlaceSearch { get; set; }

    [BindProperty(SupportsGet = true)]
    public string? CategorySearch { get; set; }

    [BindProperty(SupportsGet = true)]
    public bool? IsActive { get; set; }

    [BindProperty(SupportsGet = true)]
    public int PageNumber { get; set; } = 1;

    public int ResultCount { get; set; }
    public int TotalPages { get; set; }

    public async Task OnGetAsync()
    {
        var hasSearchCondition = !string.IsNullOrWhiteSpace(PlaceSearch)
            || !string.IsNullOrWhiteSpace(CategorySearch)
            || IsActive.HasValue;

        var places = hasSearchCondition
            ? await _veganPlaceService.GetAllAsync(PlaceSearch, CategorySearch, IsActive)
            : await _veganPlaceService.GetAllAsync();

        ResultCount = places.Count;
        TotalPages = Math.Max(1, (int)Math.Ceiling(ResultCount / (double)PageSize));
        PageNumber = Math.Clamp(PageNumber, 1, TotalPages);

        VeganPlaceDuyLd = places
            .Skip((PageNumber - 1) * PageSize)
            .Take(PageSize)
            .ToList();
    }
}

