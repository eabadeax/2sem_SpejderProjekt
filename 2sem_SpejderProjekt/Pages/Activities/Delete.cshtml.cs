
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using _2sem_SpejderProjekt.Models;
using _2sem_SpejderProjekt.Repositories;

namespace _2sem_SpejderProjekt.Pages.Activities
{
    public class DeleteModel : PageModel
    {
        private readonly ActivityRepository _activityRepository;

        public Activity Activity { get; set; } = new Activity();

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

            _activityRepository.DeleteActivity(id);

            return RedirectToPage("./Index");
        }
    }
}
