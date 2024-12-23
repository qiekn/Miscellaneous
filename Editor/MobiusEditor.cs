using UnityEngine;
using UnityEditor;

namespace ck.qiekn.Miscellanies {
    [CustomEditor(typeof(MobiusRing))]
    public class MobiusEditor : Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();

            var obj = target as MobiusRing;

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Generate")) {
                obj.GenerateMobiusStrip();
            }
            GUILayout.EndHorizontal();

            // Color
            // GUILayout.BeginHorizontal();
            // if (GUILayout.Button("Random Color")) { cube.GenerateColor(); }
            // if (GUILayout.Button("Reset Color")) { cube.ResetColor(); }
            // GUILayout.EndHorizontal();
        }
    }
}
