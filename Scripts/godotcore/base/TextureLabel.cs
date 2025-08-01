using Godot;
using System;

public partial class TextureLabel : Control
{
    // ====== 内部引用 ======
    [Export]
    public NinePatchRect PackgroundTexture { get; private set; }
    [Export]
    public Label TextLabel { get; private set; }

}
