using PlayableNodes;
using UnityEngine;

public abstract class BaseInteractAnchorElement : AnchorElement
{
    [SerializeField] private BaseTargetInteract _targetInteract;
    public override Transform Position => _targetInteract.transform;
    public override void OnEnable()
    {
        base.OnEnable();
        _targetInteract.OnInteract += OnInteract;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        _targetInteract.OnInteract -= OnInteract;
    }

    protected abstract void OnInteract();
}