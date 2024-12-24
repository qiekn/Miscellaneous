using UnityEngine;

namespace ck.qiekn.Miscellanies {
    public class CursorLock : MonoBehaviour {
        void Update() {
            if (Input.GetKeyDown(KeyCode.Escape)) {
                Cursor.lockState = CursorLockMode.None;
                Time.timeScale = 0f;
            } else if (Input.GetMouseButtonDown(0)) {
                Cursor.lockState = CursorLockMode.Locked;
                Time.timeScale = 1f;
            }
        }
    }
}
