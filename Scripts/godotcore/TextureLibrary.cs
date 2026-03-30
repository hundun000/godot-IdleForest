using Assets.Scripts.DemoGameCore.logic;
using Godot;
using Godot.Collections;
using System;
using System.Collections.Generic;


public partial class TextureLibrary : Resource
{
    [Export]
    public Texture2D coinResourceIcon;
    [Export]
    public Texture2D woodResourceIcon;
    [Export]
    public Texture2D carbonResourceIcon;
    [Export]
    public Texture2D forestConstructionIcon;
    [Export]
    public Array<Texture2D> factoryConstructionIcons;

    private System.Collections.Generic.Dictionary<string, Texture2D> resourceIconMap = new();
    private System.Collections.Generic.Dictionary<string, Texture2D> constructionIconMap = new();

    public void Initialize()
    {
        resourceIconMap.Add(ResourceType.COIN, coinResourceIcon);
        resourceIconMap.Add(ResourceType.WOOD, woodResourceIcon);
        resourceIconMap.Add(ResourceType.CARBON, carbonResourceIcon);

        constructionIconMap.Add(ConstructionPrototypeId.SMALL_TREE, forestConstructionIcon);
        constructionIconMap.Add(ConstructionPrototypeId.SMALL_FACTORY, factoryConstructionIcons[0]);
        constructionIconMap.Add(ConstructionPrototypeId.MID_FACTORY, factoryConstructionIcons[1]);
        constructionIconMap.Add(ConstructionPrototypeId.BIG_FACTORY, factoryConstructionIcons[2]);
    }

    internal Texture2D GetConstructionIcon(string prototypeId)
    {
        return constructionIconMap[prototypeId];
    }

    internal Texture2D GetResourceIcon(string type)
    {
        return resourceIconMap[type];
    }
}
