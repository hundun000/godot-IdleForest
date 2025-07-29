using hundun.idleshare.gamelib;

namespace Assets.Scripts.DemoGameCore.logic
{
    internal class IdleForestOutputComponent : BaseAutoOutputComponent
    {
        public float modifiedOutputArg = 0f;

        public IdleForestOutputComponent(BaseConstruction construction) : base(construction)
        {
        }

        override public long calculateModifiedOutputGain(long baseValue, int level, int proficiency)
        {
            return (long)((baseValue + modifiedOutputArg * level * baseValue) * (proficiency / 100.0));
        }

        override public long calculateModifiedOutputCost(long baseValue, int level, int proficiency)
        {
            return (long)((baseValue + modifiedOutputArg * level * baseValue) * (proficiency / 100.0));
        }
    }
}
