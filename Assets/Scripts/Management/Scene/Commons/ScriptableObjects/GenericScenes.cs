using Boilerplate.Attributes;
using Boilerplate.Generic;
using UnityEngine;

namespace Boilerplate.SceneManagement
{
    [CreateAssetMenu(fileName = "New GenericScenes", menuName = "ScriptableObjects/GenericScenes")]
    public class GenericScenes : DescriptionScriptableObject
    {
        #region Variables

        [Foldout("References")]
        [SerializeField] private GameScene _managersScene;
        [SerializeField] private GameScene _onboardingScene;
        [SerializeField] private GameScene _menuScene;

        #endregion

        #region Properties

        public GameScene ManagersScene => _managersScene;
        public GameScene OnboardingScene => _onboardingScene;
        public GameScene MenuScene => _menuScene;

        #endregion
    }
}