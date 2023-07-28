using System;
using Unity.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class FontAdapter : IDisposable
    {
        private NativeArray<FontLookupBuffer> _lookupBuffers;
        public readonly GraphicsBuffer Buffer;
        public readonly int[] GlyphIdByChar;
        
        public FontAdapter(Font font)
        {
            GlyphIdByChar = new int[255];
            _lookupBuffers = new NativeArray<FontLookupBuffer>(font.characterInfo.Length, Allocator.Persistent);
            for (var i = 0; i < font.characterInfo.Length; i++)
            {
                var characterInfo = font.characterInfo[i];
                _lookupBuffers[i] = FontLookupBuffer.FromCharacterInfo(characterInfo);
                GlyphIdByChar[characterInfo.index] = i;
            }

            Buffer = new GraphicsBuffer(GraphicsBuffer.Target.Structured, font.characterInfo.Length, (sizeof(float) * 8));
            Buffer.SetData(_lookupBuffers);
        }

        public int GetGlyphId(char c)
        {
            if (c >= 255)
            {
                Debug.LogError($"character {c} is not supported");
            }

            return GlyphIdByChar[c];
        }
        
        public void Dispose()
        {
            _lookupBuffers.Dispose();
        }
    }
}