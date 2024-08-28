using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UISystem
{
    public interface IFlyController
    {
        void SetParent(IFlyComponent view);
        UniTask FlyViewToTarget(IFlyComponent view, Transform to);
        UniTask FlyViewToTarget(IFlyComponent view, Transform from, Transform to);
    }
}