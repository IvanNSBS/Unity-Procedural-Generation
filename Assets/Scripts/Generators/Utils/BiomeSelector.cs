using Data;
using DataScriptable;

namespace Generators.Utils
{
    public static class BiomeSelector
    {
        #region Methods
        public static BiomeData Evaluate(BiomeMap map, float normalizedTemperature, float normalizedMoisture)
        {
            foreach (var data in map.Data)
            {
                float t = normalizedTemperature;
                float m = normalizedMoisture;
                if(IsInsideMoisture(data.Biome, m) && IsInsideTemperature(data.Biome, t))
                    return data.Biome;
            }
            
            return null;
        }
        #endregion Methods
        
        
        #region Helper Methods
        private static bool IsInsideTemperature(BiomeData biomeData, float t)
        {
            return t >= biomeData.TemperatureRange.x && t <= biomeData.TemperatureRange.y;
        }
        
        private static bool IsInsideMoisture(BiomeData biomeData, float m)
        {
            return m >= biomeData.MoistureRange.x && m <= biomeData.MoistureRange.y;
        }
        #endregion Helper Methods
    }
}