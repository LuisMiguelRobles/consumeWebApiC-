using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ConsumirWebService.Models
{
    public class Post
    {

        public int ID { get; set; }
        public string Title{ get; set; }
        public string Body { get; set; }

        internal Task<object> ToListAsync()
        {
            throw new NotImplementedException();
        }
    }
}