using GeneralSctipts;
using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.UI;
using GeneralSctipts;
using UnityEngine.SceneManagement;
using Scripts.SoundManager;

namespace MenuScripts
{
    public enum CameraState
    {
        Diagonal,
        Front,
        Left
    }

    public class MeuCamerasController : MonoBehaviour
    {
        private event Action<CameraState> OnStateChange;

        // Cameras
        [SerializeField] private CinemachineCamera diagonalCamera;
        [SerializeField] private CinemachineCamera frontCamera;
        [SerializeField] private CinemachineCamera leftCamera;

        // Sound Click Volume
        [SerializeField, Range(0f,1f)] private float clickVolume = 0.3f;

        // Front
        [SerializeField] private Button playBtn;
        [SerializeField] private Button continueBtn;
        [SerializeField] private Button settingsBtn;
        [SerializeField] private Button quitBtn;

        // Left
        [SerializeField] private Button backBtn;

        
        private CameraState _currentState; // не обращаться 
        private CameraState CurrentState
        {
            set { _currentState = value; OnStateChange?.Invoke(_currentState); }
            get { return _currentState; }
        }
        private void Awake()
        {
            playBtn.onClick.AddListener(() =>
            {
                SoundManager.Play(Sound.MenuTVSound, volume: clickVolume);

                switch (CurrentState)
                {
                    case CameraState.Diagonal:
                        ChangeCurrentState(CameraState.Front);
                        break;
                    case CameraState.Front:
                        SceneController.LoadScene(1);
                        break;
                }
            });

            continueBtn.onClick.AddListener(() =>
            {
                SoundManager.Play(Sound.MenuTVSound, volume: clickVolume);

                switch (CurrentState)
                {
                    case CameraState.Diagonal:
                        ChangeCurrentState(CameraState.Front);
                        break;
                    case CameraState.Front:
                        SceneController.LoadScene(2);
                        break;
                }
            });

            settingsBtn.onClick.AddListener(() =>
            {
                SoundManager.Play(Sound.MenuTVSound, volume: clickVolume);

                switch (CurrentState)
                {
                    case CameraState.Diagonal:
                        ChangeCurrentState(CameraState.Front);
                        break;
                    case CameraState.Front:
                        ChangeCurrentState(CameraState.Left);
                        break;
                }
            });

            quitBtn.onClick.AddListener(() =>
            {
                SoundManager.Play(Sound.MenuTVSound, volume: clickVolume);

                switch (CurrentState)
                {
                    case CameraState.Diagonal:
                        ChangeCurrentState(CameraState.Front);
                        break;
                    case CameraState.Front:
                        SceneController.Quit();
                        break;
                }
            });

            backBtn.onClick.AddListener(() =>
            {
                SoundManager.Play(Sound.MenuTVSound, volume: clickVolume);

                switch (CurrentState)
                {
                    case CameraState.Diagonal:
                        ChangeCurrentState(CameraState.Left);
                        break;
                    case CameraState.Left:
                        ChangeCurrentState(CameraState.Front);
                        break;
                }
            });
        }

        private void ChangeCurrentState(CameraState state)
        {
            if (CurrentState == state) return;

            switch (state)
            {
                case CameraState.Diagonal:
                    diagonalCamera.Priority = 1;
                    frontCamera.Priority = 0;
                    leftCamera.Priority = 0;
                    break;
                case CameraState.Front:
                    diagonalCamera.Priority = 0;
                    frontCamera.Priority = 1;
                    leftCamera.Priority = 0;
                    break;
                case CameraState.Left:
                    diagonalCamera.Priority = 0;
                    frontCamera.Priority = 0;
                    leftCamera.Priority = 1;
                    break;
            }

            CurrentState = state;
        }
    }
}

