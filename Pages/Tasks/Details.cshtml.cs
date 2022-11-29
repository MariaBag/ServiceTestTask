using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ServiceTestTask.Models;

namespace ServiceTestTask.Pages.Tasks
{
    public class DetailsModel : PageModel
    {
        private readonly Data.ApplicationContext _context;

        public DetailsModel(Data.ApplicationContext context)
        {
            _context = context;
        }
        
        public Models.Task Task { get; set; }
        [BindProperty]
        public Comment Comment { get; set; }
        

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Tasks == null)
            {
                return NotFound();
            }

            Task = await _context.Tasks
                .Include(s => s.Comments)
                .FirstOrDefaultAsync(m => m.TaskID == id);

            if (Task == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Task = await _context.Tasks
                .Include(s => s.Comments)
                .FirstOrDefaultAsync(m => m.TaskID == id);
            Comment.Task = Task;
            _context.Comments.Add(Comment);
            await _context.SaveChangesAsync();

            return Page();
        }
    }
}
