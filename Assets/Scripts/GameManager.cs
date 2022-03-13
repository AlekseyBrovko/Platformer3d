using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private Vector3 respawnPosition;

    private void Awake()
    {
        instance = this;
    }


    void Start()
    {
        //����������� �������
        Cursor.visible = false;
        //����� ������ �� ������� �� ����� ����
        Cursor.lockState = CursorLockMode.Locked;
        //���������� ����� ��������
        respawnPosition = PlayerController.instance.transform.position;
        //����� �������� ������
    }

   
    void Update()
    {
        
    }

    public void Respawn()
    {
        StartCoroutine(RespawnCo());
    }

    public IEnumerator RespawnCo()
    {
        PlayerController.instance.gameObject.SetActive(false);

        CameraController.instance.theCMBrain.enabled = false;

        yield return new WaitForSeconds(2f);
        PlayerController.instance.transform.position = respawnPosition;
        CameraController.instance.theCMBrain.enabled = true;
        PlayerController.instance.gameObject.SetActive(true);
    }
}
