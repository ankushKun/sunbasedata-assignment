using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class Response
{
    [Serializable]
    public class Clients
    {
        public bool isManager;
        public int id;
        public string Label;
    }

    [Serializable]
    public class Data
    {
        public string address;
        public string name;
        public int points;
    }

    public string label;
    public Clients[] clients;
    public Dictionary<int, Data> data;
}

public class APIController : MonoBehaviour
{
    string apiEndpoint = "https://qa2.sunbasedata.com/sunbase/portal/api/assignment.jsp?cmd=client_data";
    string jsonResponse;

    void Start()
    {
        StartCoroutine(getRequest());
    }

    IEnumerator getRequest()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(apiEndpoint))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log("Error While Sending: " + www.error);
            }
            else
            {
                // Debug.Log("Received: " + www.downloadHandler.text.Trim());
                jsonResponse = www.downloadHandler.text.Trim();
                Response res = JsonUtility.FromJson<Response>(jsonResponse);

                Debug.Log(res.label);
            }
        }
    }

    void Update()
    {

    }
}
