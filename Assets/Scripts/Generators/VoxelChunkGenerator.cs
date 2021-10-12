using Data;
using System;
using UnityEngine;

namespace Generators
{
    public class VoxelChunkGenerator : ProceduralMesh
    {
        #region Constants
        public const int CHUNK_SIZE = 16;
        public const int CHUNK_HEIGHT = 256;
        #endregion Constantss
        
        #region Fields
        public int xCoord;
        public int yCoord;
        #endregion Fields
     
        public override void DeleteMesh()
        {
        }

        public override void GenerateMesh(Texture2D heightmap, int chunkX, int chunkY, NoiseData data)
        {
            xCoord = chunkX;
            yCoord = chunkY;
            vertices = new Vector3[8*CHUNK_SIZE*CHUNK_SIZE];
            indices = new int[36*CHUNK_SIZE*CHUNK_SIZE];
            
            for (int x = 0; x < CHUNK_SIZE; x++)
            {
                for (int y = 0; y < CHUNK_SIZE; y++)
                {
                    int index = y * CHUNK_SIZE + x;
                    int verticesStride = index * 8;
                    int indicesStride = index * 36;
                    int actualX = x + chunkX * CHUNK_SIZE;
                    int actualY = y + chunkY * CHUNK_SIZE;
                    
                    float normalizedNoise = (data.noise.GetSimplexFractal(actualX, actualY) + 1) / 2f;
                    int height = (int)Math.Floor(normalizedNoise * CHUNK_HEIGHT);
                    
                    GenerateVoxel(verticesStride, indicesStride, x, height, y);
                }
            }

            var mesh = GetComponent<MeshFilter>().mesh;
            mesh.Clear();
            
            mesh.vertices = vertices;
            mesh.triangles = indices;
            mesh.Optimize();
            mesh.RecalculateNormals();

            transform.position = new Vector3(chunkX * CHUNK_SIZE, 0, chunkY * CHUNK_SIZE);
        }

        #region Helper Methods
        private void GenerateVoxel(int verticesStride, int indicesStride, int posX, int posY, int posZ)
        {
            vertices[verticesStride]   = new Vector3 (0 + posX, 0 + posY, 0 + posZ);
            vertices[verticesStride+1] = new Vector3 (0 + posX, 0 + posY, 1 + posZ);
            vertices[verticesStride+2] = new Vector3 (1 + posX, 0 + posY, 1 + posZ);
            vertices[verticesStride+3] = new Vector3 (1 + posX, 0 + posY, 0 + posZ);
                            
            vertices[verticesStride+4] = new Vector3 (0 + posX, 1 + posY, 0 + posZ);
            vertices[verticesStride+5] = new Vector3 (0 + posX, 1 + posY, 1 + posZ);
            vertices[verticesStride+6] = new Vector3 (1 + posX, 1 + posY, 1 + posZ);
            vertices[verticesStride+7] = new Vector3 (1 + posX, 1 + posY, 0 + posZ);
            
            int[] idxs = {
                verticesStride + 2, verticesStride + 1, verticesStride + 0, // bottom face
                verticesStride + 0, verticesStride + 3, verticesStride + 2,
                
                verticesStride + 4, verticesStride + 5, verticesStride + 6, // top face
                verticesStride + 6, verticesStride + 7, verticesStride + 4,
                
                verticesStride + 4, verticesStride + 0, verticesStride + 1, // side left face
                verticesStride + 1, verticesStride + 5, verticesStride + 4,
                
                verticesStride + 2, verticesStride + 3, verticesStride + 7, // side right face
                verticesStride + 7, verticesStride + 6, verticesStride + 2,
                
                verticesStride + 0, verticesStride + 4, verticesStride + 7, // front face
                verticesStride + 7, verticesStride + 3, verticesStride + 0,
                
                verticesStride + 6, verticesStride + 5, verticesStride + 1, // back face
                verticesStride + 1, verticesStride + 2, verticesStride + 6
            };

            for (int i = 0; i < idxs.Length; i++)
                indices[indicesStride + i] = idxs[i];
        }

        public void SetMaterial(Material mat) => GetComponent<MeshRenderer>().material = mat;
        #endregion Helper Methods
    }
}