using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vegan.Entities.DuyLD.Models;
using Vegan.Repositories.DuyLD.DBContext;

namespace Vegan.RazorWebApp.DuyLD.Pages.VeganPlaceDuyLds
{
    public class EditModel : PageModel
    {
        private readonly Vegan.Repositories.DuyLD.DBContext.VeganDBContext _context;

        public EditModel(Vegan.Repositories.DuyLD.DBContext.VeganDBContext context)
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

            var veganplaceduyld =  await _context.VeganPlaceDuyLds.FirstOrDefaultAsync(m => m.VeganPlaceDuyLdid == id);
            if (veganplaceduyld == null)
            {
                return NotFound();
            }
            VeganPlaceDuyLd = veganplaceduyld;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(VeganPlaceDuyLd).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VeganPlaceDuyLdExists(VeganPlaceDuyLd.VeganPlaceDuyLdid))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool VeganPlaceDuyLdExists(long id)
        {
            return _context.VeganPlaceDuyLds.Any(e => e.VeganPlaceDuyLdid == id);
        }
    }
}
