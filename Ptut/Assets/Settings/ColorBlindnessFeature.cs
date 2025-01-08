using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class ColorBlindnessFeature : ScriptableRendererFeature
{
    [System.Serializable]
    public class Settings
    {
        public Material material;
    }

    public Settings settings = new Settings();
    private ColorBlindnessPass _colorBlindnessPass;

    public override void Create()
    {
        _colorBlindnessPass = new ColorBlindnessPass(settings.material);
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        if (settings.material != null)
        {
            renderer.EnqueuePass(_colorBlindnessPass);
        }
    }

    class ColorBlindnessPass : ScriptableRenderPass
    {
        private Material _material;

        public ColorBlindnessPass(Material material)
        {
            _material = material;
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            if (_material == null) return;

            CommandBuffer cmd = CommandBufferPool.Get("ColorBlindnessEffect");

            // Utilisation de cameraColorTarget pour les versions plus anciennes
            cmd.Blit(null, renderingData.cameraData.renderer.cameraColorTargetHandle, _material);

            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }
    }
}
