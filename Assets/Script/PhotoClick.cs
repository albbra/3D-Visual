using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PhotoClick : MonoBehaviour
{
    public GameObject image_photo;
    public Texture texture1;
    public Texture texture2;
    public Texture texture3;
    public Texture texture4;
    public Texture texture5;
    public Texture texture6;
    public Texture texture7;
    public Texture texture8;
    public Texture texture9;


    private void Update()
    {

        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            RaycastHit hit;
            Ray ray = gameObject.GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 1000.0f))
            {

                if ((hit.transform != null) && (hit.transform.gameObject.tag == "PhotoGO"))
                {
                    image_photo.SetActive(true);
                    RawImage img = image_photo.GetComponent<RawImage>();
                    img.texture = InfoToImage(hit.transform.gameObject.name);
                }

            }

        }
    }


    private Texture InfoToImage(string nameInfo)
    {
        switch (nameInfo)
        {
            case "building": return texture1;
            case "building2": return texture2;
            case "building4": return texture3;
            case "building9": return texture4;
            case "Mensa": return texture5;
            case "building3": return texture6;
            case "ZME": return texture7;
            case "Parkhaus": return texture8;
            //case "campus": return texture9;
            default: return texture9;
        }
    }

}
