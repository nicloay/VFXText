using UnityEngine;
using UnityEngine.VFX;

namespace VFXText.Demo
{
    [RequireComponent(typeof(VisualEffect))]
    public class LinkOverlayCamera : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private Camera overlayCamera;

        private static readonly int MainCameraVPR0 = Shader.PropertyToID(nameof(MainCameraVPR0));
        private static readonly int MainCameraVPR1 = Shader.PropertyToID(nameof(MainCameraVPR1));
        private static readonly int MainCameraVPR2 = Shader.PropertyToID(nameof(MainCameraVPR2));
        private static readonly int MainCameraVPR3 = Shader.PropertyToID(nameof(MainCameraVPR3));
        
        private static readonly int OverlayCameraVPR0 = Shader.PropertyToID(nameof(OverlayCameraVPR0));
        private static readonly int OverlayCameraVPR1 = Shader.PropertyToID(nameof(OverlayCameraVPR1));
        private static readonly int OverlayCameraVPR2 = Shader.PropertyToID(nameof(OverlayCameraVPR2));
        private static readonly int OverlayCameraVPR3 = Shader.PropertyToID(nameof(OverlayCameraVPR3));
        
        private VisualEffect vfx;

        private void Awake()
        {
            vfx = GetComponent<VisualEffect>();
        }

        void Update()
        {
            var mainMatrix = mainCamera.projectionMatrix * mainCamera.worldToCameraMatrix;
            var overlayMatrix = transform.worldToLocalMatrix *  (overlayCamera.projectionMatrix * overlayCamera.worldToCameraMatrix).inverse;
            vfx.SetVector4(MainCameraVPR0, mainMatrix.GetRow(0));
            vfx.SetVector4(MainCameraVPR1, mainMatrix.GetRow(1));
            vfx.SetVector4(MainCameraVPR2, mainMatrix.GetRow(2));
            vfx.SetVector4(MainCameraVPR3, mainMatrix.GetRow(3));
            
            vfx.SetVector4(OverlayCameraVPR0, overlayMatrix.GetRow(0));
            vfx.SetVector4(OverlayCameraVPR1, overlayMatrix.GetRow(1));
            vfx.SetVector4(OverlayCameraVPR2, overlayMatrix.GetRow(2));
            vfx.SetVector4(OverlayCameraVPR3, overlayMatrix.GetRow(3));
        }
    }
}
