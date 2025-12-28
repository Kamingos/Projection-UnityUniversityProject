using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[VolumeComponentMenu("Post-processing/CRT")]
public class Crt : VolumeComponent, IPostProcessComponent
{
    [Header("Scanlines")]
    [Tooltip("Intensity of scanlines")]
    public FloatParameter scanlinesWeight = new FloatParameter(1f, true);

    [Tooltip("Intensity of noise")]
    public FloatParameter noiseWeight = new FloatParameter(0.1f, true);

    [Header("Screen Distortion")]
    [Tooltip("Horizontal screen bend")]
    public FloatParameter screenBendX = new FloatParameter(1000.0f, true);

    [Tooltip("Vertical screen bend")]
    public FloatParameter screenBendY = new FloatParameter(1000.0f, true);

    [Header("Vignette")]
    [Tooltip("Vignette intensity")]
    public FloatParameter vignetteAmount = new FloatParameter(0.4f, true);

    [Tooltip("Vignette size")]
    public FloatParameter vignetteSize = new FloatParameter(1.2f, true);

    [Tooltip("Vignette rounding")]
    public FloatParameter vignetteRounding = new FloatParameter(2.0f, true);

    [Tooltip("Vignette smoothing")]
    public FloatParameter vignetteSmoothing = new FloatParameter(0.5f, true);

    [Header("Scanlines Settings")]
    [Tooltip("Scanlines density")]
    public FloatParameter scanlinesDensity = new FloatParameter(200.0f, true);

    [Tooltip("Scanlines speed")]
    public FloatParameter scanlinesSpeed = new FloatParameter(-10.0f, true);

    [Tooltip("Noise amount")]
    public FloatParameter noiseAmount = new FloatParameter(250.0f, true);

    [Header("Chromatic Aberration")]
    [Tooltip("Red channel shift")]
    public Vector2Parameter chromaticRed = new Vector2Parameter(Vector2.zero, true);

    [Tooltip("Green channel shift")]
    public Vector2Parameter chromaticGreen = new Vector2Parameter(Vector2.zero, true);

    [Tooltip("Blue channel shift")]
    public Vector2Parameter chromaticBlue = new Vector2Parameter(Vector2.zero, true);

    [Header("Grille Effect")]
    [Tooltip("Grille opacity")]
    public FloatParameter grilleOpacity = new FloatParameter(0.4f, true);

    [Tooltip("Counter grille opacity")]
    public FloatParameter grilleCounterOpacity = new FloatParameter(0.2f, true);

    [Tooltip("Grille resolution")]
    public FloatParameter grilleResolution = new FloatParameter(360.0f, true);

    [Tooltip("Counter grille resolution")]
    public FloatParameter grilleCounterResolution = new FloatParameter(540.0f, true);

    [Tooltip("Grille brightness")]
    public FloatParameter grilleBrightness = new FloatParameter(15.0f, true);

    [Tooltip("UV rotation")]
    public FloatParameter grilleUvRotation = new FloatParameter(90.0f, true);

    [Tooltip("UV midpoint")]
    public FloatParameter grilleUvMidPoint = new FloatParameter(0.5f, true);

    [Tooltip("Grille shift")]
    public Vector3Parameter grilleShift = new Vector3Parameter(Vector3.one, true);

    public bool IsActive()
    {
        return scanlinesWeight.value > 0f
            || noiseWeight.value > 0f
            || vignetteAmount.value > 0f
            || grilleOpacity.value > 0f;
    }

    public bool IsTileCompatible() => false;
}