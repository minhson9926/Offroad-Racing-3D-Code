using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HienThiKetQua : MonoBehaviour {
    public Text txtTieuDeChinh;
    public Text txtTomTatChuyenDi;
    public Text[] txtHangSo;
    public Text[] txtTenTayDua;
    public Text[] txtPhuongTien;
    public Text[] txtThanhTich;
    public Image[] nenDongBang;

    void Start() {
        int hangPlayer = Mathf.Clamp(PlayerPrefs.GetInt("HangPlayer", 1), 1, 3);
        string tenNV = PlayerPrefs.GetString("NhanVatDaChon", "Tay đua 1");
        string tenXe = PlayerPrefs.GetString("XeDaChon", "Xe Jeep");
        string diaHinh = PlayerPrefs.GetString("DiaHinhDaChon", "Sa mạc");

        if (txtTieuDeChinh != null)
            txtTieuDeChinh.text = hangPlayer == 1 ? "CHÚC MỪNG VÔ ĐỊCH - TOP 1" : $"HOÀN THÀNH CUỘC ĐUA - TOP {hangPlayer}";
        if (txtTomTatChuyenDi != null)
            txtTomTatChuyenDi.text = $"Chặng đua: {diaHinh.ToUpper()}   |   Tay đua: {tenNV}   |   Xe: {tenXe}";

        for (int i = 0; i < 3; i++) {
            int top = i + 1;
            string ten = PlayerPrefs.GetString($"Top{top}_Ten", $"Tay đua {top}");
            string xe = PlayerPrefs.GetString($"Top{top}_Xe", "Xe Đua");
            float tg = PlayerPrefs.GetFloat($"Top{top}_ThoiGian", 45f + i * 2f);
            bool laPlayer = PlayerPrefs.GetInt($"Top{top}_LaPlayer", 0) == 1;

            if (txtHangSo != null && txtHangSo.Length > i) txtHangSo[i].text = "#" + top;
            if (txtTenTayDua != null && txtTenTayDua.Length > i) txtTenTayDua[i].text = ten;
            if (txtPhuongTien != null && txtPhuongTien.Length > i) txtPhuongTien[i].text = xe;
            if (txtThanhTich != null && txtThanhTich.Length > i) txtThanhTich[i].text = string.Format("{0:00}:{1:00.00}", Mathf.FloorToInt(tg / 60f), tg % 60f);
            if (nenDongBang != null && nenDongBang.Length > i && nenDongBang[i] != null)
                nenDongBang[i].color = laPlayer ? new Color(0.95f, 0.72f, 0.12f, 0.45f) : new Color(0.14f, 0.18f, 0.26f, 0.90f);
        }
    }

    public void ChoiLai() { SceneManager.LoadScene("05_DuaXe"); }
    public void VeMenuChinh() { SceneManager.LoadScene("01_MenuChinh"); }
}