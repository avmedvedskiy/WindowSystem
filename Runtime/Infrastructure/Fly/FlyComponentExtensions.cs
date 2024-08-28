using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace UISystem
{
    public static class FlyComponentExtensions
    {
        public static IFlyComponent SetActive(this IFlyComponent view, bool value)
        {
            view.transform.gameObject.SetActive(value);
            return view;
        }
        
        public static IFlyComponent SetParent(this IFlyComponent view, Transform parent)
        {
            view.transform.SetParent(parent,true);
            return view;
        }

        public static async UniTask FlyAsync(this IFlyComponent view, Transform to, CancellationToken cancellationToken = default)
        {
            await view.MoveToTarget(to, cancellationToken);
        }
        
        public static async UniTask FlyAsync(this IFlyComponent view,Transform from, Transform to, CancellationToken cancellationToken = default)
        {
            await view.MoveToTarget(from, to, cancellationToken);
        }

        public static async UniTask AndActivateView(this UniTask task, IFlyComponent view)
        {
            view.transform.gameObject.SetActive(true);
            await task;
            view.transform.gameObject.SetActive(false);
        }
    }
}