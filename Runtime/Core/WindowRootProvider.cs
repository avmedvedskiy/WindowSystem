using System;
using UnityEngine;

namespace UISystem
{
    public class WindowRootProvider : MonoBehaviour, IWindowRootProvider
    {
        [SerializeField] private Transform _root;
        [SerializeField] private Transform _overlayRoot;
        
        public Transform Root => _root;
        public Transform OverlayRoot => _overlayRoot;

        private void OnValidate()
        {
            _root ??= transform;
            _overlayRoot ??= transform;
        }
    }
}