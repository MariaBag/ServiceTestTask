using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ServiceTestTask.Pages.Tasks
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly Data.ApplicationContext _context;

        public IndexModel(Data.ApplicationContext context)
        {
            _context = context;
        }

        public IList<Models.Task> Task { get;set; } = default!;
        public string SearchString { get; set; }
        public Models.Status StatusFilter { get; set; }
        public string WorkerFilter { get; set; }
        public DateTime DateFilter { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.Tasks != null)
            {
                Task = await _context.Tasks.ToListAsync();
            }
            DateFilter = DateTime.Today;
        }

        public async Task<IActionResult> OnPostSearchAsync(string searchString)
        {
            if (_context.Tasks != null && searchString != null)
            {
                SearchString= searchString;
                Task = await _context.Tasks
                    .Where(m => m.Name.Contains(SearchString) || m.Description.Contains(SearchString))
                    .ToListAsync();
            }
            else if(_context.Tasks != null)
            {
                Task = await _context.Tasks.ToListAsync();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostFilterAsync(Models.Status statusFilter, string workerFilter,
            DateTime dateFilter)
        {
            if (_context.Tasks != null && workerFilter != null)
            {
                StatusFilter = statusFilter;
                WorkerFilter = workerFilter;
                DateFilter = dateFilter;
                Task = await _context.Tasks
                    .Where(m => m.Status == StatusFilter && m.Worker.Contains(WorkerFilter) 
                    && m.Date.Date == DateFilter.Date)
                    .ToListAsync();
            }
            else if (_context.Tasks != null)
            {
                StatusFilter = statusFilter;
                WorkerFilter = workerFilter;
                DateFilter = dateFilter;
                Task = await _context.Tasks
                    .Where(m => m.Status == StatusFilter && m.Date.Date == DateFilter.Date)
                    .ToListAsync();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostResetAsync()
        {
            if (_context.Tasks != null)
            {
                StatusFilter = Models.Status.Новая;
                WorkerFilter = null;
                DateFilter = DateTime.Today;
                Task = await _context.Tasks.ToListAsync();
            }

            return Page();
        }
    }
}
