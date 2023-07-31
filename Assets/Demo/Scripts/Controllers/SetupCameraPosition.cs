using UnityEngine;

namespace VFXText.Demo
{
    [RequireComponent(typeof(Camera))]
    public class SetupCameraPosition : MonoBehaviour
    {
        private void Awake()
        {
            var y = Screen.height / 2;
            var x = Screen.width / (float)Screen.height * y;
            transform.position = new Vector3(x, y, -50);
            GetComponent<Camera>().orthographicSize = y;
        }
    }
}