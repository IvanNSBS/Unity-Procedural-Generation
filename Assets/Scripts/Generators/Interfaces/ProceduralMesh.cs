using UnityEngine;

namespace Generators.Interfaces
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public abstract class ProceduralMesh : MonoBehaviour
    {
        #region Fields
        protected Vector3[] vertices;
        protected int[] indices;
        #endregion Fields
        
        public abstract void DeleteMesh();
        public abstract void GenerateMesh(HeightmapGenerator generator, int chunkX, int chunkY);
    }
}