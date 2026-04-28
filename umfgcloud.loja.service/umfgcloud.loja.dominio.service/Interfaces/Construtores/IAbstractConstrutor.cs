using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace umfgcloud.loja.dominio.service.Interfaces.Construtores
{
    public interface IAbstractConstrutor
    {
        void BuildId(string id);
        void BuildCreatedByUserId(string id);
        void BuildCreatedByUserEmail(string email);
        void BuildUpdatedByUserId(string id);
        void BuildUpdatedByUserEmail(string email);
    }
}
