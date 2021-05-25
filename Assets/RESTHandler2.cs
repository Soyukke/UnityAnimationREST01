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
public class RESTHandler2 : MonoBehaviour
{

    // APIから取得する構造
    class BodyStatus
    {
        public float[] accelLeft;
        public float[] accelRight;
    }

    float val1 = 0.0f;
    float val2 = 0.0f;

    SkinnedMeshRenderer skinnedMeshRenderer;
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
                BodyStatus bs = JsonUtility.FromJson<BodyStatus>(request.downloadHandler.text);
                // x
                val1 = bs.accelLeft[0] / 4000 * 100;
                val2 = bs.accelRight[0] / 4000 * 100;
                Debug.Log(val1);
                Debug.Log(val2);
            }
            else
            {
                Debug.Log("failed");
            }
        }
    }

    public void TestGetFunc()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 15;
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // REST API実行
    }

    void FixedUpdate()
    {
        StartCoroutine(Get("localhost:32000/testapi"));
        // NOTE シェイプキーIndex 0のパラメータを変更する．rerenderされる．
        skinnedMeshRenderer.SetBlendShapeWeight(0, val1);
        // NOTE シェイプキーIndex 1のパラメータを変更する．rerenderされる．
        skinnedMeshRenderer.SetBlendShapeWeight(1, val2);

    }
}
