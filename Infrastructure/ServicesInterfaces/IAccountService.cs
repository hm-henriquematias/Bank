using Bank.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bank.Infrastructure.ServicesInterfaces
{
    public interface IAccountService
    {
        bool IsExistAccount(CustomerAccount account);
        void Update(CustomerAccount account);
    }
}
