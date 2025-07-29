using hundun.idleshare.gamelib;
using System;
using static Assets.Scripts.DemoGameCore.logic.BaseIdleForestConstruction;

namespace Assets.Scripts.DemoGameCore.logic
{
    public class DesertPrototype : AbstractConstructionPrototype
    {
        private static DescriptionPackage descriptionPackageEN = new DescriptionPackageBuilder()
            .proficiency((proficiency, reachMaxProficiency) =>
            {
                return "reclamation: " + proficiency;
            })
            .build();
        private static DescriptionPackage descriptionPackageCN = new DescriptionPackageBuilder()
            .proficiency((proficiency, reachMaxProficiency) =>
            {
                return "土壤化进度" + proficiency;
            })
            .build();


        public DesertPrototype(Language language) : base(ConstructionPrototypeId.DESERT, language, null)
        {
            // override descriptionPackage
            switch (language)
            {
                case Language.CN:
                    this.descriptionPackage = DesertPrototype.descriptionPackageCN;
                    break;
                default:
                    this.descriptionPackage = DesertPrototype.descriptionPackageEN;
                    break;
            }
        }

        public override BaseConstruction getInstance(GridPosition position)
        {
            String id = prototypeId + "_" + System.Guid.NewGuid().ToString();
            BaseIdleForestConstruction construction = BaseIdleForestConstructionFactory.typeAuto(prototypeId, id, position, descriptionPackage,
                null, 0);

            construction.proficiencyComponent.promoteConstructionPrototypeId = ConstructionPrototypeId.DIRT;

            return construction;
        }
    }
}
