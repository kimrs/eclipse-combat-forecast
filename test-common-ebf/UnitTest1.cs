using Xunit;
using System.Text.Json;
using System.IO;
using System.Collections.Generic;
using EclipseBattleForecaster.Common;

namespace EclipseBattleForecaster.Test
{
    public class SpeciesParser
    {
        private readonly string ExpectedResult = "[{\"SpecieType\":0,\"Ships\":[{\"ShipType\":0,\"Components\":[\"PlasmaCannon\",\"Empty\",\"Empty\",\"Empty\"]},{\"ShipType\":1,\"Components\":[\"PlasmaCannon\",\"Empty\",\"Empty\",\"Empty\",\"Empty\",\"Empty\"]},{\"ShipType\":2,\"Components\":[\"PlasmaCannon\",\"PlasmaCannon\",\"Empty\",\"Empty\",\"Empty\",\"Empty\",\"Empty\",\"Empty\"]}]},{\"SpecieType\":1,\"Ships\":[{\"ShipType\":0,\"Components\":[\"IonCannon\",\"Empty\",\"Empty\",\"Empty\"]},{\"ShipType\":1,\"Components\":[\"IonCannon\",\"Empty\",\"Empty\",\"Empty\",\"Empty\",\"Empty\"]},{\"ShipType\":2,\"Components\":[\"PlasmaCannon\",\"IonCannon\",\"Empty\",\"Empty\",\"Empty\",\"Empty\",\"Empty\",\"Empty\"]}]}]";

        [Fact]
        public void TestDeserializeAndSerializeSpeciesAsJson()
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = false,
                Converters = 
                {
                    new ShipComponentJsonConverter()
                }
            };

            var speciesJson = File.ReadAllText("species.json");
            var species = JsonSerializer.Deserialize<IEnumerable<Specie>>(speciesJson, options);
            var json = JsonSerializer.Serialize(species, options);

            Assert.Equal(ExpectedResult, json);

            var hello = "Hellp";
            var x = $"{hello} world";

        }
    }
}
