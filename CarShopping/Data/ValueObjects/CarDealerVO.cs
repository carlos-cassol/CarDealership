﻿using CarShopping.Model.Base;

namespace CarShopping.Data.ValueObjects
{
    public class CarDealerVO : BaseEntityVO
    {
        public string Name { get; set; }
        public int CarQuantity { get; set; }
        public int Employees { get; set; }
        public double MonthRevenue { get; set; }
        public int AmountAvaliableCars { get; set; }
    }
}
