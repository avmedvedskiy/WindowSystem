using Cysharp.Threading.Tasks;

namespace UISystem
{
    public class EmptyWindowAnimation : BaseWindowAnimation
    {
        public override UniTask OpenAnimationAsync() => UniTask.CompletedTask;

        public override UniTask CloseAnimationAsync() => UniTask.CompletedTask;
    }
}