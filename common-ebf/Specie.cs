using System.Collections.Generic;

namespace EclipseBattleForecaster.Common
{
    public enum SpecieType { Terran, Planta }
    public class Specie
    {
        public SpecieType SpecieType { get; set; }
        public IEnumerable<Ship> Ships { get; set; }
    }
}