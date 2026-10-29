using _2sem_SpejderProjekt.Models;
using _2sem_SpejderProjekt.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace _2sem_SpejderProjekt.Pages.Activities
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly ActivityRepository _activityRepository;

        [BindProperty]
        public Activity Activity { get; set; } = new Activity();

        public CreateModel(ActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(Activity.Name))
            {
                ModelState.AddModelError("Activity.Name",
                    "Aktiviteten skal have et navn.");
            }

            if (Activity.MaxParticipants <= 0)
            {
                ModelState.AddModelError("Activity.MaxParticipants",
                    "Antallet af deltagere skal være større end 0.");
            }

            if (Activity.StartTime >= Activity.EndTime)
            {
                ModelState.AddModelError("Activity.EndTime",
                    "Sluttidspunktet skal ligge efter starttidspunktet.");
            }

            if (Activity.MinimumAge < 0 || Activity.MaximumAge < 0 ||
                (Activity.MinimumAge.HasValue &&
                 Activity.MaximumAge.HasValue &&
                 Activity.MinimumAge > Activity.MaximumAge))
            {
                ModelState.AddModelError("Activity.MaximumAge",
                    "Aldersgrænserne er ugyldige.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _activityRepository.CreateActivity(Activity);

            return RedirectToPage("./Index");

        }
    }
}
