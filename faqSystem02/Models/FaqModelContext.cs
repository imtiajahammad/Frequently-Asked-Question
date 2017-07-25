using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace faqSystem02.Models
{
    public class FaqModelContext : DbContext
    {
        public DbSet<FaqModel> FaqModels { get; set; }

    }
}