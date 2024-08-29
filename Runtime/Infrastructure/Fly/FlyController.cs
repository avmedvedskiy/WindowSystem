using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UISystem
{
    public class FlyController : IFlyController
    {
        private readonly IFlyRootProvider _flyRootProvider;
        private readonly IAnchorsProvider _anchorsProvider;
        private Transform Root => _flyRootProvider.Root;
        
        public FlyController(IFlyRootProvider flyRootProvider, IAnchorsProvider anchorsProvider)
        {
            _flyRootProvider = flyRootProvider;
            _anchorsProvider = anchorsProvider;
        }
        public async UniTask FlyRewardToAnchor(IFlyComponent view, int anchor, CancellationToken cancellationToken = default)
        {
            if (_anchorsProvider.TryGetAnchor(anchor, out var to))
            {
                view.SetParent(Root);
                await to.PlayOpenAnimation();
                await view
                    .MoveToTarget(to.Position, cancellationToken)
                    .AndActivateView(view,cancellationToken);
                await to.PlayCloseAnimation();
                
            }
            else
            {
                Debug.LogError($"Anchor is null {anchor}");
            }
        }
    }
}