using umfgcloud.loja.dominio.service.Entidades;

namespace umfgcloud.loja.dominio.service.Construtores
{
    public class ProdutoConstrutor
    {
        private readonly ProdutoEntity _produto;

        public ProdutoConstrutor(string userId, string userEmail)
        {
            _produto = new(userId, userEmail);
        }

        public ProdutoConstrutor(ProdutoEntity produto)
        {
            _produto = produto;
        }

        public ProdutoConstrutor BuildDescricao(string descricao)
        {
            _produto.SetDescricao(descricao);
            return this;
        }

        public ProdutoConstrutor BuildEAN(string ean)
        {
            _produto.SetEAN(ean);
            return this;
        }

        public ProdutoConstrutor BuildValorCompra(decimal valorCompra)
        {
            _produto.SetValorCompra(valorCompra);
            return this;
        }

        public ProdutoConstrutor BuildValorVenda(decimal valorVenda)
        {
            _produto.SetValorVenda(valorVenda);
            return this;
        }

        public ProdutoEntity GetProduto()
        {
            return _produto;
        }
    }
}
