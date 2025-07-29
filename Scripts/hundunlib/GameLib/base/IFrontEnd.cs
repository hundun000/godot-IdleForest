using System;


namespace hundun.unitygame.gamelib
{
    public interface IFrontend
    {
        String[] fileGetChilePathNames(String folder);

        String fileGetContent(String file);

        void log(String logTag, String format);
    }

}
