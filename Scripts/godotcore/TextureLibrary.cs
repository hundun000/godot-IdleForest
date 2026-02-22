using Assets.Scripts.DemoGameCore.logic;
using Godot;
using System.Collections.Generic;


public partial class TextureLibrary : Resource
{
    [Export]
    public Texture2D coinResourceIcon;
    [Export]
    public Texture2D woodResourceIcon;

    private Dictionary<string, Texture2D> resourceIconMap = new();

    public void Initialize()
    {
        resourceIconMap.Add(ResourceType.COIN, coinResourceIcon);
        resourceIconMap.Add(ResourceType.WOOD, woodResourceIcon);
    }


    internal Texture2D GetResourceIcon(string type)
    {
        return resourceIconMap[type];
    }
}
