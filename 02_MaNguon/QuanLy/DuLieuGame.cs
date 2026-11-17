using UnityEngine;
public static class DuLieuGame {
    public static void LuuXe(string tenXe, int indexXe) { PlayerPrefs.SetString("XeDaChon", tenXe); PlayerPrefs.SetInt("IndexXe", indexXe); PlayerPrefs.Save(); }
    public static void LuuNhanVat(string tenNV, int indexNV) { PlayerPrefs.SetString("NhanVatDaChon", tenNV); PlayerPrefs.SetInt("IndexNhanVat", indexNV); PlayerPrefs.Save(); }
    public static void LuuDiaHinh(string tenDH, int indexDH) { PlayerPrefs.SetString("DiaHinhDaChon", tenDH); PlayerPrefs.SetInt("IndexDiaHinh", indexDH); PlayerPrefs.Save(); }
    public static void LuuKetQua(int hangPlayer, float thoiGian) {
        PlayerPrefs.SetInt("HangPlayer", hangPlayer);
        PlayerPrefs.SetString("ThuHang", "Top " + hangPlayer);
        PlayerPrefs.SetFloat("ThoiGianDua", thoiGian);
        PlayerPrefs.Save();
    }
}