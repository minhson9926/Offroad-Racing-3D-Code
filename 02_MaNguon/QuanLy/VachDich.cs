using UnityEngine;
public class VachDich : MonoBehaviour {
    public QuanLyCuocDua quanLyCuocDua;
    public GameObject bangThongBaoChienThang;
    private void OnTriggerEnter(Collider other) {
        Transform xeRoot = other.attachedRigidbody != null ? other.attachedRigidbody.transform : other.transform;
        if (xeRoot.CompareTag("Player") || xeRoot.GetComponent<DieuKhienXe>() != null) {
            if (bangThongBaoChienThang != null) bangThongBaoChienThang.SetActive(true);
            if (quanLyCuocDua != null) quanLyCuocDua.GhiNhanXeVeDich(xeRoot);
        } else if (xeRoot.GetComponent<XeAI>() != null) {
            if (quanLyCuocDua != null) quanLyCuocDua.GhiNhanXeVeDich(xeRoot);
        }
    }
}