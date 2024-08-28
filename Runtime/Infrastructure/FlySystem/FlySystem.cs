using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UISystem
{
    /// <summary>
    /// Система объединяющая в себе и полет и якоря, управляет анимациями появления-удаления якорей
    /// </summary>
    public class FlySystem : IFlySystem
    {
        private readonly IFlyController _flyController;
        private readonly IAnchorsProvider _anchorsProvider;

        public FlySystem(IFlyController flyController, IAnchorsProvider anchorsProvider)
        {
            _flyController = flyController;
            _anchorsProvider = anchorsProvider;
        }
        
        public async UniTask FlyRewardToAnchor(IFlyComponent view, int anchor)
        {
            if (_anchorsProvider.TryGetAnchor(anchor, out var to))
            {
                _flyController.SetParent(view);
                await to.PlayOpenAnimation();
                await _flyController.FlyViewToTarget(view,to.Position);
                await to.PlayCloseAnimation();
                
            }
            else
            {
                Debug.LogError($"Anchor is null {anchor}");
            }
        }
    }
}