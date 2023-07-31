using System.Linq;
using UnityEngine;

namespace VFXText.Demo.NPCDemo
{
    public class SpawnDamageBubble : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Camera overlayCamera;
        [SerializeField] private TextParticleController textParticleController;


        private void Update()
        {
            if (Random.Range(0, 300) > 2)
            {
                return;
            }

            var damage = Random.Range(1, 30) * (Mathf.Pow(10, Random.Range(2, 5)));
            
            var screenPosition = mainCamera.WorldToScreenPoint(transform.position);
            var orthographicPosition = overlayCamera.ScreenToWorldPoint(screenPosition);
            var localPosition = textParticleController.transform.InverseTransformPoint(orthographicPosition);
            
            textParticleController.SpawnWord(localPosition, damage.ToString(), 0.8f, Pivot.Bottom);
        }

        private void Reset()
        {
            mainCamera = FindObjectsByType<Camera>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).FirstOrDefault(cam => cam.name == "MainCamera");
            overlayCamera = FindObjectsByType<Camera>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).FirstOrDefault(cam => cam.name == "VFXTextOverlayCamera");
            textParticleController = FindObjectOfType<TextParticleController>();
            var circlePosition = Random.insideUnitCircle * 15f;
            transform.parent.transform.position = new Vector3(circlePosition.x, 0, circlePosition.y);
        }
    }
}
