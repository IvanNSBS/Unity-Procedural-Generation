using Data;
using UnityEngine;
using System.Collections.Generic;

namespace DataScriptable
{
    [CreateAssetMenu(fileName = "Biome Map", menuName = "PCG/Biome Map", order = 0)]
    public class BiomeMap : ScriptableObject
    {
        #region Inspector Fields
        [SerializeField] private List<NoiseAndBiome> m_data;
        [SerializeField] [Range(0, 1)] private float m_seaLevel;
        #endregion Inspector Fields
        
        #region Properties
        public IReadOnlyList<NoiseAndBiome> Data => m_data;
        public float SeaLevel => m_seaLevel;
        #endregion Properties
    }
}