using Bank.Business.Domain.Contracts;
using Bank.Business.Domain.ValueObjects;
using Bank.Infrastructure.Persistence.Contexts;

namespace Bank.Infrastructure.Persistence.Repositories
{
    public class FarePlanRepository : BaseRepository<FarePlan>, IFarePlanRepository
    {
        public FarePlanRepository(BankContext bankContext) : base(bankContext)
        {
        }
    }
}
