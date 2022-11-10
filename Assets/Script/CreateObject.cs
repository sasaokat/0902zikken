using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObject : MonoBehaviour
{
    //数値を表示
    [SerializeField]
    [Tooltip("生成するGameObject")]
    private GameObject createPrefab;
    [SerializeField]
    [Tooltip("注視点のGameObject")]
    private GameObject gazePoint;
    [SerializeField]
    [Tooltip("生成する範囲A")]
    private Transform rangeA;
    [SerializeField]
    [Tooltip("生成する範囲B")]
    private Transform rangeB;
    [SerializeField] 
    [Tooltip("生成するGameObject数")]
    int itemCount = 8000;
    [SerializeField] 
    [Tooltip("cameraの速度")]
    float speed = 5.0f;
    [SerializeField] 
    [Tooltip("ループ距離")]
    float loop = 40.0f;
    [SerializeField] 
    [Tooltip("制限時間")]
    int limit = 10;
    


    // アイテムのインスタンス
    List<GameObject> items = new List<GameObject>();

    //カメラの設定
    Camera cam;
    Vector3 direction = new Vector3(0f,0f,45f);

    float elapsedTime;
        
    void Start()
    {
        //注視点明示化
        gazePoint.SetActive(true);
        StartCoroutine("SphereRandom");    
    }

    void Update ()
    {
        //1秒に1足していく
		elapsedTime += Time.deltaTime; 
/*         Debug.Log(elapsedTime); */



	}

    private IEnumerator SphereRandom()
    {
        yield return new WaitForSeconds(2.0f);
        //注視点非明示化
        gazePoint.SetActive (false);
                
        // ランダムに球を作る
        for (int i = 0; i < itemCount ; i++)
        {        
        Vector3 halfExtents = new Vector3(0.5f, 0.5f, 0.5f);

        // rangeAとrangeBのx座標の範囲内でランダムな数値を作成
        float x = Random.Range(rangeA.position.x, rangeB.position.x);
        // rangeAとrangeBのy座標の範囲内でランダムな数値を作成
        float y = Random.Range(rangeA.position.y, rangeB.position.y);
        // rangeAとrangeBのz座標の範囲内でランダムな数値を作成
        float z = Random.Range(rangeA.position.z, rangeB.position.z);
        
        Vector3 pos = new Vector3(x,y,z);

            // 10回試す
            for (int n = 0; n < 10; n++)
            {
                // ボックスとアイテムが重ならないとき
                if (!Physics.CheckBox(pos, halfExtents, Quaternion.identity))
                {
                    // アイテムをインスタンス化
                    items.Add(Instantiate(createPrefab, pos, Quaternion.identity));
                    break;
                } 
            }
        }
        yield return null;
        StartCoroutine("MoveCamera");
    }

    private IEnumerator MoveCamera()
    {
        cam = Camera.main;

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
            if(elapsedTime > limit)
            {
            break;
            }
        }   
        StartCoroutine("QuitGame");
    }
    
    private IEnumerator QuitGame()
    {    
        //終了処理
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #elif UNITY_STANDALONE
            UnityEngine.Application.Quit();
        #endif
        yield return null; 
    }

}
