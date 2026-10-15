using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Vegan.Entities.DuyLD.Models;
using Vegan.Repositories.DuyLD.DBContext;

namespace Vegan.RazorWebApp.DuyLD.Pages.VeganPlaceDuyLds
{
    public class CreateModel : PageModel
    {
        private readonly Vegan.Repositories.DuyLD.DBContext.VeganDBContext _context;

        public CreateModel(Vegan.Repositories.DuyLD.DBContext.VeganDBContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public VeganPlaceDuyLd VeganPlaceDuyLd { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.VeganPlaceDuyLds.Add(VeganPlaceDuyLd);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
