using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DbElems
{
    [DataContract]
    public class Project
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

        [Key]
        public int Id { get; set; }

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

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Info { get; set; }

        [DataMember]
        public byte[] Icon { get; set; }

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
            }
        }


        List<Item> items = new List<Item>();
        [DataMember]
        public List<Item> Items
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
