using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace UISystem
{
    [Serializable]
    public class AnchorsController
    {
        private Dictionary<string, List<IAnchor>> _anchors = new();

        public event Action<IAnchor> OnAnchorAdded;
        public event Action<IAnchor> OnAnchorRemoved;

        public IAnchor GetAnchor(string id)
        {
            if (_anchors.ContainsKey(id) && _anchors[id].Count > 0)
            {
                return _anchors[id].Last();
            }
            return null;
        }

        public Vector3 GetPosition(string id)
        {
            if (!_anchors.ContainsKey(id))
            {
                return default;
            }

            return GetAnchor(id).Position.position;
        }

        public void AddAnchor(IAnchor anchor)
        {
            if (anchor == null)
                return;

            if (!_anchors.ContainsKey(anchor.Id))
            {
                _anchors.Add(anchor.Id, new List<IAnchor>());
            }

            _anchors[anchor.Id].Remove(anchor);
            _anchors[anchor.Id].Add(anchor);

            OnAnchorAdded?.Invoke(anchor);
        }

        public void RemoveAnchor(IAnchor anchor)
        {
            if (anchor != null && _anchors.ContainsKey(anchor.Id))
            {
                _anchors[anchor.Id].Remove(anchor);
                OnAnchorRemoved?.Invoke(anchor);
            }
        }

    }
}