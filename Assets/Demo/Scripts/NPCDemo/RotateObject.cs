using UnityEngine;

namespace VFXText.Demo.NPCDemo
{
    public class RotateObject : MonoBehaviour
    {
        [SerializeField] private float speed;
        private void Update()
        {
            transform.RotateAround(Vector3.zero, Vector3.up, Time.deltaTime * speed);
        }

        public void SetSpeed(float newSpeed)
        {
            speed = newSpeed;
        }
    }
}
