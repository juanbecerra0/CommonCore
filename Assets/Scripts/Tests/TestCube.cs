using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCube : MonoBehaviour
{
    // Public attributes (shows up in GameObject inspector window)

    [SerializeField]
    public float RotationDegreesPerSecond = 90f;

    [SerializeField]
    public float MovementDistanceScale = 2f;

    [SerializeField]
    public float MovementTimeScale = 2f;

    // Start is called before the first frame update
    void Start()
    {
        // Print to Unity log
        Debug.Log(name + ": Hello, world!");
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate cube over time
        transform.Rotate(0f, 0f, RotationDegreesPerSecond * Time.deltaTime);

        // Move cube up and down over time
        transform.position += new Vector3(0f, Mathf.Cos(Time.time) * MovementDistanceScale) * Time.deltaTime * MovementTimeScale;
    }
}
