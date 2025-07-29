using hundun.idleshare.gamelib;
using System;
using static Assets.Scripts.DemoGameCore.logic.BaseIdleForestConstruction;

namespace Assets.Scripts.DemoGameCore.logic
{
    public class DirtPrototype : AbstractConstructionPrototype
    {
        private static DescriptionPackage descriptionPackageEN = new DescriptionPackageBuilder()
            .build();
        private static DescriptionPackage descriptionPackageCN = new DescriptionPackageBuilder()
            .build();

        public DirtPrototype(Language language) : base(ConstructionPrototypeId.DIRT, language, null)
        {

            // override descriptionPackage
            switch (language)
            {
                case Language.CN:
                    this.descriptionPackage = DirtPrototype.descriptionPackageCN;
                    break;
                default:
                    this.descriptionPackage = DirtPrototype.descriptionPackageEN;
                    break;
            }
        }

        public override BaseConstruction getInstance(GridPosition position)
        {
            String id = prototypeId + "_" + System.Guid.NewGuid().ToString();
            BaseIdleForestConstruction construction = BaseIdleForestConstructionFactory.typeNoOutputConstProficiency(prototypeId, id, position, descriptionPackage);

            construction.allowPositionOverwrite = true;

            return construction;
        }
    }
}
