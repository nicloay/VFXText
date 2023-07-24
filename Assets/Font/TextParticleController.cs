using DefaultNamespace;
using UnityEngine;
using UnityEngine.VFX;

public class TextParticleController : MonoBehaviour
{
    [SerializeField] private Font fontAsset;
    
    private VisualEffect _vfx;

    private FontAdapter _fontAdapter;
    
    private static readonly int TextBuffer = Shader.PropertyToID(nameof(TextBuffer));
    private static readonly int FontBuffer = Shader.PropertyToID(nameof(FontBuffer));
    
    private void Awake()
    {
        _vfx = GetComponent<VisualEffect>();
        _fontAdapter = new FontAdapter(fontAsset);
    }

    private void OnDestroy()
    {
        _fontAdapter?.Dispose();
    }

    private void Update()
    {
        if (Time.frameCount % 100 != 0)
        {
            return;
        }

        var phrase = "Hello World!";
        var count = phrase.Length;
        var buff = new GraphicsBuffer(GraphicsBuffer.Target.Structured, count, (sizeof(float) * 2 + sizeof(int)));
        
        buff.SetData(GetGlyphArray(phrase));
        _vfx.Reinit();
        _vfx.SetGraphicsBuffer(TextBuffer, buff);
        _vfx.SetGraphicsBuffer(FontBuffer, _fontAdapter.Buffer);
        _vfx.enabled = true;
    }

    private GlyphBufferInfo[] GetGlyphArray(string str)
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
                    Offset = position + new Vector2( characterInfo.bearing,  0)
                };
                position.x += characterInfo.advance;
            }
            i++;
        }

        return result;
    }
}
