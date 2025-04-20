using Domain.Models;

namespace Infrastructure.IRepository;

public interface ILocationRepository : IRepository<Location>
{
    // Custom queries for Location can be added here in the future
}
