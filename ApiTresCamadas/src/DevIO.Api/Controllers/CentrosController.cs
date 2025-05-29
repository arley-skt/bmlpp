using AutoMapper;
using DevIO.Api.ViewModels;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DevIO.Api.Controllers
{
    [Route("api/Centros")]
    public class CentrosController : MainController
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly ICentroService _centroService;
        private readonly IMapper _mapper;

        public CentrosController(IProdutoRepository produtoRepository,
                                  ICentroService centroService,
                                  IMapper mapper,
                                  INotificador notificador) : base(notificador)
        {
            _produtoRepository = produtoRepository;
            _centroService = centroService;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<ActionResult<ProdutoViewModel>> Adicionar(CentroViewModel centroViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _centroService.Adicionar(_mapper.Map<Centro>(centroViewModel));

            return CustomResponse(HttpStatusCode.Created, centroViewModel);
        }

        [HttpPost("Rotateste")]
        public async Task<ActionResult<ProdutoViewModel>> Rto(CentroViewModel centroViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _centroService.Adicionar(_mapper.Map<Centro>(centroViewModel));

            return CustomResponse(HttpStatusCode.Created, centroViewModel);
        }

    }
}