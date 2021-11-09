using vehicle_tracking.Persistences.Contexts;

namespace vehicle_tracking.Persistences {
    public abstract class BaseRepository {
        protected readonly AppDbContext _context;

        public BaseRepository(AppDbContext context) {
            _context = context;
        }
    }
}
