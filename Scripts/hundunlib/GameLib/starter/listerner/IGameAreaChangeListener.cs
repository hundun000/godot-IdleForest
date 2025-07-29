using System;


namespace hundun.unitygame.gamelib
{
    public interface IGameAreaChangeListener
    {
        void onGameAreaChange(String last, String current);
    }

}
