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