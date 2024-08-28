using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UISystem
{
    public interface IFlyComponent
    {
        Transform transform { get; }
        UniTask MoveToTarget(Transform to, CancellationToken cancellationToken = default);
        UniTask MoveToTarget(Transform from, Transform to, CancellationToken cancellationToken = default);
    }
}