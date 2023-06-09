using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMap : MonoBehaviour
{
    public GameObject map;
    public RectTransform playerBlimp;
    public RectTransform uiMapCorner;
    public Transform levelCentre;
    public Transform levelCorner;

    private Vector3 normalized, mapped;
    private Transform player, cameraControls;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        cameraControls = GameObject.Find("CameraControls").GetComponent<Transform>();
    }

    private void Start()
    {
        map.SetActive(false);
    }
    private void Update()
    {
        if (!PauseSystem.instance.isPaused || PauseSystem.instance.documentsAreOpen)
            map.SetActive(false);
    }

    private static Vector3 Divide(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x / b.x, a.y / b.y, a.z / b.z);
    }

    private static Vector3 Multiply(Vector3 a, Vector3 b)
    {
        return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
    }
    
    public void ShowMap()
    {
        map.SetActive(!map.activeSelf);

        normalized = Divide(levelCentre.InverseTransformPoint(player.transform.position), levelCorner.position - levelCentre.position);
        normalized.y = normalized.z;
        mapped = Multiply(normalized, uiMapCorner.localPosition);
        mapped.z = 0;
        playerBlimp.localPosition = mapped;
        playerBlimp.localRotation = Quaternion.Euler(0, 0, -cameraControls.eulerAngles.y);
    }
}
