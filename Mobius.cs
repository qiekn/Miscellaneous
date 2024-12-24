using UnityEngine;

namespace ck.qiekn.Miscellanies {
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class Mobius : MonoBehaviour {
        [Range(3, 100)] public int uSegments = 50; // Around of Ring Segments
        [Range(2, 50)] public int vSegments = 10;  // Width Segemnts
        public float radius = 1.0f;                // 半径 
        public float width = 0.2f;                 // 宽度 
        public float thickness = 0.05f;            // 厚度

        // Colors for front and back faces
        [SerializeField] Color frontColor = Color.red;
        [SerializeField] Color backColor = Color.blue;

        void Start() {
            GenerateMobiusStrip();
        }

        public void GenerateMobiusStrip() {
            Mesh mesh = new Mesh();

            int vertexCount = (uSegments + 1) * (vSegments + 1) * 2; // Double vertex count (for double-sided rendering)
            Vector3[] vertices = new Vector3[vertexCount];
            int[] triangles = new int[uSegments * vSegments * 6 * 2]; // Double triangle count (similarly)
            Color[] colors = new Color[vertexCount];

            // Generate vertices and colors
            for (int i = 0; i <= uSegments; i++) {
                float u = i * Mathf.PI * 2 / uSegments; // Main parameter range [0, 2π]
                for (int j = 0; j <= vSegments; j++) {
                    float v = (j / (float)vSegments - 0.5f) * width; // In width direction
                    float x = Mathf.Cos(u) * (radius + v * Mathf.Cos(u / 2));
                    float y = Mathf.Sin(u) * (radius + v * Mathf.Cos(u / 2));
                    float z = v * Mathf.Sin(u / 2);

                    int frontIndex = i * (vSegments + 1) + j;
                    int backIndex = frontIndex + (uSegments + 1) * (vSegments + 1);

                    // Front face vertex
                    vertices[frontIndex] = new Vector3(x, y, z);
                    colors[frontIndex] = frontColor;

                    // Back face vertex (same position but for the inverted triangle)
                    vertices[backIndex] = new Vector3(x, y, z);
                    colors[backIndex] = backColor;
                }
            }

            // Generate triangles
            int t = 0;
            for (int i = 0; i < uSegments; i++) {
                for (int j = 0; j < vSegments; j++) {
                    int current = i * (vSegments + 1) + j;
                    int next = (i + 1) * (vSegments + 1) + j;

                    // Front face triangles
                    triangles[t++] = current;
                    triangles[t++] = next;
                    triangles[t++] = current + 1;

                    triangles[t++] = next;
                    triangles[t++] = next + 1;
                    triangles[t++] = current + 1;

                    // Back face triangles (winding order reversed)
                    int currentBack = current + (uSegments + 1) * (vSegments + 1);
                    int nextBack = next + (uSegments + 1) * (vSegments + 1);

                    triangles[t++] = currentBack;
                    triangles[t++] = currentBack + 1;
                    triangles[t++] = nextBack;

                    triangles[t++] = nextBack;
                    triangles[t++] = currentBack + 1;
                    triangles[t++] = nextBack + 1;
                }
            }

            // Assign to mesh
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.colors = colors;
            mesh.RecalculateNormals();

            // Assign mesh to MeshFilter
            GetComponent<MeshFilter>().mesh = mesh;
        }

        public void GenerateColor() {
            frontColor = Random.ColorHSV();
            backColor = Random.ColorHSV();
        }

        public void ResetColor() {
            frontColor = Color.red;
            backColor = Color.blue;
        }
    }
}