using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace DbElems
{
    [DataContract]
    public class Option
    {
        //Конструктор для EF
        public Option() { }

        public Option(string Name, decimal Value)
        {
            this.Name = Name;
            this.Value = Value;
        }

        [Key]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; private set; }

        [DataMember]
        public decimal Value { get; private set; }
    }
}
