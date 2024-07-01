using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UISystem
{
    public interface IClosedWindow
    {
        event Action<Status> OnStatusChanged;
        string Id { get;}
        Status Status { get;}
        IWindowService Parent { get;}
        GameObject gameObject { get; }
        UniTask CloseAsync();
        internal void SetStatus(Status status);
        internal void Initialize(string id, IWindowService parent);
    }
}