using UnityEngine;

namespace Generators
{
    [RequireComponent(typeof(MeshFilter))]
    public abstract class ProceduralMesh : MonoBehaviour
    {
        #region Fields
        protected Vector3[] vertices;
        protected int[] indices;
        #endregion Fields
        
        public abstract void DeleteMesh();
        public abstract void GenerateMesh(Texture2D heightmap);
    }
}