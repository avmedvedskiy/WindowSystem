using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UISystem
{
    public abstract class BaseWindowAnimation : MonoBehaviour, IWindowAnimation
    {
        public abstract UniTask OpenAnimation();
        public abstract UniTask CloseAnimation();
    }
}