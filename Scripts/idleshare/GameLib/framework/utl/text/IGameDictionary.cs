using System;
using System.Collections.Generic;

namespace hundun.idleshare.gamelib
{
    public interface IGameDictionary
    {
        String constructionPrototypeIdToShowName(Language language, String prototypeId);
        String constructionPrototypeIdToDetailDescroptionConstPart(Language language, String prototypeId);
        List<String> getMemuScreenTexts(Language language);

        List<String> getPlayScreenTexts(Language language);
        List<String> getAchievementTexts(Language language);
        Dictionary<Language, String> getLanguageShowNameMap();
        List<String> getStageSelectMaskBoardTexts(Language language);
    }
}
