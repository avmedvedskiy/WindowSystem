using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UISystem
{
    public interface IClosedWindow
    {
        GameObject gameObject { get; }
        UniTask CloseAsync();
    }
}