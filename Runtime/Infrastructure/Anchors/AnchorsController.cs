using Cysharp.Threading.Tasks;

namespace UISystem
{
    public class AnchorsController : IAnchorsController
    {
        private readonly IAnchorsProvider _anchorsProvider;
        private IDefaultAnchorsProvider _defaultAnchorsProvider = new NullDefaultAnchorsProvider();

        public AnchorsController(IAnchorsProvider anchorsProvider)
        {
            _anchorsProvider = anchorsProvider;
        }

        /// <summary>
        /// Useful for late binding, load default anchors from addressables and set here
        /// </summary>
        public void OverrideDefaultAnchorsView(IDefaultAnchorsProvider defaultAnchorsProvider)
        {
            _defaultAnchorsProvider = defaultAnchorsProvider;
        }

        public async UniTask<IAnchor> ShowAnchor(int id)
        {
            var anchor = _anchorsProvider.GetAnchor(id) ?? _defaultAnchorsProvider.GetAnchor(id);
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