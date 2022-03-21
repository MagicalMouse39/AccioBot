using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxController : Controller
{
    protected override IEnumerator Apply(Environment environment)
    {
        // Fade out
        float startVal = RenderSettings.skybox.GetFloat("_Exposure");
        yield return this.StartCoroutine(this.Interpolate(.25f, startVal, 0f, this.UpdateExposureCallback));

        // Set Texture
        RenderSettings.skybox.SetFloat("_Rotation", environment.m_WorldRotation);
        RenderSettings.skybox.mainTexture = environment.m_Background;

        // Fade in
        startVal = RenderSettings.skybox.GetFloat("_Exposure");
        yield return this.StartCoroutine(this.Interpolate(.25f, startVal, 1f, this.UpdateExposureCallback));
    }

    private void UpdateExposureCallback(float value) =>
        RenderSettings.skybox.SetFloat("_Exposure", value);
}
