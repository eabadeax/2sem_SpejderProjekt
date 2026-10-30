using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using _2sem_SpejderProjekt.Models;
using _2sem_SpejderProjekt.Repositories;

namespace _2sem_SpejderProjekt.Pages.Activities
{
    public class IndexModel : PageModel
    {
        private readonly ActivityRepository _activityRepository;
        private readonly RegistrationRepository _registrationRepository;

        public List<Activity> Activities { get; set; } = new();

        public Dictionary<int, int> ParticipantCounts { get; set; }
            = new();

        public HashSet<int> RegisteredActivityIds { get; set; }
            = new();

        [TempData]
        public string? ErrorMessage { get; set; }

        public IndexModel(
            ActivityRepository activityRepository,
            RegistrationRepository registrationRepository)
        {
            _activityRepository = activityRepository;
            _registrationRepository = registrationRepository;
        }

        public void OnGet()
        {
            LoadActivities();
        }

        private void LoadActivities()
        {
            Activities = _activityRepository.GetAllActivities();

            ParticipantCounts =
                _registrationRepository.GetParticipantCounts();

            if (User.IsInRole("Scout"))
            {
                int? scoutId = GetScoutId();

                if (scoutId.HasValue)
                {
                    RegisteredActivityIds = _registrationRepository
                        .GetRegisteredActivityIds(scoutId.Value);
                }
            }
        }

        private int? GetScoutId()
        {
            string? id = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            if (int.TryParse(id, out int scoutId))
                return scoutId;

            return null;
        }

        public IActionResult OnPostRegister(int activityId)
        {
            if (User.Identity?.IsAuthenticated != true)
                return Challenge();

            if (!User.IsInRole("Scout"))
                return Forbid();

            int? scoutId = GetScoutId();

            if (!scoutId.HasValue)
                return Forbid();

            string? error = _registrationRepository
                .Register(scoutId.Value, activityId);

            if (error != null)
                ErrorMessage = error;

            return RedirectToPage();
        }

        public IActionResult OnPostUnregister(int activityId)
        {
            if (User.Identity?.IsAuthenticated != true)
                return Challenge();

            if (!User.IsInRole("Scout"))
                return Forbid();

            int? scoutId = GetScoutId();

            if (!scoutId.HasValue)
                return Forbid();

            _registrationRepository.Unregister(
                scoutId.Value, activityId);

            return RedirectToPage();
        }
    }
}
