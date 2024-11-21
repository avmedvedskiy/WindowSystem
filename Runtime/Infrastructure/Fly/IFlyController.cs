using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UISystem
{
    public interface IFlyController
    {
        UniTask FlyToAnchor(IFlyComponent view, int anchor, CancellationToken cancellationToken = default);
        UniTask FlyToTarget(IFlyComponent view, Transform target, CancellationToken cancellationToken = default);
        UniTask FlyToTarget(IFlyComponent view,Transform from, Transform target, CancellationToken cancellationToken = default);
    }
}