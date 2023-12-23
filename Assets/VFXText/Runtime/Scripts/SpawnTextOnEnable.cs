using UnityEngine;

namespace VFXText
{
    [RequireComponent(typeof(TextParticleController))]
    public class SpawnTextOnEnable : MonoBehaviour
    {
        [SerializeField] private string text;

        [ContextMenu("ShowText")]
        private void Start()
        {
            GetComponent<TextParticleController>().SpawnWord(Vector3.zero, text);
        }
    }
}
