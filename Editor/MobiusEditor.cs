using UnityEngine;
using UnityEditor;

namespace ck.qiekn.Miscellanies {
    [CustomEditor(typeof(Mobius))]
    public class MobiusEditor : Editor {
        public override void OnInspectorGUI() {
            base.OnInspectorGUI();
            var obj = target as Mobius;

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Generate")) { obj.GenerateMobiusStrip(); }
            if (GUILayout.Button("Random Color")) { obj.GenerateColor(); }
            if (GUILayout.Button("Reset Color")) { obj.ResetColor(); }
            GUILayout.EndHorizontal();
        }
    }
}
