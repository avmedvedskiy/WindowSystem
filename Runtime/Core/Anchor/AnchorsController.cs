using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace UISystem
{
    [Serializable]
    public class AnchorsController
    {
        private Dictionary<string, List<IAnchor>> anchors = new Dictionary<string, List<IAnchor>>();

        public event Action<IAnchor> OnAnchorAdded;
        public event Action<IAnchor> OnAnchorRemoved;

        public IAnchor GetAnchor(string id)
        {
            if (anchors.ContainsKey(id) && anchors[id].Count > 0)
            {
                return anchors[id].Last();
            }
            return null;
        }

        public Vector3 GetPosition(string id)
        {
            if (!anchors.ContainsKey(id))
            {
                return default(Vector3);
            }

            return GetAnchor(id).Position.position;
        }

        public void AddAnchor(IAnchor anchor)
        {
            if (anchor == null)
                return;

            if (!anchors.ContainsKey(anchor.Id))
            {
                anchors.Add(anchor.Id, new List<IAnchor>());
            }

            anchors[anchor.Id].Remove(anchor);
            anchors[anchor.Id].Add(anchor);

            OnAnchorAdded?.Invoke(anchor);
        }

        public void RemoveAnchor(IAnchor anchor)
        {
            if (anchor != null && anchors.ContainsKey(anchor.Id))
            {
                anchors[anchor.Id].Remove(anchor);
                OnAnchorRemoved?.Invoke(anchor);
            }
        }

    }
}