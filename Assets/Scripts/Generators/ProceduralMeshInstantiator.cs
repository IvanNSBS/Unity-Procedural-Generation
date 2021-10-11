using UnityEngine;
using Data;

namespace Generators
{
    public class ProceduralMeshInstantiator : MonoBehaviour
    {
        #region Inspector Fields
        [SerializeField] [Range(4, 32)] private int m_renderDistance = 4;
        [SerializeField] private Vector3 m_playerPosition;
        [SerializeField] private NoiseData m_noiseData;
        #endregion Inspector Fields

        #region Fields
        #endregion Fields
        
        
        #region MonoBehaviour Methods
        private void Update()
        {
            if (ShouldUpdateChunks())
                UpdateVisibleChunks();
        }

        private void Start()
        {
            for (int x = 0; x < m_renderDistance; x++)
            {
                for (int y = 0; y < m_renderDistance; y++)
                {
                    var instance = new GameObject($"Chunk ({x}, {y})");
                    instance.transform.parent = transform;
                    
                    var mesh = instance.AddComponent<VoxelChunkGenerator>();
                    mesh.GenerateMesh(new Texture2D(1, 1), x, y, m_noiseData);
                }
            }
        }
        #endregion MonoBehaviour Methods


        #region Methods

        #endregion Methods

        
        #region Helper Methods
        private void UpdateVisibleChunks()
        {
            
        }
        
        private bool ShouldUpdateChunks()
        {
            return false;
        }
        #endregion Helper Methods
    }
}