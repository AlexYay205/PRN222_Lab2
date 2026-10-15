using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Vegan.Entities.DuyLD.Models;
using Vegan.Repositories.DuyLD.DBContext;

namespace Vegan.RazorWebApp.DuyLD.Pages.VeganPlaceDuyLds
{
    public class DeleteModel : PageModel
    {
        private readonly Vegan.Repositories.DuyLD.DBContext.VeganDBContext _context;

        public DeleteModel(Vegan.Repositories.DuyLD.DBContext.VeganDBContext context)
        {
            _context = context;
        }

        [BindProperty]
        public VeganPlaceDuyLd VeganPlaceDuyLd { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veganplaceduyld = await _context.VeganPlaceDuyLds.FirstOrDefaultAsync(m => m.VeganPlaceDuyLdid == id);

            if (veganplaceduyld == null)
            {
                return NotFound();
            }
            else
            {
                VeganPlaceDuyLd = veganplaceduyld;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var veganplaceduyld = await _context.VeganPlaceDuyLds.FindAsync(id);
            if (veganplaceduyld != null)
            {
                VeganPlaceDuyLd = veganplaceduyld;
                _context.VeganPlaceDuyLds.Remove(VeganPlaceDuyLd);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
