using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

namespace Managers
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

        private void Update()
        {
            MoveMap();
            UpdateTextures();
        }

        private void MoveMap()
        {
            Vector2Int offset = new Vector2Int(0, 0);
            if (Input.GetKey(KeyCode.W))
                offset += new Vector2Int(0, 1);
            if (Input.GetKey(KeyCode.A))
                offset += new Vector2Int(-1, 0);
            if (Input.GetKey(KeyCode.S))
                offset += new Vector2Int(0, -1);
            if (Input.GetKey(KeyCode.D))
                offset += new Vector2Int(1, 0);
            m_position += offset;
        }

        private void UpdateTextures()
        {
            for (int x = 0; x < m_imageSize.x; x++)
            {
                for (int y = 0; y < m_imageSize.y; y++)
                {
                    float p = 0;
                    foreach (var noiseData in m_noiseData)
                    {
                        noiseData.SetNoise();
                        p += noiseData.noise.GetSimplexFractal(x + m_position.x, y + m_position.y);
                    }

                    var baseCol = new Color(0.05f, 0.6f, 0.1f) * (p+1);
                    m_texture.SetPixel(x, y, baseCol);
                }
            }
            
            m_texture.Apply();
        }
        #endregion Methods
    }
}