using UnityEngine;

namespace UISystem
{
    public class WindowFlyRootProvider : IFlyRootProvider
    {
        private readonly IWindowRootProvider _rootProvider;
        public Transform Root => _rootProvider.Root;

        public WindowFlyRootProvider(IWindowRootProvider rootProvider)
        {
            _rootProvider = rootProvider;
        }

    }
}