using Cysharp.Threading.Tasks;

namespace UISystem
{
    public interface IFlySystem
    {
        UniTask FlyRewardToAnchor(IFlyComponent view, int anchor);
    }
}