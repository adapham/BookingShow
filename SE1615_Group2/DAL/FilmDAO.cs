using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SE1615_Group2.DAL
{
    public  class FilmDAO: DAO
    {
        static FilmDAO Instance;
        FilmDAO() { }
        static FilmDAO() => Instance = new FilmDAO();
        public static FilmDAO GetInstance() => Instance;
        public DataTable GetDataTable() => GetDataTable("select * from films");
        public DataTable GetDataTable1() => GetDataTable1("select * from films");
    }
}
