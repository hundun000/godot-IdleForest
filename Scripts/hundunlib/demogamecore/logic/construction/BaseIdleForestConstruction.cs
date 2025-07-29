using hundun.idleshare.gamelib;
using System;
using System.Linq;

namespace Assets.Scripts.DemoGameCore.logic
{


    public class BaseIdleForestConstruction : BaseConstruction
    {


        public enum CarbonStage
        {
            /// <summary>低碳状态</summary>
            LOW,
            /// <summary>碳平衡状态</summary>
            MID,
            /// <summary>碳饱和状态1</summary>
            HIGH_1,
            /// <summary>碳饱和状态2</summary>
            HIGH_2,
            /// <summary>碳饱和状态3</summary>
            HIGH_3
        }

        public static CarbonStage carbonAmountToCarbonStage(long amount)
        {
            if (amount <= 3000)
            {
                return CarbonStage.LOW;
            }
            else if (amount <= 6000)
            {
                return CarbonStage.MID;
            }
            else if (amount <= 8000)
            {
                return CarbonStage.HIGH_1;
            }
            else if (amount <= 10000)
            {
                return CarbonStage.HIGH_2;
            }
            else
            {
                return CarbonStage.HIGH_3;
            }
        }

        private static readonly Func<long, int, long> IDLE_FOREST_UPGRADE_COST_FUNCTION = (baseValue, level) =>
        {
            return baseValue;
        };


        public static ProficiencySpeedCalculator IDLE_FOREST_PROFICIENCY_SPEED_CALCULATOR = (thiz) =>
        {
            int neighborTreeCount = thiz.neighbors.Values.ToList()
                .Where(it => it != null && it.saveData.prototypeId.Equals(ConstructionPrototypeId.SMALL_TREE))
                .Count()
                ;
            int neighborFactoryCount = thiz.neighbors.Values.ToList()
                .Where(it => it != null)
                .Where(it => it.saveData.prototypeId.Equals(ConstructionPrototypeId.SMALL_FACTORY)
                    || it.saveData.prototypeId.Equals(ConstructionPrototypeId.MID_FACTORY)
                    || it.saveData.prototypeId.Equals(ConstructionPrototypeId.BIG_FACTORY)
                )
                .Count()
                ;
            int neighborLakeCount = thiz.neighbors.Values.ToList()
                .Where(it => it != null)
                .Where(it => it.saveData.prototypeId.Equals(ConstructionPrototypeId.LAKE))
                .Count()
                ;

            switch (thiz.prototypeId)
            {
                case ConstructionPrototypeId.SMALL_TREE:
                    return 2 + 2 * neighborTreeCount + 1 * neighborLakeCount;
                case ConstructionPrototypeId.SMALL_FACTORY:
                    return 5 + 1 * neighborFactoryCount + 1 * neighborLakeCount;
                case ConstructionPrototypeId.MID_FACTORY:
                    return 3 + 1 * neighborFactoryCount + 1 * neighborLakeCount;
                case ConstructionPrototypeId.BIG_FACTORY:
                    return 1 + 1 * neighborFactoryCount + 1 * neighborLakeCount;
                case ConstructionPrototypeId.LAKE:
                    return neighborTreeCount * -1 + neighborFactoryCount * 1;
                case ConstructionPrototypeId.DESERT:
                    return neighborTreeCount * 1;
                default:
                    return 0;
            }


        };

        private BaseIdleForestConstruction(
            String prototypeId,
            String id,
            GridPosition position,
            DescriptionPackage descriptionPackage) : base(prototypeId, id)
        {

            this.descriptionPackage = descriptionPackage;

            UpgradeComponent upgradeComponent = new UpgradeComponent(this);
            upgradeComponent.calculateCostFunction = IDLE_FOREST_UPGRADE_COST_FUNCTION;
            this.upgradeComponent = (upgradeComponent);

            LevelComponent levelComponent = new LevelComponent(this, false);
            this.levelComponent = (levelComponent);

            ExistenceComponent existenceComponent = new ExistenceComponent(this);
            this.existenceComponent = existenceComponent;

            this.saveData.position = position;
            this.saveData.level = 1;
            this.saveData.workingLevel = 1;
        }

        public class BaseIdleForestConstructionFactory
        {
            public static BaseIdleForestConstruction typeAuto(
                String prototypeId,
                String id,
                GridPosition position,
                DescriptionPackage descriptionPackage,
                int? upgradeLostProficiency,
                float modifiedOutputArg
                )
            {
                BaseIdleForestConstruction thiz = new BaseIdleForestConstruction(prototypeId, id, position, descriptionPackage);

                var outputComponent = new IdleForestOutputComponent(thiz);
                outputComponent.modifiedOutputArg = modifiedOutputArg;
                thiz.outputComponent = outputComponent;

                var proficiencyComponent = new IdleForestProficiencyComponent(thiz, 1, upgradeLostProficiency);
                thiz.proficiencyComponent = proficiencyComponent;
                proficiencyComponent.proficiencySpeedCalculator = BaseIdleForestConstruction.IDLE_FOREST_PROFICIENCY_SPEED_CALCULATOR;

                return thiz;
            }

            public static BaseIdleForestConstruction typeNoOutputConstProficiency(String prototypeId,
                String id,
                GridPosition position,
                DescriptionPackage descriptionPackage)
            {
                BaseIdleForestConstruction thiz = new BaseIdleForestConstruction(prototypeId, id, position, descriptionPackage);

                var outputComponent = new EmptyOutputComponent(thiz);
                thiz.outputComponent = outputComponent;

                var proficiencyComponent = new ConstProficiencyComponent(thiz);
                thiz.proficiencyComponent = proficiencyComponent;

                return thiz;
            }

            public static BaseIdleForestConstruction typeGovernment(String prototypeId,
                String id,
                GridPosition position,
                DescriptionPackage descriptionPackage)
            {
                BaseIdleForestConstruction thiz = new BaseIdleForestConstruction(prototypeId, id, position, descriptionPackage);

                OutputComponent outputComponent = new EmptyOutputComponent(thiz);
                thiz.outputComponent = outputComponent;

                var proficiencyComponent = new GovernmentProficiencyComponent(thiz);
                thiz.proficiencyComponent = proficiencyComponent;

                return thiz;
            }

            public static BaseIdleForestConstruction typeOrganization(String prototypeId,
                String id,
                GridPosition position,
                DescriptionPackage descriptionPackage)
            {
                BaseIdleForestConstruction thiz = new BaseIdleForestConstruction(prototypeId, id, position, descriptionPackage);

                OutputComponent outputComponent = new EmptyOutputComponent(thiz);
                thiz.outputComponent = outputComponent;

                var proficiencyComponent = new OrganizationProficiencyComponent(thiz);
                thiz.proficiencyComponent = proficiencyComponent;

                return thiz;
            }
        }
    }
}
