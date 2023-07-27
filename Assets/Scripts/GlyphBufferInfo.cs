using UnityEngine;
using UnityEngine.VFX;

[VFXType(VFXTypeAttribute.Usage.GraphicsBuffer)]
public struct GlyphBufferInfo
{
    public Vector2 Offset;
    public int GlyphId;
    public float Scale;
}
