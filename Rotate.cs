using UnityEngine;

namespace ck.qiekn.Miscellanies
{
    public class Rotate : MonoBehaviour
    {
        [SerializeField] float speed = 15f;
        [SerializeField] Vector3 multiplier = new Vector3(1f, 2f, 3f);

        private void Update()
        {
            transform.Rotate(multiplier * speed * Time.deltaTime);
        }
    }
}
