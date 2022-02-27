using System.Linq;
using System.ComponentModel;
using System;
using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Collections.Generic;

namespace EclipseBattleForecaster.Common
{
    public enum ShipComponentType 
    { 
        GluonComputer, PositronComputer, ElectronComputer,
        AntimatterCannon, SolitonCannon, PlasmaCannon, IonCannon,
        CoinfoldField, ImprovedHull, Hull,
        PhaseShield, GausShield,
        PlasmaMissile, FluxMissile,
        Empty
    }

    public enum DiceColor
    {
        Red, Blue, Orange, Yellow
    }

    public class ShipComponentConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
            => sourceType == typeof(string) 
            || base.CanConvertFrom(context, sourceType);

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
            => value is string input 
            ? ShipComponent.All.First(x => x.ToString() == input)
            : base.ConvertFrom(context, culture, value);
    }

    [TypeConverter(typeof(ShipComponentConverter))]
    public abstract class ShipComponent
    {
        public static ShipComponent[] All => new ShipComponent[]
        {
            new Empty(),
            new IonCannon(),
            new PlasmaCannon()
        };
    }

    public class ShipComponentJsonConverter : JsonConverter<IEnumerable<ShipComponent>>
    {
        private static string _prefix => $"{typeof(ShipComponent).Namespace}.";
        public override IEnumerable<ShipComponent> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var list = new List<ShipComponent>();

            while(reader.Read())
            {
                switch(reader.TokenType)
                {
                    case JsonTokenType.String:
                        var name = $"{_prefix}{reader.GetString()}";
                        var shipComponent = ShipComponent.All.First(x => x.ToString() == name);
                        list.Add(shipComponent); 
                        break;
                    case JsonTokenType.EndArray:
                        return list;
                }
            }

            throw new JsonException("Failed creating components collection");
        }

        public override void Write(Utf8JsonWriter writer, IEnumerable<ShipComponent> value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            foreach(var component in value)
                writer.WriteStringValue(component.GetType().ToString().Replace(_prefix, string.Empty));
            writer.WriteEndArray();
        }
    }

    public interface IConsumePower
    {
        int PowerConsumption { get; }
    }

    public interface IProvideInitiative
    {
        int Initiative { get; }
    }

    public sealed class Empty : ShipComponent { }

    public sealed class IonCannon : ShipComponent, IConsumePower
    {
        public int PowerConsumption => 1;
        public DiceColor DiceColor => DiceColor.Yellow;
    }

    public sealed class PlasmaCannon : ShipComponent, IConsumePower, IProvideInitiative
    {
        public int PowerConsumption => 2;
        public int Initiative => 1;
        public DiceColor DiceColor => DiceColor.Orange;
    }

}