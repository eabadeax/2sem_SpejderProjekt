
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using _2sem_SpejderProjekt.Models;
using _2sem_SpejderProjekt.Repositories;

namespace _2sem_SpejderProjekt.Pages.Activities
{
    public class EditModel : PageModel
    {
        private readonly ActivityRepository _activityRepository;

        [BindProperty]
        public Activity Activity { get; set; } = new Activity();

        public EditModel(ActivityRepository activityRepository)
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
                    "Max deltagere skal være større end 0.");
            }

            if (Activity.Date == default)
            {
                ModelState.AddModelError("Activity.Date",
                    "Du skal vælge en dato.");
            }

            if (Activity.StartTime >= Activity.EndTime)
            {
                ModelState.AddModelError("Activity.EndTime",
                    "Sluttidspunktet skal være efter starttidspunktet.");
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

            Activity? existingActivity =
                _activityRepository.GetActivityById(Activity.ActivityId);

            if (existingActivity == null)
            {
                return NotFound();
            }

            existingActivity.Name = Activity.Name;
            existingActivity.Description = Activity.Description;
            existingActivity.Date = Activity.Date;
            existingActivity.StartTime = Activity.StartTime;
            existingActivity.EndTime = Activity.EndTime;
            existingActivity.MaxParticipants = Activity.MaxParticipants;
            existingActivity.MinimumAge = Activity.MinimumAge;
            existingActivity.MaximumAge = Activity.MaximumAge;

            _activityRepository.UpdateActivity(existingActivity);

            return RedirectToPage("./Index");
        }
    }
}
