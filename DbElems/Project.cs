using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DbElems
{
    [DataContract]
    public class Project : INotifyPropertyChanged
    {
        public static readonly string SharedProjectReservedGuid="00000000-0000-0000-0000-000000000000";

        //Конструктор для EF
        public Project(){}

        public Project(string Name,string Info, byte[] Icon=null,Item[] Items=null)
        {
            this.Name = Name;
            this.Info = Info;
            this.Icon = Icon;

            foreach(var item in Items ?? Enumerable.Empty<Item>())
            {
                this.Items.Add(item);
            }
        }

        int id;
        [Key]
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Id"));
            }
        }

        [Index(IsUnique = true)]
        [DataMember]
        public Guid GUID { get; set; }

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

        byte[] icon;
        [DataMember]
        public byte[] Icon
        {
            get
            {
                return icon;
            }
            set
            {
                icon = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Icon"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IconBase64"));
            }
        }

        [NotMapped]
        public string IconBase64
        {
            get
            {
                if (Icon == null) return null;
                else return Convert.ToBase64String(Icon);
            }
            set
            {
                if (value == null) Icon = new byte[0];
                else Icon = Convert.FromBase64String(value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Icon"));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IconBase64"));
            }
        }


        ObservableCollection<Item> items = new ObservableCollection<Item>();
        [DataMember]
        public ObservableCollection<Item> Items
        {
            get
            {
                return items;
            }
            set
            {
                items = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void SetSharedProject()
        {
            GUID = new Guid(SharedProjectReservedGuid);
        }

        public void SetRandomGuid()
        {
            do
            {
                GUID =  Guid.NewGuid();
            }
            while (GUID.ToString() == SharedProjectReservedGuid);
        }
    }
}
