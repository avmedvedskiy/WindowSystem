using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UISystem
{
    public abstract class BaseWindowAnimation : MonoBehaviour
    {
        public abstract UniTask OpenAnimationAsync(CancellationToken cancellationToken =default);
        public abstract UniTask CloseAnimationAsync(CancellationToken cancellationToken =default);
    }
}