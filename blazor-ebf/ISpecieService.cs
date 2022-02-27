using System.Collections.Generic;
using System.Threading.Tasks;
using EclipseBattleForecaster.Common;

namespace blazor_ebf
{
    public interface ISpecieService
    {
        Task< IEnumerable<ShipType> > GetShipTypes(SpecieType specieType);
        Task< IEnumerable<ShipComponent> > GetComponents(SpecieType specie, ShipType ship);
    }
}