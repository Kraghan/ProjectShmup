using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    [SerializeField]
    public CameraController camera;

    [SerializeField]
    public GameInitializer gameManager;

    [SerializeField]
    public MusicManager musicManager;

    [SerializeField]
    public Canvas UI;

    [SerializeField]
    public PlayerSpawner playerSpawner;
}