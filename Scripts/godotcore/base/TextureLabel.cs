using Godot;
using System;

public partial class TextureLabel : Control
{
    // ====== 内部引用 ======
    [Export]
    private NinePatchRect _backgroundTexture;
    [Export]
    private Label _textLabel;

}
