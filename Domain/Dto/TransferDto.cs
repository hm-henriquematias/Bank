﻿namespace Bank.Domain.Dto
{
    public class TransferDto
    {
        public decimal Value { get; set; }
        public CustomerAccountDto From { get; set; }
        public CustomerAccountDto To { get; set; }
    }
}
