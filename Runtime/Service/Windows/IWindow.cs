using Cysharp.Threading.Tasks;

namespace UISystem
{
    public interface IWindow : IClosedWindow
    {
        UniTask OpenAsync();
    }
}