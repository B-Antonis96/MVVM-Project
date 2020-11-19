using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Match4Ever_DAL.Data
{
    public class Match4EverEntities : DbContext
    {
        public Match4EverEntities(): base ("Match4EverDB")
        {

        }
    }
}
