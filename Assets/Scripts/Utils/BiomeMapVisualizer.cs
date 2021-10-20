using System;
using Data;
using DataScriptable;
using Generators.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
    public class BiomeMapVisualizer : MonoBehaviour
    {
        #region Inspector Fields
        [SerializeField] private BiomeMap m_biomeMap;
        [SerializeField] private RawImage m_img;
        #endregion Inspector Fields

        #region Fields
        private Texture2D m_texture;
        #endregion Fields
        
        
        #region MonoBehaviour Methods
        private void Awake() => UpdateDiagram();
        private void OnValidate() => UpdateDiagram();
        #endregion MonoBehaviour Methods
        
        
        #region Methods

        private void UpdateDiagram()
        {
            if (!m_img)
                return;

            int size = 128;
            m_texture = new Texture2D(size, size);
            m_texture.filterMode = FilterMode.Point;
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    float nTemperature = x /(float)size;
                    float nMoisture = y /(float)size;
                    
                    var biome = BiomeSelector.Evaluate(m_biomeMap, nTemperature, nMoisture);
                    if(biome != null)
                        m_texture.SetPixel(x, y, biome.Color);
                    else
                        m_texture.SetPixel(x, y, Color.black);
                }
            }
            
            m_texture.Apply();
            m_img.texture = m_texture;
        }
        #endregion Methods
    }
}