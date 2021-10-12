using System.Collections.Generic;
using System.Linq;
using Data;
using UnityEngine;

namespace Generators
{
    public class ProceduralMeshInstantiator : MonoBehaviour
    {
        #region Inspector Fields
        [SerializeField] private Material m_chunkMaterial;
        [SerializeField] [Range(2, 16)] private int m_renderDistance = 4;
        [SerializeField] private Transform m_playerPosition;
        [SerializeField] private NoiseData m_noiseData;
        #endregion Inspector Fields

        #region Fields
        private Dictionary<Vector2Int, VoxelChunkGenerator> m_chunks;
        private Vector2Int m_playerCoords;
        #endregion Fields
        
        
        #region MonoBehaviour Methods
        private void Update()
        {
            TryUpdateChunks();
        }

        private void Awake()
        {
            m_chunks = new Dictionary<Vector2Int, VoxelChunkGenerator>();
            m_playerPosition.position = new Vector3(0, m_playerPosition.position.y, 0);
            m_playerCoords = new Vector2Int(0, 0);
            m_noiseData.SetNoise();
            
            for (int y = -m_renderDistance; y < m_renderDistance; y++)
            {
                for (int x = -m_renderDistance; x < m_renderDistance; x++)
                {
                    if (!IsInsideRenderRadius(x, y))
                        continue;
                    
                    SpawnChunk(x, y);
                }
            }
        }
        #endregion MonoBehaviour Methods


        #region Methods
        private void UpdateVisibleChunks()
        {
            foreach (var chunk in m_chunks.ToList())
            {
                if (!IsInsideRenderRadius(chunk.Value.xCoord, chunk.Value.yCoord))
                {
                    m_chunks.Remove(chunk.Key);
                    Destroy(chunk.Value.gameObject);
                }
            }

            int created = 0;
            for (int y = -m_renderDistance; y < m_renderDistance; y++)
            {
                for (int x = -m_renderDistance; x < m_renderDistance; x++)
                {
                    int x_ = x + m_playerCoords.x;
                    int y_ = y + m_playerCoords.y;

                    if (IsInsideRenderRadius(x_, y_))
                        SpawnChunk(x_, y_);
                }
            }
            
            Debug.Log("Created: " + created);
        }
        #endregion Methods

        
        #region Helper Methods
        private void TryUpdateChunks()
        {
            int size = VoxelChunkGenerator.CHUNK_SIZE;
            var pos = m_playerPosition.position;
            int xCoord = (int)pos.x/size;
            int yCoord = (int)pos.z/size;
            
            if (m_playerCoords.x != xCoord || m_playerCoords.y != yCoord)
            {
                m_playerCoords.x = xCoord;
                m_playerCoords.y = yCoord;
                
                UpdateVisibleChunks();
            }
        }

        private bool SpawnChunk(int x, int y)
        {
            if (m_chunks.ContainsKey(new Vector2Int(x, y)))
                return false;
        
            var instance = new GameObject($"Chunk ({x}, {y})");
            instance.transform.parent = transform;
            
            var mesh = instance.AddComponent<VoxelChunkGenerator>();
            mesh.SetMaterial(m_chunkMaterial);
            mesh.GenerateMesh(new Texture2D(1, 1), x, y, m_noiseData);
            
            m_chunks.Add(new Vector2Int(x, y), mesh);
            return true;
        }

        private bool IsInsideRenderRadius(int x, int y)
        {
            var distance = Vector2.Distance(m_playerCoords, new Vector2(x, y));
            return distance < m_renderDistance;
        }

        [ContextMenu("Reset")]
        private void Reset()
        {
            foreach (var chunk in m_chunks.ToList())
            {
                m_chunks.Remove(chunk.Key);
                Destroy(chunk.Value.gameObject);
            }
            
            m_noiseData.SetNoise();

            for (int y = -m_renderDistance; y < m_renderDistance; y++)
            {
                for (int x = -m_renderDistance; x < m_renderDistance; x++)
                {
                    if (!IsInsideRenderRadius(x, y))
                        continue;
                    
                    SpawnChunk(x, y);
                }
            }
        }
        #endregion Helper Methods
    }
}