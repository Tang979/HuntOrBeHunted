using UnityEngine;
using Cinemachine;

public class FreeLookMouseControl : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera; // Tham chiếu đến Cinemachine FreeLook Camera
    private bool isControllingCamera = false;  // Biến kiểm tra trạng thái điều khiển camera

    void Start()
    {
        // Ban đầu vô hiệu hóa input chuột cho FreeLook Camera
        if (freeLookCamera != null)
        {
            freeLookCamera.m_XAxis.m_InputAxisName = ""; // Xóa tên trục để ngăn camera nhận input từ chuột
            freeLookCamera.m_YAxis.m_InputAxisName = "";
        }
    }

    void Update()
    {
        // Kiểm tra khi người chơi nhấn chuột trái
        if (Input.GetMouseButtonDown(0))
        {
            // Khóa và ẩn con trỏ chuột
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isControllingCamera = true;

            // Kích hoạt lại input cho FreeLook Camera
            if (freeLookCamera != null)
            {
                freeLookCamera.m_XAxis.m_InputAxisName = "Mouse X";
                freeLookCamera.m_YAxis.m_InputAxisName = "Mouse Y";
            }
        }

        // Kiểm tra khi người chơi nhấn ESC để dừng điều khiển camera
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Hiển thị lại con trỏ chuột và thả khóa
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isControllingCamera = false;

            // Vô hiệu hóa input chuột cho FreeLook Camera nhưng giữ nguyên vị trí hiện tại
            if (freeLookCamera != null)
            {
                freeLookCamera.m_XAxis.m_InputAxisName = ""; // Ngừng nhận input từ chuột nhưng giữ nguyên vị trí trục X
                freeLookCamera.m_YAxis.m_InputAxisName = ""; // Ngừng nhận input từ chuột nhưng giữ nguyên vị trí trục Y
            }
        }
    }
}
