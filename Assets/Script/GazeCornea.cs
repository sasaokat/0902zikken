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

            public class GazeCornea : MonoBehaviour
            {

                //excelファイル作成
                public string filename ="Eye";
                StreamWriter sw;
                Vector3 GazeOriginCombinedLocalC, GazeDirectionCombinedLocalC;

                //トリガーがどれだけ押されているのかを取得するためのtriggerpullという関数にSteamVR_Actions.default_TriggerPullを固定
                private SteamVR_Action_Single triggerpull = SteamVR_Actions.default_TriggerPull;

                //結果の格納用floot型関数
                private float pullleft;

                //⓪取得呼び出し-----------------------------
                //呼び出したデータ格納用の関数
                EyeData eye;
                //-------------------------------------------


                //②視線の起点の座標(角膜の中心）mm単位------
                //呼び出したデータ格納用の関数
                Vector3 LeftGazeOrigin;
                Vector3 RightGazeOrigin;
                //-------------------------------------------

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
                //結果をGetLastAxisで取得してpullleftに格納
                //SteamVR_Input_Sources.機器名（ここは左コントローラ）
                    pullleft = triggerpull.GetLastAxis(SteamVR_Input_Sources.LeftHand);
                    

                // 書き出し

                    //②視線の起点の座標(角膜の中心）mm単位------ -
                    //左目の眼球データ（視線原点）が妥当ならば取得　目をつぶるとFalse　判定精度はまあまあ
                    if (eye.verbose_data.left.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_GAZE_ORIGIN_VALIDITY))
                    {
                        LeftGazeOrigin = eye.verbose_data.left.gaze_origin_mm;
                        string[] str = { "" + LeftGazeOrigin.x, "" + LeftGazeOrigin.y, "" + LeftGazeOrigin.z,"" + pullleft,""+ UnityEngine.Time.time};
                        string str2 = string.Join(",", str);
                        sw.WriteLine(str2);
                        sw.Flush();
                        Debug.Log(11111);
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
