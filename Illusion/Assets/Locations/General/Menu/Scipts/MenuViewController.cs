using GeneralSctipts;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Scripts.Menu
{

    public class MenuViewController : MonoBehaviour
    {
        [SerializeField] private MenuView menuView;

        [SerializeField] private InputActionAsset inputActions;

        private InputAction _menuBtn;

        private MenuState _currentState = MenuState.Closed;

        private void Awake()
        {
            _menuBtn = inputActions.FindAction("MenuBtn");

            _menuBtn.started += (_) =>
            {
                if (_currentState == MenuState.Closed)
                {
                    OpenMenu();
                }
                else
                {
                    CloseMenu();
                }
            };

            menuView.OnContinueBtn += () =>
            {
                CloseMenu();

                SoundManager.SoundManager.Play(SoundManager.Sound.UIClick);
            };

            menuView.OnSettingsBtn += () =>
            {
                SoundManager.SoundManager.Play(SoundManager.Sound.VineBoom);
            };

            menuView.OnQuitBtn += () =>
            {
                SoundManager.SoundManager.Play(SoundManager.Sound.UIClick);

                SceneController.LoadScene(0);
            };

            menuView.OnCrossBtn += () =>
            {
                CloseMenu();

                SoundManager.SoundManager.Play(SoundManager.Sound.UIClick);
            };

            menuView.OnMinusBtn += () =>
            {
                SoundManager.SoundManager.Play(SoundManager.Sound.VineBoom);
            };

            menuView.OnContinueBtnSelect += (_) => { menuView.SetContinueGif(); SoundManager.SoundManager.Play(SoundManager.Sound.UISelect); };
            menuView.OnSettingsBtnSelect += (_) => { menuView.SetSettingsGif();SoundManager.SoundManager.Play(SoundManager.Sound.UISelect); };
            menuView.OnQuitBtnSelect += (_) => { menuView.SetQuitGif();SoundManager.SoundManager.Play(SoundManager.Sound.UISelect); };
            menuView.OnCrossBtnSelect += (_) => { SoundManager.SoundManager.Play(SoundManager.Sound.UISelect); };
            menuView.OnMinusBtnSelect += (_) => { SoundManager.SoundManager.Play(SoundManager.Sound.UISelect); };

            menuView.SetActive(false);
        }

        private void OpenMenu()
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            //Time.timeScale = 0;

            menuView.SetActive(true);
            _currentState = MenuState.Open;
        }

        private void CloseMenu()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;

            Time.timeScale = 1;

            menuView.SetActive(false);
            _currentState = MenuState.Closed;
        }


    }



    public enum MenuState
    {
        Closed,
        Open
    }
}