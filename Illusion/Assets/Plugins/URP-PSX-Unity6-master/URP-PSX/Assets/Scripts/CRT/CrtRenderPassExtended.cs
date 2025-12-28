using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering.RenderGraphModule;

public class CrtRenderPassExtended : ScriptableRenderPass
{
    private Material crtMaterial;
    private Crt crtVolume;

    private RTHandle cameraColorTarget;
    private RTHandle tempTexture;

    private const string PROFILER_TAG = "CRT Post Processing";
    private static readonly int MainTexId = Shader.PropertyToID("_MainTex");

    public CrtRenderPassExtended(Material material)
    {
        crtMaterial = material;
        renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
    }

    public void Setup(RTHandle colorHandle)
    {
        cameraColorTarget = colorHandle;
    }

    public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
    {
        ConfigureTarget(cameraColorTarget);
    }

    public override void Configure(CommandBuffer cmd, RenderTextureDescriptor cameraTextureDescriptor)
    {
        // Настройка временной текстуры
        RenderTextureDescriptor descriptor = cameraTextureDescriptor;
        descriptor.depthBufferBits = 0;
        descriptor.msaaSamples = 1;

        RenderingUtils.ReAllocateIfNeeded(ref tempTexture, descriptor,
            name: "_CRTTempTexture",
            wrapMode: TextureWrapMode.Clamp,
            filterMode: FilterMode.Bilinear);
    }

    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        if (crtMaterial == null || cameraColorTarget == null)
            return;

        var stack = VolumeManager.instance.stack;
        crtVolume = stack.GetComponent<Crt>();
        if (crtVolume == null || !crtVolume.IsActive())
            return;

        CommandBuffer cmd = CommandBufferPool.Get(PROFILER_TAG);
        using (new ProfilingScope(cmd, new ProfilingSampler(PROFILER_TAG)))
        {
            // Устанавливаем все параметры в материал
            SetMaterialParameters();

            // Устанавливаем основную текстуру
            crtMaterial.SetTexture(MainTexId, cameraColorTarget);

            // Блитуем с эффектом через временную текстуру
            Blitter.BlitCameraTexture(cmd, cameraColorTarget, tempTexture, crtMaterial, 0);
            Blitter.BlitCameraTexture(cmd, tempTexture, cameraColorTarget);
        }

        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }

    public override void OnCameraCleanup(CommandBuffer cmd)
    {
        // Очищаем временные ресурсы если нужно
        if (cmd != null)
        {
            cmd.ReleaseTemporaryRT(Shader.PropertyToID("_CRTTempTexture"));
        }
    }

    public void Cleanup()
    {
        tempTexture?.Release();
    }

    private void SetMaterialParameters()
    {
        if (crtMaterial == null || crtVolume == null) return;

        // Scanlines
        crtMaterial.SetFloat("_ScanlinesWeight", crtVolume.scanlinesWeight.value);
        crtMaterial.SetFloat("_NoiseWeight", crtVolume.noiseWeight.value);

        // Screen distortion
        crtMaterial.SetFloat("_ScreenBendX", crtVolume.screenBendX.value);
        crtMaterial.SetFloat("_ScreenBendY", crtVolume.screenBendY.value);

        // Vignette
        crtMaterial.SetFloat("_VignetteAmount", crtVolume.vignetteAmount.value);
        crtMaterial.SetFloat("_VignetteSize", crtVolume.vignetteSize.value);
        crtMaterial.SetFloat("_VignetteRounding", crtVolume.vignetteRounding.value);
        crtMaterial.SetFloat("_VignetteSmoothing", crtVolume.vignetteSmoothing.value);

        // Scanlines settings
        crtMaterial.SetFloat("_ScanLinesDensity", crtVolume.scanlinesDensity.value);
        crtMaterial.SetFloat("_ScanLinesSpeed", crtVolume.scanlinesSpeed.value);
        crtMaterial.SetFloat("_NoiseAmount", crtVolume.noiseAmount.value);

        // Chromatic aberration
        crtMaterial.SetVector("_ChromaticRed", crtVolume.chromaticRed.value);
        crtMaterial.SetVector("_ChromaticGreen", crtVolume.chromaticGreen.value);
        crtMaterial.SetVector("_ChromaticBlue", crtVolume.chromaticBlue.value);

        // Grille effect
        crtMaterial.SetFloat("_GrilleOpacity", crtVolume.grilleOpacity.value);
        crtMaterial.SetFloat("_GrilleCounterOpacity", crtVolume.grilleCounterOpacity.value);
        crtMaterial.SetFloat("_GrilleResolution", crtVolume.grilleResolution.value);
        crtMaterial.SetFloat("_GrilleCounterResolution", crtVolume.grilleCounterResolution.value);
        crtMaterial.SetFloat("_GrilleBrightness", crtVolume.grilleBrightness.value);
        crtMaterial.SetFloat("_GrilleUvRotation", crtVolume.grilleUvRotation.value);
        crtMaterial.SetFloat("_GrilleUvMidPoint", crtVolume.grilleUvMidPoint.value);
        crtMaterial.SetVector("_GrilleShift", crtVolume.grilleShift.value);
    }
}