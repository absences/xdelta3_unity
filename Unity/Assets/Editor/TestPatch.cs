using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class TestPatch 
{
    
    [MenuItem("tool/genpatch")]
    public static void GenPatch()
    {
        var patch = Application.dataPath + "/../example/patch.bytes";

       File.Delete(patch);
       var t= Xdelta3Tool.GenPatch(patch,
           Application.dataPath + "/../example/begine.apk",
            Application.dataPath + "/../example/target.apk"
              );

        Debug.Log("gen patch done" + t);

    }

    [MenuItem("tool/apply")]
    public static void apply()
    {
        var patch = Application.dataPath + "/../example/patch.bytes";


       var t= Xdelta3Tool.ApplyPatch(patch,
           Application.dataPath + "/../example/begine.apk",
            Application.dataPath + "/../example/target1.apk");

        Debug.Log("apply patch done"+t);

    }
}
