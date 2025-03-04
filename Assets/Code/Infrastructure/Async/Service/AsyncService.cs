using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.Async.Service
{
  public class AsyncService : IAsyncService
  {
    public async UniTask NextFrame() =>
      await UniTask.NextFrame();
  }
}