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
        [Description("Мощность (квт)")]
        Kvt, //Ват
        [Description("Вес (кг)")]
        Kg, //Вес
        d, //Диметр
    }


    [DataContract]
    public class Option : INotifyPropertyChanged
    {
        //Конструктор для EF
        public Option() { }

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


        double v;
        [DataMember]
        public double Value
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
