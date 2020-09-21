using Bank.Domain.DomainServices;
using Bank.Domain.Entities;
using Bank.Domain.ValueObjects;
using System;
using Xunit;

namespace BankDomainTests.Services
{
    public class TransferServiceTest
    {
        //DbContextOptions<BankContext> options;
        //var builder = new DbContextOptionsBuilder<BankContext>();
        //builder.UseInMemoryDatabase("BankTest");
        //    options = builder.Options;
        //    BankContext bankContext = new BankContext(options);
        //bankContext.Database.EnsureDeleted();
        //    bankContext.Database.EnsureCreated();
        //    return new CustomerAccountRepository(bankContext);
        [Theory]
        [InlineData(1, 0)]
        [InlineData(3, 10.00)]
        public void GetTransferTax(int amountTransferencesFromCurrentMonth, decimal expected)
        {
            FarePlan planOriginAccount = new FarePlan()
            {
                FreeTransfersQuantity = 2,
            };

            TransferDomainService service = new TransferDomainService();
            decimal transferenceTax = service.GetTransferTax(amountTransferencesFromCurrentMonth, planOriginAccount);

            Assert.Equal(expected, transferenceTax);
        }      
        
        [Theory]
        [InlineData(1, true)]
        [InlineData(3, false)]
        public void HasFreeTransfersAvailableInOriginAccount(int amountTransferencesFromCurrentMonth, bool expected)
        {
            FarePlan planOriginAccount = new FarePlan()
            {
                FreeTransfersQuantity = 2,
            };
            TransferDomainService service = new TransferDomainService();
            Assert.Equal(expected, service.HasFreeTransfersAvailableInOriginAccount(amountTransferencesFromCurrentMonth, planOriginAccount));
        }    
        
        [Fact]
        public void ValidateTransfer_BalanceAvailable()
        {
            Transfer transfer = new Transfer()
            {
                Value = 100,
                From = new CustomerAccount()
                {
                    BankBranch = 1,
                    BankAccount = 1,
                    Balance = 100,
                    IsActiveAccount = true
                },
                To = new CustomerAccount()
                {
                    BankBranch = 2,
                    BankAccount = 2,
                    Balance = 0,
                    IsActiveAccount = true
                }
            };

            TransferDomainService service = new TransferDomainService();
            Action action = () => service.ValidateTransfer(transfer);
            ArgumentException exception = Assert.Throws<ArgumentException>(action);
        }

        [Fact]
        public void ValidateTransfer_BalanceNotAvailable()
        {
            Transfer transfer = new Transfer()
            {
                Value = 100,
                From = new CustomerAccount()
                {
                    BankBranch = 1,
                    BankAccount = 1,
                    Balance = 0
                },
                To = new CustomerAccount()
                {
                    BankBranch = 2,
                    BankAccount = 2,
                    Balance = 0
                }
            };

            TransferDomainService service = new TransferDomainService();
            Action action = () => service.ValidateTransfer(transfer);
            ArgumentException exception = Assert.Throws<ArgumentException>(action);
        }

        [Fact]
        public void ValidateTransfer_BalanceNullOriginAccount()
        {
            Transfer transfer = new Transfer()
            {
                Value = 100,
                From = new CustomerAccount()
                {
                    Balance = 0
                },
                To = new CustomerAccount()
                {
                    BankBranch = 2,
                    BankAccount = 2,
                    Balance = 0
                }
            };

            TransferDomainService service = new TransferDomainService();
            Action action = () => service.ValidateTransfer(transfer);
            ArgumentException exception = Assert.Throws<ArgumentException>(action);
        }

        [Fact]
        public void ValidateTransfer_BalanceNullDestinationAccount()
        {
            Transfer transfer = new Transfer()
            {
                Value = 100,
                From = new CustomerAccount()
                {
                    BankBranch = 1,
                    BankAccount = 1,
                    Balance = 0
                },
                To = new CustomerAccount()
                {
                    Balance = 0
                }
            };

            TransferDomainService service = new TransferDomainService();
            Action action = () => service.ValidateTransfer(transfer);
            ArgumentException exception = Assert.Throws<ArgumentException>(action);
        }

        [Fact]
        public void ValidateTransfer_BalanceNullAccounts()
        {
            Transfer transfer = new Transfer()
            {
                Value = 100,
                From = new CustomerAccount()
                {
                    Balance = 0
                },
                To = new CustomerAccount()
                {
                    Balance = 0
                }
            };

            TransferDomainService service = new TransferDomainService();
            Action action = () => service.ValidateTransfer(transfer);
            ArgumentException exception = Assert.Throws<ArgumentException>(action);
        }

        [Fact]
        public void ValidateTransfer_InativeOriginAccount()
        {
            Transfer transfer = new Transfer()
            {
                Value = 100,
                From = new CustomerAccount()
                {
                    BankBranch = 1,
                    BankAccount = 1,
                    Balance = 0,
                    IsActiveAccount = false
                },
                To = new CustomerAccount()
                {
                    BankBranch = 2,
                    BankAccount = 2,
                    Balance = 0,
                    IsActiveAccount = true
                }
            };

            TransferDomainService service = new TransferDomainService();
            Action action = () => service.ValidateTransfer(transfer);
            ArgumentException exception = Assert.Throws<ArgumentException>(action);
        }


        [Fact]
        public void ValidateTransfer_InativeDestinationAccount()
        {
            Transfer transfer = new Transfer()
            {
                Value = 100,
                From = new CustomerAccount()
                {
                    BankBranch = 1,
                    BankAccount = 1,
                    Balance = 0,
                    IsActiveAccount = true
                },
                To = new CustomerAccount()
                {
                    BankBranch = 2,
                    BankAccount = 2,
                    Balance = 0,
                    IsActiveAccount = false
                }
            };

            TransferDomainService service = new TransferDomainService();
            Action action = () => service.ValidateTransfer(transfer);
            ArgumentException exception = Assert.Throws<ArgumentException>(action);
        }

        [Fact]
        public void ValidateTransfer_InativeAccount()
        {
            Transfer transfer = new Transfer()
            {
                Value = 100,
                From = new CustomerAccount()
                {
                    BankBranch = 1,
                    BankAccount = 1,
                    Balance = 0,
                    IsActiveAccount = false
                },
                To = new CustomerAccount()
                {
                    BankBranch = 2,
                    BankAccount = 2,
                    Balance = 0,
                    IsActiveAccount = false
                }
            };

            TransferDomainService service = new TransferDomainService();
            Action action = () => service.ValidateTransfer(transfer);
            ArgumentException exception = Assert.Throws<ArgumentException>(action);
        }
    }
}
