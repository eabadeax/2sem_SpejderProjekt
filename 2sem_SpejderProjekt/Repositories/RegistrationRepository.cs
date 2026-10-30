using _2sem_SpejderProjekt.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace _2sem_SpejderProjekt.Repositories
{
    public class RegistrationRepository
    {
        private readonly AppDbContext _context;

        public RegistrationRepository(AppDbContext context)
        {
            _context = context;
        }

        // Tilmeld en spejder
        public string? Register(int scoutId, int activityId)
        {
            using var transaction = _context.Database
                .BeginTransaction(IsolationLevel.Serializable);

            var scout = _context.Scouts
                .FirstOrDefault(s => s.ScoutId == scoutId);

            var activity = _context.Activities
                .FirstOrDefault(a => a.ActivityId == activityId);

            if (scout == null || activity == null)
                return "Spejder eller aktivitet findes ikke.";

            bool alreadyRegistered = _context.Registrations
                .Any(r => r.ScoutId == scoutId &&
                          r.ActivityId == activityId);

            if (alreadyRegistered)
                return "Du er allerede tilmeldt.";

            if (activity.MinimumAge.HasValue &&
                scout.Age < activity.MinimumAge.Value)
                return "Du er ikke gammel nok til aktiviteten.";

            if (activity.MaximumAge.HasValue &&
                scout.Age > activity.MaximumAge.Value)
                return "Du er for gammel til aktiviteten.";

            int count = _context.Registrations
                .Count(r => r.ActivityId == activityId);

            if (count >= activity.MaxParticipants)
                return "Aktiviteten er fuldt booket.";

            var registration = new Registration
            {
                ScoutId = scoutId,
                ActivityId = activityId,
                RegistrationDate = DateTime.UtcNow
            };

            _context.Registrations.Add(registration);
            _context.SaveChanges();

            transaction.Commit();

            return null;
        }

        // Frameld en spejder
        public bool Unregister(int scoutId, int activityId)
        {
            var registration = _context.Registrations
                .FirstOrDefault(r => r.ScoutId == scoutId &&
                                     r.ActivityId == activityId);

            if (registration == null)
                return false;

            _context.Registrations.Remove(registration);
            _context.SaveChanges();

            return true;
        }

        // Find aktiviteter, spejderen er tilmeldt
        public HashSet<int> GetRegisteredActivityIds(int scoutId)
        {
            return _context.Registrations
                .Where(r => r.ScoutId == scoutId)
                .Select(r => r.ActivityId)
                .ToHashSet();
        }

        // Antal deltagere pr. aktivitet
        public Dictionary<int, int> GetParticipantCounts()
        {
            return _context.Registrations
                .GroupBy(r => r.ActivityId)
                .Select(g => new
                {
                    ActivityId = g.Key,
                    Count = g.Count()
                })
                .ToDictionary(x => x.ActivityId, x => x.Count);
        }

        // Hent tilmeldte spejdere
        public List<Scout> GetParticipants(int activityId)
        {
            return (from registration in _context.Registrations
                    join scout in _context.Scouts
                    on registration.ScoutId equals scout.ScoutId
                    where registration.ActivityId == activityId
                    orderby scout.Name
                    select scout).ToList();
        }
    }
}
