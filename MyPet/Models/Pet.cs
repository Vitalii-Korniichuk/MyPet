//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyPet.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string TypeImageSource { get; set; }
        public System.DateTime Created { get; set; }
        public System.DateTime Visited { get; set; }
        public int Money { get; set; }
        public int Hunger { get; set; }
        public int Thirst { get; set; }
        public int Exhaustion { get; set; }
        public int Boredom { get; set; }
        public int Apples { get; set; }
        public int Pizzas { get; set; }
        public int Coke { get; set; }
        public int Juice { get; set; }
        public bool IsSleeping { get; set; }
    }
}
