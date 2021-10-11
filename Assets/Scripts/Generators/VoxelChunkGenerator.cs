using System;
using UnityEngine;
using System.Collections.Generic;
using Data;
using DefaultNamespace;

namespace Generators
{
    public class VoxelChunkGenerator : ProceduralMesh
    {
        #region Constants
        private const int CHUNK_WIDTH = 64;
        private const int CHUNK_HEIGHT = 256;
        #endregion Constantss
     
        #region Inspector Fields
        [SerializeField] private NoiseData m_noiseData;
        #endregion Inspector Fields
        
        public override void DeleteMesh()
        {
        }

        public override void GenerateMesh(Texture2D heightmap)
        {
            List<Vector3> allVerts = new List<Vector3>();
            List<int> allIdxs = new List<int>();
            
            m_noiseData.SetNoise();
            
            for (int x = 0; x < CHUNK_WIDTH; x++)
            {
                for (int y = 0; y < CHUNK_WIDTH; y++)
                {
                    int idx = y * CHUNK_WIDTH + x;
                    float normalizedNoise = (m_noiseData.noise.GetSimplexFractal(x, y) + 1) / 2f;
                    int height = (int)Math.Floor(normalizedNoise * CHUNK_HEIGHT);
                    if(x == 8 && y == 8)
                        Debug.Log("height: " + normalizedNoise);
                    
                    var tuple = GenerateVoxel(8*idx, x, height, y);
                    
                    allVerts.AddRange(tuple.Item1);
                    allIdxs.AddRange(tuple.Item2);
                }
            }

            var mesh = GetComponent<MeshFilter>().mesh;
            mesh.Clear();
            
            // // TODO: Use more efficient method here
            mesh.vertices = vertices = allVerts.ToArray();
            mesh.triangles = indices = allIdxs.ToArray();
            Debug.Log($"Vertices Size: {vertices.Length}");
            mesh.Optimize();
        }

        [ContextMenu("Generate Mesh")]
        private void GenMesh() => GenerateMesh(new Texture2D(16, 16));
         
        private void Start()
        {
            GenMesh();
        }


        #region Helper Methods
        private Tuple<Vector3[], int[]> GenerateVoxel(int idx, int posX, int posY, int posZ)
        {
            Vector3[] verts = {
                new Vector3 (0 + posX, 0 + posY, 0 + posZ),
                new Vector3 (0 + posX, 0 + posY, 1 + posZ),
                new Vector3 (1 + posX, 0 + posY, 1 + posZ),
                new Vector3 (1 + posX, 0 + posY, 0 + posZ),
                
                new Vector3 (0 + posX, 1 + posY, 0 + posZ),
                new Vector3 (0 + posX, 1 + posY, 1 + posZ),
                new Vector3 (1 + posX, 1 + posY, 1 + posZ),
                new Vector3 (1 + posX, 1 + posY, 0 + posZ),
            };
            
            // Vector3[] normals = {
            //     new Vector3 (0, 1, 0), // up
            //     new Vector3 (0, -1,0), // bottom
            //     new Vector3 (-1, 0, 0), // left
            //     new Vector3 (1, 0, 0), // right
            //     
            //     new Vector3 (0, -1, 0), // front
            //     new Vector3 (0, 1, 0) // back
            // };

            int[] idxs = {
                idx + 2, idx + 1, idx + 0, // bottom face
                idx + 0, idx + 3, idx + 2,
                
                idx + 4, idx + 5, idx + 6, // top face
                idx + 6, idx + 7, idx + 4,
                
                idx + 4, idx + 0, idx + 1, // side left face
                idx + 1, idx + 5, idx + 4,
                
                idx + 2, idx + 3, idx + 7, // side right face
                idx + 7, idx + 6, idx + 2,
                
                idx + 0, idx + 4, idx + 7, // front face
                idx + 7, idx + 3, idx + 0,
                
                idx + 6, idx + 5, idx + 1, // back face
                idx + 1, idx + 2, idx + 6
            };
 
            // Vector2[] texCoords =
            // {
            //     new Vector2(0, 0),
            //     new Vector2(1, 0),
            //     new Vector2(1, 1),
            //     new Vector2(0, 1)
            // };
            //
            // int[] texIdxs = { 0, 1, 3, 3, 1, 2 };
            return new Tuple<Vector3[], int[]>(verts, idxs);
        }
        #endregion Helper Methods
    }
}