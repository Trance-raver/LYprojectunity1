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
        public float x1, y1, l;
        public Texture2D graphnew;

        [SerializeField]
        public Sprite s1;
        public SpriteRenderer r1;
        public Texture2D graph;
        void Start()
        {
            Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        }



        // Update is called once per frame
        void Update()
        {

            if (Object.GetComponent<image2>().image_exist)
            {
                graph = Object.GetComponent<image2>().graph;
                x1 = Object.GetComponent<image2>().x1;
                y1 = Object.GetComponent<image2>().y1;
                l = Object.GetComponent<image2>().l;

                Mat mainMat = new Mat(graph.height, graph.width, MatType.CV_8UC3);
                Mat grayMat = new Mat();

                mainMat = Unity.TextureToMat(graph);
                Cv2.CvtColor(mainMat, grayMat, ColorConversionCodes.BGR2GRAY);

                Cv2.GaussianBlur(grayMat, grayMat, new Size(5, 5), 0);
                

                Cv2.Canny(grayMat, grayMat, 10.0, 70.0);

                Cv2.Threshold(grayMat, grayMat, 70.0, 255.0, ThresholdTypes.BinaryInv);

                //  Cv2.Threshold(grayMat, grayMat, 0, 255, ThresholdTypes.Triangle);



                graphnew = Unity.MatToTexture(grayMat);
                Color[] pixels = graphnew.GetPixels(0, 0, graphnew.width, graphnew.height, 0);

                if (graphnew.GetPixel(200, 200).Equals(new Color(1, 1, 1, 1)) || graphnew.GetPixel(200, 200).Equals(new Color(0, 0, 0, 1)))
                {
                    Debug.Log(" PIXEL IS WHITE or black");
                }
                else
                {
                    Debug.Log("unknown color");
                }
                for (int p = 0; p < pixels.Length; p++)
                {
                    if (pixels[p].Equals(new Color(1, 1, 1, 1)))
                        pixels[p] = new Color(0, 0, 0, 0);
                }
                graphnew.SetPixels(0, 0, graphnew.width, graphnew.height, pixels, 0);
                graphnew.Apply();

                UnityEngine.Rect r2 = new UnityEngine.Rect(0, 0, graphnew.width, graphnew.height);
                s1 = Sprite.Create(graphnew, r2, new Vector2(0.5f, 0.5f), 100.0f);
                r1.sprite = s1;
                Instantiate(r1, new Vector3(0, 0, 10), Quaternion.identity);
                var bytes = graphnew.EncodeToPNG();
                Debug.Log("" + bytes);
                System.IO.File.WriteAllBytes("ImageSaveTest2.png", bytes);
                Debug.Log("b/w image done");

                Object.GetComponent<image2>().image_exist = false;
            }
        }
    }
}