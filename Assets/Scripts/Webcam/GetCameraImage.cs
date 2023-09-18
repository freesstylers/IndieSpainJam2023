using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetCameraImage : MonoBehaviour
{
    WebCamTexture webCamTexture_;
    public string path_;
    public RawImage rawImage_;

    // Start is called before the first frame update
    void Start()
    {
        webCamTexture_ = new WebCamTexture();
        GetComponent<Renderer>().material.mainTexture = webCamTexture_;
        webCamTexture_.Play();
    }
}
