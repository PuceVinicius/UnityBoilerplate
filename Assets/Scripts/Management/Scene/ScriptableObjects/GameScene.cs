using Boilerplate.Attributes;
using Boilerplate.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Boilerplate.SceneManagement
{
    [CreateAssetMenu(fileName = "New GameSceneData", menuName = "ScriptableObjects/GameScene/Data")]
    public class GameScene : DescriptionScriptableObject
    {
        #region Variables

        [Foldout("References")]
        [SerializeField] private AssetReference _sceneReference;
        [SerializeField] private bool _isPermanent;
        [SerializeField] private bool _hasLoadingScreen;

        #endregion Variables

        #region Properties

        public AssetReference SceneReference => _sceneReference;
        public bool IsPermanent => _isPermanent;
        public bool HasLoadingScreen => _hasLoadingScreen;

        #endregion Properties
    }
}