namespace UISystem
{
    /// <summary>
    ///Use for show default anchors when other are disabled, can be used in reward windows in events etc
    /// </summary>
    public interface IDefaultAnchorsProvider
    {
        IAnchor GetAnchor(int id);
    }

    public class NullDefaultAnchorsProvider : IDefaultAnchorsProvider
    {
        public IAnchor GetAnchor(int id) => null;
    }
}