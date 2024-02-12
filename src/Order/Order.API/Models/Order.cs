﻿using System.ComponentModel.DataAnnotations;
using Order.API.Models.Dto;
using Shared;

namespace Order.API.Models
{
    public class Order : IEntity<int>
    {

        public int Id { get; set; }
        public int SubId { get; set; }
        public double TotalSum { get; set; }
        public DateTime DateOfOrder { get; set; }
        public int BasketId { get; set; }
    }
}

