using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using EclipseBattleForecaster.Common;

namespace blazor_ebf
{
    public class SpecieService : ISpecieService
    {
        private IEnumerable<Specie> _species;

        private HttpClient _httpClient;

        private readonly JsonSerializerOptions serializerOptions = new JsonSerializerOptions {
            Converters = { new ShipComponentJsonConverter() }
        };

        public SpecieService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task< IEnumerable<ShipType> > GetShipTypes(SpecieType specieType) 
            => (await _httpClient.GetFromJsonAsync<IEnumerable<Specie>>("sample-data/species.json", serializerOptions))
            .First(x => x.SpecieType == specieType).Ships
            .Select(x => x.ShipType);
        
        public async Task< IEnumerable<ShipComponent> > GetComponents(SpecieType specieType, ShipType shipType) 
            => (await _httpClient.GetFromJsonAsync<IEnumerable<Specie>>("sample-data/species.json", serializerOptions))
            .First(x => x.SpecieType == specieType).Ships
            .First(x => x.ShipType == shipType).Components;
    }
}