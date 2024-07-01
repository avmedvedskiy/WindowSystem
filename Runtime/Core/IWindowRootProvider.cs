using UnityEngine;

namespace UISystem
{
    public interface IWindowRootProvider
    {
        Transform Root { get; }
    }
}