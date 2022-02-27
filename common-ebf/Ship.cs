using System.Collections.Generic;

namespace EclipseBattleForecaster.Common
{
    public enum ShipType { Interceptor, Cruiser, Dreadnought}

    public class Ship
    {
        public ShipType ShipType { get; set; }
        public IEnumerable<ShipComponent> Components { get; set; }
    }
}