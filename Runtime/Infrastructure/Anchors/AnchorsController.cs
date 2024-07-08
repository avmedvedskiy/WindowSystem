using Cysharp.Threading.Tasks;

namespace UISystem
{
    public class AnchorsController : IAnchorsController
    {
        private readonly IAnchorsProvider _anchorsProvider;
        private IFallbackAnchorsProvider _fallbackAnchorsProvider = new NullFallbackAnchorsProvider();

        public AnchorsController(IAnchorsProvider anchorsProvider)
        {
            _anchorsProvider = anchorsProvider;
        }

        /// <summary>
        /// Useful for late binding, load fallback anchors from addressables and set here
        /// </summary>
        public void OverrideDefaultAnchorsView(IFallbackAnchorsProvider fallbackAnchorsProvider)
        {
            _fallbackAnchorsProvider = fallbackAnchorsProvider;
        }

        public async UniTask<IAnchor> ShowAnchor(int id)
        {
            var anchor = _anchorsProvider.GetAnchor(id) ?? _fallbackAnchorsProvider.GetAnchor(id);
            if (anchor != null)
            {
                await anchor.PlayOpenAnimation();
            }

            return anchor;
        }

        public async UniTask HideAnchor(int id)
        {
            var anchor = _anchorsProvider.GetAnchor(id);
            if (anchor != null)
            {
                await anchor.PlayCloseAnimation();
            }
        }
    }
}