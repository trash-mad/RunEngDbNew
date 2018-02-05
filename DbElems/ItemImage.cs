using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace DbElems
{
    /*
     * Класс для хранения картинок товаров
     * лучше использовать только для картинок в описании, EF может обосраться
     * 
     */

    [DataContract]
    public class ItemImage
    {
        public ItemImage() { }

        [Key]
        public int Id { get; set; }

        [DataMember]
        public byte[] Value { get; set; }
    }
}
