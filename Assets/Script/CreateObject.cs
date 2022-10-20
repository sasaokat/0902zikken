using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateObject : MonoBehaviour
{
    //数値の設定
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

    // アイテムのインスタンス
    List<GameObject> items = new List<GameObject>();

    //カメラの設定
    Camera cam;
    Vector3 direction = new Vector3(5f,0f,45f);
    float speed = 6.0f;
    
    // CubeプレハブをGameObject型で取得
    GameObject obj = (GameObject)Resources.Load ("Cube");

    void Start()
    {
        // Cubeプレハブを元に、インスタンスを生成、
        Instantiate (obj, new Vector3(5.0f,0.0f,30.0f), Quaternion.identity);

        cam = Camera.main;

        StartCoroutine("CubeInstant");        
    }

    private IEnumerator CubeInstant()
    {
        obj.SetActive (false);
        yield return new WaitForSeconds(5.0f);
        

        StartCoroutine("SphereRandom");
    }

    private IEnumerator SphereRandom()
    {
        yield return new WaitForSeconds(5.0f);
                
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
        StartCoroutine("MoveCamera");

    }

    private IEnumerator MoveCamera()
    {
        float step = speed * Time.deltaTime;
        cam.transform.position = Vector3.MoveTowards(cam.transform.position, direction, step);
          if (cam.transform.position.z > 40f)
        {
            cam.transform.position = new Vector3(5f, 0f, 0f);
        }
        yield return step;
    }
}
