using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UISystem
{
    public abstract class BaseWindowAnimation : MonoBehaviour, IWindowAnimation
    {
        public abstract UniTask OpenAnimationAsync();
        public abstract UniTask CloseAnimationAsync();
    }
}