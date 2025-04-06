using AutoMapper;
using DevIO.Api.ViewModels;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace DevIO.Api.Controllers
{
    [Route("api/Auth")]
    public class AuthController : MainController
    {
        private readonly IMapper _mapper;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioService _usuarioService;

        public AuthController(IMapper mapper,
                                      IUsuarioRepository usuarioRepository,
                                      IUsuarioService usuarioService,
                                      INotificador notificador) : base(notificador)
        {
            _mapper = mapper;
            _usuarioRepository = usuarioRepository;
            _usuarioService = usuarioService;
        }

        //[EnableCors("Development")]
        [HttpPost("nova-conta")]
        public async Task<ActionResult> Registrar(RegisterUserViewModel registerUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new Usuario
            {
                Senha = registerUser.Password,
                Email = registerUser.Email,
                Ativo = true
            };

            await _usuarioRepository.Adicionar(user);

            var userio = _mapper.Map < UserTokenViewModel >( await _usuarioRepository.Obter(user.Email, user.Senha));
            if (userio.ativo)
            {
                //await _signInManager.SignInAsync(user, false);
                return CustomResponse(HttpStatusCode.Created, await GerarJwt(userio));
            }
            //foreach (var error in result.Errors)
            //{
            //    NotificarErro(error.Description);
            //}

            return CustomResponse(HttpStatusCode.Created, await GerarJwt(userio));
        }

        [HttpPost("entrar")]
        public async Task<ActionResult> Login(LoginUserViewModel loginUser)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var result =  _mapper.Map<UserTokenViewModel>
                (await _usuarioService.Obter(_mapper.Map<Usuario>(loginUser)));

            if (result != null && result.ativo)
            {
                //_logger.LogInformation("Usuario " + loginUser.Email + " logado com sucesso");
                return CustomResponse(HttpStatusCode.OK, await GerarJwt(result));
            }
            //if (result.IsLockedOut)
            //{
            //    NotificarErro("Usuário temporariamente bloqueado por tentativas inválidas");
            //    return CustomResponse(loginUser);
            //}

            NotificarErro("Usuário ou Senha incorretos");
            return CustomResponse(HttpStatusCode.BadRequest, loginUser);
        }

        private async Task<LoginResponseViewModel> GerarJwt(UserTokenViewModel user)
        {
            //var user = await _userManager.FindByEmailAsync(email);
            //var claims = await _userManager.GetClaimsAsync(user);
            //var userRoles = await _userManager.GetRolesAsync(user);

            //claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id));
            //claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            //claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            //claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            //claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));
            //foreach (var userRole in userRoles)
            //{
            //    claims.Add(new Claim("role", userRole));
            //}

            var identityClaims = new ClaimsIdentity();
            identityClaims.AddClaim(new Claim(JwtRegisteredClaimNames.Email, user.Email));

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("MEUSEGREDOSUPERSECRETO");//_appSettings.Secret
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = "MeuSistema",//_appSettings.Emissor,
                Audience = "http://localhost",
                Subject = identityClaims,
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encodedToken = tokenHandler.WriteToken(token);

            var response = new LoginResponseViewModel
            {
                AccessToken = encodedToken,
                ExpiresIn = TimeSpan.FromHours(2).TotalSeconds,//_appSettings.ExpiracaoHoras
                UserToken = new UserTokenViewModel
                {
                    Id = user.Id.ToString(),
                    Email = user.Email,
                    //Claims = claims.Select(c => new ClaimViewModel { Type = c.Type, Value = c.Value })
                }
            };

            return response;
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);

        //    [HttpGet]
        //    public async Task<IEnumerable<FornecedorViewModel>> ObterTodos()
        //    {
        //        return _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
        //    }

        //    [HttpGet("{id:guid}")]
        //    public async Task<ActionResult<FornecedorViewModel>> ObterPorId(Guid id)
        //    {
        //        var fornecedor = await ObterFornecedorProdutosEndereco(id);

        //        if (fornecedor == null) return NotFound();

        //        return fornecedor;
        //    }

        //    [HttpPost]
        //    public async Task<ActionResult<FornecedorViewModel>> Adicionar(FornecedorViewModel fornecedorViewModel)
        //    {
        //        if (!ModelState.IsValid) return CustomResponse(ModelState);

        //        await _fornecedorService.Adicionar(_mapper.Map<Fornecedor>(fornecedorViewModel));

        //        return CustomResponse(HttpStatusCode.Created, fornecedorViewModel);
        //    }

        //    [HttpPut("{id:guid}")]
        //    public async Task<ActionResult<FornecedorViewModel>> Atualizar(Guid id, FornecedorViewModel fornecedorViewModel)
        //    {
        //        if (id != fornecedorViewModel.Id)
        //        {
        //            NotificarErro("O id informado não é o mesmo que foi passado na query");
        //            return CustomResponse();
        //        }

        //        if (!ModelState.IsValid) return CustomResponse(ModelState);

        //        await _fornecedorService.Atualizar(_mapper.Map<Fornecedor>(fornecedorViewModel));

        //        return CustomResponse(HttpStatusCode.NoContent);
        //    }

        //    [HttpDelete("{id:guid}")]
        //    public async Task<ActionResult<FornecedorViewModel>> Excluir(Guid id)
        //    {
        //        await _fornecedorService.Remover(id);

        //        return CustomResponse(HttpStatusCode.NoContent);
        //    }

        //    private async Task<FornecedorViewModel> ObterFornecedorProdutosEndereco(Guid id)
        //    {
        //        return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorProdutosEndereco(id));
        //    }
        //}
        [HttpGet("todosusuarios")]
        public async Task<ActionResult> Todos(bool ativo)
        {
            var lista = _usuarioService.Todos(ativo);

            return CustomResponse(HttpStatusCode.OK, lista);
        }
        //public async Task<ActionResult> Rotas(bool ativo) {
        //  var routs = await  _usuarioService.Rotas(ativo);
        //  return  CustomResponse(HttpStatusCode.OK, routs);






        //}

    }
}
