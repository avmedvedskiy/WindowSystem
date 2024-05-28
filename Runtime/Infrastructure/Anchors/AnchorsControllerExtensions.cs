using Cysharp.Threading.Tasks;

namespace UISystem
{
    public static class AnchorsControllerExtensions
    {
        public static async UniTask<(IAnchor from, IAnchor to)> ShowAnchor(this IAnchorsController anchorsController, int id1, int id2) =>
            await UniTask.WhenAll(
                anchorsController.ShowAnchor(id1), 
                anchorsController.ShowAnchor(id2));

        public static async UniTask HideAnchor(this IAnchorsController anchorsController, int id1, int id2) =>
            await UniTask.WhenAll(
                anchorsController.HideAnchor(id1),
                anchorsController.HideAnchor(id2)
            );
    }
}