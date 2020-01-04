using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace OpenCvSharp
{


    public class spriterender : MonoBehaviour
    {
        // Start is called before the first frame update
        private Texture2D graphnew;
        public GameObject ob;
        void Start()
        {
            graphnew = ob.GetComponent<OpenCVImage>().graphnew;
            //UnityEngine.Rect r1 = UnityEnginnew Rect(0, 0, graphnew.width, graphnew.height);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}