using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingController : MonoBehaviour
{
    Volume postProcessing;
    ChromaticAberration chromaticAberration;

    void Start()
    {
        postProcessing = GetComponent<Volume>();
        if (postProcessing.profile.TryGet<ChromaticAberration>(out ChromaticAberration cma)) chromaticAberration = cma;
    }

    public void PageCollected(VoidEvent ve)
    {
        chromaticAberration.intensity.value += 0.1f;
    }
}
