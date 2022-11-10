using UnityEngine;
using UnityEngine.Events;

namespace FUNSET
{
   /// <summary>
   /// 簡易のオブジェクトカリングシステム
   /// どこか空オブジェクトにアタッチして、該当するNPCやオブジェクトを登録する
   /// プレイヤーのカメラから遠く離れてかつ視覚の外に消えるか判定する
   ///　単純に、カメラから離れてるオブジェクト（子オブジェクトを抱えた）を非表示にする
   /// </summary>
   public class OutRangeCuller : MonoBehaviour
   {
       [SerializeField, Header("カリング対象をセット"), Tooltip("カリング対象")]
       GameObject[] _CullActivateSet;

       [SerializeField, Header("プレイヤーかカメラの位置"), Tooltip("プレイヤーかカメラの位置")]
       GameObject PlayerCamera;

       [SerializeField, Header("アウトレンジ判定距離"), Tooltip("アウトレンジ判定距離")]
       float Distance=30;


       [Header("カリング判定頻度（秒）"), Tooltip("カリング判定頻度")]
       [SerializeField]
       float Checkinterval = 0.3f;
       float CheckTime = 0;

       private void Start()
       {
           CheckTime = Checkinterval;
       }

       public void Update()
       {

           CheckTime -= Time.deltaTime;
           if (CheckTime > 0) { return; }
           CheckTime = Checkinterval ;


           for (int i = 0; i < _CullActivateSet.Length; i++)
           {
               CheckDistance(i);
           }

       }

       /// <summary>
       /// カメラからの距離を判定　
       /// </summary>
       /// <param name="a">_cullActivateSetの要素数</param>
       public void CheckDistance(int a)
       {

           //距離でカリング判定する。カメラの向きから補正する
           Vector3 cameramukihosei;
           cameramukihosei = PlayerCamera.transform.forward * Distance * 0.4f;

           float _d = ((PlayerCamera.transform.position + cameramukihosei) - _CullActivateSet[a].transform.position).sqrMagnitude;
           //Debug.Log("Distance" + _d + "Distance" + Mathf.Pow(Distance, 2));

           //距離が一定以上離れたら非表示に
           if ((_d > Mathf.Pow(Distance, 2))) { _CullActivateSet[a].SetActive(false); }

           else
           {
               _CullActivateSet[a].SetActive(true);
           }


       }
   }
}