using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEditor;



public class image : MonoBehaviour
{

    public bool k1;

    private PIXEL_FORMAT mPixelFormat = PIXEL_FORMAT.UNKNOWN_FORMAT;
    public Sprite s1;

    public Texture2D graph;
    // Start is called before the first frame update
    void Start()
    {
        graph = new Texture2D(256, 256, TextureFormat.RGB24, false);


        mPixelFormat = PIXEL_FORMAT.GRAYSCALE;

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            k1 = true;


            if (k1)
            {

                if (CameraDevice.Instance.SetFrameFormat(PIXEL_FORMAT.GRAYSCALE, true))
                {
                    Debug.Log("Successfully registered pixel format " + mPixelFormat.ToString());
                }
                else
                {
                    Debug.LogError("\nFailed to register pixel format: " + mPixelFormat.ToString() + "\nThe format may be unsupported by your device." + "\nConsider using a different pixel format.\n");
                    return;
                }

                Image image = CameraDevice.Instance.GetCameraImage(PIXEL_FORMAT.GRAYSCALE);
                k1 = false;

                byte[] pixels = image.Pixels;
                if (pixels.Length == 0)
                {
                    Debug.Log(" 0");
                }
                Debug.Log("" + pixels[0]);
                image.CopyToTexture(graph);
                Debug.Log("found image");
                Debug.Log("" + graph.GetPixels());
                var bytes = graph.EncodeToPNG();
                Debug.Log("" + bytes);
                System.IO.File.WriteAllBytes("ImageSaveTest1.png", bytes);
                Debug.Log("doune image");



            }
        }
    }


}
