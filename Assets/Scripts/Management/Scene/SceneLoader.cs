using System;
using System.Collections;
using Boilerplate.Attributes;
using Boilerplate.EventChannels;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

namespace Boilerplate.SceneManagement
{
    public class SceneLoader : MonoBehaviour
    {
        [Foldout("References")]
        [SerializeField] private GenericScenes _genericScenes;

        [Foldout("Listeners")]
        [SerializeField] private GameSceneEventChannel _loadSceneChannel;

        [Foldout("Broadcasters")]
        [SerializeField] private GameSceneEventChannel _onSceneLoadedChannel;
        [SerializeField] private BoolEventChannel _toggleLoadingScreenChannel;

        private GameScene _currentSceneLoaded = null;

        private bool _isLoading = false;
        private string _coldStartupSceneName = "";

        private void OnEnable()
        {
            EventUtils.AddEventListener(_loadSceneChannel, LoadScene);
        }

        private void OnDisable()
        {
            EventUtils.RemoveEventListener(_loadSceneChannel, LoadScene);
        }

        private void Start()
        {
#if UNITY_EDITOR
            if (Bootstrap.IsColdStartup)
                GetColdStartupScene();
#endif
        }

        private void LoadScene(GameScene nextScene)
        {
            if (_isLoading) return;

            StartCoroutine(LoadSceneFlow(nextScene));
        }

        private IEnumerator LoadSceneFlow(GameScene nextScene)
        {
            SetupUnload(nextScene);

            yield return StartCoroutine(UnloadPreviousScene());

            yield return StartCoroutine(LoadNextScene(nextScene));

            OnLoadEnd(nextScene);
        }

        private void SetupUnload(GameScene nextScene)
        {
            _isLoading = true;
            if (nextScene.HasLoadingScreen)
                EventUtils.BroadcastEvent(_toggleLoadingScreenChannel, true);
        }

        private void OnLoadEnd(GameScene nextScene)
        {
            _isLoading = false;
            if (nextScene.HasLoadingScreen)
                EventUtils.BroadcastEvent(_toggleLoadingScreenChannel, false);
            EventUtils.BroadcastEvent(_onSceneLoadedChannel, _currentSceneLoaded);
        }

        private IEnumerator UnloadPreviousScene()
        {
#if UNITY_EDITOR
            UnloadColdStartupScene();
#endif

            if (!_currentSceneLoaded
                || !_currentSceneLoaded.SceneReference.OperationHandle.IsValid()
                || _currentSceneLoaded.IsPermanent)
                yield break;

            var asyncUnload = _currentSceneLoaded.SceneReference.UnLoadScene();

            yield return asyncUnload;
        }

        private IEnumerator LoadNextScene(GameScene nextScene)
        {
            var asyncLoad = nextScene.SceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);

            yield return asyncLoad;

            if (asyncLoad.Status != AsyncOperationStatus.Succeeded)
                yield break;

            SceneManager.SetActiveScene(asyncLoad.Result.Scene);
            _currentSceneLoaded = nextScene;
        }
#if UNITY_EDITOR

        private void GetColdStartupScene()
        {
            _coldStartupSceneName = SceneManager.GetActiveScene().name;
        }

        private void UnloadColdStartupScene()
        {
            if (!Bootstrap.IsColdStartup)
                return;

            if (_coldStartupSceneName == "")
                return;

            SceneManager.UnloadSceneAsync(_coldStartupSceneName);
            _coldStartupSceneName = "";
        }
#endif
    }
}