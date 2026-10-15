using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Vegan.Entities.DuyLD.Models;
using Vegan.Services.DuyLD;

namespace Vegan.RazorWebApp.DuyLD.Pages.VeganPlaceDuyLds
{
    public class IndexModel : PageModel
    {
        private readonly IVeganPlaceServiceDuyLd _veganPlaceServiceDuyLd;

        public IndexModel(IVeganPlaceServiceDuyLd veganPlaceServiceDuyLd)
        {
            _veganPlaceServiceDuyLd = veganPlaceServiceDuyLd;
        }

        public IList<VeganPlaceDuyLd> VeganPlaceDuyLd { get; set; } = new List<VeganPlaceDuyLd>();

        [BindProperty(SupportsGet = true)]
        public string? PlaceSearch { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? CategorySearch { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool? IsActive { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PageNumber { get; set; } = 1;

        public int TotalPlaces { get; set; }
        public int ActivePlaces { get; set; }
        public int CategoryCount { get; set; }
        public decimal AverageRating { get; set; }
        public int ResultCount { get; set; }
        public int TotalPages { get; set; }
        public const int PageSize = 5;

        public async Task OnGetAsync()
        {
            var places = await _veganPlaceServiceDuyLd.GetAllAsync();

            // Summary describes all places, even when the table is filtered.
            TotalPlaces = places.Count;
            ActivePlaces = places.Count(p => p.IsActive);
            CategoryCount = places.Where(p => !string.IsNullOrWhiteSpace(p.CategoryName))
                .Select(p => p.CategoryName).Distinct().Count();
            AverageRating = places.Where(p => p.Rating.HasValue)
                .Select(p => p.Rating!.Value).DefaultIfEmpty(0).Average();

            var results = places.AsEnumerable();
            if (!string.IsNullOrWhiteSpace(PlaceSearch))
                results = results.Where(p => p.PlaceName != null && p.PlaceName.Contains(PlaceSearch.Trim(), StringComparison.OrdinalIgnoreCase));
            if (!string.IsNullOrWhiteSpace(CategorySearch))
                results = results.Where(p => p.CategoryName != null && p.CategoryName.Contains(CategorySearch.Trim(), StringComparison.OrdinalIgnoreCase));
            if (IsActive.HasValue)
                results = results.Where(p => p.IsActive == IsActive.Value);

            var filteredPlaces = results.OrderBy(p => p.VeganPlaceDuyLdid).ToList();
            ResultCount = filteredPlaces.Count;
            TotalPages = Math.Max(1, (int)Math.Ceiling(ResultCount / (double)PageSize));
            PageNumber = Math.Clamp(PageNumber, 1, TotalPages);
            VeganPlaceDuyLd = filteredPlaces.Skip((PageNumber - 1) * PageSize).Take(PageSize).ToList();
        }
    }
}
