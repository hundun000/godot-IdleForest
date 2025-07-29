using System;

namespace hundun.idleshare.gamelib
{
    public class ResourcePair
    {
        public String type;
        public long amount;

        public ResourcePair(string type, long amount)
        {
            this.type = type;
            this.amount = amount;
        }
    }
}
