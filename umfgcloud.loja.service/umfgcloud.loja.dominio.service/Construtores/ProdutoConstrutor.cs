using umfgcloud.loja.dominio.service.Entidades;
using umfgcloud.loja.dominio.service.Interfaces.Construtores;

namespace umfgcloud.loja.dominio.service.Construtores
{
    public class ProdutoConstrutor : IProdutoConstrutor
    {
        private readonly ProdutoEntity _produto;

        public ProdutoConstrutor(string userId, string userEmail)
        {
            _produto = new(userId, userEmail);
        }

        public ProdutoConstrutor BuildDescricao(string descricao)
        {
            this._produto.SetDescricao(descricao);
            return this;
        }

        public void BuildEAN(string ean)
        {
            this._produto.SetEAN(ean);
        }

        public void BuildValorCompra(decimal valorCompra)
        {
            this._produto.SetValorCompra(valorCompra);
        }

        public void BuildValorVenda(decimal valorVenda)
        {
            this._produto.SetValorVenda(valorVenda);
        }

        public void Reset()
        {
            this._produto = new ProdutoEntity();
        }

        public ProdutoEntity GetProduct()
        {
            ProdutoEntity result = this._produto;

            this.Reset();

            return result;
        }

        void IAbstractConstrutor.BuildId(string id)
        {
            this._produto.Id
        }

        void IAbstractConstrutor.BuildCreatedByUserId(string id)
        {
            throw new NotImplementedException();
        }

        void IAbstractConstrutor.BuildCreatedByUserEmail(string email)
        {
            throw new NotImplementedException();
        }

        void IAbstractConstrutor.BuildUpdatedByUserId(string id)
        {
            throw new NotImplementedException();
        }

        void IAbstractConstrutor.BuildUpdatedByUserEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
