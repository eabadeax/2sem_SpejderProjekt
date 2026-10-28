
using Microsoft.AspNetCore.Mvc.RazorPages;
using _2sem_SpejderProjekt.Models;
using _2sem_SpejderProjekt.Repositories;

namespace _2sem_SpejderProjekt.Pages.Activities
{
    public class IndexModel : PageModel
    {
        private readonly ActivityRepository _activityRepository;

        public List<Activity> Activities { get; set; } = new List<Activity>();

        public IndexModel(ActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        public void OnGet()
        {
            Activities = _activityRepository.GetAllActivities();
        }
    }
}

