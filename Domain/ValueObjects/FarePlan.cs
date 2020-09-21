using Bank.Domain.Enums;

namespace Bank.Domain.ValueObjects
{
    public class FarePlan
    {
        public int FarePlanId { get; set; }
        public string FarePlanDescription { get; set; }
        public int FreeTransfersQuantity { get; set; }
        
        public bool IsServicosEssenciais()
        {
            return FarePlanId.Equals((int) FarePlanEnum.ServicosEssenciais);
        }

        public bool IsBasico()
        {
            return FarePlanId.Equals((int) FarePlanEnum.Basico);
        }
    }
}
