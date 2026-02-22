using Godot;
using GodotIdleForest.Scripts.godotcore;
using System;

public partial class SceneManager : Node
{
    [Export] // 导出PackedScene变量，可以在编辑器中拖拽赋值
    public PackedScene DemoPlayScreen { get; set; } // 下一个关卡场景

}
