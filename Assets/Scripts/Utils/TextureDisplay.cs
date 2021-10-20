using Data;
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
        [SerializeField] private NoiseData[] m_noiseData;
        #endregion Inspector Fields

        #region Fields
        private Vector2Int m_position;
        private Texture2D m_texture;
        #endregion Fields
        
        
        #region Methods
        private void Awake()
        {
            m_texture = new Texture2D(m_imageSize.x, m_imageSize.y);
            m_texture.filterMode = FilterMode.Point;
            m_position = new Vector2Int(0, 0);
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
                    float p = 0;

                    var noiseData = x > m_imageSize.x/2 ? m_noiseData[0] : m_noiseData[1];
                    // foreach (var noiseData in m_noiseData)
                    // {
                        noiseData.SetNoise();
                        float noise = (noiseData.noise.GetSimplexFractal(x + m_position.x, y + m_position.y) + 1) / 2f;
                        noise *= noiseData.maxHeight;
                        p += noise;
                    // }
                    

                    if (p > 1)
                        p = 1;
                    
                    var baseCol = new Color(0.05f, p*2, 0.02f);
                    if (p < 0.15f)
                        baseCol = new Color(0.05f, 0.02f, p*2);
                    if (p > 0.88f)
                        baseCol = new Color(0.8f*p, 0.8f*p, 0.8f*p*2);
                    m_texture.SetPixel(x, y, baseCol);
                }
            }
            
            m_texture.Apply();
        }
        #endregion Methods
    }
}