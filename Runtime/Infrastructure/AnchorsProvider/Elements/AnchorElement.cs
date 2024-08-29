using Cysharp.Threading.Tasks;
using UISystem;
using UnityEngine;

/// <summary>
/// Базовый якорь, служит примером для других якорей
/// </summary>
public class AnchorElement : MonoBehaviour, IAnchor
{
    private readonly IAnchorsProvider _anchorsProvider = IAnchorsProvider.Current;
    #if VALUE_STORAGE_ENABLED
    [ValueStorage.ConstValue("AnchorNameContainer")] 
    #endif
    [SerializeField] private int _id;
    [SerializeField] private bool _static;
    [SerializeField] private BaseWindowAnimation _animation;
    protected int Id => _id;
    public virtual Transform Position => transform;

    public bool Static
    {
        get => _static;
        set => _static = value;
    }
    
    public bool EqualId(int id) => _id == id;

    public virtual void OnEnable()
    {
        _anchorsProvider.AddAnchor(_id,this);
    }

    public virtual void OnDisable()
    {
        _anchorsProvider.RemoveAnchor(_id,this);
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