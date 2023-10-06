using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UISystem
{
    public interface IClosedWindow
    {
        string Id { get; set; }
        IWindowService Parent { get; set; }
        GameObject gameObject { get; }
        UniTask CloseAsync();
    }
}