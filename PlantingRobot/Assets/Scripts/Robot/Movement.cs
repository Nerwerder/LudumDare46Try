using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    public bool rotateToMouse = true;   //TODO: make it possible to change the rotation mode

    // Update is called once per frame
    void Update() {
        RotateToCursor();

        float moveSpeed = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(transform.worldToLocalMatrix.MultiplyVector(transform.forward) * moveSpeed);

        //transform.Translate(Input.GetAxis("Vertical") * Time.deltaTime * speed, 0f, -Input.GetAxis("Horizontal") * Time.deltaTime * speed);
    }

    void RotateToCursor() {
        //Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        //Get the angle between the points
        float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        //Apply Angle
        transform.rotation = Quaternion.Euler(new Vector3(0f, -angle, 0f));
    }

    float AngleBetweenTwoPoints(Vector3 a, Vector3 b) {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}
