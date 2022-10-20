using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;
using Valve.VR;

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

                //トリガーがどれだけ押されているのかを取得するためのtriggerpullという関数にSteamVR_Actions.default_TriggerPullを固定
                private SteamVR_Action_Single triggerpull = SteamVR_Actions.default_TriggerPull;

                //結果の格納用floot型関数
                private float pullleft;

                void Start()
                {
                    sw = new StreamWriter(@"" + filename + ".csv", false);
                    string[] s1 = { "x", "y", "z","trigger","time"};
                    string s2 = string.Join(",", s1);
                    sw.WriteLine(s2);
                }

                // 視線計測
                void Update()
                {

                    SRanipal_Eye.GetGazeRay(GazeIndex.COMBINE, out GazeOriginCombinedLocalC, out GazeDirectionCombinedLocalC);

                    Vector3 GazeDirectionCombinedC = Camera.main.transform.TransformDirection(GazeDirectionCombinedLocalC);
                    RaycastHit hitC;

                //結果をGetLastAxisで取得してpullleftに格納
                //SteamVR_Input_Sources.機器名（ここは左コントローラ）
                    pullleft = triggerpull.GetLastAxis(SteamVR_Input_Sources.LeftHand);

                // 書き出し
                    if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.position + GazeDirectionCombinedC * 50, out hitC))
                    {
                        string[] str = { "" + hitC.point.x, "" + hitC.point.y, "" + hitC.point.z,"" + pullleft,""+ UnityEngine.Time.time};
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
