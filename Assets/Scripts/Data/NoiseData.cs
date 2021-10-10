using System;
using Libs;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public class NoiseData
    {
        [SerializeField] [Range(0, 1)]public float maxHeight = 0.5f;
        [SerializeField] public bool active = true;
        [SerializeField] private int m_seed = 1337;
        [SerializeField] [Range(0f, 0.5f)] private float m_fractalFrequency = 0.02f;
        [SerializeField] [Range(0f, 10f)] private float m_fractalGain = 0.5f;
        [SerializeField] [Range(0f, 10f)] private float m_fractalLacunarity = 2f;
        [SerializeField] [Range(1, 8)] private int m_fractalOctaves = 5;
        [SerializeField] private FastNoise.FractalType m_fractalType = FastNoise.FractalType.FBM;
        [SerializeField] private FastNoise.Interp m_interpolation = FastNoise.Interp.Linear;

        public FastNoise noise;

        public NoiseData()
        {
            noise = new FastNoise();
        }
        
        public void SetNoise()
        {
            noise.SetSeed(m_seed);
            noise.SetFrequency(m_fractalFrequency);
            noise.SetFractalType(m_fractalType);
            noise.SetFractalGain(m_fractalGain);
            noise.SetFractalLacunarity(m_fractalLacunarity);
            noise.SetFractalOctaves(m_fractalOctaves);
            noise.SetInterp(m_interpolation);
        }
    }
}