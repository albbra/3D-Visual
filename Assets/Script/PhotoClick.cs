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


    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
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
            default: return texture1;
        }
    }

}
