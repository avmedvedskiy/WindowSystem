using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UISystem
{
    [RequireComponent(typeof(Animation))]
    public class WindowAnimation : MonoBehaviour, IWindowAnimation
    {
        [SerializeField] private Animation _animation;
        [SerializeField] private string _openAnimation;
        [SerializeField] private string _closeAnimation;

        UniTask IWindowAnimation.OpenAnimation()
        {
            _animation.Play(_openAnimation);
            return UniTask.WaitWhile(() => _animation.isPlaying);
        }

        UniTask IWindowAnimation.CloseAnimation()
        {
            _animation.Play(_closeAnimation);
            return UniTask.WaitWhile(() => _animation.isPlaying);
        }

        private void OnValidate()
        {
            _animation ??= GetComponent<Animation>();
        }
    }
}