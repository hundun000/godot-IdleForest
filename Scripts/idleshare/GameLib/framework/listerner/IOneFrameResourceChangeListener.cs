using System;
using System.Collections.Generic;

namespace hundun.idleshare.gamelib
{
    public interface IOneFrameResourceChangeListener
    {
        void onResourceChange(Dictionary<String, long> changeMap, Dictionary<string, List<long>> deltaHistoryMap);
    }
}
