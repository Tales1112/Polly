using APIContagem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;

namespace APIContagem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContadorController : ControllerBase
    {
        private static readonly Contador _Contador = new();
        private readonly ILogger<ContadorController> _logger;
        private readonly IConfiguration _configuration;

        public ContadorController(ILogger<ContadorController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public ResultadoContador Get()
        {
            int valorAtualContador;

            lock (_Contador)
            {
                _Contador.Incrementar();
                valorAtualContador = _Contador.ValorAtual;
            }

            if(valorAtualContador % 4 != 0)
            {
                _logger.LogError("Simulando falha...");
                throw new Exception("Simelando falha!");
            }

            _logger.LogInformation($"Contador - Valor Atual:{valorAtualContador}");

            return new()
            {
                ValorAtual = valorAtualContador,
                Local = _Contador.Local,
                Kernel = _Contador.Kernel,
                TargetFramework = _Contador.TargetFrameWork,
                MensagemFixa = "Teste",
                MensagemVariavel = _configuration["MensagemVariavel"]
            };
        }
    }
}
