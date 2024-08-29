using System.Threading;
using Cysharp.Threading.Tasks;
using PlayableNodes;
using PlayableNodes.Extensions;
using UnityEngine;

namespace UISystem
{
    [RequireComponent(typeof(TrackPlayerCollection))]
    public class FlyTrackComponent : MonoBehaviour, IFlyComponent
    {
        private const string FLY_TRACK_NAME = "Fly";
        private const int END_TARGET_PIN = 1;
        private const int FROM_TARGET_PIN = 2;
        
        private ITracksPlayer _playerCollection;

        private void Awake()
        {
            _playerCollection = GetComponent<ITracksPlayer>();
        }

        public async UniTask MoveToTarget(Transform to, CancellationToken cancellationToken = default)
        {
            _playerCollection.ChangeEndValueByPin(END_TARGET_PIN, to);
            await _playerCollection.PlayAsync(FLY_TRACK_NAME,cancellationToken);
        }
        
        public async UniTask MoveToTarget(Transform from, Transform to, CancellationToken cancellationToken = default)
        {
            transform.position = from.transform.position;
            _playerCollection.ChangeEndValueByPin(END_TARGET_PIN, to);
            _playerCollection.ChangeEndValueByPin(FROM_TARGET_PIN, from);
            await _playerCollection.PlayAsync(FLY_TRACK_NAME, cancellationToken);
        }
    }
}