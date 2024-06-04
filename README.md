# xdelta3_unity

unity android 使用 xdelta3 进行差分更新apk

  tips:google app store 不可使用安装apk权限，会被拒

#文件目录

  /Unity  unity 2019.4.40 示例工程

  /XdeltaLib 安卓工程，编译aar供unity调用

  /xdelta vs2022 可编译工程生成64位exe（Debug x64）32位更改报错处为4即可

  github action 工具已经写好，提交代码即可触发构建 :)

调用: 应用patch 

    AndroidJavaObject jc = new AndroidJavaObject("com.gygame.lib.UnityTool");
    var jo = jc.CallStatic<AndroidJavaObject>("Instance");
    return jo.Call<int>("xdelta3_patch", opType, inPath, srcPath, outPath);

整包差分更新流程：

生成新包与旧包的差分文件上传至云端

客户端下载差分文件

将Context.getPackageCodePath() 与差分文件合并为新的apk

安装apk

    AndroidJavaObject jc = new AndroidJavaObject("com.gygame.lib.UnityTool");
    var jo = jc.CallStatic<AndroidJavaObject>("Instance");
    jo.Call("InstallApk", apkurl );

