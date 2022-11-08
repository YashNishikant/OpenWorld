
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Building : MonoBehaviour
{

    private RaycastHit hit;
    private bool building;
    private bool firsttime;
    private int blockChoice;
    private Vector3 translatepos;
    private Vector3 objrotation;
    List<GameObject> holoList = new List<GameObject>();
    GameObject h;
    [SerializeField]
    private float dist;
    [SerializeField]
    private List<GameObject> BuildPrefab = new List<GameObject>();
    [SerializeField]
    private List<GameObject> BuildHolographs = new List<GameObject>();
    [SerializeField]
    private Image crosshair;
    [SerializeField]
    private Image b_image;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.B))
        {
            building = !building;
        }

        if (building)
        {
            raycasting();
            changeBuild();
            b_image.enabled = true;
        }
        else
        {
            b_image.enabled = false;
            if(h != null)
                Destroy(h.gameObject);
            holoList.Clear();
            destroy();
        }
    }

    void destroy()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, dist))
            {
                if (hit.collider.tag == "placed")
                {
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }

    void changeBuild()
    {

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            blockChoice++;
        }

        if (blockChoice > BuildPrefab.Count - 1)
        {
            blockChoice = 0;
        }

    }

    void raycasting()
    {

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, dist))
        {
            firsttime = true;

            crosshair.enabled = true;

            h = Instantiate(BuildHolographs[blockChoice], transform.TransformDirection(Vector3.forward), Quaternion.identity);
            holoList.Add(h);
            h.transform.Rotate(objrotation);

            if (holoList.Count > 1) {
                Destroy(holoList[holoList.Count - 2].gameObject);
                holoList.RemoveAt(holoList.Count - 2);
            }

            h.SetActive(true);
            determineplacement(h, hit);
        }
        else if(firsttime)
        {
            crosshair.enabled = false;
            if(holoList.Count > 0)
            holoList[holoList.Count - 1].gameObject.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0))
        {

            if (blockChoice == 2) {
                objrotation += new Vector3(0, 90, 0);
            }

        }

        if (Input.GetMouseButtonDown(1))
        {

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, dist))
            {

                if (blockChoice == 0)
                    translatepos = hit.transform.localScale;

                if (blockChoice == 1)
                    translatepos = hit.transform.localScale * 2;

                if (blockChoice == 2)
                    translatepos = hit.transform.localScale * 2;

                GameObject item = Instantiate(BuildPrefab[blockChoice]) as GameObject;
                item.transform.Rotate(objrotation);
                determineplacement(item, hit);
            }
        }
    }

    void determineplacement(GameObject item, RaycastHit hit2)
    {

        item.transform.position = hit2.point + hit2.normal;

        if (hit.collider.tag.Equals("placed"))
        {

            if (Physics.Raycast(item.transform.position, Vector3.left, out hit, item.transform.localScale.x))
            {
                item.transform.position = hit.transform.position + new Vector3(translatepos.x, 0, 0);
                return;
            }

            if (Physics.Raycast(item.transform.position, Vector3.right, out hit, item.transform.localScale.x))
            {
                item.transform.position = hit.transform.position + new Vector3(-translatepos.x, 0, 0);
                return;
            }

            if (Physics.Raycast(item.transform.position, Vector3.forward, out hit, item.transform.localScale.z))
            {
                item.transform.position = hit.transform.position + new Vector3(0, 0, -translatepos.z);
                return;
            }

            if (Physics.Raycast(item.transform.position, Vector3.back, out hit, item.transform.localScale.z))
            {
                item.transform.position = hit.transform.position + new Vector3(0, 0, translatepos.z);
                return;
            }

            if (Physics.Raycast(item.transform.position, Vector3.down, out hit, item.transform.localScale.y))
            {
                item.transform.position = hit.transform.position + new Vector3(0, translatepos.y, 0);
                return;
            }

            if (Physics.Raycast(item.transform.position, Vector3.up, out hit, item.transform.localScale.y))
            {
                item.transform.position = hit.transform.position + new Vector3(0, -translatepos.y, 0);
                return;
            }

        }

    }
}
