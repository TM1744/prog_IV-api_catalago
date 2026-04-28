using Mapster;
using Microsoft.AspNetCore.Http;
using umfgcloud.loja.dominio.service.Construtores;
using umfgcloud.loja.dominio.service.DTO;
using umfgcloud.loja.dominio.service.Interfaces.Repositorios;
using umfgcloud.loja.dominio.service.Interfaces.Servicos;

namespace umfgcloud.loja.aplicacao.service.Classes
{
    public sealed class ProdutoServico : AbstractServico, IProdutoServico
    {
        private readonly IProdutoRepositorio _repositorio;

        public ProdutoServico(IHttpContextAccessor httpContextAccessor, 
            IProdutoRepositorio repositorio)
            : base(httpContextAccessor)
        {
            _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
        }

        public async Task AdicionarAsync(ProdutoDTO.ProdutoRequest dto)
        {
            var produtoConstrutor = new ProdutoConstrutor(UserId, UserEmail);

            produtoConstrutor.BuildDescricao(dto.Descricao)
                    .BuildEAN(dto.EAN)
                    .BuildValorCompra(dto.ValorCompra)
                    .BuildValorVenda(dto.ValorVenda);

            await _repositorio.AdicionarAsync(produtoConstrutor.GetProduto());
        }

        public async Task AtualizarAsync(ProdutoDTO.AbstractProdutoWithIdDTO dto)
        {
            var produtoConstrutor = new ProdutoConstrutor(await _repositorio.ObterPorIdAsync(dto.Id));

            produtoConstrutor.BuildDescricao(dto.Descricao)
                            .BuildEAN(dto.EAN)
                            .BuildValorCompra(dto.ValorCompra)
                            .BuildValorVenda(dto.ValorVenda);

            produtoConstrutor.GetProduto().Update(UserId, UserEmail);

            await _repositorio.AtualizarAsync(produtoConstrutor.GetProduto());
        }

        //so consegue fazer a conversa, de atributos publicos na classe destino e ICollection/List
        public async Task<ProdutoDTO.ProdutoResponse> ObterPorIdAsync(Guid id)
            => (await _repositorio.ObterPorIdAsync(id)).Adapt<ProdutoDTO.ProdutoResponse>();

        public async Task<IEnumerable<ProdutoDTO.ProdutoResponse>> ObterTodosAsync()
            => (await _repositorio.ObterTodosAsync()).Adapt<IEnumerable<ProdutoDTO.ProdutoResponse>>();

        public async Task RemoverAsync(Guid id)
            => await _repositorio.RemoverAsync(await _repositorio.ObterPorIdAsync(id));
    }
}
