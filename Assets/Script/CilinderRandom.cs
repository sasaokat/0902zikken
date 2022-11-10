using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.Events;

namespace FUNSET
{
public class CilinderRandom : MonoBehaviour
{
    //unityに表示
    [SerializeField]
    [Tooltip("生成するGameObject")]
    private GameObject CreaturePrefab ;
    [SerializeField]
    [Tooltip("注視点のGameObject")]
    private GameObject gazePoint;

    [Header("分布")]
    [SerializeField] Transform CenterPosition;  
    [SerializeField] float MaxRedius = 100;         // 配置位置の最大半径
    [SerializeField] float MinRedius = 10;          // 配置位置の最小半径
    [SerializeField] int itemCount = 8000;          // 配置個数
    [SerializeField] int Cylinderlength = 600;      // 奥行き

    [Header("移動")]
    [SerializeField] 
    [Tooltip("cameraの速度")]
    float speed = 30.0f;
    [SerializeField] 
    [Tooltip("ループ距離")]
    float loop = 500.0f;
    [SerializeField] 
    [Tooltip("制限時間")]
    int limit = 60;
    [SerializeField] 
    [Tooltip("注視点表示")]
    float gpwait = 5.0f;

    [SerializeField, Header("アウトレンジ判定距離"), Tooltip("アウトレンジ判定距離")]
    float Distance = 30;

    //カメラの設定
    Camera cam;
    
    Vector3 direction = new Vector3(0f,0f,1000f);

    float elapsedTime;

    GameObject go;

    // アイテムのインスタンス
    List<GameObject> items = new List<GameObject>();

    

        
    void Start()
    {
        //注視点明示化
        gazePoint.SetActive(true);
        StartCoroutine("SphereRandom");

        cam = Camera.main;    
    }

    void Update ()
    {
        //1秒に1足していく
		elapsedTime += Time.deltaTime;
     
	}

    private IEnumerator SphereRandom()
    {
        yield return new WaitForSeconds(gpwait);

        //注視点非明示化
        gazePoint.SetActive (false);
        Debug.Log(elapsedTime);
                
        GameObject[] CreatureRange = new GameObject[itemCount];

        double maxR = Math.Pow(MaxRedius, 2);
        double minR = Math.Pow(MinRedius, 2);

        for (int i = 0; i < CreatureRange.Length; i++)
        {
            //ランダム生成
            while (CreatureRange[i] == null){
                float x = Random.Range(-MaxRedius, MaxRedius);
                float y = Random.Range(-MaxRedius, MaxRedius);
                float z = Random.Range(0,Cylinderlength);

                Vector3 pos = new Vector3(x,y,z);
 
                //円の範囲を設定
                double xAbs = Math.Abs(Math.Pow(x, 2));
                double yAbs = Math.Abs(Math.Pow(y, 2));

                // 特定の範囲内か確認
                if (maxR > xAbs + yAbs && xAbs + yAbs > minR)
                {
                    GameObject go = Instantiate(CreaturePrefab, pos + CenterPosition.position, Quaternion.identity) as GameObject;
                    CreatureRange[i] = go;
                }
            }
        } 

        Vector3 cameramukihosei;
        cameramukihosei = cam.transform.forward * Distance * 0.4f;

        float _d = ((cam.transform.position + cameramukihosei) - go.transform.position).sqrMagnitude;
        //Debug.Log("Distance" + _d + "Distance" + Mathf.Pow(Distance, 2));

        //距離が一定以上離れたら非表示に
        if ((_d > Mathf.Pow(Distance, 2)))
        {
            go.SetActive(false); 
        }

        else
        {
            go.SetActive(true);
        }

        yield return null;
        StartCoroutine("MoveCamera");
    }

    private IEnumerator MoveCamera()
    {

        //カメラの移動
        while (true)
        { 
            float step = speed * Time.deltaTime;
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, direction, step);

            yield return null; 
            //ループの設定
            if(cam.transform.position.z > loop)
            {
                cam.transform.position = new Vector3(0f, 0f, 0f);
            }
            //制限時間
            if(elapsedTime > limit + gpwait)
            {
            break;
            }
        }   
        StartCoroutine("QuitGame");
    }
    
    private IEnumerator QuitGame()
    {    
        Debug.Log(elapsedTime);
        //終了処理
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_STANDALONE
            UnityEngine.Application.Quit();
        #endif
        yield return null; 
    }

}
}
