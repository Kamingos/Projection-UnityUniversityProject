using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Scripts.Menu
{
    public class MenuView : MonoBehaviour
    {
        public event Action OnContinueBtn;
        public event Action OnSettingsBtn;
        public event Action OnQuitBtn;

        public event Action OnCrossBtn;
        public event Action OnMinusBtn;

        public event Action<PointerEventData> OnContinueBtnSelect;
        public event Action<PointerEventData> OnSettingsBtnSelect;
        public event Action<PointerEventData> OnQuitBtnSelect;
        public event Action<PointerEventData> OnCrossBtnSelect;
        public event Action<PointerEventData> OnMinusBtnSelect;

        [Header("Ѕлок кнопок")]
        [SerializeField] private GameObject menuTextObj;
        [SerializeField] private TMP_Text menuTextTMP;
        [SerializeField] private GameObject menuPanel;

        [Header("Ѕлок гифок")]
        [SerializeField] private GameObject continueGifObj;
        [SerializeField] private GameObject settingsGifObj;
        [SerializeField] private GameObject quitGifObj;

        [Header("Ѕлок кнопок")]
        [SerializeField] private Button continueBtn;
        [SerializeField] private Button settingsBtn;
        [SerializeField] private Button quitBtn;
        [SerializeField] private Button crossBtn;
        [SerializeField] private Button minusBtn;

        [SerializeField] private ButtonSelectListener conttinueBtnListener;
        [SerializeField] private ButtonSelectListener settingsBtnListener;
        [SerializeField] private ButtonSelectListener quitBtnListener;
        [SerializeField] private ButtonSelectListener crossBtnListener;
        [SerializeField] private ButtonSelectListener minusBtnListener;

        #region јнимаци€
        [Header("Ѕлок настроек анимации масштаба")]
        [SerializeField] private float scaleDeltaTime;
        [SerializeField] private float xKaef;
        [SerializeField] private float yKaef;

        [Header("Ѕлок настроек анимации текста")]
        [SerializeField] private float contentDeltaTime;
        #endregion

        private void Awake()
        {
            continueBtn.onClick.AddListener(() => OnContinueBtn?.Invoke());
            settingsBtn.onClick.AddListener(() => OnSettingsBtn?.Invoke());
            quitBtn.onClick.AddListener(() => OnQuitBtn?.Invoke());

            crossBtn.onClick.AddListener(() => OnCrossBtn?.Invoke());
            minusBtn.onClick.AddListener(() => OnMinusBtn?.Invoke());

            conttinueBtnListener.OnButtonSelected += (_) => OnContinueBtnSelect?.Invoke(_);
            settingsBtnListener.OnButtonSelected += (_) => OnSettingsBtnSelect?.Invoke(_);
            quitBtnListener.OnButtonSelected += (_) => OnQuitBtnSelect?.Invoke(_);
            crossBtnListener.OnButtonSelected += (_) => OnCrossBtnSelect?.Invoke(_);
            minusBtnListener.OnButtonSelected += (_) => OnMinusBtnSelect?.Invoke(_);

            StartCoroutine(TextPositionAnimation());
            StartCoroutine(TextContentAnimation());
        }

        public void SetActive(bool value) => menuPanel.SetActive(value);

        public void SetContinueGif()
        {
            continueGifObj.SetActive(true);
            settingsGifObj.SetActive(false);
            quitGifObj.SetActive(false);
        }

        public void SetSettingsGif()
        {
            continueGifObj.SetActive(false);
            settingsGifObj.SetActive(true);
            quitGifObj.SetActive(false);
        }

        public void SetQuitGif()
        {
            continueGifObj.SetActive(false);
            settingsGifObj.SetActive(false);
            quitGifObj.SetActive(true);
        }

        IEnumerator TextPositionAnimation()
        {
            WaitForSeconds wfs = new(scaleDeltaTime);

            float xTemp = 0;
            float yTemp = 0;

            while (true)
            {
                menuTextObj.transform.localScale += new Vector3((float)Math.Sin(xTemp) * xKaef, (float)Math.Sin(yTemp) * yKaef);

                xTemp += 0.1f;
                yTemp += 0.1f;

                yield return wfs;
            }
        }

        IEnumerator TextContentAnimation()
        {
            WaitForSeconds wfs = new(contentDeltaTime);

            while (true)
            {
                yield return wfs;

                menuTextTMP.text = "м≈Ќё";

                yield return wfs;

                menuTextTMP.text = "ћеЌё";

                yield return wfs;

                menuTextTMP.text = "ћ≈нё";

                yield return wfs;

                menuTextTMP.text = "ћ≈Ќю";

                yield return wfs;

                menuTextTMP.text = "м≈Ќю";

                yield return wfs;

                menuTextTMP.text = "ћеЌю";

                yield return wfs;

                menuTextTMP.text = "ћ≈ню";

                yield return wfs;

                menuTextTMP.text = "м≈ню";

                yield return wfs;

                menuTextTMP.text = "ћеню";

                yield return wfs;

                menuTextTMP.text = "меню";

                yield return wfs;

                menuTextTMP.text = "ћ≈Ќё";

                yield return wfs;

                menuTextTMP.text = "меню";

                yield return wfs;

                menuTextTMP.text = "ћ≈Ќё";

                yield return wfs;

                menuTextTMP.text = "меню";

                yield return wfs;

                menuTextTMP.text = "ћ≈Ќё";

                yield return wfs;
            }
        }
    }
}