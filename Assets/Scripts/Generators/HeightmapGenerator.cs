using System;
using DataScriptable;
using UnityEngine;

namespace Generators
{
    public class HeightmapGenerator : MonoBehaviour
    {
        #region Inspector Fields
        [SerializeField] private BiomeMap m_biomes;
        #endregion Inspector Fields
        
        
        #region MonoBehaviour Methods
        private void Awake()
        {
            SetAllBiomeNoises();            
        }
        #endregion MonoBehaviour Methods
        
        
        #region Methods
        public float GetHeightAt(float x, float y)
        {
            if (m_biomes.Data.Count == 0)
            {
                Debug.LogWarning("Trying to get height but there's no biome data");
                return 0;
            }
                
            // TODO: Get Temperature And Height to find correct Biome here
            float height = m_biomes.Data[0].Noise.noise.GetSimplexFractal(x, y);
            return (height + 1f) / 2f;
        }
        #endregion Methods
        
        
        #region Helper Methods
        private float HeightToTemperature(float normalizedHeight)
        {
            return normalizedHeight;
        }

        private float GetMoistureAt(int x, int y)
        {
            return 0;
        }
        
        private void SetAllBiomeNoises()
        {
            foreach (var noiseAndBiome in m_biomes.Data)
                noiseAndBiome.Noise.SetNoise();
        }
        #endregion Helper Methods
    }
}