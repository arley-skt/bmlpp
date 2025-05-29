using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Repository
{
    public class CentroRepository : Repository<Centro>, ICentroRepository
    {
        public CentroRepository(MeuDbContext context) : base(context) { }

        public async Task<IEnumerable<Centro>> ObterCentros()
        {
            return await Db.Centros.AsNoTracking()
                .ToListAsync();
        }

        public async Task <Centro> ObterPorNome(string nome)
        {
            return await Db.Centros.Where(w=>w.Nome == nome).FirstOrDefaultAsync();
        }


        //public async Task<IEnumerable<Produto>> ObterProdutosFornecedores()
        //{
        //    return await Db.Produtos.AsNoTracking()
        //        .Include(f => f.Fornecedor)
        //        .OrderBy(p => p.Nome)
        //        .ToListAsync();
        //}

        //public async Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId)
        //{
        //    return await Buscar(p => p.FornecedorId == fornecedorId);
        //}
    }
}
