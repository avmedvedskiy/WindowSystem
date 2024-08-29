using System.Collections.Generic;

namespace UISystem
{
    public class AnchorsProvider : IAnchorsProvider
    {
        private readonly Dictionary<int, List<IAnchor>> _anchors = new();
        private IFallbackAnchorsProvider _fallbackAnchorsProvider = new NullFallbackAnchorsProvider();

        public AnchorsProvider()
        {
            IAnchorsProvider.Current = this;
        }
        
        /// <summary>
        /// Useful for late binding, load fallback anchors from addressables and set here
        /// </summary>
        public void OverrideDefaultAnchorsView(IFallbackAnchorsProvider fallbackAnchorsProvider)
        {
            _fallbackAnchorsProvider = fallbackAnchorsProvider;
        }

        public bool TryGetAnchor(int id, out IAnchor anchor)
        {
            if (_anchors.TryGetValue(id, out var anchors) && anchors is { Count: > 0 })
            {
                anchor = anchors[^1];
            }
            else
            {
                anchor = _fallbackAnchorsProvider.GetAnchor(id);
            }
            return anchor != null;
        }

        public void AddAnchor(int id,IAnchor anchor)
        {
            if (anchor == null)
                return;

            if (!_anchors.ContainsKey(id))
            {
                _anchors.Add(id, new List<IAnchor>());
            }

            _anchors[id].Add(anchor);
        }

        public void RemoveAnchor(int id,IAnchor anchor)
        {
            if ( _anchors.TryGetValue(id, out var list))
            {
                list.Remove(anchor);
            }
        }
    }
}