using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace faqSystem02.Models
{
       [Table("table_info2")]
    public class FaqModel
    {
        public int id { get; set; }
        public string question { get; set; }
        public string answer { get; set; }
        public string category { get; set; }
        public int? askedAmount { get; set; }
        
    }
}