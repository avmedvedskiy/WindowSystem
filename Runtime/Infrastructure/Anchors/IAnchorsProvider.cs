namespace UISystem
{
    /// <summary>
    /// Доступ к любым UI якорям в текущем экране
    /// </summary>
    public interface IAnchorsProvider
    {
        IAnchor GetAnchor(int id);
        void AddAnchor(int id,IAnchor anchor);
        void RemoveAnchor(int id,IAnchor anchor);
    }
}