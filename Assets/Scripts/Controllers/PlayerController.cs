using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controllers
{
    public class PlayerController : MonoBehaviour
    {
        private IPlayerControl _player;
        private PlayerInput _input;
        
        private void Awake()
        {
            _player = GetComponent<IPlayerControl>() ?? throw new NullReferenceException();
            InitInputHandler();
        }

        private void InitInputHandler()
        {
            _input = new PlayerInput();
            _input.Player.MoveHorizontal.performed += MoveHandler;
            _input.Player.MoveVertical.performed += MoveHandler;
            _input.Player.MoveHorizontal.canceled += MoveHandler;
            _input.Player.MoveVertical.canceled += MoveHandler;
            _input.Player.RotateVertical.performed += RotateVerticalHandler;
            _input.Player.RotateHorizontal.performed += RotateHorizontalHandler;
            _input.Player.RotateVertical.canceled += RotateVerticalHandler;
            _input.Player.RotateHorizontal.canceled += RotateHorizontalHandler;
            _input.Player.UseItem.performed += UseItemHandler;
            _input.Player.Left.performed += UILeftHandler;
            _input.Player.Right.performed += UIRightHandler;
        }

        private void MoveHandler(InputAction.CallbackContext context)
        {
            var dir = new Vector2()
            {
                x = _input.Player.MoveHorizontal.ReadValue<float>(),
                y = _input.Player.MoveVertical.ReadValue<float>()
            };
            
            _player.Move(dir);
        }

        private void RotateHorizontalHandler(InputAction.CallbackContext context)
        {
            _player.RotateX(context.ReadValue<float>());
        }

        private void RotateVerticalHandler(InputAction.CallbackContext context)
        {
            _player.RotateY(context.ReadValue<float>() * -1);
        }

        private void UILeftHandler(InputAction.CallbackContext context) => _player.UILeft();

        private void UIRightHandler(InputAction.CallbackContext context) => _player.UIRight();

        private void UseItemHandler(InputAction.CallbackContext context)
        {
            _player.UseItem();
        }

        private void OnEnable()
        {
            _input.Enable();
        }

        private void OnDisable()
        {
            _input.Player.MoveHorizontal.performed -= MoveHandler;
            _input.Player.MoveVertical.performed -= MoveHandler;
            _input.Player.MoveHorizontal.canceled -= MoveHandler;
            _input.Player.MoveVertical.canceled -= MoveHandler;
            _input.Player.RotateVertical.performed -= RotateVerticalHandler;
            _input.Player.RotateHorizontal.performed -= RotateHorizontalHandler;
            _input.Player.UseItem.performed -= UseItemHandler;
            _input.Player.Left.performed -= UILeftHandler;
            _input.Player.Right.performed -= UIRightHandler;
            _input.Disable();
        }
    }

    internal interface IPlayerControl
    {
        void Move(Vector2 direction);
        void RotateX(float direction);
        void RotateY(float direction);
        void UseItem();
        void UILeft();
        void UIRight();
    }
}
