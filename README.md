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

    整包差分更新流程：

     生成新包与旧包的差分文件上传至云端

     客户端下载差分文件

     将Context.getPackageCodePath() 与差分文件合并

    安装apk
    AndroidJavaObject jc = new AndroidJavaObject("com.gygame.lib.UnityTool");
    var jo = jc.CallStatic<AndroidJavaObject>("Instance");
    jo.Call("InstallApk", apkurl );

