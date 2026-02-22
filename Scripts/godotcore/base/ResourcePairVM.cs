using Godot;
using System;

public partial class ResourcePairVM : Control
{
    [Export]
    public TextureRect Icon { get; private set; }
    [Export]
    public Label Value { get; private set; }
}
