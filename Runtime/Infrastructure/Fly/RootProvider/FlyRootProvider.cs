using UnityEngine;

namespace UISystem
{
    public class FlyRootProvider : IFlyRootProvider
    {
        public Transform Root { get; private set; }

        public FlyRootProvider(Transform transform)
        {
            Root = transform;
        }
    }
}