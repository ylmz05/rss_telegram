namespace Rss.CDO.Enums.TLBot
{
    public enum NextInstruction
    {
        None = -1,
        AddRss = 0,
        AddAlias = 1,
        AddChannel = 2,
        RemoveRssFromChannel = 3,
        WaitingCode = 4,
        CodeValidated = 5,
        CodeNotValidated = 6,
        CodeNotValidatedTimeOut = 7,
        RemoveChannelGroupFromRss = 8,
        BugReport = 9,
    }
}
