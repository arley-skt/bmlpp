using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;
using System.Threading.Tasks;

namespace DevIO.Business.Services
{
    public class UsuarioService : BaseService, IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository,
                                 INotificador notificador) : base(notificador)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<Usuario> Obter(Usuario usuario)
        {
            //if (!ExecutarValidacao(new UsuarioValidation(), usuario))
            //    //|| !ExecutarValidacao(new EnderecoValidation(), usuario.Endereco))
            //    return;
            var existe = await _usuarioRepository.Obter(usuario.Email, usuario.Senha);

            if (existe == null)
            {
                Notificar("Já existe um fornecedor com este documento infomado.");
                return existe;
            }

            var existeatv = await _usuarioRepository.Obterativo(usuario.Email, usuario.Senha);

            if (existe.Email == usuario.Email && existe.Senha==usuario.Senha)
            {

            }
            return existe;
        }

        public async Task<Usuario> ObterusuarioporId(Guid Id)
        {
            //if (!ExecutarValidacao(new UsuarioValidation(), usuario))
            //    //|| !ExecutarValidacao(new EnderecoValidation(), usuario.Endereco))
            //    return;


            var existe = await _usuarioRepository.ObterusuarioporId(Id);

            if (existe == null)
            {
                Notificar("Opa usuario nao encontrado.");
                return existe;
            }
           
            return existe;
        }

        public async Task<Usuario> Obter(string email, string senha)
        {
            //if (!ExecutarValidacao(new UsuarioValidation(), usuario))
            //    //|| !ExecutarValidacao(new EnderecoValidation(), usuario.Endereco))
            //    return;
            var existe = await _usuarioRepository.Obter(email, senha);

            if (existe == null)
            {
                Notificar("Já existe um fornecedor com este documento infomado.");
                return existe;
            }

            if (existe.Email == email && existe.Senha == senha)
            {

            }
            return existe;
        }

        public async Task Adicionar(Usuario usuario)
        {
            if (!ExecutarValidacao(new UsuarioValidation(), usuario))
                //|| !ExecutarValidacao(new EnderecoValidation(), usuario.Endereco))
                return;

            if(_usuarioRepository.Buscar(f => f.Email == usuario.Email).Result.Any())
            {
                Notificar("Já existe um fornecedor com este documento infomado.");
                return;
            }

            await _usuarioRepository.Adicionar(usuario);
        }

        public async Task Atualizar(Usuario usuario)
        {
            if (!ExecutarValidacao(new UsuarioValidation(), usuario)) return;

            if (_usuarioRepository.Buscar(f => f.Email == usuario.Email && f.Id != usuario.Id).Result.Any())
            {
                Notificar("Já existe um fornecedor com este documento infomado.");
                return;
            }

            await _usuarioRepository.Atualizar(usuario);
        }

        public async Task Remover(Guid id)
        {
            var usuario = await _usuarioRepository.ObterFornecedorProdutosEndereco(id);

            if (usuario == null)
            {
                Notificar("Fornecedor não existe!");
                return;
            }

            //if (usuario.Produtos.Any())
            //{
            //    Notificar("O fornecedor possui produtos cadastrados!");
            //    return;
            //}

            //var endereco = await _fornecedorRepository.ObterEnderecoPorFornecedor(id);

            //if (endereco != null)
            //{
            //    await _fornecedorRepository.RemoverEnderecoFornecedor(endereco);
            //}

            await _usuarioRepository.Remover(id);
        }

        public void Dispose()
        {
            _usuarioRepository?.Dispose();
        }

        public async Task<List<Usuario>> Todos(bool ativo)
        {
            var lista = await _usuarioRepository.Todos(ativo);

            return lista.ToList();
        }

        public async Task<List<Usuario>> Rotas(bool ativo) {
            var qualquerll = await _usuarioRepository.Rotas(ativo);
            return qualquerll;


        }

    }
}
