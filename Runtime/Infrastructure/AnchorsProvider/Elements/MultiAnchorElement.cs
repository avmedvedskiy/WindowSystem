using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UISystem;
using UnityEngine;

/// <summary>
/// Множественный якорь для нескольких валют которые летят в одно место. Например склад и предменты склада
/// </summary>
public class MultiAnchorElement : MonoBehaviour, IAnchor
{
    private readonly IAnchorsProvider _anchorsProvider = IAnchorsProvider.Current;
    
#if VALUE_STORAGE_ENABLED
    [ValueStorage.ConstValue("AnchorNameContainer")] 
#endif
    [SerializeField]
    private List<int> _ids;
    [SerializeField] private bool _static;
    [SerializeField] private BaseWindowAnimation _animation;

    public virtual Transform Position => transform;
    protected List<int> IDs => _ids;

    public bool Static
    {
        get => _static;
        set => _static = value;
    }
    public bool EqualId(int id) => _ids.Contains(id);

    public virtual void OnEnable()
    {
        foreach (var id in _ids)
        {
            _anchorsProvider.AddAnchor(id,this);
        }
    }

    public virtual void OnDisable()
    {
        foreach (var id in _ids)
        {
            _anchorsProvider.RemoveAnchor(id, this);
        }
    }

    public async UniTask PlayOpenAnimation()
    {
        if (_static)
            return;
        if(gameObject.activeSelf)
            return;
        gameObject.SetActive(true);
        await _animation.OpenAnimationAsync(destroyCancellationToken);
    }

    public async UniTask PlayCloseAnimation()
    {
        if (_static)
            return;
        if(gameObject.activeSelf == false)
            return;
        await _animation.CloseAnimationAsync(destroyCancellationToken);
        gameObject.SetActive(false);
    }
}