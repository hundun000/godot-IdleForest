using hundun.idleshare.gamelib;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;
using static Assets.Scripts.DemoGameCore.logic.BaseIdleForestConstruction;

namespace Assets.Scripts.DemoGameCore.logic
{
    public class BigFactoryPrototype : AbstractConstructionPrototype
    {
        public BigFactoryPrototype(Language language) : base(ConstructionPrototypeId.BIG_FACTORY, language, null)
        {
            switch (language)
            {
                case Language.CN:
                    this.descriptionPackage = SmallFactoryPrototype.descriptionPackageCN;
                    break;
                default:
                    this.descriptionPackage = SmallFactoryPrototype.descriptionPackageEN;
                    break;
            }
        }

        public override BaseConstruction getInstance(GridPosition position)
        {
            String id = prototypeId + "_" + System.Guid.NewGuid().ToString();
            BaseIdleForestConstruction construction = BaseIdleForestConstructionFactory.typeAuto(prototypeId, id, position, descriptionPackage,
                40, 1f / 6f
                );

            construction.existenceComponent.destoryCostPack = DemoBuiltinConstructionsLoader.toPack(new Dictionary<string, int>());
            construction.existenceComponent.destoryGainPack = DemoBuiltinConstructionsLoader.toPack(new Dictionary<string, int>());
            construction.existenceComponent.allowAnyProficiencyDestory = true;

            construction.outputComponent.outputGainPack = (DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.COIN, 180,
                    ResourceType.CARBON, 1000
                    )));

            construction.upgradeComponent.upgradeCostPack = (DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.COIN, 20000,
                    ResourceType.WOOD, 800
                    )));

            return construction;
        }
    }
}
