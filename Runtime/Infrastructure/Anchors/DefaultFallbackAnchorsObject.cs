using System.Collections.Generic;
using System.Linq;
using UISystem;
using UnityEngine;

public class DefaultFallbackAnchorsObject : MonoBehaviour,IFallbackAnchorsProvider
{
    private List<IAnchor> _anchorElements;
    
    private void Awake()
    {
        _anchorElements = GetComponentsInChildren<IAnchor>(true).ToList();
        foreach (var anchorElement in _anchorElements)
        {
            anchorElement.gameObject.SetActive(false);
            anchorElement.Static = false;
        }
    }

    public IAnchor GetAnchor(int id)
    {
        foreach (var anchorElement in _anchorElements)
        {
            if (anchorElement.Id == id)
                return anchorElement;
        }
        return null;
    }
}