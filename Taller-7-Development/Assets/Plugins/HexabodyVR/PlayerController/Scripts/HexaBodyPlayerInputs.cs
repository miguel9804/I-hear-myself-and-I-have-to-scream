using System;
using UnityEngine;

namespace HexabodyVR.PlayerController
{
    public class HexaBodyPlayerInputs : MonoBehaviour
    {
        [Header("Debugging")]
        public bool KeyboardDebug;
        public KeyCode CrouchKey = KeyCode.X;
        public KeyCode StandKey = KeyCode.Z;
        public KeyCode JumpKey = KeyCode.Space;

        public Vector2 MovementAxis;
        public Vector2 TurnAxis;

        public bool SprintRequiresDoubleClick;
        public bool SprintingPressed;
        public bool RecalibratePressed;

        public bool JumpPressed;
        public bool CrouchPressed;
        public bool StandPressed;

        internal PlayerInputState JumpState;
        internal PlayerInputState CrouchState;
        internal PlayerInputState StandState;

        void Update()
        {
            ResetState(ref CrouchState);
            ResetState(ref StandState);
            ResetState(ref JumpState);

            SetState(ref CrouchState, CrouchPressed || KeyboardDebug && Input.GetKey(CrouchKey));
            SetState(ref StandState, StandPressed || KeyboardDebug && Input.GetKey(StandKey));
            SetState(ref JumpState, JumpPressed || KeyboardDebug && Input.GetKey(JumpKey));
        }

        protected void ResetState(ref PlayerInputState buttonState)
        {
            buttonState.JustDeactivated = false;
            buttonState.JustActivated = false;
            buttonState.Value = 0f;
        }

        protected void SetState(ref PlayerInputState buttonState, bool pressed)
        {
            if (pressed)
            {
                if (!buttonState.Active)
                {
                    buttonState.JustActivated = true;
                    buttonState.Active = true;
                }
            }
            else
            {
                if (buttonState.Active)
                {
                    buttonState.Active = false;
                    buttonState.JustDeactivated = true;
                }
            }
        }
    }

    [Serializable]
    public struct PlayerInputState
    {
        public bool Active;
        public bool JustActivated;
        public bool JustDeactivated;
        public float Value;
    }
}