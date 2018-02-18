using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace DbElems
{
    [DataContract]
    public enum OptionType
    {
        Vatt, //Ват
        Dv, //Диаметр вала
        H, //Высота
        W, //Ширина
        L, //Длинна
    }


    [DataContract]
    public class Option : INotifyPropertyChanged
    {
        //Конструктор для EF
        public Option() { }

        public Option(OptionType Type, decimal Value)
        {
            this.Type = Type;
            this.Value = Value;
        }

        [Key]
        public int Id { get; set; }

        OptionType type;
        [DataMember]
        public OptionType Type
        {
            get
            {
                return type;
            }
            set
            {
                type = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Type"));
            }
        }


        decimal v;
        [DataMember]
        public decimal Value
        {
            get
            {
                return v;
            }
            set
            {
                v = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Value"));
            }
        }

        //Ключ Item, который будет подставлятся из базы данных
        public int ItemId { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
