namespace UISystem
{
    /// <summary>
    /// Доступ к любым UI якорям в текущем экране
    /// </summary>
    public interface IAnchorsProvider
    {
        bool TryGetAnchor(int id, out IAnchor anchor);
        void AddAnchor(int id,IAnchor anchor);
        void RemoveAnchor(int id,IAnchor anchor);

        /// <summary>
        /// Useful for late binding, load fallback anchors from addressables and set here
        /// </summary>
        void OverrideDefaultAnchorsView(IFallbackAnchorsProvider fallbackAnchorsProvider);
    }
}