using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UISystem
{
    [RequireComponent(typeof(Animation))]
    public class WindowAnimation : BaseWindowAnimation
    {
        [SerializeField] private Animation _animation;
        [SerializeField] private string _openAnimation = "Open";
        [SerializeField] private string _closeAnimation= "Close";


        public override UniTask OpenAnimation()
        {
            _animation.Play(_openAnimation);
            return UniTask.WaitWhile(() => _animation.isPlaying);
        }
        
        public override UniTask CloseAnimation()
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