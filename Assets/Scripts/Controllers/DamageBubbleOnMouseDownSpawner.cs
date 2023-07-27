using System;
using UnityEngine;

namespace DefaultNamespace.Controllers
{
    [RequireComponent(typeof(TextParticleController))]
    public class DamageBubbleOnMouseDownSpawner : MonoBehaviour
    {
        private TextParticleController _particleController;

        private void Awake()
        {
            _particleController = GetComponent<TextParticleController>();
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                var damage = Time.frameCount % 60 * 1000;
                _particleController.SpawnWord(Input.mousePosition, damage.ToString());
            }
        }
    }
}