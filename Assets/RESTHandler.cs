using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

/*
MonoBehaviourを使うことで IEnumeratorを扱うStartCoroutineが使える．
非同期処理とかに使うのだろうか．処理がどうなってるのかはまだ理解していない．

このMonoBehaviourはobjectMoverTest01で使用している．
同一オブジェクトにこのコンポーネントをセットすることで，別コンポーネントから取得・利用ができる．
*/
public class RESTHandler : MonoBehaviour
{
    // REST GETを試す
    public IEnumerator Get(string url)
    {
        var request = new UnityWebRequest();
        request.downloadHandler = new DownloadHandlerBuffer();
        request.url = url;
        request.SetRequestHeader("Content-Type", "application/json; charset=UTF-8");
        request.method = UnityWebRequest.kHttpVerbGET;
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(request.error);
        }
        else
        {
            if (request.responseCode == 200)
            {
                Debug.Log("success");
                Debug.Log(request.downloadHandler.text);
            }
            else
            {
                Debug.Log("failed");
            }
        }
    }

    public void TestGetFunc()
    {
        StartCoroutine(Get("google.com"));
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
