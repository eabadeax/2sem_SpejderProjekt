using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using _2sem_SpejderProjekt.Models;
using _2sem_SpejderProjekt.Repositories;

namespace _2sem_SpejderProjekt.Pages.Activities
{
    [Authorize(Roles = "Admin")]
    public class ParticipantsModel : PageModel
    {
        private readonly ActivityRepository _activityRepository;
        private readonly RegistrationRepository _registrationRepository;

        public Activity Activity { get; set; } = new();

        public List<Scout> Participants { get; set; } = new();

        public ParticipantsModel(
            ActivityRepository activityRepository,
            RegistrationRepository registrationRepository)
        {
            _activityRepository = activityRepository;
            _registrationRepository = registrationRepository;
        }

        public IActionResult OnGet(int id)
        {
            var activity = _activityRepository.GetActivityById(id);

            if (activity == null)
            {
                return NotFound();
            }

            Activity = activity;

            Participants = _registrationRepository.GetParticipants(id);

            return Page();
        }
    }
}
