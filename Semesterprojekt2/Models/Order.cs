﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Semesterprojekt2.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        //billed 

        public string Info { get; set; }

        public string Comments { get; set; }

        public Order()
        {
            
        }

        public Order(string info)
        {
            Info = info;
        }
    }
}
