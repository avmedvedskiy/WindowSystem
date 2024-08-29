using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UISystem
{
    public static class FlyComponentExtensions
    {
        public static IFlyComponent SetParent(this IFlyComponent view, Transform parent)
        {
            view.transform.SetParent(parent, true);
            return view;
        }
        
        public static async UniTask AndActivateView(this UniTask task,
            IFlyComponent view,
            CancellationToken cancellationToken)
        {
            view.transform.gameObject.SetActive(true);
            await task;
            if (!cancellationToken.IsCancellationRequested)
                view.transform.gameObject.SetActive(false);
        }
    }
}