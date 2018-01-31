using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace DbElems
{
    [DataContract]
    public class Manufacturer
    {
        //Конструктор для EF
        public Manufacturer() { }

        public Manufacturer(string Name, string Info, byte Rate, byte[] Bitmap=null)
        {
            this.Name = Name;
            this.Info = Info;
            this.Rate = Rate;
            this.Bitmap = Bitmap;
        }

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

        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public byte Rate { get; set; }
        [DataMember]
        public string Info { get; set; }
    }
}
