using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRandom : MonoBehaviour

{

    [Header("分布")]
    [SerializeField] Transform CenterPosition;                 // 対象オブジェクト
    [SerializeField] int MaxRedius = 1000;         // 配置位置の最大半径
    [SerializeField] int MinRedius = 500;         // 配置位置の最小半径
    [SerializeField] int Height = 10;              // 配置位置の高さ


    [Header("個数")]
    [SerializeField] GameObject CreaturePrefab;                 // 対象オブジェクト
    [SerializeField] int CreatureLength = 100;                 // 配置位置の最大


    private System.Random random;                               // 乱数機

    // Use this for initialization
    void Start () {

        GameObject[] CreatureRange = new GameObject[CreatureLength];
        random = new System.Random();

        int x;
        int z;

        double xAbs;
        double zAbs;

        double maxR = Math.Pow(MaxRedius, 2);
        double minR = Math.Pow(MinRedius, 2);

        for (int i = 0; i < CreatureRange.Length; i++)
        {
            while (CreatureRange[i] == null){
                x = random.Next(-MaxRedius, MaxRedius);
                z = random.Next(-MaxRedius, MaxRedius);

                xAbs = Math.Abs(Math.Pow(x, 2));
                zAbs = Math.Abs(Math.Pow(z, 2));

                // 特定の範囲内化確認
                if (maxR > xAbs + zAbs && xAbs + zAbs > minR)
                {
                    GameObject go = Instantiate(
                        CreaturePrefab,             // 個体のオブジェクト
                        (new Vector3(x, Height, z)) + CenterPosition.position,        // 初期座標
                        Quaternion.identity         // 回転位置
                    );
                    CreatureRange[i] = go;
                }
            }
        }

    }
}