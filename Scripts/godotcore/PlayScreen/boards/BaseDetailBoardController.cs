using Godot;
using hundun.idleshare.gamelib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GodotIdleForest.Scripts.godotcore.PlayScreen.boards
{
    public abstract partial class BaseDetailBoardController : Control
    {
        public BaseConstruction model { protected set; get; }
        public DemoPlayScreen parent { protected set; get; }
        protected BoardManager boardManager;

        public override void _EnterTree()
        {
            parent = GodotUtils.FindParentOfType<DemoPlayScreen>(this);
            boardManager = GodotUtils.FindParentOfType<BoardManager>(this);
        }

        public void setModel(BaseConstruction constructionExportData)
        {
            this.model = constructionExportData;
            AfterSetModel();
            BoardUpdate();
        }

        public abstract void BoardUpdate();

        public virtual void AfterSetModel() { }
    }
}
