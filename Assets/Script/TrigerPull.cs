using System;
using UnityEngine;
using Valve.VR;

//クラス名はファイル名（今回はTrigerPull）
public class TrigerPull : MonoBehaviour
{

    //トリガーがどれだけ押されているのかを取得するためのtriggerpullという関数にSteamVR_Actions.default_TriggerPullを固定
    private SteamVR_Action_Single triggerpull = SteamVR_Actions.default_TriggerPull;
    //結果の格納用floot型関数
    private float pullleft, pullright;

    //1フレーム毎に呼び出されるUpdateメゾット
    void Update()
    {
        //結果をGetLastAxisで取得してpullleftに格納
        //SteamVR_Input_Sources.機器名（ここは左コントローラ）
        pullleft = triggerpull.GetLastAxis(SteamVR_Input_Sources.LeftHand);
        //pullleftの中身を確認
        Debug.Log("Left:" + pullleft);

        //結果をGetLastAxisで取得してpullrightに格納
        //SteamVR_Input_Sources.機器名（ここは右コントローラ）
        pullright = triggerpull.GetLastAxis(SteamVR_Input_Sources.RightHand);
        //pullrightの中身を確認
        Debug.Log("Right:" + pullright);
    }
}