package com.gygame.lib;
public class XdeltaPatch {

    static {
        System.loadLibrary("xdelta3");
    }

    /**
     * native 方法定义 在[patcher.c]中实现
     * @param encode  1编码/0解码 (低版本 apk + 差分包合成安装包传 0)
     * @param inPath  patch包路径
     * @param srcPath 旧版Apk包路径
     * @param outPath 合成的新包路径
     * @return 处理结果状态码，0成功/其他失败
     */
    public static native int nativePatch(int encode, String inPath, String srcPath, String outPath);
}
