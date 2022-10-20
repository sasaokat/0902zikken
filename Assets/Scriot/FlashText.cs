using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashText : MonoBehaviour
{
    public Text message;
 
    void OnMouseDown()
      {
        message.text ="Text Message";//テキスト表示
        StartCoroutine("TextSet");//コルーチンを実行
      }
    
    //実行内容 1秒待ってからテキスト非表示
    IEnumerator TextSet()
    {
        yield return new WaitForSeconds(1.0f);
        message.text = "";
    }
}
