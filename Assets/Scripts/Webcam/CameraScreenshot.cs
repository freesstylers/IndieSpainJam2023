using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScreenshot : MonoBehaviour
{
    [SerializeField]
    GetCameraImage CameraReference;
    [SerializeField]
    Renderer ObjectToRenderOnto;

    [SerializeField]
    int blurSize = 2;
    [SerializeField]
    int iterations = 2;
    // Start is called before the first frame update
    public void onClick()
    {
        StartCoroutine(ScreenShotCorroutine());
    }

    IEnumerator ScreenShotCorroutine()
    {
        yield return new WaitForEndOfFrame();

        Texture tex = CameraReference.GetComponent<Renderer>().material.mainTexture;
        Texture2D tex2D = new Texture2D((int)(tex.width/1.5f), (int)(tex.height/1.5f));
        tex2D.ReadPixels(new Rect(0, 0, tex2D.width, tex2D.height), 0, 0);
        tex2D.filterMode = FilterMode.Point;
        tex2D.Apply();

        //
        for (int i = 0; i < iterations; i++)
        {
            tex2D = BlurImage(tex2D, 2, true);
            tex2D = BlurImage(tex2D, 2, false);
        }
        //

        ObjectToRenderOnto.material.mainTexture = tex2D;
    }

    private float avgR = 0;
    private float avgG = 0;
    private float avgB = 0;
    private float avgA = 0;
    private float blurPixelCount = 0;

    Texture2D BlurImage(Texture2D image, int blurSize, bool horizontal)
    {

        Texture2D blurred = new Texture2D(image.width, image.height);
        int _W = image.width;
        int _H = image.height;
        int xx, yy, x, y;

        if (horizontal)
        {
            for (yy = 0; yy < _H; yy++)
            {
                for (xx = 0; xx < _W; xx++)
                {
                    ResetPixel();

                    //Right side of pixel

                    for (x = xx; (x < xx + blurSize && x < _W); x++)
                    {
                        AddPixel(image.GetPixel(x, yy));
                    }

                    //Left side of pixel

                    for (x = xx; (x > xx - blurSize && x > 0); x--)
                    {
                        AddPixel(image.GetPixel(x, yy));

                    }


                    CalcPixel();

                    for (x = xx; x < xx + blurSize && x < _W; x++)
                    {
                        blurred.SetPixel(x, yy, new Color(avgR, avgG, avgB, 1.0f));

                    }
                }
            }
        }

        else
        {
            for (xx = 0; xx < _W; xx++)
            {
                for (yy = 0; yy < _H; yy++)
                {
                    ResetPixel();

                    //Over pixel

                    for (y = yy; (y < yy + blurSize && y < _H); y++)
                    {
                        AddPixel(image.GetPixel(xx, y));
                    }
                    //Under pixel

                    for (y = yy; (y > yy - blurSize && y > 0); y--)
                    {
                        AddPixel(image.GetPixel(xx, y));
                    }
                    CalcPixel();
                    for (y = yy; y < yy + blurSize && y < _H; y++)
                    {
                        blurred.SetPixel(xx, y, new Color(avgR, avgG, avgB, 1.0f));

                    }
                }
            }
        }

        blurred.Apply();
        return blurred;
    }

    void AddPixel(Color pixel)
    {
        avgR += pixel.r;
        avgG += pixel.g;
        avgB += pixel.b;
        blurPixelCount++;
    }

    void ResetPixel()
    {
        avgR = 0.0f;
        avgG = 0.0f;
        avgB = 0.0f;
        blurPixelCount = 0;
    }

    void CalcPixel()
    {
        avgR = avgR / blurPixelCount;
        avgG = avgG / blurPixelCount;
        avgB = avgB / blurPixelCount;
    }
}
