using System;
using DefaultNamespace;
using UnityEngine;

namespace Generators
{
    public class ProceduralMeshInstantiator : MonoBehaviour
    {
        #region Inspector Fields
        [SerializeField] [Range(4, 16)] private int m_renderDistance = 4;
        [SerializeField] private ProceduralMesh m_mesh;

        [SerializeField] private Vector3 m_playerPosition;
        [SerializeField] private NoiseData m_noiseData;
        #endregion Inspector Fields


        #region MonoBehaviour Methods
        private void Start()
        {
            for (int x = 0; x < m_renderDistance; x++)
            {
                for (int y = 0; y < m_renderDistance; y++)
                {
                    var instance = Instantiate(m_mesh);
                    instance.GenerateMesh(new Texture2D(1, 1), x, y, m_noiseData);
                }
            }
        }
        #endregion MonoBehaviour Methods


        #region Methods

        #endregion Methods

        
        #region Helper Methods
        private bool CheckShouldRegenerate()
        {
            return false;
        }
        #endregion Helper Methods
    }
}