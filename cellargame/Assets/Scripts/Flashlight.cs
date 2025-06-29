using System.Collections;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    private Light flashlight;
    private bool lightOn = true;

    private float startIntensity;
    private float maxIntensity;
    private float minIntensity;

    private float angle = 0;
    private bool checkAngle = true;

    void Start()
    {
        flashlight = GetComponentInChildren<Light>();

        startIntensity = flashlight.intensity;
        maxIntensity = flashlight.intensity;
        minIntensity = flashlight.intensity - maxIntensity / 8;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ToggleLight();
        }

        Quaternion rotation = Quaternion.Euler(-angle, transform.localEulerAngles.y, transform.localEulerAngles.z);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, rotation, 15 * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (lightOn) Flicker();
    }

    private void Flicker()
    {
        flashlight.intensity += Random.Range(-5f, 5f);

        if (flashlight.intensity < minIntensity) flashlight.intensity = minIntensity;
        if (flashlight.intensity > maxIntensity) flashlight.intensity = maxIntensity;
    }

    public void ToggleLight()
    {
        lightOn = !lightOn;

        switch (lightOn)
        {
            case true:
                flashlight.intensity = Random.Range(minIntensity, maxIntensity);
                break;
            case false:
                flashlight.intensity = 0;
                break;
        }
    }

    public void SetLightAngle(FloatEvent ctx)
    {
        if (checkAngle) { angle = ctx.FloatValue; }
    }

    public void PageCollected(VoidEvent ve)
    {
        maxIntensity -= startIntensity / 32;
        minIntensity -= startIntensity / 8;
        StartCoroutine(TimedAngle(3));
    }

    IEnumerator TimedAngle(float time)
    {
        checkAngle = false;
        angle = -30f;
        yield return new WaitForSeconds(time);
        checkAngle = true;
    }
}