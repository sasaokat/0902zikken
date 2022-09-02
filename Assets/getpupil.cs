using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace ViveSR
{
    namespace anipal
    {
        namespace Eye
        {


            public class getpupil : MonoBehaviour
                {
                    

/*                     //csv
                    public string filename ="LeftEye";
                    StreamWriter sw; */
                    Vector2 lefteye;
    
                    void Start()
                    {
/*                         sw = new StreamWriter(@"" + filename + ".csv", false);
                        string[] s1 = { "left_x", "left_y", "time"};
                        string s2 = string.Join(",", s1);
                        sw.WriteLine(s2); */
                    }

                    void Update()
                    {
                        SRanipal_Eye.GetPupilPosition(EyeIndex.LEFT, out lefteye);
                        Debug.Log(lefteye);
                    }
                }
        }
    }
}
