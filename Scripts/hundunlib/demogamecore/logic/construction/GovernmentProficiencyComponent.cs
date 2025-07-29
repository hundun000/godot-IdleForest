using hundun.idleshare.gamelib;
using hundun.unitygame.adapters;
using hundun.unitygame.gamelib;
using static Assets.Scripts.DemoGameCore.logic.BaseIdleForestConstruction;

namespace Assets.Scripts.DemoGameCore.logic
{

    internal class GovernmentProficiencyComponent : BaseAutoProficiencyComponent
    {
        protected const int Government_AUTO_PROFICIENCY_SECOND_MAX = 1; // n秒生长一次
        protected const int F = 20; // 周期，单位秒
        protected const int SPEED = (100 / (F / Government_AUTO_PROFICIENCY_SECOND_MAX));


        public GovernmentProficiencyComponent(BaseIdleForestConstruction construction) : base(construction, Government_AUTO_PROFICIENCY_SECOND_MAX, null)
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
                    case CarbonStage.LOW:
                        construction.gameplayContext.storageManager.modifyAllResourceNum(JavaFeatureForGwt.mapOf(
                            ResourceType.COIN, 300L,
                            ResourceType.FLAG_GOV_REWARD, 1L
                            ), true);
                        construction.gameplayContext.eventManager.notifyNotification("政府进行了一次奖励");
                        break;
                    case CarbonStage.HIGH_1:
                        construction.gameplayContext.storageManager.modifyAllResourceNum(JavaFeatureForGwt.mapOf(
                            ResourceType.COIN, 500L,
                            ResourceType.FLAG_GOV_PUNISH, -1L
                            ), false);
                        construction.gameplayContext.eventManager.notifyNotification("政府进行了一次惩罚");
                        break;
                    case CarbonStage.HIGH_2:
                    case CarbonStage.HIGH_3:
                        construction.gameplayContext.storageManager.modifyAllResourceNum(JavaFeatureForGwt.mapOf(
                            ResourceType.COIN, 1500L,
                            ResourceType.FLAG_GOV_PUNISH, -1L
                            ), false);
                        construction.gameplayContext.eventManager.notifyNotification("政府进行了一次惩罚");
                        break;
                    default:
                        construction.gameplayContext.frontend.log(this.getClass().getSimpleName(), "政府无事发生");
                        break;
                }
            }
        }
    }
}
