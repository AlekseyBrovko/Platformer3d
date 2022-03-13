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
        //невидимость курсора
        Cursor.visible = false;
        //чтобы курсор не выходил за экран игры
        Cursor.lockState = CursorLockMode.Locked;
        //назначение точки респауна
        respawnPosition = PlayerController.instance.transform.position;
        //точка респауна камеры
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
