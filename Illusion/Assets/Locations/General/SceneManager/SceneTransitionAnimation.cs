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
        [SerializeField] private float animationFadeOutTime;
        [SerializeField] private float animationFadeInTime;
        [SerializeField] private Vector2 endValue;

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

            _constPixelizationWidth = _pixelation.widthPixelation.value;
            _constPixelizationHeight = _pixelation.heightPixelation.value;
        }

        public IEnumerator PlayAnimation(bool isFadingOut)
        {
            float animationTime = (isFadingOut) ? animationFadeOutTime : animationFadeInTime;

            float widthTo = (isFadingOut) ? endValue.x : _constPixelizationWidth;
            float heightTo = (isFadingOut) ? endValue.y : _constPixelizationHeight;

            float postExposureTo = (isFadingOut) ? -2.5f : 0;

            float animationExposureTime = animationTime + ((isFadingOut) ? 0.5f : (-animationTime+0.5f));

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

            if (isFadingOut)
            {
                yield return new WaitUntil(() => _pixelation.widthPixelation.value <= endValue.x);
                yield return new WaitUntil(() => _pixelation.heightPixelation.value <= endValue.y);
                yield return new WaitUntil(() => _colorAdjustments.postExposure.value <= postExposureTo);
            }
        }
    }
}

