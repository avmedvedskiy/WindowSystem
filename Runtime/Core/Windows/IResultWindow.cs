using Cysharp.Threading.Tasks;

namespace UISystem
{
    public interface IResultWindow<TResult>
    {
        UniTaskCompletionSource<TResult> Result { get;}
    }
}