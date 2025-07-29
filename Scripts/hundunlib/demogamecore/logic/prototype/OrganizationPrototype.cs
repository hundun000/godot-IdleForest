using hundun.idleshare.gamelib;
using System;
using static Assets.Scripts.DemoGameCore.logic.BaseIdleForestConstruction;

namespace Assets.Scripts.DemoGameCore.logic
{
    public class OrganizationPrototype : AbstractConstructionPrototype
    {

        public OrganizationPrototype(Language language) : base(ConstructionPrototypeId.ORGANIZATION, language, null)
        {

        }

        public override BaseConstruction getInstance(GridPosition position)
        {
            String id = prototypeId + "_" + System.Guid.NewGuid().ToString();
            BaseIdleForestConstruction construction = BaseIdleForestConstructionFactory.typeOrganization(prototypeId, id, position, descriptionPackage);

            return construction;
        }
    }
}
