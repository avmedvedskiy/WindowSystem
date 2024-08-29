using System.Threading;
using Cysharp.Threading.Tasks;

namespace UISystem
{
    public class EmptyWindowAnimation : BaseWindowAnimation
    {
        public override UniTask OpenAnimationAsync(CancellationToken cancellationToken =default) => UniTask.CompletedTask;

        public override UniTask CloseAnimationAsync(CancellationToken cancellationToken =default) => UniTask.CompletedTask;
    }
}