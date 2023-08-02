using System;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace VFXText.Demo.NPCDemo
{
    //  Convert position from one camera to another:
    //     var screenPosition = mainCamera.WorldToScreenPoint(transform.position);
    //     var orthographicPosition = overlayCamera.ScreenToWorldPoint(screenPosition);
    //     var localPosition = textParticleController.transform.InverseTransformPoint(orthographicPosition);
    //     
    //  the same but with matrix calculation:
    //     var vector =
    //         (mainCamera.projectionMatrix * mainCamera.worldToCameraMatrix) 
    //         * new Vector4(transform.position.x,transform.position.y, transform.position.z, 1f);
    //     
    //     for (var i = 0; i < 3; i++)
    //     {
    //         vector[i] /= vector.w;
    //     }
    //
    //     vector.w = 1;
    //     
    //     var orthographicMatrix = textParticleController.transform.worldToLocalMatrix 
    //                              * (overlayCamera.projectionMatrix * overlayCamera.worldToCameraMatrix).inverse;
    //     var localPosition =  orthographicMatrix * vector;
    //     */
    public class SpawnDamageBubble : MonoBehaviour
    {
        [SerializeField] private TextParticleController textParticleController;


        private void Update()
        {
            if (Random.Range(0, 300) > 2)
            {
                return;
            }
            var damage = Random.Range(1, 30) * (Mathf.Pow(10, Random.Range(2, 5)));
            textParticleController.SpawnWord(transform.position, damage.ToString(), 0.8f, Pivot.Bottom);
        }

       
        private void Reset()
        {
            textParticleController = FindObjectOfType<TextParticleController>();
            var circlePosition = Random.insideUnitCircle * 15f;
            transform.parent.transform.position = new Vector3(circlePosition.x, 0, circlePosition.y);
        }
    }
}
