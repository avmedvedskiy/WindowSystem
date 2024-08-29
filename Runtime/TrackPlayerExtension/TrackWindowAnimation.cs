using System.Threading;
using Cysharp.Threading.Tasks;
using PlayableNodes;
using UnityEngine;

namespace UISystem
{
    [RequireComponent(typeof(TrackPlayerCollection))]
    public class TrackWindowAnimation : BaseWindowAnimation
    {
        [SerializeField]public TrackPlayerCollection _trackPlayer;
        [SerializeField] private string _openAnimation = "Open";
        [SerializeField] private string _closeAnimation= "Close";

        private bool _isOpened;
        public override async UniTask OpenAnimationAsync(CancellationToken cancellationToken = default)
        {
            if(_isOpened)
                return;
            
            _isOpened = true;
            await _trackPlayer.PlayAsync(_openAnimation, cancellationToken);
        }

        public override async UniTask CloseAnimationAsync(CancellationToken cancellationToken = default)
        { 
            if(!_isOpened)
                return;
            _isOpened = false;
            await _trackPlayer.PlayAsync(_closeAnimation, cancellationToken);
        } 
        
        private void OnValidate()
        {
            _trackPlayer ??= GetComponent<TrackPlayerCollection>();
        }
    }
}