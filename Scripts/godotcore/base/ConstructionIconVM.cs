using Godot;
using hundun.idleshare.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using static Godot.VisualShaderNode;

namespace GodotIdleForest.Scripts.godotcore
{
    public partial class ConstructionIconVM : Control
    {
        [Export]
        public Label nameLabel;
        [Export]
        public TextureRect imageBox;

        internal void AfterSetModel(DemoIdleGame game, AbstractConstructionPrototype prototype)
        {
            nameLabel.Text = game.idleGameplayExport.gameplayContext.gameDictionary.constructionPrototypeIdToShowName(prototype.language, prototype.prototypeId);
            imageBox.Texture = GameContainer.Instance.TextureLib.GetConstructionIcon(prototype.prototypeId); 
        }
    }
}
