using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra_FM.DML
{
    public class Beneficiario
    {
        public long Id { get; set; }
      
        public string Nome { get; set; }
       
        public string CPF { get; set; }
        
        public long ClienteModelID { get; set; }

        //public Cliente Cliente { get; set; }
    }
}
