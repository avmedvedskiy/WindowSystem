using System;
using UnityEngine;

namespace UISystem
{
    public class WindowRootProvider : MonoBehaviour, IWindowRootProvider
    {
        [SerializeField] private Transform _root;
        
        public Transform Root => _root;

        private void OnValidate()
        {
            _root ??= transform;
        }
    }
}