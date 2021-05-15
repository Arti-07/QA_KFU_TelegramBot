using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace QA_KFU_TelegramBot
{
    [DataContract]
     public class Users
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public  int ID { get; set; }
       
        public  Users()
        {

        }
        public Users(string name, int id)
        {
            Name = name;
            ID = id;
        }
    }
}
