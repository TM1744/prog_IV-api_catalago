using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace umfgcloud.loja.dominio.service.Interfaces.Construtores
{
    public interface IProdutoConstrutor : IAbstractConstrutor
    {
        void BuildDescricao(string descricao);
        void BuildEAN(string ean);
        void BuildValorCompra(decimal valorCompra);
        void BuildValorVenda(decimal valorVenda);
    }
}
