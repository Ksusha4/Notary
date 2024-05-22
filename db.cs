using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace WindowsFormsApp2
{
    class db
    {
       public SqlConnection con = new SqlConnection($@"Data Source=DESKTOP-MN5J9PP\SQLEXPRESS; Initial Catalog=Нотариус;Integrated Security = True");
    }
}
