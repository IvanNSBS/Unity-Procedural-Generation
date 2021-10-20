using System;
using UnityEngine;

namespace Data
{
    
    [Serializable]
    public class NoiseAndBiome
    {
        #region Inspector Fields
        [SerializeField] private string m_name;
        [SerializeField] private BiomeData m_biome;
        [SerializeField] private NoiseData m_noise;
        #endregion Inspector Fields

        #region Propertie
        public string Name => m_name;
        public BiomeData Biome => m_biome;
        public NoiseData Noise => m_noise;
        #endregion Properties
    }
}