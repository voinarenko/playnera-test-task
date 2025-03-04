using Code.Infrastructure.Async.Service;
using Cysharp.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.Loading
{
  public class SceneLoader : ISceneLoader
  {
    private readonly IAsyncService _asyncService;
    
    public SceneLoader(IAsyncService asyncService) =>
      _asyncService = asyncService;

    public void LoadScene(string name, Action onLoaded = null) =>
      Load(name, onLoaded).Forget();

    private async UniTaskVoid Load(string nextScene, Action onLoaded)
    {
      var waitNextScene = SceneManager.LoadSceneAsync(nextScene);
      while (!waitNextScene!.isDone)
        await _asyncService.NextFrame();
      
      onLoaded?.Invoke();
    }
  }
}