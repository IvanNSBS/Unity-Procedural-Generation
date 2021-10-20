using Data;
using Generators;
using UnityEngine;
using UnityEngine.UI;

namespace Utils
{
    public class TextureDisplay : MonoBehaviour
    {
        #region Inspector Fields
        [SerializeField] private RawImage m_imageRef;
        [SerializeField] private Vector2Int m_imageSize;
        [Header("Noise Data")] 
        [SerializeField] private HeightmapGenerator m_generator;
        [SerializeField] [Range(0, 1)] private float m_waterLevel;
        #endregion Inspector Fields

        #region Fields
        private Texture2D m_texture;
        #endregion Fields
        
        
        #region Methods
        private void Awake()
        {
            m_texture = new Texture2D(m_imageSize.x, m_imageSize.y);
            m_texture.filterMode = FilterMode.Point;
            m_imageRef.texture = m_texture;
            
            UpdateTextures();
        }
        
        private void OnValidate()
        {
            if (!m_texture)
                return;
            UpdateTextures();
        }

        [ContextMenu("Generate Texture")]
        private void UpdateTextures()
        {
            for (int x = 0; x < m_imageSize.x; x++)
            {
                for (int y = 0; y < m_imageSize.y; y++)
                {
                    float p = m_generator.GetHeightAt(x, y);

                    if (p > 1)
                        p = 1;
                    
                    var baseCol = p < m_waterLevel ?  new Color(0.05f, p, 0.02f) : new Color(0.05f, 0.02f, p);
                    m_texture.SetPixel(x, y, baseCol);
                }
            }
            
            m_texture.Apply();
        }
        #endregion Methods
    }
}