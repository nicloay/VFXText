using UnityEditor;
using UnityEngine;

namespace VFXText.Demo
{
    [RequireComponent(typeof(TextParticleController))]
    public class DamageBubbleOnMouseDownSpawner : MonoBehaviour
    {
        [SerializeField] private Pivot pivot = Pivot.Bottom;
        [SerializeField] private float particleNumber = 5;
        [SerializeField] private float textScale = 0.5f;
        
        private Camera cam; 
        private TextParticleController _particleController;

        private void Awake()
        {
            _particleController = GetComponent<TextParticleController>();
            cam = FindObjectOfType<Camera>();
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                if (particleNumber >= 1f)
                {
                    for (var i = 0; i < (int)particleNumber; i++)
                    {
                        SpawnParticle();
                    }
                }
                else
                {
                    if (Random.value < particleNumber)
                    {
                        SpawnParticle();
                    }
                }
            }
        }

        private void SpawnParticle()
        {
            var damage = Time.frameCount % 60 * 1000;
            var worldPosition = cam.ScreenToWorldPoint(Input.mousePosition);
            var localOffset = transform.InverseTransformPoint(worldPosition);
            _particleController.SpawnWord(localOffset, damage.ToString(), textScale, pivot);
        }
    }
}