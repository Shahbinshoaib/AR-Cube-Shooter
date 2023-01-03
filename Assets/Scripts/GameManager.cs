using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    public Transform arCamera;
    public GameObject cubeHolder;
    public GameObject loading;
    public AnimationClip CameraOn;
    public AnimationClip CameraOff;
    public GameObject[] Cubes;
    public Transform spawnPoint;
    public TextMeshProUGUI scoreText;
    public AudioSource collectSound;
    public int cubesInStack;
    Vector3 spawnPos;

    public Transform shooterSpawnPoint;

    int score = 0;
    public bool isShooted = false;
    bool shooterInitialized = false;
    public GameObject spawnedCube;
    private Animation loadingAnim;
    private AudioSource loadionAudio;
    // Start is called before the first frame update
    void Start()
    {
        loadingAnim = loading.GetComponent<Animation>();
        loadionAudio = loading.GetComponent<AudioSource>();
        collectSound = GetComponent<AudioSource>();
        StartCoroutine(InitilizeGamePlay());//Start Game
    }

    IEnumerator InitilizeGamePlay()
    { 
        loadingAnim.clip = CameraOn;// Setting the Camera Shutter On Animation
        yield return new WaitForSeconds(1);
        loadingAnim.Play();
        loadionAudio.Play();
        yield return new WaitForSeconds(1);
        loading.SetActive(false);

    }

    private void LateUpdate()
    {
        //Updating the Shooter Cube Position

        if (!isShooted)
        {
            spawnedCube.transform.position = cubeHolder.transform.position;
            spawnedCube.transform.rotation = cubeHolder.transform.rotation;
        }
        if((spawnedCube.transform.position - cubeHolder.transform.position).magnitude > 5 && !shooterInitialized)
        {
            Destroy(spawnedCube);
            InitilizeShooter();

        }
       

        //Input.GetTouch(0);
    }

    public void SpawnCubes() //Cube Stack Spawner
    {
        spawnPos = spawnPoint.position; //Setting the cube stack just above the visual mark
        for(int i = 0; i < cubesInStack; i++)
        {
            int random = Random.Range(0, Cubes.Length);
            GameObject cube = Instantiate(Cubes[random]);
            cube.GetComponent<Rigidbody>().isKinematic = true; //Fixing the position of Cubes
            cube.GetComponent<CubeController>().enabled = false;
            spawnPos += new Vector3(0, 0.05f, 0); //Distance between each cube
        }
        if (!shooterInitialized)
        {
            InitilizeShooter();

        }
        
        
    }

    public void InitilizeShooter()// Place the user cube shooter
    {
        //Spawns a random cube every time just below the AR Camera
        shooterInitialized = true;
        isShooted = false;
        int random = Random.Range(0, Cubes.Length);
        spawnedCube = Instantiate(Cubes[random], cubeHolder.transform.position, cubeHolder.transform.rotation);
        spawnedCube.GetComponent<Rigidbody>().isKinematic = true;
    }

    public void ShootCube() // Shoots the cube
    {
        isShooted = true;
        shooterInitialized = false;
        Rigidbody cubeRB = spawnedCube.GetComponent<Rigidbody>();
        cubeRB.isKinematic = false;
        cubeRB.AddForce(cubeHolder.transform.forward * 4, ForceMode.Impulse);
        cubeRB.AddForce(cubeHolder.transform.up * 2, ForceMode.Impulse);


    }

    public void AddScore()
    {
        //Destroy the cube and add the score

        collectSound.Play();
        score++;
        scoreText.text = score.ToString();
        Destroy(spawnedCube);
        InitilizeShooter();
        Debug.Log("4");
        isShooted = false;
    }
}
