using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ck.qiekn.Miscellanies {
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class MobiusRing : MonoBehaviour {
        [Range(3, 100)] public int uSegments = 50; // 环的分段数
        [Range(2, 50)] public int vSegments = 10;  // 宽度分段数
        public float radius = 1.0f;                // 半径
        public float width = 0.2f;                 // 宽度

        public void GenerateMobiusStrip() {
            Mesh mesh = new Mesh();
            Vector3[] vertices = new Vector3[(uSegments + 1) * (vSegments + 1)];
            int[] triangles = new int[uSegments * vSegments * 6];

            // Generate vertices
            for (int i = 0; i <= uSegments; i++) {
                float u = i * Mathf.PI * 2 / uSegments; // 主参数范围 [0, 2π]
                for (int j = 0; j <= vSegments; j++) {
                    float v = (j / (float)vSegments - 0.5f) * width; // 宽度方向
                    float x = Mathf.Cos(u) * (radius + v * Mathf.Cos(u / 2));
                    float y = Mathf.Sin(u) * (radius + v * Mathf.Cos(u / 2));
                    float z = v * Mathf.Sin(u / 2);
                    vertices[i * (vSegments + 1) + j] = new Vector3(x, y, z);
                }
            }

            // Generate triangles
            int t = 0;
            for (int i = 0; i < uSegments; i++) {
                for (int j = 0; j < vSegments; j++) {
                    int current = i * (vSegments + 1) + j;
                    int next = (i + 1) * (vSegments + 1) + j;

                    // First triangle
                    triangles[t++] = current;
                    triangles[t++] = next;
                    triangles[t++] = current + 1;

                    // Second triangle
                    triangles[t++] = next;
                    triangles[t++] = next + 1;
                    triangles[t++] = current + 1;
                }
            }

            // Assign to mesh
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();

            // Assign mesh to MeshFilter
            GetComponent<MeshFilter>().mesh = mesh;
        }
    }
}