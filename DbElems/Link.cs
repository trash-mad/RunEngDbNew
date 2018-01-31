using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace DbElems
{
    [DataContract]
    public class Link
    {
        //Конструктор для EF
        public Link() { }

        public Link(string Title, string Url)
        {
            this.Title = Title;
            this.Url = Url;
        }

        [Key]
        public int Id { get; set; }

        [DataMember]
        public string Title { get; private set; }

        [DataMember]
        public string Url { get; private set; }
    }
}
