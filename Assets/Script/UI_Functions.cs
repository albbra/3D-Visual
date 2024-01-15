
using UnityEngine;
using TMPro;

public class UI_Functions : MonoBehaviour
{

    [Header("Menu")]
    public TMP_Dropdown dropdownCamera;
    public TMP_Dropdown dropdownScene;
    public TMP_Dropdown dropdownLight;

    [Header("Cameras")]
    public GameObject avatarCamera;
    public GameObject droneCamera;
    public GameObject canvas;

    [Header("Scenes")]
    public GameObject scene_scenario1;

    [Header("Light")]
    public GameObject dayLight;
    public GameObject nightLight;
    public GameObject satellitePlane;
    public Material skyBoxMaterialDay;
    public Material skyBoxMaterialNight;
    public Material planeMaterialDay;
    public Material planeMaterialNight;




    // Start is called before the first frame update
    void Start()
    {
        dropdownCamera.onValueChanged.AddListener(delegate
        {
            switchCamera(dropdownCamera.value);
        });

        dropdownScene.onValueChanged.AddListener(delegate
        {
            switchScene(dropdownScene.value);
        });

        dropdownLight.onValueChanged.AddListener(delegate
        {
            switchDayNight(dropdownLight.value);
        });

        

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void switchScene(int change)
    {
        if (change == 0) //status quo
        {
            scene_scenario1.SetActive(false);

        }
        else // scenario 1
        {
            scene_scenario1.SetActive(true);
            DynamicGI.UpdateEnvironment();
        }
    }



    void switchCamera(int change)
    {

        if (change == 0) //avatar
        {
            Camera cam = avatarCamera.GetComponent<Camera>();
 
            avatarCamera.SetActive(true);
            droneCamera.SetActive(false);
            canvas.GetComponent<Canvas>().worldCamera = Camera.main;
        }
        else if (change == 1) //drone
        {
            Camera cam = droneCamera.GetComponent<Camera>();
 
            droneCamera.SetActive(true);
            avatarCamera.SetActive(false);
            canvas.GetComponent<Canvas>().worldCamera = Camera.main;
        }
    }

    void switchDayNight(int change)
    {
        if (change == 0) //day
        {
            dayLight.SetActive(true);
            nightLight.SetActive(false);
            satellitePlane.GetComponent<MeshRenderer>().material = planeMaterialDay;
            RenderSettings.skybox = skyBoxMaterialDay;
            DynamicGI.UpdateEnvironment();
        }
        else //night
        {
            dayLight.SetActive(false);
            nightLight.SetActive(true);
            satellitePlane.GetComponent<MeshRenderer>().material = planeMaterialNight;
            RenderSettings.skybox = skyBoxMaterialNight;
            DynamicGI.UpdateEnvironment();
        }
    }
}
