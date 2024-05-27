package com.gygame.lib;

import android.app.Activity;
import android.content.ClipData;
import android.content.ClipboardManager;
import android.content.Intent;

import android.os.Looper;
import android.util.Log;
import android.widget.Toast;

import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;

/***
 * unity交互工具，unity to android /android to unity
 */
public class UnityTool {

    private static UnityTool instance;

    public static UnityTool Instance(){
        if(instance==null) {

            System.loadLibrary("xdelta3");
            instance = new UnityTool();
        }
        return instance;
    }
    public void ShowToast(final String msg){
        Toast.makeText(getActivity(),msg,Toast.LENGTH_SHORT).show();
    }

    /***
     * 重启应用
     */
    private void restartApp() {
        Intent intent = getActivity().getPackageManager().getLaunchIntentForPackage(getActivity().getPackageName());
        intent.addFlags(Intent.FLAG_ACTIVITY_NEW_TASK | Intent.FLAG_ACTIVITY_CLEAR_TOP
                | Intent.FLAG_ACTIVITY_CLEAR_TASK);
        getActivity().startActivity(intent);
        android.os.Process.killProcess(android.os.Process.myPid());
        System.exit(0);
    }

    /***
     * 将Unity发送过来的文本内容复制到粘贴板
     * @param str
     * @throws Exception
     */
    public  boolean CopyTextToClipboard( String str){

        if(Looper.myLooper() ==null){
            Looper.prepare();
        }

        ClipboardManager clipboard = (ClipboardManager) getActivity().getSystemService(Activity.CLIPBOARD_SERVICE);
        ClipData textCd = ClipData.newPlainText("Label", str);
        clipboard.setPrimaryClip(textCd);
        return true;
    }

    private Activity _unityActivity;
    public Activity getActivity() {
        if(null == _unityActivity) {
            try {
                Class<?> classtype = Class.forName("com.unity3d.player.UnityPlayer");
                Activity activity = (Activity) classtype.getDeclaredField("currentActivity").get(classtype);
                _unityActivity = activity;
            } catch (ClassNotFoundException e) {
                Log.d("Unity2Android", "getActivity: "+e);
            } catch (IllegalAccessException e) {
                Log.d("Unity2Android", "getActivity: "+e);
            } catch (NoSuchFieldException e) {
                Log.d("Unity2Android", "getActivity: "+e);
            }
        }
        return _unityActivity;
    }

    /**
     * 调用Unity的方法
     * @param gameObjectName    调用的GameObject的名称
     * @param functionName      方法名
     * @param args              参数
     * @return                  调用是否成功
     */
    public boolean callUnity(String gameObjectName, String functionName, String args){
        try {
            Class<?> classtype = Class.forName("com.unity3d.player.UnityPlayer");
            Method method =classtype.getMethod("UnitySendMessage", String.class,String.class,String.class);
            method.invoke(classtype,gameObjectName,functionName,args);
            return true;
        } catch (ClassNotFoundException e) {
            Log.d("Unity2Android", "callUnity: "+e);
        } catch (NoSuchMethodException e) {
            Log.d("Unity2Android", "callUnity: "+e);
        } catch (IllegalAccessException e) {
            Log.d("Unity2Android", "callUnity: "+e);
        } catch (InvocationTargetException e) {
            Log.d("Unity2Android", "callUnity: "+e);
        }
        return false;
    }

    public int xdelta3_patch(int encode, String inPath, String srcPath, String outPath)
    {
       return XdeltaPatch.nativePatch(encode,inPath,srcPath,outPath);
    }
}
