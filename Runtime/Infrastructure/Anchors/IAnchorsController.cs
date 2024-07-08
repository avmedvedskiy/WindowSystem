using Cysharp.Threading.Tasks;

namespace UISystem
{
    /// <summary>
    /// Предоставляет доступ к показу якорей. Если нет доступного якоря - то анимированно покажет дефолтный
    /// </summary>
    public interface IAnchorsController
    {
        void OverrideDefaultAnchorsView(IFallbackAnchorsProvider fallbackAnchorsProvider);
        UniTask<IAnchor> ShowAnchor(int id);
        UniTask HideAnchor(int id);
    }
}