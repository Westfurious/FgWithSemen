using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeRoom : MonoBehaviour
{
    public int camSize;
    public Vector3 cameraChangePos;
    public Vector3 playerChangePos;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<Camera>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position += playerChangePos;
            cam.transform.position += cameraChangePos;
            cam.orthographicSize = camSize;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
