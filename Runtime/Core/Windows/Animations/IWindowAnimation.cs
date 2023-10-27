using Cysharp.Threading.Tasks;

namespace UISystem
{
    public interface IWindowAnimation
    {
        UniTask OpenAnimationAsync();
        UniTask CloseAnimationAsync();
    }
}