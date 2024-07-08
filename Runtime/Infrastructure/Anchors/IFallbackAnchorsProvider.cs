namespace UISystem
{
    /// <summary>
    ///Use for show default anchors when other are disabled, can be used in reward windows in events etc
    /// </summary>
    public interface IFallbackAnchorsProvider
    {
        IAnchor GetAnchor(int id);
    }

    public class NullFallbackAnchorsProvider : IFallbackAnchorsProvider
    {
        public IAnchor GetAnchor(int id) => null;
    }
}