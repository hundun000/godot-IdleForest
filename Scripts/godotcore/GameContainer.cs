using Godot;
using GodotIdleForest.Scripts;
using System;

public partial class GameContainer : Node
{
	public static DemoIdleGame Instance
	{
		get; private set;
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		Instance = new DemoIdleGame();
		Instance.create();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
