using Boilerplate.Attributes;
using Boilerplate.EventChannels;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Boilerplate.Input
{
    public partial class PlayerInputReader : MonoBehaviour
    {
        #region Variables

        [Foldout("References")]
        [SerializeField] private PlayerInput _playerInput;

        [Foldout("Broadcasters")]
        [SerializeField] private PlayerInputEventChannel _onControlsChangedEvent;
        [SerializeField] private VoidEventChannel _inputAnyEvent;

        [Foldout("Listeners")]
        [SerializeField] private BoolEventChannel _onToggleInputsEvent;
        [SerializeField] private StringEventChannel _onChangeInputSchemeEvent;

        #endregion Variables

        #region Messages

        private void OnEnable()
        {
            EventUtils.AddEventListener(_onToggleInputsEvent, OnToggleInputs);
            EventUtils.AddEventListener(_onChangeInputSchemeEvent, SwitchActionMap);
        }

        private void OnDisable()
        {
            EventUtils.RemoveEventListener(_onToggleInputsEvent, OnToggleInputs);
            EventUtils.RemoveEventListener(_onChangeInputSchemeEvent, SwitchActionMap);
        }

        #endregion Messages

        #region Methods

        private void SwitchActionMap(string actionMap)
        {
            _playerInput.SwitchCurrentActionMap(actionMap);
        }

        private void OnToggleInputs(bool value)
        {
            if (value)
                _playerInput.ActivateInput();
            else
                _playerInput.DeactivateInput();
        }

        #endregion Methods

        #region Input Callbacks

        public void OnControlsChanged()
        {
            if (!_playerInput)
                return;

            EventUtils.BroadcastEvent(_onControlsChangedEvent, _playerInput);
        }

        public void OnAnyInput(InputAction.CallbackContext context)
        {
            EventUtils.BroadcastEvent(_inputAnyEvent);
        }

        #endregion Input Callbacks
    }
}