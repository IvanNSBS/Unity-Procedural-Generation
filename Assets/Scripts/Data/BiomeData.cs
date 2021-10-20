using System;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class BiomeData
    {
        #region Fields
        [SerializeField] private Color m_color;
        [SerializeField] [Range(0, 1)] private float m_baseHeight;
        [SerializeField] private Vector2 m_temperatureRange;
        [SerializeField] private Vector2 m_moistureRange;
        #endregion Fields
        
        #region Properties
        public Color Color => m_color;
        public Vector2 TemperatureRange => m_temperatureRange;
        public Vector2 MoistureRange => m_moistureRange;
        #endregion Properties
    }
}