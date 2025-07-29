using hundun.unitygame.adapters;
using System;
using System.Collections.Generic;

namespace Assets.Scripts.DemoGameCore.logic
{
    internal class GameArea
    {
        public const String AREA_SINGLE = "ENUM_AREA@AREA_SINGLE";

        public static readonly List<String> values = JavaFeatureExtension.ArraysAsList(AREA_SINGLE);
    }
}
