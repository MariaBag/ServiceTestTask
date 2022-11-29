using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ServiceTestTask.Models;

namespace ServiceTestTask.Pages.Tasks
{
    public class EditModel : PageModel
    {
        private readonly Data.ApplicationContext _context;

        public EditModel(Data.ApplicationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Models.Task Task { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Tasks == null)
            {
                return NotFound();
            }

            var task =  await _context.Tasks.FirstOrDefaultAsync(m => m.TaskID == id);
            if (task == null)
            {
                return NotFound();
            }
            Task = task;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var taskToUpdate = await _context.Tasks.FirstOrDefaultAsync(m => m.TaskID == Task.TaskID);
            taskToUpdate.Worker = Task.Worker;
            taskToUpdate.Status = Task.Status;
            taskToUpdate.Date = Task.Date;

            try
            {
                await _context.SaveChangesAsync();
                
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(Task.TaskID))
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

        private bool TaskExists(int id)
        {
          return _context.Tasks.Any(e => e.TaskID == id);
        }
    }
}
