using UnityEngine;
using UnityEngine.VFX;

namespace VFXText
{
    [VFXType(VFXTypeAttribute.Usage.GraphicsBuffer)]
    public struct GlyphBufferInfo
    {
        public Vector2 Offset;
        public int GlyphId;
    }
}