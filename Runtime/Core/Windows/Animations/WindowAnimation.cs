using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UISystem
{
    [RequireComponent(typeof(Animation))]
    public class WindowAnimation : BaseWindowAnimation
    {
        [SerializeField] private Animation _animation;
        [SerializeField] private string _openAnimation = "Open";
        [SerializeField] private string _closeAnimation = "Close";

        public override async UniTask OpenAnimationAsync(CancellationToken cancellationToken = default)
        {
            _animation.Play(_openAnimation);
            await UniTask.WaitWhile(() => _animation.isPlaying, cancellationToken: cancellationToken);
            if (cancellationToken.IsCancellationRequested)
                _animation.Stop();
        }

        public override async UniTask CloseAnimationAsync(CancellationToken cancellationToken = default)
        {
            _animation.Play(_closeAnimation);
            await UniTask.WaitWhile(() => _animation.isPlaying, cancellationToken: cancellationToken);
            if (cancellationToken.IsCancellationRequested)
                _animation.Stop();
        }

        private void OnValidate() => _animation ??= GetComponent<Animation>();
    }
}