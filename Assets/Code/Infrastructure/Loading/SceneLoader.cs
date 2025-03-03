using Cysharp.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;

namespace Code.Infrastructure.Loading
{
  public class SceneLoader : ISceneLoader
  {
    public void LoadScene(string name, Action onLoaded = null) =>
      Load(name, onLoaded).Forget();

    private async UniTaskVoid Load(string nextScene, Action onLoaded)
    {
      var waitNextScene = SceneManager.LoadSceneAsync(nextScene);
      while (!waitNextScene!.isDone)
        await UniTask.NextFrame();
      
      onLoaded?.Invoke();
    }
  }
}