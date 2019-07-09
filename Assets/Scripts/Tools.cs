using UnityEngine;

public static class Tools
{
	public static void SetMaterialAlpha(Renderer renderer, float alpha) {
		foreach (Material m in renderer.materials) {
			if (alpha == 1)
				m.SetFloat("_Mode", 0);// set render mode to Opaque
			else
				m.SetFloat("_Mode", 2);// set render mode to Fade

			// There is a known problem when setting the Render Mode from script.
			// You must also update all other properties as well to make the change take effect.
			if (alpha == 1) {
				m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
				m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
			} else {
				m.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
				m.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
			}

			// Controls whether pixels from this object are written to the depth buffer (default is On).
			// If you’re drawng solid objects, leave this on.
			// If you’re drawing semitransparent effects, switch to ZWrite Off. For more details read below.
			m.SetInt("_ZWrite", alpha == 1 ? 1 : 0);

			m.DisableKeyword("_ALPHATEST_ON");
			m.EnableKeyword("_ALPHABLEND_ON");
			m.DisableKeyword("_ALPHAPREMULTIPLY_ON");

			if (alpha == 1)
				m.renderQueue = -1; // default renderQueue
			else
				m.renderQueue = 3000; // transparent renderQueue

			// modify color alpha
			m.SetColor("_Color", new Color(m.color.r, m.color.g, m.color.b, alpha));
		}
	}
}