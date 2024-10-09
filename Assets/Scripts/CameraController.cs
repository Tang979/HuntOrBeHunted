using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class Cameracontroller : MonoBehaviour
{
	public CinemachineFreeLook ThirdPerson; // Tham chiếu đến Cinemachine FreeLook Camera
	public CinemachineVirtualCamera FirstPerson;

	public GameObject inventory;
	bool isFirstPerson = true;
	private bool isControllingCamera = false;  // Biến kiểm tra trạng thái điều khiển camera
	public Transform playerBody;  // Đối tượng nhân vật cần xoay cùng camera
	public float mouseSensitivity = 100f;  // Độ nhạy của chuột
	private float xRotation = 0f;  // Trục xoay X cho việc xoay camera theo chiều dọc
    private bool isInventoryOpen = false;

    void Start()
	{
		EneableCamera("", "");
	}

	void Update()
	{
        if (InventorySystem.Instance.isOpen)
        {
            isInventoryOpen = true;
        }
        else
        {
            isInventoryOpen = false;
        }

        // Nếu inventory đang mở thì ngừng điều khiển camera
        if (isInventoryOpen)
        {
            EneableCamera("", "");
            Cursor.lockState = CursorLockMode.None;  // Hiển thị con trỏ chuột
            Cursor.visible = true;
            isControllingCamera = false;

            return;  // Dừng các thao tác camera khác
        }
        else if (!isInventoryOpen && !isControllingCamera)
        {
            // Khi InventorySystem đóng, tự động bật điều khiển camera mà không cần click
            EneableCamera("Mouse X", "Mouse Y");
            Cursor.lockState = CursorLockMode.Locked;  // Ẩn con trỏ chuột
            Cursor.visible = false;
            isControllingCamera = true;
        }

        // Các xử lý điều khiển camera khi inventory đóng
        if (isFirstPerson)
		{
			ThirdPerson.Priority = 0;
			FirstPerson.Priority = 1;
		}
		else
		{
			ThirdPerson.Priority = 1;
			FirstPerson.Priority = 0;
		}

		if (Input.GetMouseButtonDown(0))
		{
			EneableCamera("Mouse X", "Mouse Y");
			// Hiển thị lại con trỏ chuột và thả khóa
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			isControllingCamera = true;
		}

		if (!isControllingCamera)
		{
			EneableCamera("", "");
			return;
		}

		if (isFirstPerson)
		{
			// Lấy hướng của Cinemachine Camera
			Vector3 cameraForward = Camera.main.transform.forward; // Hướng nhìn của camera
			Vector3 cameraRight = Camera.main.transform.right; // Hướng ngang của camera

			// Tính toán hướng xoay cho nhân vật dựa trên góc quay của camera
			Vector3 newForward = new Vector3(cameraForward.x, 0f, cameraForward.z).normalized;
			playerBody.rotation = Quaternion.Slerp(playerBody.rotation, Quaternion.LookRotation(newForward), Time.deltaTime * 10f);

			// Xoay camera theo chiều dọc (nếu cần)
			transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
		}
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			EneableCamera("", "");
			ResetCamera(0, 0);
			// Hiển thị lại con trỏ chuột và thả khóa
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			isControllingCamera = false;
		}
		if (Input.GetKeyDown(KeyCode.C))
		{
			isFirstPerson = !isFirstPerson;
		}
	}

	void ResetCamera(float inputX, float inputY)
	{
		ThirdPerson.m_XAxis.m_InputAxisValue = inputX;
		ThirdPerson.m_YAxis.m_InputAxisValue = inputY;
		FirstPerson.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_InputAxisValue = inputX;
		FirstPerson.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_InputAxisValue = inputY;
	}
	void EneableCamera(String inputX, String inputY)
	{
		ThirdPerson.m_XAxis.m_InputAxisName = inputX;
		ThirdPerson.m_YAxis.m_InputAxisName = inputY;
		FirstPerson.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_InputAxisName = inputX;
		FirstPerson.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_InputAxisName = inputY;
	}

	void RotatePlayer()
	{

	}

}
