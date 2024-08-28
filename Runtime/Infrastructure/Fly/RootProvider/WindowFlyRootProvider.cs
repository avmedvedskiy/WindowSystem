using UnityEngine;

namespace UISystem
{
    public class WindowFlyRootProvider : IFlyRootProvider
    {
        private readonly IWindowRootProvider _windowRootProvider;
        public Transform Root => _windowRootProvider.Root;

        public WindowFlyRootProvider(IWindowRootProvider windowRootProvider)
        {
            _windowRootProvider = windowRootProvider;
        }

    }
}