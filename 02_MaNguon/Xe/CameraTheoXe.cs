using UnityEngine;
public class CameraTheoXe : MonoBehaviour {
    public QuanLyCuocDua quanLyCuocDua;
    public Vector3 khoangCach = new Vector3(0f, 4.5f, -8.5f);
    public float doMuot = 8f;
    private Transform mucTieuXe;
    void LateUpdate() {
        if (mucTieuXe == null && quanLyCuocDua != null && quanLyCuocDua.xeNguoiChoi != null) mucTieuXe = quanLyCuocDua.xeNguoiChoi.transform;
        if (mucTieuXe == null) return;
        transform.position = Vector3.Lerp(transform.position, mucTieuXe.TransformPoint(khoangCach), doMuot * Time.deltaTime);
        transform.LookAt(mucTieuXe.position + Vector3.up * 1.2f);
    }
}