
using Microsoft.EntityFrameworkCore;
using _2sem_SpejderProjekt.Data;
using _2sem_SpejderProjekt.Models;

namespace _2sem_SpejderProjekt.Repositories
{
    public class ActivityRepository
    {
        private readonly AppDbContext _context;

        public ActivityRepository(AppDbContext context)
        {
            _context = context;
        }

        public List<Activity> GetAllActivities()
        {
            return _context.Activities.ToList();
        }

        public Activity? GetActivityById(int id)
        {
            return _context.Activities
                .FirstOrDefault(a => a.ActivityId == id);
        }

        public void CreateActivity(Activity activity)
        {
            _context.Activities.Add(activity);
            _context.SaveChanges();
        }

        public void UpdateActivity(Activity activity)
        {
            _context.Activities.Update(activity);
            _context.SaveChanges();
        }

        public void DeleteActivity(int id)
        {
            Activity? activity = GetActivityById(id);

            if (activity != null)
            {
                _context.Activities.Remove(activity);
                _context.SaveChanges();
            }
        }
    }
}
