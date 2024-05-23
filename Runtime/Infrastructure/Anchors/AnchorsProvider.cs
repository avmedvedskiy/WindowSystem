using System.Collections.Generic;

namespace UISystem
{
    public class AnchorsProvider : IAnchorsProvider
    {
        private readonly Dictionary<int, List<IAnchor>> _anchors = new();

        public IAnchor GetAnchor(int id)
        {
            if (_anchors.TryGetValue(id, out var anchors) && anchors is { Count: > 0 })
                return anchors[^1];
            return null;
        }

        public void AddAnchor(IAnchor anchor)
        {
            if (anchor == null)
                return;

            if (!_anchors.ContainsKey(anchor.Id))
            {
                _anchors.Add(anchor.Id, new List<IAnchor>());
            }

            _anchors[anchor.Id].Add(anchor);
        }

        public void RemoveAnchor(IAnchor anchor)
        {
            if ( _anchors.TryGetValue(anchor.Id, out var list))
            {
                list.Remove(anchor);
            }
        }
    }
}