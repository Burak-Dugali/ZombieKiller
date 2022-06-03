using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneManager : MonoBehaviour
{
    public CameraController Camera;
    public PlayerController PlayerController;
    public CharacterController CharacterController;

    public void Start()
    {
        Camera.enabled = false;
        CharacterController.enabled = false;
        PlayerController.enabled = false;
    }
}
