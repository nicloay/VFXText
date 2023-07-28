using UnityEngine;
using UnityEngine.VFX;

namespace DefaultNamespace
{
    [VFXType(VFXTypeAttribute.Usage.GraphicsBuffer)]
    public struct FontLookupBuffer
    {
        public Vector2 Size;
        public Vector2 BottomLeft;
        public Vector2 BottomRight;
        public Vector2 TopLeft;
        

        public static FontLookupBuffer FromCharacterInfo(CharacterInfo characterInfo)
        {
            var uv = characterInfo.uv;
            
            if (characterInfo.flipped)
            {
                return new FontLookupBuffer()
                {
                    Size = new Vector2(characterInfo.vert.width, characterInfo.vert.height),
                    BottomLeft = new Vector2(uv.x + uv.width, uv.y),
                    BottomRight = new Vector2(0 , uv.height),
                    TopLeft = new Vector2( -uv.width, 0)
                };
            }
            else
            {
                return new FontLookupBuffer()
                {
                    Size = new Vector2(characterInfo.vert.width, characterInfo.vert.height),
                    BottomLeft = new Vector2(uv.x, uv.y+uv.height),
                    BottomRight = new Vector2( uv.width, 0),
                    TopLeft = new Vector2(0,  -uv.height)
                };
            }
        }
    }
}