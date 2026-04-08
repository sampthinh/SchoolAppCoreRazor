using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SchoolAppCoreRazor.Models;

namespace SchoolAppCoreRazor.Pages.Courses
{
    public class CreateModel : PageModel
    {
        private readonly SchoolAppCoreRazor.Models.SchoolContext _context;

        public CreateModel(SchoolAppCoreRazor.Models.SchoolContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            // Check if the Departments table is null or empty
            if (_context.Departments == null || !_context.Departments.Any())
            {
                // Handle the case where there are no departments
                ViewData["DepartmentID"] = new SelectList(new List<SelectListItem>(), "Value", "Text");
                return Page(); // Optionally, inform the user or display a message
            }
            //ViewData["DepartmentID"] = new SelectList(_context.Departments, "DepartmentID", "Name");
            // Populate ViewData with DepartmentID and DepartmentName combined
            ViewData["DepartmentID"] = new SelectList(
            _context.Departments.Select(d => new
            {
                d.DepartmentID,
                DisplayText = d.DepartmentID + " - " + d.Name
            }),
            "DepartmentID",
            "DisplayText"
            );
            return Page();
        }

        [BindProperty]
        public Course Course { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Courses.Add(Course);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
