using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoodLight : MonoBehaviour
{

    public GameObject _lamp;
    public float blinkTime = 0.2f;
    public float lightPower = 20f;
    public float lightAlpha = 1.5f;
    Material _lampMaterial;
    Vector4 _defaultColor;
    Vector4 currentColor;

    // Start is called before the first frame update
    void Start()
    {
        _lampMaterial = _lamp.GetComponent<Renderer>().material;
        _defaultColor = _lampMaterial.GetVector("Vector4_9C65C2CC");
    }

    //Set LampColor, values between 0.0 and 1.0
    public void SetColor(Vector3 newColor)
    {
        Vector4 color = new Vector4(newColor.x * lightPower, newColor.y * lightPower, newColor.z * lightPower, lightAlpha);
        _lampMaterial.SetVector("Vector4_9C65C2CC", color);
        currentColor = color;
    }

    public void SetColor(Color newColor)
    {
        Vector4 color = new Vector4(newColor.r * lightPower, newColor.g * lightPower, newColor.b * lightPower, lightAlpha);
        _lampMaterial.SetVector("Vector4_9C65C2CC", color);
        currentColor = color;
    }

    //set color for x seconds
    public void SetColorForTime(Vector3 newColor, int sec)
    {
        Vector4 color = new Vector4(newColor.x * lightPower, newColor.y * lightPower, newColor.z * lightPower, lightAlpha);
        StartCoroutine(ColorForTime(color, sec));
    }

    public void SetColorForTime(Color newColor, int sec) {
        Vector4 color = new Vector4(newColor.r * lightPower, newColor.g * lightPower, newColor.b * lightPower, lightAlpha);
        StartCoroutine(ColorForTime(color, sec));
    }

    //blink color x times
    public void SetColorBlink(Vector3 newColor, int times)
    {
        Vector4 color = new Vector4(newColor.x * lightPower, newColor.y * lightPower, newColor.z * lightPower, lightAlpha);
        StartCoroutine(BlinkColor(color, times));
    }

    //blink color x times
    public void SetColorBlink(Color newColor, int times)
    {
        Vector4 color = new Vector4(newColor.r * lightPower, newColor.g * lightPower, newColor.b * lightPower, lightAlpha);
        StartCoroutine(BlinkColor(color, times));
    }

    IEnumerator ColorForTime(Vector4 color, int time)
    {
        Vector4 currentColor = _lampMaterial.GetVector("Vector4_9C65C2CC");
        _lampMaterial.SetVector("Vector4_9C65C2CC", color);

        yield return new WaitForSeconds(time);

        _lampMaterial.SetVector("Vector4_9C65C2CC", currentColor);
    }

    IEnumerator BlinkColor(Vector4 color, int times)
    {      
        for(int i = 0; i<times; i++)
        {
            _lampMaterial.SetVector("Vector4_9C65C2CC", color);
            yield return new WaitForSeconds(blinkTime);
            _lampMaterial.SetVector("Vector4_9C65C2CC", new Vector4(0.0f, 0.0f, 0.0f, 0.0f));
            yield return new WaitForSeconds(blinkTime);
        }

        _lampMaterial.SetVector("Vector4_9C65C2CC", currentColor);
    }

}
