using Bank.Domain.Contracts;
using Bank.Domain.ValueObjects;
using Bank.Infrastructure.Contexts;

namespace Bank.Infrastructure.Repositories
{
    public class FarePlanRepository : BaseRepository<FarePlan>, IFarePlanRepository
    {
        public FarePlanRepository(BankContext bankContext) : base(bankContext)
        {
        }
    }
}
