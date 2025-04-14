using Domain.Models;

namespace Infrastructure.IRepository;

public interface IEventRepository: IRepository<Event>
{
    // Additional methods for Event if needed
}