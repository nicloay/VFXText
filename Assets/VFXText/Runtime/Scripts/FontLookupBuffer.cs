using UnityEngine;
using UnityEngine.VFX;

namespace VFXText
{
    [VFXType(VFXTypeAttribute.Usage.GraphicsBuffer)]
    public struct FontLookupBuffer
    {
        public Vector2 ScaleXY;
        public Vector2 BottomLeft;
        public Vector2 BottomRight;
        public Vector2 TopLeft;
        
        public static FontLookupBuffer FromCharacterInfo(CharacterInfo characterInfo)
        {
            return new FontLookupBuffer
            {
                ScaleXY = new Vector2(characterInfo.vert.width, characterInfo.vert.height),
                BottomLeft = characterInfo.uvBottomLeft,
                BottomRight = characterInfo.uvBottomRight,
                TopLeft = characterInfo.uvTopLeft
            };
            
            var uv = characterInfo.uv;
            if (characterInfo.flipped)
                return new FontLookupBuffer
                {
                    ScaleXY = new Vector2(characterInfo.vert.width, characterInfo.vert.height),
                    BottomLeft = new Vector2(uv.x + uv.width, uv.y),
                    BottomRight = new Vector2(0, uv.height),
                    TopLeft = new Vector2(-uv.width, 0)
                };
            return new FontLookupBuffer
            {
                ScaleXY = new Vector2(characterInfo.vert.width, characterInfo.vert.height),
                BottomLeft = new Vector2(uv.x, uv.y + uv.height),
                BottomRight = new Vector2(uv.width, 0),
                TopLeft = new Vector2(0, -uv.height)
            };
        }
    }
}