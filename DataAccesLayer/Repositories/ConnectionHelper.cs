using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccesLayer.Repositories
{
    static internal class ConnectionHelper
    {

        internal static string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["Game_DBConnectionString"].ConnectionString;
            }

        }

    }
}
