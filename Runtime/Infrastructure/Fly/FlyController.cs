using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UISystem
{
    public class FlyController : IFlyController
    {
        private readonly IFlyRootProvider _flyRootProvider;
        private Transform Root => _flyRootProvider.Root;
        
        public FlyController(IFlyRootProvider flyRootProvider)
        {
            _flyRootProvider = flyRootProvider;
        }

        public void SetParent(IFlyComponent view)
        {
            view
                .SetParent(Root);
        }

        public async UniTask FlyViewToTarget(IFlyComponent view, Transform to)
        {
            await view
                .SetParent(Root)
                .FlyAsync(to)
                .AndActivateView(view);
        }
        
        public async UniTask FlyViewToTarget(IFlyComponent view, Transform from, Transform to)
        {
            await view
                .SetParent(Root)
                .FlyAsync(from,to)
                .AndActivateView(view);
        }
    }
}