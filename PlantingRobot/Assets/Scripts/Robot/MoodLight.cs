using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoodLight : MonoBehaviour
{

    public GameObject _lamp;
    Material _lampMaterial;
    Vector4 _defaultColor;


    // Start is called before the first frame update
    void Start()
    {
        _lampMaterial = _lamp.GetComponent<Renderer>().material;
        _defaultColor = _lampMaterial.GetVector("Vector4_9C65C2CC");
        SetColorBlink(new Vector3(0.0f, 1.0f, 0.0f), 10);
    }

    //Set LampColor, values between 0.0 and 1.0
    public void SetColor(Vector3 newColor)
    {
        Vector4 color = new Vector4(newColor.x*40, newColor.y*40, newColor.z*40, 2.4f);
        _lampMaterial.SetVector("Vector4_9C65C2CC", color);
    }

    //set color for x seconds
    public void SetColorForTime(Vector3 newColor, int sec)
    {
        Vector4 color = new Vector4(newColor.x * 40, newColor.y * 40, newColor.z * 40, 2.4f);
        StartCoroutine(ColorForTime(color, sec));
    }

    //blink color x times
    public void SetColorBlink(Vector3 newColor, int times)
    {
        Vector4 color = new Vector4(newColor.x * 40, newColor.y * 40, newColor.z * 40, 2.4f);
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
        Vector4 currentColor = _lampMaterial.GetVector("Vector4_9C65C2CC");
       
        for(int i = 0; i<times; i++)
        {
            _lampMaterial.SetVector("Vector4_9C65C2CC", color);
            yield return new WaitForSeconds(0.5f);
            _lampMaterial.SetVector("Vector4_9C65C2CC", new Vector4(0.0f, 0.0f, 0.0f, 0.0f));
            yield return new WaitForSeconds(0.5f);
        }

        _lampMaterial.SetVector("Vector4_9C65C2CC", currentColor);
    }

}
