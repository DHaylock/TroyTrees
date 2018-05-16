using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using UnityEngine.UI;
using TMPro;

public class Getter : MonoBehaviour {


    public Canvas cnv;
    public GameObject target;
    public Image prefab;
    [Range(0, 100)]
    public int distance = 50;

    // Use this for initialization
    void Start()
    {
        Wifi
        //Debug.Log(nics.Length);
	}
	
    public void GetTrees() {
        foreach(Transform child in target.transform) {
            GameObject.Destroy(child.gameObject);    
        }

        StartCoroutine(GetTressNearby());
    }

    IEnumerator GetTressNearby() {

        float lat = GPSManager.Instance.lat;
        float lng = GPSManager.Instance.lng;
        UnityWebRequest www = UnityWebRequest.Get("https://opendata.bristol.gov.uk/api/records/1.0/search/?dataset=trees&rows=200&facet=site_name&facet=type&facet=dead&facet=feature_type_name&facet=common_name&facet=crown_area&facet=location_risk_zone&geofilter.distance="+lat.ToString()+"%2C"+lng.ToString()+"%2C"+distance.ToString());
        yield return www.SendWebRequest();

        if(www.isNetworkError) {
            Debug.Log(www.error);
        }
        else {



            // Show results as text
            //Debug.Log(www.downloadHandler.text);
            var data = JSON.Parse(www.downloadHandler.text.ToString());
            Debug.Log(data);

            Debug.Log(data["records"]);

            foreach(JSONObject tree in data["records"]) {
                //Debug.Log(tree["fields"]["dist"]);
                Debug.Log(tree["fields"]["dead"]);
                //Debug.Log(tree["fields"]["latin_name"]);
                Image newImage = Instantiate(prefab, cnv.transform.position, Quaternion.identity);
                newImage.transform.parent = target.transform;


                if (tree["fields"]["dead"].ToString() == "Y")
                {
                    newImage.color = new Color(189, 195, 199);
                }

                

                newImage.rectTransform.localPosition = new Vector2(Random.Range(-300, 300), Random.Range(-400, 450));
                newImage.rectTransform.sizeDelta = new Vector2(250 - tree["fields"]["dist"].AsInt * 3,250 - tree["fields"]["dist"].AsInt * 3);
                newImage.GetComponentInChildren<TextMeshProUGUI>().text = tree["fields"]["latin_name"];
            }


            Debug.Log(data["nhits"].AsInt);
        }
    }

}
