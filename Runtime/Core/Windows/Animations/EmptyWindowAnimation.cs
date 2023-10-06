using Cysharp.Threading.Tasks;

namespace UISystem
{
    public class EmptyWindowAnimation : BaseWindowAnimation
    {
        public override UniTask OpenAnimation() => UniTask.CompletedTask;

        public override UniTask CloseAnimation() => UniTask.CompletedTask;
    }
}