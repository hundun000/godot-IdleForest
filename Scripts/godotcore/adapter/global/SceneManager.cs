using Godot;
using GodotIdleForest.Scripts.godotcore;
using System;

public partial class SceneManager : Node
{
    [Export] // 导出PackedScene变量，可以在编辑器中拖拽赋值
    public PackedScene DemoPlayScreen { get; set; } // 下一个关卡场景

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
