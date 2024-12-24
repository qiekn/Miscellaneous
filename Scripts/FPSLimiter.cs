using UnityEngine;

namespace ck.qiekn.Miscellanies {
    public class FPSLimiter : MonoBehaviour {
        [SerializeField] int fps = 144;

        void Update() {
            Application.targetFrameRate = fps;
        }
    }
}
