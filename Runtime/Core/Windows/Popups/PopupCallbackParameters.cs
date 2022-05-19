using UnityEngine;
using System.Collections;
namespace UISystem
{
    public struct PopupCallbackParameters
    {

        public PopupCallbackType CallbackType { get; set; }
        public object CustomData { get; set; }

    }
}