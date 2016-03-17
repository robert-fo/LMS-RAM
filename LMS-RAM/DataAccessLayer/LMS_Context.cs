using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LMS_RAM.DataAccessLayer
{
    public class LMS_Context : DbContext
    {
        public LMS_Context() : base("DefaultConnection") { }

    }
}