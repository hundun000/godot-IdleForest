using hundun.idleshare.gamelib;
using hundun.unitygame.gamelib;
using System;
using System.Collections.Generic;
using static Assets.Scripts.DemoGameCore.logic.BaseIdleForestConstruction;

namespace Assets.Scripts.DemoGameCore.logic
{
    public class MidFactoryPrototype : AbstractConstructionPrototype
    {
        public MidFactoryPrototype(Language language) : base(ConstructionPrototypeId.MID_FACTORY, language, null)
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
                30, 1f / 8f
                );

            construction.existenceComponent.destoryCostPack = DemoBuiltinConstructionsLoader.toPack(new Dictionary<string, int>());
            construction.existenceComponent.destoryGainPack = DemoBuiltinConstructionsLoader.toPack(new Dictionary<string, int>());
            construction.existenceComponent.allowAnyProficiencyDestory = true;

            construction.outputComponent.outputGainPack = (DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.COIN, 80,
                    ResourceType.CARBON, 500
                    )));

            construction.upgradeComponent.upgradeCostPack = (DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.COIN, 3000,
                    ResourceType.WOOD, 300
                    )));
            construction.upgradeComponent.transformCostPack = (DemoBuiltinConstructionsLoader.toPack(JavaFeatureForGwt.mapOf(
                    ResourceType.COIN, 15000,
                    ResourceType.WOOD, 1000
                    )));
            construction.upgradeComponent.transformConstructionPrototypeId = ConstructionPrototypeId.BIG_FACTORY;

            return construction;
        }
    }
}
