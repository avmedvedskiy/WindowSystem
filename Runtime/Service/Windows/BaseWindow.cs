using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UISystem
{
    [RequireComponent(typeof(BaseWindowAnimation))]
    public abstract class BaseWindow : MonoBehaviour, IWindow
    {
        [SerializeField] private BaseWindowAnimation _windowAnimation;

        public virtual UniTask OpenAsync() => _windowAnimation.OpenAnimation();

        public virtual UniTask CloseAsync() => _windowAnimation.CloseAnimation();

        private void OnValidate()
        {
            _windowAnimation ??= GetComponent<BaseWindowAnimation>();
        }
    }
}