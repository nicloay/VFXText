using System;
using UnityEngine;
using UnityEngine.VFX;

namespace VFXText
{
    public class TextParticleController : MonoBehaviour
    {
        private static readonly int
            TextBufferStride =
                sizeof(float) * 2 + sizeof(int); // 2float - position 1int-GlyphId + 1float-Scale

        private static readonly int TextBufferSize = 100000; // 100k glyphs 

        private static readonly int TextBuffer = Shader.PropertyToID(nameof(TextBuffer));
        private static readonly int TextBufferSizeName = Shader.PropertyToID(nameof(TextBufferSize));

        private static readonly int FontBuffer = Shader.PropertyToID(nameof(FontBuffer));
        private static readonly int ShowText = Shader.PropertyToID(nameof(ShowText));
        private static readonly int Position = Shader.PropertyToID("position");
        private static readonly int Size = Shader.PropertyToID("size");
        private static readonly int TextBufferStartPosition = Shader.PropertyToID(nameof(TextBufferStartPosition));
        [SerializeField] private Font fontAsset;
        [SerializeField] private float DefaultScale = 0.1f;
        private VFXEventAttribute _eventAttribute;
        private FontAdapter _fontAdapter;
        private int _targetBufferPosition;
        private GraphicsBuffer _textBuffer;

        private VisualEffect _vfx;

        private void Awake()
        {
            _vfx = GetComponent<VisualEffect>();
            // init font buffer with all glyphs info (Uv, size, etc..)
            _fontAdapter = new FontAdapter(fontAsset);
            _vfx.SetGraphicsBuffer(FontBuffer, _fontAdapter.Buffer);
            // init words buffer
            _textBuffer = new GraphicsBuffer(GraphicsBuffer.Target.Structured, TextBufferSize, TextBufferStride);
            _vfx.SetGraphicsBuffer(TextBuffer, _textBuffer);
            _vfx.SetInt(TextBufferSizeName, TextBufferSize);

            _eventAttribute = _vfx.CreateVFXEventAttribute();
        }

        private void OnDestroy()
        {
            _fontAdapter?.Dispose();
            _textBuffer?.Dispose();
            _eventAttribute?.Dispose();
        }

        public void SpawnWord(Vector3 screenPosition, string word, Pivot pivot = Pivot.Bottom)
        {
            SpawnWord(screenPosition, word, DefaultScale, pivot);
        }


        public void SpawnWord(Vector3 screenPosition, string word, float scale, Pivot pivot, Action<VFXEventAttribute> customEventAttribute = null)
        {
            var wordLength = word.Length;
            if (_targetBufferPosition + wordLength >= TextBufferSize)
            {
                _targetBufferPosition = 0;
            }

            var array = GetGlyphArray(word, pivot);
            _textBuffer.SetData(array, 0, _targetBufferPosition, wordLength);
            _eventAttribute.SetFloat("spawnCount", wordLength);
            _eventAttribute.SetInt(TextBufferStartPosition, _targetBufferPosition);

            if (customEventAttribute != null)
            {
                customEventAttribute(_eventAttribute);
            }
            else
            {
                _eventAttribute.SetVector3(Position, screenPosition);
                _eventAttribute.SetFloat(Size, scale);
            }
            _vfx.SendEvent(ShowText, _eventAttribute);
            _targetBufferPosition += wordLength;
        }

        private GlyphBufferInfo[] GetGlyphArray(string str, Pivot pivot)
        {
            var result = new GlyphBufferInfo[str.Length];
            var totalWidth = 0f;
            var i = 0;
            CharacterInfo characterInfo;
            var position = Vector2.zero;

            foreach (var c in str)
            {
                if (fontAsset.GetCharacterInfo(c, out characterInfo))
                {
                    result[i] = new GlyphBufferInfo
                    {
                        GlyphId = _fontAdapter.GetGlyphId(c),
                        Offset = position + characterInfo.vert.center
                    };
                    var advance = characterInfo.advance;
                    position.x += advance;
                    totalWidth += advance;
                }
                i++;
            }

            var height = fontAsset.fontSize;

            var (offsetX, offsetY) = pivot.GetOffset(totalWidth, height);
            for (var j = 0; j < result.Length; j++)
            {
                result[j].Offset.x += offsetX;
                result[j].Offset.y += offsetY;
            }

            return result;
        }
    }
}