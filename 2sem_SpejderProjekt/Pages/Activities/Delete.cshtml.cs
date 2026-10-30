using _2sem_SpejderProjekt.Models;
using _2sem_SpejderProjekt.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace _2sem_SpejderProjekt.Pages.Activities
{
    [Authorize(Roles = "Admin")]
    public class DeleteModel : PageModel
    {
        private readonly ActivityRepository _activityRepository;

        public Activity Activity { get; set; } = new Activity();

        public string? ErrorMessage { get; set; }

        public DeleteModel(ActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public IActionResult OnGet(int id)
        {
            Activity? activity = _activityRepository.GetActivityById(id);

            if (activity == null)
            {
                return NotFound();
            }

            Activity = activity;

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            Activity? activity = _activityRepository.GetActivityById(id);

            if (activity == null)
            {
                return NotFound();
            }

            Activity = activity;

            try
            {
                _activityRepository.DeleteActivity(id);
            }
            catch (DbUpdateException)
            {
                ErrorMessage =
                    "Aktiviteten kunne ikke slettes pga. en databasefejl. Prøv igen.";

                return Page();
            }

            return RedirectToPage("./Index");
        }
    }
}
