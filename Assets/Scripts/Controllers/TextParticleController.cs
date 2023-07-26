using System.Threading.Tasks;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.VFX;

public class TextParticleController : MonoBehaviour
{
    [SerializeField] private Font fontAsset;
    
    private static readonly int TextBufferStride = (sizeof(float) * 2 + sizeof(int)); // 2float - position 1int-GlyphId
    private static readonly int TextBufferSize = 100000; // 100k glyphs 
    
    private static readonly int TextBuffer = Shader.PropertyToID(nameof(TextBuffer));
    private static readonly int TextBufferSizeName = Shader.PropertyToID(nameof(TextBufferSize));
    
    private static readonly int FontBuffer = Shader.PropertyToID(nameof(FontBuffer));
    private static readonly int ShowText = Shader.PropertyToID(nameof(ShowText));
    private static readonly int TextBufferStartPosition = Shader.PropertyToID(nameof(TextBufferStartPosition));
    
    private VisualEffect _vfx;
    private FontAdapter _fontAdapter;
    private GraphicsBuffer _textBuffer;
    private VFXEventAttribute _eventAttribute;
    private int _targetBufferPosition;
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
        
        
        _vfx.Play();
        
    }

    private void OnDestroy()
    {
        _fontAdapter?.Dispose();
        _textBuffer?.Dispose();
    }

    public void SpawnWord(Vector2 screenPosition, string word)
    {
        //word = "Hello World!";
        var wordLength = word.Length;

        var array = GetGlyphArray(screenPosition, word); // fix this (get real word from input parameter!
        if ((_targetBufferPosition + wordLength) >= TextBufferSize)
        {
            _targetBufferPosition = 0;
        }
        
        _textBuffer.SetData(array, 0, _targetBufferPosition, wordLength);
        
        var targetPosition = _targetBufferPosition; // TODO:  check if we still fit target array before setting the data
        _targetBufferPosition += wordLength;
        _eventAttribute = _vfx.CreateVFXEventAttribute();
        _eventAttribute.SetInt(TextBufferStartPosition,  wordLength * TextBufferSize + targetPosition);
        _vfx.SendEvent(ShowText, _eventAttribute);
    }

    private GlyphBufferInfo[] GetGlyphArray(Vector2 centerPosition, string str)
    {
        var result = new GlyphBufferInfo[str.Length];
        var totalWidth = 0f;
        var i = 0;
        CharacterInfo characterInfo;
        var position  = Vector2.zero;
        foreach (var c in str)
        {
            if (fontAsset.GetCharacterInfo(c, out characterInfo))
            {
                result[i] = new GlyphBufferInfo()
                {
                    GlyphId = _fontAdapter.GetGlyphId(c),
                    Offset = centerPosition + position +  characterInfo.vert.center
                };
                position.x += characterInfo.advance;
            }
            i++;
        }

        return result;
    }
}
