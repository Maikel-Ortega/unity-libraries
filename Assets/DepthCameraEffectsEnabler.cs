using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class DepthCameraEffectsEnabler : MonoBehaviour 
{
    Camera cam;

    void OnEnable()
    {
        if(cam == null)
        {
            cam = GetComponent<Camera>();    
        }
        cam.depthTextureMode = DepthTextureMode.DepthNormals;
    }
}
