using System;
using UnityEngine;
using System.Collections.Generic;

namespace Data
{
    [Serializable]
    public class BiomeMap
    {
        #region Fields
        [SerializeField] private List<Biome> m_biomes;
        [SerializeField] private float m_seaLevel;
        #endregion Fields
        
        
        #region Methods
        public Biome Evaluate(float normalizedTemperature, float normalizedMoisture)
        {
            foreach (var biome in m_biomes)
            {
                float t = normalizedTemperature;
                float m = normalizedMoisture;
                if(IsInsideMoisture(biome, m) && IsInsideTemperature(biome, t))
                    return biome;
            }
            
            return new Biome();
        }
        #endregion Methods
        
        
        #region Helper Methods
        private bool IsInsideTemperature(Biome biome, float t)
        {
            return t >= biome.TemperatureRange.x && t <= biome.TemperatureRange.y;
        }
        
        private bool IsInsideMoisture(Biome biome, float m)
        {
            return m >= biome.MoistureRange.x && m <= biome.MoistureRange.y;
        }
        #endregion Helper Methods
    }
}