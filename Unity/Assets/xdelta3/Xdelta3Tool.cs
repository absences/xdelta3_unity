using System;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class Xdelta3Tool
{
    public static string getPackageCodePath()
    {
        AndroidJavaObject jc = new AndroidJavaObject("com.gygame.lib.UnityTool");
        var jo = jc.CallStatic<AndroidJavaObject>("Instance");
        return jo.Call<string>("getPackageCodePath");
    }

    public static void InstallApk(string url)
    {
        AndroidJavaObject jc = new AndroidJavaObject("com.gygame.lib.UnityTool");
        var jo = jc.CallStatic<AndroidJavaObject>("Instance");
        jo.Call("InstallApk", url);
    }

#if UNITY_ANDROID && !UNITY_EDITOR

    //处理结果状态码，0成功/其他失败
    private static int xdelta3_patch(int opType,
        string inPath,
        string srcPath,
        string outPath)
    {
        AndroidJavaObject jc = new AndroidJavaObject("com.gygame.lib.UnityTool");
        var jo = jc.CallStatic<AndroidJavaObject>("Instance");
        return jo.Call<int>("xdelta3_patch", opType, inPath, srcPath, outPath);
    }

#else
    private static int xdelta3_filemd5(string filePath, ref byte result)
    {
        return 0;
    }
    /// <summary>
    /// inPath  patch包路径    srcPath 旧版Apk包路径     outPath 合成的新包路径
    /// </summary>
    /// <param name="opType"></param>
    /// <param name="inPath"></param>
    /// <param name="srcPath"></param>
    /// <param name="outPath"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    private static int xdelta3_patch(int opType, string inPath, string srcPath, string outPath)
    {
        var process = new Process();
        process.StartInfo.FileName = "xdelta3.exe";
#if UNITY_EDITOR
        process.StartInfo.FileName = UnityEngine.Application.dataPath.Replace("Assets", "example/xdelta3.exe");
#endif
        string Arguments = null;
        switch ((EXdelta3Op)opType)
        {
            case EXdelta3Op.GenPatch:
                Arguments = string.Join(" ", new string[] {
                    "-v",
                    "-e",
                    "-s",
                    srcPath,//旧文件
                    outPath,//新文件
                    inPath,//patch文件
                });
                break;
            case EXdelta3Op.ApplyPatch:
                Arguments = string.Join(" ", new string[] {
                    "-v",
                    "-d",
                    "-s",
                    srcPath,//旧文件
                    inPath,//patch文件
                    outPath,//新文件
                });
                break;
            default:
                throw new Exception("Unsupport EXdelta3Op:" + opType);
        }
        process.StartInfo.Arguments = Arguments;
        if (!process.Start())
            return -1;
        process.WaitForExit();
        return 0;
    }

#endif

    /// <summary>
    /// 创建补丁文件
    /// </summary>
    public static int GenPatch(string inPath, string srcPath, string outPath)
    {
        return xdelta3_patch((int)EXdelta3Op.GenPatch, inPath, srcPath, outPath);
    }
    public static int ApplyPatch(string inPath, string srcPath, string outPath)
    {
        return xdelta3_patch((int)EXdelta3Op.ApplyPatch, inPath, srcPath, outPath);
    }
    private enum EXdelta3Op
    {
        ApplyPatch = 0,
        GenPatch = 1,
    }
}
