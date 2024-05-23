using Cysharp.Threading.Tasks;
using UnityEngine;
namespace UISystem
{
    /// <summary>
    /// Якорь который служит для указания целей полета ресурсов внутри игры. Например после окна награждения должны полететь монетки
    /// Если якорь интегрирован в какое то окно и не умеет скрываться, то ставим Static = true
    /// </summary>
    public interface IAnchor
    {
        int Id { get; }
        Transform Position { get; }
        bool Static { get; set; }
        GameObject gameObject { get; }

        public UniTask PlayOpenAnimation();

        public UniTask PlayCloseAnimation();
    }
}
