# xdelta3_unity

unity使用 xdelta3 差分更新apk

android studio jni编译xdelta

调用: 生成patch或者应用patch
  AndroidJavaObject jc = new AndroidJavaObject("com.gygame.lib.UnityTool");
  var jo = jc.CallStatic<AndroidJavaObject>("Instance");
  return jo.Call<int>("xdelta3_patch", opType, inPath, srcPath, outPath);


  com.gygame.lib 是安卓工程包名

  生成debug.aar 放入Plugins/Android
    添加androidx
 或者直接使用Plugins
