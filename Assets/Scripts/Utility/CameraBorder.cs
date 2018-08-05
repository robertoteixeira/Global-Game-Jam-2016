using UnityEngine;
using System.Collections;

public class CameraBorder : MonoBehaviour {

    float camHeight;
    float camWidth;

    void OnDrawGizmos()
    {
        Camera cam = Camera.main;
        camHeight = 2f * cam.orthographicSize;
        camWidth = camHeight * cam.aspect;
        Gizmos.DrawWireCube(Vector3.zero, new Vector3(camWidth, camHeight, 0));
    }
}
