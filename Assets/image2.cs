using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using OpenCvSharp;

public class image2 : MonoBehaviour
{

    private PIXEL_FORMAT mPixelFormat = PIXEL_FORMAT.UNKNOWN_FORMAT;
    private bool mAccessCameraImage = true;

    // public RawImage sourceRawImage;
    //  public RawImage targetRawImage;

    private bool image_exist;
    private bool mFormatRegistered = false;
    public Texture2D graph;

    // Start is called before the first frame update
    private void OnVuforiaStarted()
    {
        // Try register camera image format
        if (CameraDevice.Instance.SetFrameFormat(mPixelFormat, true))
        {
            Debug.Log("Successfully registered pixel format " + mPixelFormat.ToString());
            mFormatRegistered = true;
        }
        else
        {
            Debug.LogError("Failed to register pixel format " + mPixelFormat.ToString() +
                "\n the format may be unsupported by your device;" +
                "\n consider using a different pixel format.");
            mFormatRegistered = false;
        }
    }
    void OnTrackablesUpdated()
    {
        if (mFormatRegistered)
        {
            if (mAccessCameraImage)
            {
                if (Input.GetKeyDown("space"))
                {
                    Vuforia.Image image = CameraDevice.Instance.GetCameraImage(mPixelFormat);
                    image.CopyToTexture(graph);
                    Debug.Log("found image" + image.Pixels.Length);
                    //  Debug.Log("" + graph.GetPixels());
                    var bytes = graph.EncodeToPNG();
                    Debug.Log("" + bytes);
                    System.IO.File.WriteAllBytes("ImageSaveTest1.png", bytes);
                    Debug.Log("doune image");
                    image_exist = true;

                    if (image != null)
                    {
                        Debug.Log(
                            "\nImage Format: " + image.PixelFormat +
                            "\nImage Size:   " + image.Width + "x" + image.Height +
                            "\nBuffer Size:  " + image.BufferWidth + "x" + image.BufferHeight +
                            "\nImage Stride: " + image.Stride + "\n"
                        );
                        byte[] pixels = image.Pixels;
                        if (pixels != null && pixels.Length > 0)
                        {
                            Debug.Log(
                                "\nImage pixels: " +
                                pixels[0] + ", " +
                                pixels[1] + ", " +
                                pixels[2] + ", ...\n"
                            );
                        }
                    }
                }
            }
        }
    }
    void OnPause(bool paused)
    {
        if (paused)
        {
            Debug.Log("App was paused");
            UnregisterFormat();
        }
        else
        {
            Debug.Log("App was resumed");
            RegisterFormat();
        }
    }
    /// <summary>
    /// Register the camera pixel format
    /// </summary>
    void RegisterFormat()
    {
        if (CameraDevice.Instance.SetFrameFormat(mPixelFormat, true))
        {
            Debug.Log("Successfully registered camera pixel format " + mPixelFormat.ToString());
            mFormatRegistered = true;
        }
        else
        {
            Debug.LogError("Failed to register camera pixel format " + mPixelFormat.ToString());
            mFormatRegistered = false;
        }
    }
    /// <summary>
    /// Unregister the camera pixel format (e.g. call this when app is paused)
    /// </summary>
    void UnregisterFormat()
    {
        Debug.Log("Unregistering camera pixel format " + mPixelFormat.ToString());
        CameraDevice.Instance.SetFrameFormat(mPixelFormat, false);
        mFormatRegistered = false;
    }
    void Start()
    {
        graph = new Texture2D(256, 256);


        Mat mainMat = new Mat(graph.height, graph.width, Cv2.CV_8UC3);
        Mat grayMat = new Mat();

        mPixelFormat = PIXEL_FORMAT.GRAYSCALE;
        VuforiaARController.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
        VuforiaARController.Instance.RegisterTrackablesUpdatedCallback(OnTrackablesUpdated);
        VuforiaARController.Instance.RegisterOnPauseCallback(OnPause);

    }

    void imageEdge()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (image_exist)
        {
            imageEdge();
        }

    }
}
