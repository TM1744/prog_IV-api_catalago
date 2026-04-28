
using umfgcloud.loja.dominio.service.Interfaces.Construtores;

namespace umfgcloud.loja.dominio.service.Diretores
{
    public class ProdutoDiretor
    {
        private IProdutoConstrutor _builder;

        public IProdutoConstrutor Builder
        {
            get { return _builder; }
        }

        public void BuildFullProduto(string descricao, string ean, decimal valorCompra, decimal valorVenda)
        {
            this._builder.BuildDescricao(descricao);
            this._builder.BuildEAN(ean);
            this._builder.BuildValorCompra(valorCompra);
            this._builder.BuildValorVenda(valorVenda);
        }
    }
}
