using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace DbElems
{
    [DataContract]
    public class Link : INotifyPropertyChanged
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

        string title;
        [DataMember]
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Title"));
            }
        }

        string url;
        [DataMember]
        public string Url
        {
            get
            {
                return url;
            }
            set
            {
                url = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Url"));
            }
        }

        //Ключ Item, который будет подставлятся из базы данных
        public int ItemId { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
