using UnityEngine;

namespace VFXText.Demo
{
    public class TriggerComponentOnButtonPress : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour target;


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space)) target.enabled = !target.enabled;
        }
    }
}