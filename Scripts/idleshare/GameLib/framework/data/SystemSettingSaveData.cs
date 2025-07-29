namespace hundun.idleshare.gamelib
{
    public class SystemSettingSaveData
    {
        public Language language;

        public SystemSettingSaveData()
        {
        }

        public SystemSettingSaveData(Language language)
        {
            this.language = language;
        }

        public override string ToString()
        {
            return "SystemSettingSaveData(" + "language=" + language + ")";
        }
    }
}
