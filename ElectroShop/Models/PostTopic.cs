namespace ElectroShop
{ 
    using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


    public class PostTopic
    {
        public int PostId { get; set; }
        public string PostName { get; set; }
        public string PostImg { get; set; }
        public int PostStatus { get; set; }
        public string TopicName { get; set; }
        public DateTime PostCreated_At { get; set; }
    }
}