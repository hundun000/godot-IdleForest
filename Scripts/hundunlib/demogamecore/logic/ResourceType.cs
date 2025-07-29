using hundun.unitygame.adapters;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.DemoGameCore.logic
{
    public class ResourceType
    {
        public const String COIN = "ENUM_RESC@COIN";
        public const String WOOD = "ENUM_RESC@WOOD";
        public const String CARBON = "ENUM_RESC@CARBON";
        //public static final String WIN_TROPHY = "ENUM_RESC@TROPHY";

        public const String FLAG_CUT_FOREST = "ENUM_RESC@FLAG_CUT_FOREST";
        public const String FLAG_GOV_REWARD = "ENUM_RESC@FLAG_GOV_REWARD";
        public const String FLAG_GOV_PUNISH = "ENUM_RESC@FLAG_GOV_PUNISH";

        public static readonly List<String> VALUES_FOR_SHOW_ORDER = JavaFeatureExtension.ArraysAsList(COIN, WOOD, CARBON);

    }
}
