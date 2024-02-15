using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ventas_pt.Data
{
    public class ConnectionConfig
    {
        private string Server = "lab-defontana-202310.caporvnn6sbh.us-east-1.rds.amazonaws.com";
        private string Database = "Prueba";
        private string User = "ReadOnly";
        private string Pass = "d*3PSf2MmRX9vJtA5sgwSphCVQ26*T53uU";

        public string GetConnectionString()
        {
            return $"Server={Server};Database={Database};User={User};Password={Pass};TrustServerCertificate=true";
        }
    }
}
