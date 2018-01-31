using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;
using System.Runtime.Serialization;
using System.Reflection;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbElems
{
    [DataContract]
    public class Item : INotifyPropertyChanged
    {

        //Конструктор для EF
        public Item() { }

        public Item(string Name, string Info, string Category, string Subcategory, Manufacturer Man, decimal Price=0, byte Delivery=0, byte Rate=0, Link[] links = null,Option[] options= null, byte[][] images=null, byte[] Logo = null)
        {
            this.name = Name;
            this.logo = Logo;
            this.info = Info;
            this.category = Category;
            this.subcategory = Subcategory;
            this.man = Man;
            this.price = Price;
            this.delivery = Delivery;
            this.rate = Rate;

            foreach(var item in links ?? Enumerable.Empty<Link>())
            {
                this.links.Add(item);
            }

            foreach(var item in options ?? Enumerable.Empty<Option>())
            {
                this.options.Add(item);
            }

            foreach(var item in images ?? Enumerable.Empty<byte[]>())
            {
                this.images.Add(item);
            }
        }

        [Key]
        public int Id { get; set; }

        //Поля элемента базы данных
        string name;
        [DataMember]
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
            }
        }

        byte[] logo;
        [DataMember]
        public byte[] Logo
        {
            get
            {
                return logo;
            }
            set
            {
                logo = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Logo"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LogoBase64"));
            }
        }

        
        [NotMapped]
        public string LogoBase64
        {
            get
            {
                if (logo == null) return null;
                else return Convert.ToBase64String(logo);
            }
            set
            {
                if (value == null) logo = new byte[0];
                else logo = Convert.FromBase64String(value);

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Logo"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LogoBase64"));
            }
        }


        Guid guid=Guid.NewGuid();
        [Index(IsUnique = true)]
        [DataMember]
        public Guid GUID
        {
            get
            {
                return guid;
            }
            set
            {
                guid = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("GUID"));
            }
        }

        [NotMapped]
        public string GUIDstring
        {
            get
            {
                return GUID.ToString();
            }
            set
            {
                GUID = new Guid(value);
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

        string category;
        [DataMember]
        public string Category
        {
            get
            {
                return category;
            }
            set
            {
                category = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Category"));
            }
        }


        string subcategory;
        [DataMember]
        public string Subcategory
        {
            get
            {
                return subcategory;
            }
            set
            {
                subcategory = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Subcategory"));
            }
        }

        Manufacturer man;
        [DataMember]
        public Manufacturer Man
        {
            get
            {
                return man;
            }
            set
            {
                man = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Manufacturer"));
            }
        }

        decimal price;
        [DataMember]
        public decimal Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Price"));
            }
        }

        byte delivery;
        [DataMember]
        public byte Delivery
        {
            get
            {
                return delivery;
            }
            set
            {
                delivery = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Delivery"));
            }
        }

        byte rate;
        [DataMember]
        public byte Rate
        {
            get
            {
                return rate;
            }
            set
            {
                rate = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Rate"));
            }
        }

        List<byte[]> images=new List<byte[]>();
        [DataMember]
        public List<byte[]> Images
        {
            get
            {
                return images;
            }
            set
            {
                images = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Images"));
            }
        }

        List<Link> links=new List<Link>();
        [DataMember]
        public List<Link> Links
        {
            get
            {
                return links;
            }
            set
            {
                links = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Links"));
            }
        }

        List<Option> options=new List<Option>();
        [DataMember]
        public List<Option> Options
        {
            get
            {
                return options;
            }
            set
            {
                options = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Options"));
            }
        }

        //Изменение полей и вычисление контрольных сумм

        string md5;
        [DataMember]
        public string MD5 { get; }

        private void UpdateHash()
        {
            /*PropertyInfo[] props = this.GetType().GetProperties();
            StringBuilder data = new StringBuilder();
            foreach(var item in props)
            {
                Console.WriteLine(item);
            }
            
            string newhash = StaticFuncs.CalculateMD5Hash(data.ToString());
            if (newhash != md5)
            {
                md5 = newhash;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MD5"));
            }*/

        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
