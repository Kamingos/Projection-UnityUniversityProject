using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class SimpleCrtFeature : ScriptableRendererFeature
{
    class SimpleCrtPass : ScriptableRenderPass
    {
        private Material crtMaterial;
        private RTHandle cameraColorTarget;

        public SimpleCrtPass(Material material)
        {
            crtMaterial = material;
            renderPassEvent = RenderPassEvent.BeforeRenderingPostProcessing;
        }

        public void SetTarget(RTHandle target)
        {
            cameraColorTarget = target;
        }

        public override void OnCameraSetup(CommandBuffer cmd, ref RenderingData renderingData)
        {
            ConfigureTarget(cameraColorTarget);
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (crtMaterial == null || cameraColorTarget == null)
                return;

            CommandBuffer cmd = CommandBufferPool.Get("Simple CRT");

            // Просто применяем эффект
            Blitter.BlitCameraTexture(cmd, cameraColorTarget, cameraColorTarget, crtMaterial, 0);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }
    }

    private SimpleCrtPass crtPass;
    private Material crtMaterial;

    public override void Create()
    {
        Debug.Log("Create CRT Feature");

        // Создаем материал из шейдера
        Shader crtShader = Shader.Find("PostEffect/CRT");
        if (crtShader != null)
        {
            Debug.Log($"Shader found: {crtShader.name}");
            crtMaterial = CoreUtils.CreateEngineMaterial(crtShader);
            crtPass = new SimpleCrtPass(crtMaterial);
        }
        else
        {
            Debug.LogError("CRT shader not found!");
        }
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (crtPass == null)
        {
            Debug.Log("CRT Pass is null");
            return;
        }

        if (!renderingData.cameraData.postProcessEnabled)
        {
            Debug.Log("Post processing disabled");
            return;
        }

        Debug.Log($"Adding CRT pass for camera: {renderingData.cameraData.camera.name}");

        crtPass.SetTarget(renderer.cameraColorTargetHandle);
        renderer.EnqueuePass(crtPass);
    }

    protected override void Dispose(bool disposing)
    {
        CoreUtils.Destroy(crtMaterial);
        base.Dispose(disposing);
    }
}