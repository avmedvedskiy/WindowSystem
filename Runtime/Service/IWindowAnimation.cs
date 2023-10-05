using Cysharp.Threading.Tasks;

namespace UISystem
{
    public interface IWindowAnimation
    {
        UniTask OpenAnimation();
        UniTask CloseAnimation();
    }
}