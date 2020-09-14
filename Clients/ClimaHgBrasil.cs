using Microsoft.Extensions.Configuration;
using RestSharp;
using System;
using System.Net.Http;
using System.Text.Json;

namespace MonitorTempo.Client
{
    public class ClimaHgBrasil
    {
        private string _baseUrl;
        private JsonSerializerOptions _jsonSerializerOptionsResult;

        public ClimaHgBrasil(IConfiguration configuration)
        {
            _baseUrl = $"{configuration["MonitoriaTempo:Client"]}?key={configuration["MonitoriaTempo:Chave"]}&city_name=";
            _jsonSerializerOptionsResult = new JsonSerializerOptions()
            {

                WriteIndented = true,
                AllowTrailingCommas = false,
                IgnoreNullValues = true,
                IgnoreReadOnlyProperties = true
            };
        }

        public DadosMetereologicos BuscarDadosCidade(string cidade)
        {
            var client = new RestClient($"{_baseUrl}{cidade}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            var ret = response.Content;

            return JsonSerializer.Deserialize<DadosMetereologicos>(ret, _jsonSerializerOptionsResult);
        }
    }
}
