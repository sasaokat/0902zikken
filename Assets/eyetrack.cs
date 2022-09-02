using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;

namespace ViveSR
{
    namespace anipal
    {
        namespace Eye
        {

            public class eyetrack : MonoBehaviour
            {

                //excelファイル作成
                public string filename ="Eye";
                StreamWriter sw;
                Vector3 GazeOriginCombinedLocalC, GazeDirectionCombinedLocalC;

                void Start()
                {
                    sw = new StreamWriter(@"" + filename + ".csv", false);
                    string[] s1 = { "x", "y", "z","time"};
                    string s2 = string.Join(",", s1);
                    sw.WriteLine(s2);
                }

                // 視線計測
                void Update()
                {

                    SRanipal_Eye.GetGazeRay(GazeIndex.COMBINE, out GazeOriginCombinedLocalC, out GazeDirectionCombinedLocalC);

                    Vector3 GazeDirectionCombinedC = Camera.main.transform.TransformDirection(GazeDirectionCombinedLocalC);
                    RaycastHit hitC;

                // 書き出し
                    if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.position + GazeDirectionCombinedC * 50, out hitC))
                    {
                        string[] str = { "" + hitC.point.x, "" + hitC.point.y, "" + hitC.point.z,""+ UnityEngine.Time.time };
                        string str2 = string.Join(",", str);
                        sw.WriteLine(str2);
                    }
                // スペースで終了
                   if (Keyboard.current.spaceKey.wasPressedThisFrame)
                    {
                        sw.Close();
                    }
                }
            }
        }
    }
}
