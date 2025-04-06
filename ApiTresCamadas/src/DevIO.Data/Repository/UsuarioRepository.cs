using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Repository
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(MeuDbContext context) : base(context) { }

        public async Task<Usuario> Obter(string email, string senha)
        {
            return await Db.Usuarios.AsNoTracking()
                .Where(w=>w.Email==email && w.Senha == senha)
                .FirstOrDefaultAsync();
        }

        public async Task<Usuario> Obterativo(string email, string senha)
        {
            return await Db.Usuarios.AsNoTracking()
                .Where(w => w.Email == email && w.Senha == senha && w.Ativo)
                .FirstOrDefaultAsync();
        }
        public async Task<Fornecedor> ObterFornecedorEndereco(Guid id)
        {
            return await Db.Fornecedores.AsNoTracking()
                .Include(c => c.Endereco)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id)
        {
            return await Db.Fornecedores.AsNoTracking()
                .Include(c => c.Produtos)
                .Include(c => c.Endereco)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId)
        {
            return await Db.Enderecos.AsNoTracking()
                .FirstOrDefaultAsync(f => f.FornecedorId == fornecedorId);
        }

        public async Task RemoverEnderecoFornecedor(Endereco endereco)
        {
            Db.Enderecos.Remove(endereco);
            await SaveChanges();
        }

        public async Task<IEnumerable<Usuario>> Todos(bool ativo)
        {
            try
            {
                //var teste = ICollection<Usuario>?;
                var teste =   Db.Usuarios.Where(x=>x.Ativo).ToList();
                    //Db.Usuarios.Where(w=>!w.Ativo).ToListAsync().Result;
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
            


        }
        public async Task<List<Usuario>> Rotas(bool ativo) {
            var qualquer = Db.Usuarios.ToList();
            return qualquer;

        
        }

    }
}