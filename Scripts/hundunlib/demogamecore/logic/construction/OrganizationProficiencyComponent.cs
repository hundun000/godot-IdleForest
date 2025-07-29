using hundun.idleshare.gamelib;
using hundun.unitygame.adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using static Assets.Scripts.DemoGameCore.logic.BaseIdleForestConstruction;


namespace Assets.Scripts.DemoGameCore.logic
{

    internal class OrganizationProficiencyComponent : BaseAutoProficiencyComponent
    {
        protected const int Organization_AUTO_PROFICIENCY_SECOND_MAX = 1; // n秒生长一次
        protected const int F = 20; // 周期，单位秒
        protected const int SPEED = (100 / (F / Organization_AUTO_PROFICIENCY_SECOND_MAX));


        public OrganizationProficiencyComponent(BaseIdleForestConstruction construction) : base(construction, Organization_AUTO_PROFICIENCY_SECOND_MAX, null)
        {

        }


        override protected void tryProficiencyOnce()
        {
            this.changeProficiency(SPEED);

            if (construction.proficiencyComponent.isMaxProficiency())
            {
                construction.proficiencyComponent.cleanProficiency();

                long amount = construction.gameplayContext.storageManager.getResourceNumOrZero(ResourceType.CARBON);
                CarbonStage carbonStage = BaseIdleForestConstruction.carbonAmountToCarbonStage(amount);
                switch (carbonStage)
                {
                    case CarbonStage.HIGH_3:
                        List<BaseConstruction> closeList = RandomClose();
                        construction.gameplayContext.eventManager.notifyNotification("环保组织进行了一次关停:" +
                            string.Join(", ", closeList.Select(it => it.position.toShowText()).ToList())
                            );
                        break;
                    default:
                        construction.gameplayContext.frontend.log(this.getClass().getSimpleName(), "环保组织无事发生");
                        break;
                }
            }
        }

        private List<BaseConstruction> RandomClose()
        {
            List<BaseConstruction> closeList = construction.gameplayContext.constructionManager.getConstructions()
                .Where(it => it.saveData.prototypeId.Equals(ConstructionPrototypeId.SMALL_FACTORY) || it.saveData.prototypeId.Equals(ConstructionPrototypeId.MID_FACTORY) || it.saveData.prototypeId.Equals(ConstructionPrototypeId.BIG_FACTORY))
                .OrderBy(a => Guid.NewGuid())
                .Take(1)
                .ToList();
            closeList.ForEach(it => it.proficiencyComponent.cleanProficiency());
            return closeList;
        }
    }
}
