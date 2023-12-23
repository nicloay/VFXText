# vfx-text
VFX Text Particles


Setup
-----

Minimal VFX graph config
------------------------
1. Reference proper shader
2. Link Font texture to VFX graph
3. Font import settings (must provide character set)
4. Capacity set manually. max number of different glyphs on the screen (100k in TextParticleController -> 10k in system)
5. Importance to connect color to the shader parameter

explain UV (BottomLeft, BottomRight, TopLeft)
* font adapter limitation ( GlyphIdByChar = new int[255]; only support asci, for UTF - need to replace array to dictionary)

Pass text to VFX graph with buffer
----------------------------------

Demo Projects
-------------



Font Setup
---------
assets use legacy font from unity, so you can import font in to the project normal way.

hint: If you want to apply style to the glyphs (bewel, etc.. ) You need to get raw texture, to do that 
1. Select your font
2. In Inspector click triple dots
3. Create editable copy
   
   ![image](https://github.com/nicloay/VFXText/assets/1671030/1cb12141-c0ac-4370-8635-64f8c86b76ca)



