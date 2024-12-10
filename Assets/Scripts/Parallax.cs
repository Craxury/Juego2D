using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    public float effectParallax;
    private Transform camera;
    private Vector3 lastPositionCamera;
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main.transform;
        lastPositionCamera = camera.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 distance = camera.position - lastPositionCamera;
        transform.position += new Vector3 (effectParallax * distance.x, 0, 0);
        lastPositionCamera = camera.position;
    }
}
