using Domain.Models;
using Infrastructure.IRepository;
using Infrastructure.MainContext;

namespace Infrastructure.Repository;

public class BannerRepository : Repository<Banner>, IBannerRepository
{
    public BannerRepository(ApplicationDbContext context) : base(context) { }
}