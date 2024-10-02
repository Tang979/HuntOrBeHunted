using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class Cameracontroller : MonoBehaviour
{
	public CinemachineFreeLook ThirdPerson; // Tham chiếu đến Cinemachine FreeLook Camera
	public CinemachineVirtualCamera FirstPerson;


	bool isFirstPerson = true;
	private bool isControllingCamera = false;  // Biến kiểm tra trạng thái điều khiển camera

	void Start()
	{
		EneableCamera("","");
	}

	void Update()
	{
		if(isFirstPerson)
		{
			ThirdPerson.Priority = 0;
			FirstPerson.Priority = 1;
		}
		else
		{
			ThirdPerson.Priority = 1;
			FirstPerson.Priority = 0;
		}
		
		// Kiểm tra khi người chơi nhấn chuột trái
		if (Input.GetMouseButtonDown(0))
		{
			// Khóa và ẩn con trỏ chuột
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			isControllingCamera = true;
			EneableCamera("Mouse X", "Mouse Y");
		}

		if(!isControllingCamera)
			return;
		
		// Kiểm tra khi người chơi nhấn ESC để dừng điều khiển camera
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			EneableCamera("", "");
			ResetCamera(0,0);
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
