using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DSC.Actor;
using DSC.Core;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace DSC.Template.Actor.Input
{
    [RequireComponent(typeof(PlayerInput))]
    public class DSC_ActorInput : BaseActorInput
    {
        #region Variable

        #region Variable - Inspector
#pragma warning disable 0649

        [SerializeField] BaseActorController m_hBaseActor;

        [Header("Axis")]
        [Min(0)]
        [SerializeField] protected float m_fSensitivity = 3f;
        [Min(0)]
        [SerializeField] protected float m_fGravity = 3f;

#pragma warning restore 0649
        #endregion

        #region Variable - Property

        protected override BaseActorController baseActor { get { return m_hBaseActor; } }
        protected override Dictionary<InputButtonType, GetInputType> previousGetType { get { return m_dicPreviousGetType; } set { m_dicPreviousGetType = value; } }

        public override InputData inputData { get { return m_hInputData; } set { m_hInputData = value; } }

        public override event UnityAction<InputButtonType, GetInputType> onRunEventInput
        {
            add
            {
                m_hOnRunEventInput += value;
            }

            remove
            {
                m_hOnRunEventInput -= value;
            }
        }

        #endregion

        protected InputData m_hInputData;
        protected UnityAction<InputButtonType, GetInputType> m_hOnRunEventInput;

        protected Dictionary<InputButtonType, GetInputType> m_dicPreviousGetType = new Dictionary<InputButtonType, GetInputType>();

        protected PlayerInput m_hInput;
        protected InputAction m_hHorizontal;
        protected InputAction m_hVertical;
        protected InputAction m_hCursorX;
        protected InputAction m_hCursorY;

        #endregion

        #region Base - Mono

        protected virtual void Awake()
        {
            m_hInput = GetComponent<PlayerInput>();
            InitInputAction();
        }

        protected virtual void Start()
        {
            m_hBaseActor.RegisterBaseInput(this);
        }

        protected virtual void Update()
        {
            UpdateInputMoveAxis();
        }

        #endregion

        #region Base - Override

        protected override void RunEventInput(InputButtonType eEventType, GetInputType eGetType)
        {
            m_hOnRunEventInput?.Invoke(eEventType, eGetType);
        }

        protected override void SetHoldingInputData(InputButtonType eButtonType, GetInputType eGetType)
        {
            var eHoldingInput = m_hInputData.m_eHoldingInput;

            if (eGetType == GetInputType.Down)
            {
                AddHoldFlag(eButtonType);
            }
            else if (eGetType == GetInputType.Up)
            {
                RemoveHoldFlag(eButtonType);
            }

            m_hInputData.m_eHoldingInput = eHoldingInput;
        }

        #endregion

        #region Events

        public void OnDPadUp(InputAction.CallbackContext hContext)
        {
            OnPressInput(InputButtonType.DPadUp, hContext.ReadValueAsButton());
        }

        public void OnDPadDown(InputAction.CallbackContext hContext)
        {
            OnPressInput(InputButtonType.DPadDown, hContext.ReadValueAsButton());
        }

        public void OnDPadLeft(InputAction.CallbackContext hContext)
        {
            OnPressInput(InputButtonType.DPadLeft, hContext.ReadValueAsButton());
        }

        public void OnDPadRight(InputAction.CallbackContext hContext)
        {
            OnPressInput(InputButtonType.DPadRight, hContext.ReadValueAsButton());
        }

        public void OnNorth(InputAction.CallbackContext hContext)
        {
            OnPressInput(InputButtonType.North, hContext.ReadValueAsButton());
        }

        public void OnSouth(InputAction.CallbackContext hContext)
        {
            OnPressInput(InputButtonType.South, hContext.ReadValueAsButton());
        }

        public void OnWest(InputAction.CallbackContext hContext)
        {
            OnPressInput(InputButtonType.West, hContext.ReadValueAsButton());
        }

        public void OnEast(InputAction.CallbackContext hContext)
        {
            OnPressInput(InputButtonType.East, hContext.ReadValueAsButton());
        }

        public void OnL1(InputAction.CallbackContext hContext)
        {
            OnPressInput(InputButtonType.L1, hContext.ReadValueAsButton());
        }

        public void OnL2(InputAction.CallbackContext hContext)
        {
            OnPressInput(InputButtonType.L2, hContext.ReadValueAsButton());
        }

        public void OnL3(InputAction.CallbackContext hContext)
        {
            OnPressInput(InputButtonType.L3, hContext.ReadValueAsButton());
        }

        public void OnR1(InputAction.CallbackContext hContext)
        {
            OnPressInput(InputButtonType.R1, hContext.ReadValueAsButton());
        }

        public void OnR2(InputAction.CallbackContext hContext)
        {
            OnPressInput(InputButtonType.R2, hContext.ReadValueAsButton());
        }

        public void OnR3(InputAction.CallbackContext hContext)
        {
            OnPressInput(InputButtonType.R3, hContext.ReadValueAsButton());
        }

        public void OnStart(InputAction.CallbackContext hContext)
        {
            OnPressInput(InputButtonType.Start, hContext.ReadValueAsButton());
        }

        public void OnSelect(InputAction.CallbackContext hContext)
        {
            OnPressInput(InputButtonType.Select, hContext.ReadValueAsButton());
        }

        public void OnConfirm(InputAction.CallbackContext hContext)
        {
            OnPressInput(InputButtonType.Confirm, hContext.ReadValueAsButton());
        }

        public void OnCancel(InputAction.CallbackContext hContext)
        {
            OnPressInput(InputButtonType.Cancel, hContext.ReadValueAsButton());
        }

        #endregion

        #region Main

        protected virtual void UpdateInputMoveAxis()
        { 
            if(m_hHorizontal != null)
                InputUtility.CalculateAxis(ref m_hInputData.m_fHorizontal, m_hHorizontal.ReadValue<float>(), m_fSensitivity, m_fGravity, Time.deltaTime);
            
            if(m_hVertical != null)
                InputUtility.CalculateAxis(ref m_hInputData.m_fVertical, m_hVertical.ReadValue<float>(), m_fSensitivity, m_fGravity, Time.deltaTime);

            if (m_hCursorX != null)
                InputUtility.CalculateAxis(ref m_hInputData.m_fCursorX, m_hCursorX.ReadValue<float>(), m_fSensitivity, m_fGravity, Time.deltaTime);

            if (m_hCursorY != null)
                InputUtility.CalculateAxis(ref m_hInputData.m_fCursorY, m_hCursorY.ReadValue<float>(), m_fSensitivity, m_fGravity, Time.deltaTime);
        }

        #endregion

        #region Helper

        protected virtual void InitInputAction()
        {
            var hAction = m_hInput.actions;
            var hMap = hAction.FindActionMap("Gameplay");
            m_hHorizontal = hMap.FindAction("Horizontal");
            m_hVertical = hMap.FindAction("Vertical");
            m_hCursorX = hMap.FindAction("CursorX");
            m_hCursorY = hMap.FindAction("CursorY");
        }

        protected virtual void AddHoldFlag(InputButtonType eButtonType)
        {
            m_hInputData.m_eHoldingInput |= eButtonType;
        }

        protected virtual void RemoveHoldFlag(InputButtonType eButtonType)
        {
            m_hInputData.m_eHoldingInput &= ~eButtonType;
        }

        #endregion
    }
}