namespace OpenCvSharp
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using OpenCvSharp;
    using OpenCvSharp.Aruco;
    using UnityEngine.UI;
    using OpenCvSharp.Util;
    public class OpenCVImage : MonoBehaviour
    {
        // Start is called before the first frame update

        public GameObject Object;


        public Texture2D graph;
        void Start()
        {
        }



        // Update is called once per frame
        void Update()
        {

            if (Object.GetComponent<image2>().image_exist)
            {
                graph = Object.GetComponent<image2>().graph;

                Mat mainMat = new Mat(graph.height, graph.width, MatType.CV_8UC3);
                Mat grayMat = new Mat();

                mainMat = Unity.TextureToMat(graph);
                Cv2.CvtColor(mainMat, grayMat, ColorConversionCodes.BGR2GRAY);

                Cv2.GaussianBlur(grayMat, grayMat, new Size(5, 5), 0);

                Cv2.Canny(grayMat, grayMat, 10.0, 70.0);

                Cv2.Threshold(grayMat, grayMat, 70.0, 255.0, ThresholdTypes.BinaryInv);

                //  Cv2.Threshold(grayMat, grayMat, 0, 255, ThresholdTypes.Triangle);



                Texture2D graphnew = Unity.MatToTexture(grayMat);

                if (graphnew.GetPixel(200, 200).Equals(new Color(1, 1, 1, 1)) || graphnew.GetPixel(200, 200).Equals(new Color(0, 0, 0, 1)))
                {
                    Debug.Log(" PIXEL IS WHITE or black");
                }
                else
                {
                    Debug.Log("unknown color");
                }

                var bytes = graphnew.EncodeToPNG();
                Debug.Log("" + bytes);
                System.IO.File.WriteAllBytes("ImageSaveTest2.png", bytes);
                Debug.Log("b/w image done");

                Object.GetComponent<image2>().image_exist = false;
            }
        }
    }
}