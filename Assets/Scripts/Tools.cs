using UnityEngine;

public static class Tools
{
	public static void SetMaterialAlpha(Renderer renderer, float alpha) {
        foreach (Material m in renderer.materials) {
            // modify color alpha
            m.SetColor("_Color", new Color(m.color.r, m.color.g, m.color.b, alpha));

            if (alpha == 1)
                m.SetFloat("_Mode", 2);// set render mode to Fade
            else
                m.SetFloat("_Mode", 0);// set render mode to Opaque

            // Controls whether pixels from this object are written to the depth buffer (default is On).
            // If you’re drawng solid objects, leave this on.
            // If you’re drawing semitransparent effects, switch to ZWrite Off. For more details read below.
            m.SetInt("_ZWrite", alpha == 1 ? 1 : 0);
            if (alpha == 1)
                m.renderQueue = -1; // default renderQueue
            else
                m.renderQueue = 3000; // transparent renderQueue

            // There is a known problem when setting the Render Mode from script.
            // You must also update all other properties as well to make the change take effect.
            m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            m.DisableKeyword("_ALPHATEST_ON");
            m.EnableKeyword("_ALPHABLEND_ON");
            m.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        }
    }
}