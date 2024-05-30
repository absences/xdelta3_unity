using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
         //t.text = "success patch!";

         StartCoroutine(Download());
    }

    public Text t;
    IEnumerator Download()
    {
       var request2 = UnityWebRequest.Get(Application.streamingAssetsPath+"/begine.apk");
      
        yield return request2.SendWebRequest();

        var begine = Application.persistentDataPath + "/begine.apk";


        File.WriteAllBytes(begine, request2.downloadHandler.data);

        var request = UnityWebRequest.Get(Application.streamingAssetsPath + "/patch.bytes");

        yield return request.SendWebRequest();

        var patch = Application.persistentDataPath + "/patch.bytes";
        File.WriteAllBytes(patch, request.downloadHandler.data);

        var target1 = Application.persistentDataPath + "/target1.apk";

        var r = Xdelta3Tool.ApplyPatch(
            patch,
              begine,//normally USE  Xdelta3Tool.getPackageCodePath()
             target1);

        Debug.Log("apply result  " + r);
        

        Xdelta3Tool.InstallApk(target1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
