using Cysharp.Threading.Tasks;

namespace Code.Infrastructure.Async.Service
{
  public interface IAsyncService
  {
    UniTask NextFrame();
  }
}