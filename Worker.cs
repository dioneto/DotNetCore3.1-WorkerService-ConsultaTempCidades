using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MonitorTempo.Client;
using RestSharp;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace MonitorTempo
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConfiguration _configuration;
        private readonly ClimaHgBrasil _climaHgBrasil;

        public Worker(ILogger<Worker> logger, IConfiguration configuration, ClimaHgBrasil climaHgBrasil)
        {
            _logger = logger;
            _configuration = configuration;
            _climaHgBrasil = climaHgBrasil;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("In�cio da execu��o do Worker - {time}", DateTimeOffset.Now);

                var cidades = _configuration["MonitoriaTempo:Cidades"]
                    .Split('|', StringSplitOptions.RemoveEmptyEntries);

                foreach (var cidade in cidades)
                {

                    var dados = _climaHgBrasil.BuscarDadosCidade(cidade);

                    _logger.LogInformation($"{cidade} - {dados.results.temp}�C ({dados.results.description}) (Pesquisa realizada �s {dados.results.time})");
                }

                await Task.Delay(Convert.ToInt32(_configuration["ServiceConfigurations:Intervalo"]), stoppingToken);
            }
        }
    }
}
