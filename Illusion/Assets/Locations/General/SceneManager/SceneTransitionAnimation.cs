using DG.Tweening;
using PSX;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace GeneralSctipts
{
    public class SceneTransitionAnimation : MonoBehaviour
    {
        [Header("FadeOut")]
        [SerializeField] private float animationPixelizationFadeOutTime;
        [SerializeField] private float animationPostExposureFadeOutTime;

        [Header("FadeIn")]
        [SerializeField] private Vector2 pixelizationEndValue;
        [SerializeField] private float postExposureEndValue;

        [SerializeField] private float animationPixelizationFadeInTime;
        [SerializeField] private float animationPostExposureFadeInTime;

        [SerializeField] private Volume volume;

        private Pixelation _pixelation;
        private ColorAdjustments _colorAdjustments;

        private float _constPixelizationWidth;
        private float _constPixelizationHeight;

        private void Awake()
        {
            volume.profile.TryGet<Pixelation>(out _pixelation);
            volume.profile.TryGet<ColorAdjustments>(out _colorAdjustments);

            if (_pixelation == null) Debug.Log("Не удалось взять Pixelation!");
            if (_colorAdjustments == null) Debug.Log("Не удалось взять ColorAdjustments!");
        }

        public IEnumerator PlayAnimation(bool isFadingOut)
        {
            float animationTime = (isFadingOut) ? animationPixelizationFadeOutTime : animationPixelizationFadeInTime;

            float widthTo = (isFadingOut) ? 0 : pixelizationEndValue.x;
            float heightTo = (isFadingOut) ? 0 : pixelizationEndValue.y;

            float postExposureTo = (isFadingOut) ? postExposureEndValue : 0;

            float animationExposureTime = (isFadingOut) ? animationPostExposureFadeOutTime : animationPostExposureFadeInTime;

            DOTween.To(
                () => _pixelation.widthPixelation.value,                    // Геттер
                x => _pixelation.widthPixelation.Override(x),               // Сеттер с Override
                widthTo,                                                    // Конечное значение
                animationTime                                               // Длительность
            );

            DOTween.To(
                () => _pixelation.heightPixelation.value,                   // Геттер
                x => _pixelation.heightPixelation.Override(x),              // Сеттер с Override
                heightTo,                                                   // Конечное значение
                animationTime                                               // Длительность
            );

            DOTween.To(
                () => _colorAdjustments.postExposure.value,                 // Геттер
                x => _colorAdjustments.postExposure.Override(x),            // Сеттер с Override
                postExposureTo,                                             // Конечное значение
                animationExposureTime                                       // Длительность
            );

            if (!isFadingOut)
            {
                DOTween.To(
                    () => _pixelation.colorPrecision.value,                 // Геттер
                    x => _pixelation.colorPrecision.Override(x),            // Сеттер с Override
                    100,                                                    // Конечное значение
                    animationTime                                           // Длительность
                );
            }

            if (isFadingOut)
            {
                yield return new WaitUntil(() => _pixelation.widthPixelation.value <= 2);
                yield return new WaitUntil(() => _pixelation.heightPixelation.value <= 2);
                yield return new WaitUntil(() => _colorAdjustments.postExposure.value <= postExposureTo);
            }
        }
    }
}

