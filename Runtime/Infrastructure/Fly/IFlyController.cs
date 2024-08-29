using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UISystem
{
    public interface IFlyController
    {
        UniTask FlyRewardToAnchor(IFlyComponent view, int anchor, CancellationToken cancellationToken = default);
    }
}