using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UISystem
{
    public interface IAnchor
    {
        string Id { get; set; }
        Transform Position { get; set; }

        void Interact();
    }
}
