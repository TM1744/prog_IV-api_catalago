using umfgcloud.loja.dominio.service.DTO;

namespace umfgcloud.aplicacao.service.testes.Classes
{
    [TestClass]
    public sealed class ProdutoServicoTestes : AbstractServicoTestes
    {
        private const string C_OWNER = "Juliano Maciel";
        private const string C_CATEGORY = "produto";
        private const decimal C_VALOR_NEGATIVO = -89.90m;

        private ProdutoDTO.ProdutoRequest createProdutoRequest()
        {
            return new()
            {
                Descricao = "TESTE",
                EAN = "123456789",
                ValorCompra = 39.90m,
                ValorVenda = 89.90m,
            };
        }

        private ProdutoDTO.ProdutoRequest createProdutoRequestTwo()
        {
            return new()
            {
                Descricao = "TESTE 2",
                EAN = "987654321",
                ValorCompra = 49.90m,
                ValorVenda = 99.90m,
            };
        }

        private ProdutoDTO.ProdutoRequestWithId createProdutoRequestWithId(Guid id)
        {
            return new()
            {
                Id = id,
                Descricao = "TESTE 3",
                EAN = "987654455",
                ValorCompra = 20.87m,
                ValorVenda = 34.71m,
            };
        }
        
        private ProdutoDTO.ProdutoRequestWithId createProdutoRequestWithNewId()
        {
            return new()
            {
                Id = Guid.NewGuid(),
                Descricao = "TESTE 3",
                EAN = "987654455",
                ValorCompra = 20.87m,
                ValorVenda = 34.71m,
            };
        }
        

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_AdicionarAsync_Sucesso()
        {
            try
            {
                //o objetivo do using é o desenvolvedor ter controlle sobre o 
                //dispose do objeto
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

                var servico = GetProdutoServicoValidJWT(context);
                var dto = createProdutoRequest();

                await servico.AdicionarAsync(dto);

                var produto = (await servico.ObterTodosAsync()).FirstOrDefault();

                Assert.IsNotNull(produto);
                Assert.IsFalse(Guid.Empty.Equals(produto.Id));
                Assert.AreEqual("TESTE", produto.Descricao);
                Assert.AreEqual("123456789", produto.EAN);
                Assert.AreEqual(39.90m, produto.ValorCompra);
                Assert.AreEqual(89.90m, produto.ValorVenda);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_AdicionarAsync_FalhaValorCompraNegativo()
        {
            try
            {
                //o objetivo do using é o desenvolvedor ter controlle sobre o 
                //dispose do objeto
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

                var servico = GetProdutoServicoValidJWT(context);
                var dto = createProdutoRequest();
                dto.ValorCompra = -39.90m;

                await Assert.ThrowsExceptionAsync<InvalidDataException>(() => servico.AdicionarAsync(dto));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_AdicionarAsync_FalhaValorVendaNegativo()
        {
            try
            {
                //o objetivo do using é o desenvolvedor ter controlle sobre o 
                //dispose do objeto
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

                var servico = GetProdutoServicoValidJWT(context);
                var dto = createProdutoRequest();
                dto.ValorVenda = -89.90m;

                await Assert.ThrowsExceptionAsync<InvalidDataException>(() => servico.AdicionarAsync(dto));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        
        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_AdicionarAsync_FalhaEanNulo()
        {
            try
            {
                //o objetivo do using é o desenvolvedor ter controlle sobre o 
                //dispose do objeto
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

                var servico = GetProdutoServicoValidJWT(context);
                var dto = createProdutoRequest();
                dto.EAN = null;

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => servico.AdicionarAsync(dto));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        
        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_AdicionarAsync_FalhaDescricaoNula()
        {
            try
            {
                //o objetivo do using é o desenvolvedor ter controlle sobre o 
                //dispose do objeto
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

                var servico = GetProdutoServicoValidJWT(context);
                var dto = createProdutoRequest();
                dto.Descricao = null;

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => servico.AdicionarAsync(dto));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public void ProdutoServico_Instanciar_Falha()
        {
            try
            {
                //o objetivo do using é o desenvolvedor ter controlle sobre o 
                //dispose do objeto
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());

                Assert.ThrowsException<InvalidDataException>(() => GetProdutoServicoInvalidJWT(context));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_ObterTodosAsync_Sucesso()
        {
            try
            {
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());
                var servico = GetProdutoServicoValidJWT(context);

                var dto = createProdutoRequest();
                var otherDto = createProdutoRequestTwo();

                await servico.AdicionarAsync(dto);
                await servico.AdicionarAsync(otherDto);

                var produtos = await servico.ObterTodosAsync();

                Assert.AreEqual(2, produtos.Count());
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        
        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_ObterTodosAsync_SucessoNenhumProduto()
        {
            try
            {
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());
                var servico = GetProdutoServicoValidJWT(context);

                var produtos = await servico.ObterTodosAsync();

                Assert.IsTrue(!produtos.Any());
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        
        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_ObterTodosAsync_SucessoNenhumProdutoAtivo()
        {
            try
            {
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());
                var servico = GetProdutoServicoValidJWT(context);
                
                var dto = createProdutoRequest();
                await servico.AdicionarAsync(dto);
                var produto = (await servico.ObterTodosAsync()).FirstOrDefault();
                await servico.RemoverAsync(produto.Id);
                
                var produtos = await servico.ObterTodosAsync();

                Assert.IsTrue(!produtos.Any());
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        
        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_AtualizarAsync_Sucesso()
        {
            try
            {
                //o objetivo do using é o desenvolvedor ter controlle sobre o 
                //dispose do objeto
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());
                var servico = GetProdutoServicoValidJWT(context);
                var dto = createProdutoRequest();
                await servico.AdicionarAsync(dto);
                
                var produto = (await servico.ObterTodosAsync()).FirstOrDefault();
                var updateDto = createProdutoRequestWithId(produto.Id);

                await servico.AtualizarAsync(updateDto);
                produto = (await servico.ObterTodosAsync()).FirstOrDefault();
                
                Assert.AreEqual(produto.Id, updateDto.Id);
                Assert.AreEqual(produto.Descricao, updateDto.Descricao);
                Assert.AreEqual(produto.EAN, updateDto.EAN);
                Assert.AreEqual(produto.ValorVenda, updateDto.ValorVenda);
                Assert.AreEqual(produto.ValorCompra, updateDto.ValorCompra);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        
        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_AtualizarAsync_FalhaDescricaoNula()
        {
            try
            {
                //o objetivo do using é o desenvolvedor ter controlle sobre o 
                //dispose do objeto
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());
                var servico = GetProdutoServicoValidJWT(context);
                var dto = createProdutoRequest();
                await servico.AdicionarAsync(dto);
                
                var produto = (await servico.ObterTodosAsync()).FirstOrDefault();
                var updateDto = createProdutoRequestWithId(produto.Id);
                updateDto.Descricao = null;

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => servico.AtualizarAsync(updateDto));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        
        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_AtualizarAsync_FalhaEanNulo()
        {
            try
            {
                //o objetivo do using é o desenvolvedor ter controlle sobre o 
                //dispose do objeto
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());
                var servico = GetProdutoServicoValidJWT(context);
                var dto = createProdutoRequest();
                await servico.AdicionarAsync(dto);
                
                var produto = (await servico.ObterTodosAsync()).FirstOrDefault();
                var updateDto = createProdutoRequestWithId(produto.Id);
                updateDto.EAN = null;

                await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => servico.AtualizarAsync(updateDto));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        
        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_AtualizarAsync_FalhaValorCompraNegativo()
        {
            try
            {
                //o objetivo do using é o desenvolvedor ter controlle sobre o 
                //dispose do objeto
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());
                var servico = GetProdutoServicoValidJWT(context);
                var dto = createProdutoRequest();
                await servico.AdicionarAsync(dto);
                
                var produto = (await servico.ObterTodosAsync()).FirstOrDefault();
                var updateDto = createProdutoRequestWithId(produto.Id);
                updateDto.ValorCompra = -87.21m;

                await Assert.ThrowsExceptionAsync<InvalidDataException>(() => servico.AtualizarAsync(updateDto));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        
        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_AtualizarAsync_FalhaValorVendaNegativo()
        {
            try
            {
                //o objetivo do using é o desenvolvedor ter controlle sobre o 
                //dispose do objeto
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());
                var servico = GetProdutoServicoValidJWT(context);
                var dto = createProdutoRequest();
                await servico.AdicionarAsync(dto);
                
                var produto = (await servico.ObterTodosAsync()).FirstOrDefault();
                var updateDto = createProdutoRequestWithId(produto.Id);
                updateDto.ValorVenda = -87.21m;

                await Assert.ThrowsExceptionAsync<InvalidDataException>(() => servico.AtualizarAsync(updateDto));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        
        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_AtualizarAsync_FalhaIdInvalido()
        {
            try
            {
                //o objetivo do using é o desenvolvedor ter controlle sobre o 
                //dispose do objeto
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());
                var servico = GetProdutoServicoValidJWT(context);
                var dto = createProdutoRequest();
                await servico.AdicionarAsync(dto);
                
                var updateDto = createProdutoRequestWithNewId();
                
                await Assert.ThrowsExceptionAsync<ApplicationException>(() => servico.AtualizarAsync(updateDto));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        
        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_ObterPorIdAsync_FalhaIdInvalido()
        {
            try
            {
                //o objetivo do using é o desenvolvedor ter controlle sobre o 
                //dispose do objeto
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());
                var servico = GetProdutoServicoValidJWT(context);

                await Assert.ThrowsExceptionAsync<ApplicationException>(() => servico.ObterPorIdAsync(Guid.NewGuid()));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        
        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_ObterPorIdAsync_Sucesso()
        {
            try
            {
                //o objetivo do using é o desenvolvedor ter controlle sobre o 
                //dispose do objeto
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());
                var servico = GetProdutoServicoValidJWT(context);
                var dto = createProdutoRequest();
                await servico.AdicionarAsync(dto);
                
                var produto = (await servico.ObterTodosAsync()).FirstOrDefault();
                
                var produtoCompleto = await servico.ObterPorIdAsync(produto.Id);
                
                Assert.AreEqual(produto.Id, produtoCompleto.Id);
                Assert.AreEqual(produto.Descricao, produtoCompleto.Descricao);
                Assert.AreEqual(produto.EAN, produtoCompleto.EAN);
                Assert.AreEqual(produto.ValorVenda, produtoCompleto.ValorVenda);
                Assert.AreEqual(produto.ValorCompra, produtoCompleto.ValorCompra);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        
        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_ObterPorIdAsync_FalhaProdutoInativo()
        {
            try
            {
                //o objetivo do using é o desenvolvedor ter controlle sobre o 
                //dispose do objeto
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());
                var servico = GetProdutoServicoValidJWT(context);
                var dto = createProdutoRequest();
                await servico.AdicionarAsync(dto);
                var produto = (await servico.ObterTodosAsync()).FirstOrDefault();
                await servico.RemoverAsync(produto.Id);
                
                await Assert.ThrowsExceptionAsync<ApplicationException>(() => servico.ObterPorIdAsync(produto.Id));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        
        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_RemoverAsync_Sucesso()
        {
            try
            {
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());
                var servico = GetProdutoServicoValidJWT(context);
                
                var dto = createProdutoRequest();
                await servico.AdicionarAsync(dto);
                var produto = (await servico.ObterTodosAsync()).FirstOrDefault();
                await servico.RemoverAsync(produto.Id);
                
                var produtos = await servico.ObterTodosAsync();

                Assert.IsTrue(!produtos.Any());
                
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        
        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_RemoverAsync_FalhaIdInvalido()
        {
            try
            {
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());
                var servico = GetProdutoServicoValidJWT(context);
                
                await Assert.ThrowsExceptionAsync<ApplicationException>(() => servico.RemoverAsync(Guid.NewGuid()));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        
        [TestMethod]
        [Owner(C_OWNER)]
        [TestCategory(C_CATEGORY)]
        public async Task ProdutoServico_RemoverAsync_FalhaProdutoInativo()
        {
            try
            {
                using var context = GetSqlServerDatabaseContext(Guid.NewGuid().ToString());
                var servico = GetProdutoServicoValidJWT(context);
                
                var dto = createProdutoRequest();
                await servico.AdicionarAsync(dto);
                var produto = (await servico.ObterTodosAsync()).FirstOrDefault();
                await servico.RemoverAsync(produto.Id);
                
                await Assert.ThrowsExceptionAsync<ApplicationException>(() => servico.RemoverAsync(produto.Id));
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
        
        
        
    }
}