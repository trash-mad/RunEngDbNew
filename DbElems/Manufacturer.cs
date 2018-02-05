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
    public class Manufacturer : INotifyPropertyChanged
    {
        //Конструктор для EF
        public Manufacturer() { }

        [Key]
        public int Id { get; set; }

        [DataMember]
        public byte[] Bitmap { get; set; }

        [NotMapped]
        public string BitmapBase64
        {
            get
            {
                if (Bitmap == null) return null;
                else return Convert.ToBase64String(Bitmap);
            }
            set
            {
                if (value == null) Bitmap = new byte[0];
                else Bitmap = Convert.FromBase64String(value);
            }
        }


        string name="Название";
        [DataMember]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) return;
                name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
            }
        }

        byte rate=1;
        [DataMember]
        public byte Rate
        {
            get
            {
                return rate;
            }
            set
            {
                if (!(value > 0 || value < 6)) return;
                rate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Rate"));
            }
        }

        string info;
        [DataMember]
        public string Info
        {
            get
            {
                return info;
            }
            set
            {
                info = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Info"));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
    }

}
