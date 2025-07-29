using hundun.idleshare.gamelib;


namespace Assets.Scripts.DemoGameCore.logic
{
    public delegate int ProficiencySpeedCalculator(BaseIdleForestConstruction thiz);

    internal class IdleForestProficiencyComponent : BaseAutoProficiencyComponent
    {

        public ProficiencySpeedCalculator proficiencySpeedCalculator;

        public IdleForestProficiencyComponent(BaseIdleForestConstruction construction, int? second, int? upgradeLostProficiency) : base(construction, second, upgradeLostProficiency)
        {

        }

        public override void onSubLogicFrame()
        {
            base.onSubLogicFrame();
            checkAutoPromoteDemote();
        }

        protected override void tryProficiencyOnce()
        {
            if (this.proficiencySpeedCalculator != null)
            {
                this.changeProficiency(proficiencySpeedCalculator.Invoke((BaseIdleForestConstruction)construction));
            }
        }
    }
}
