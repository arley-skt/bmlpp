using AutoMapper;
using DevIO.Api.ViewModels;
using DevIO.Business.Models;

namespace DevIO.Api.Configurations
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig() 
        {
            CreateMap<Fornecedor, FornecedorViewModel>().ReverseMap();
            CreateMap<Endereco, EnderecoViewModel>().ReverseMap();
            CreateMap<ProdutoViewModel, Produto>();
            CreateMap<UserTokenViewModel, Usuario>().ReverseMap();
            CreateMap<LoginUserViewModel, Usuario>()
                .ForMember(dest=>dest.Email, opt => opt.MapFrom(usr=>usr.Email))
                .ForMember(dest=>dest.Senha, opt=> opt.MapFrom(usr=>usr.Password)).ReverseMap();

            CreateMap<Produto, ProdutoViewModel>()
                .ForMember(dest => dest.NomeFornecedor, opt => opt.MapFrom(src => src.Fornecedor.Nome));
        }
    }
}
