using hundun.idleshare.gamelib;
using System;
using static Assets.Scripts.DemoGameCore.logic.BaseIdleForestConstruction;

namespace Assets.Scripts.DemoGameCore.logic
{
    public class GovernmentPrototype : AbstractConstructionPrototype
    {

        public GovernmentPrototype(Language language) : base(ConstructionPrototypeId.GOVERNMENT, language, null)
        {

        }

        public override BaseConstruction getInstance(GridPosition position)
        {
            String id = prototypeId + "_" + System.Guid.NewGuid().ToString();
            BaseIdleForestConstruction construction = BaseIdleForestConstructionFactory.typeGovernment(prototypeId, id, position, descriptionPackage);

            return construction;
        }
    }
}
