using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class Client
{
    public bool isManager;
    public int id;
    public string label;
}

[Serializable]
public class Data
{
    public string address;
    public string name;
    public int points;
}

[Serializable]
public class Response
{
    public string label;
    public Client[] clients;
    public Dictionary<string, Data> data;
}

public class APIController : MonoBehaviour
{
    string apiEndpoint = "https://qa2.sunbasedata.com/sunbase/portal/api/assignment.jsp?cmd=client_data";

    public Response res;
    public GameObject ClientPrefab;
    public GameObject ClientContainer;
    public GameObject PopupContainer;

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
                string jsonResponse = www.downloadHandler.text.Trim();
                res = JsonConvert.DeserializeObject<Response>(jsonResponse);

                showAll();
            }
        }
    }

    void clearList()
    {
        foreach (Transform client in ClientContainer.transform)
        {
            Destroy(client.gameObject);
        }
    }

    public void dropdownValChanged(int newVal)
    {
        clearList();
        if (newVal == 0) showAll();
        else if (newVal == 1) showManagers();
        else if (newVal == 2) showNonManagers();
        else showAll();
    }

    void showAll()
    {
        int index = 0;
        foreach (Client client in res.clients)
        {
            // Debug.Log(client.label);
            GameObject clientX = Instantiate(ClientPrefab);
            clientX.transform.parent = ClientContainer.transform;
            clientX.GetComponent<ClientItemController>().setDetails(client.label, client.isManager, client.id, index, res);
            index++;
        }
    }

    void showManagers()
    {
        int index = 0;
        foreach (Client client in res.clients)
        {
            if (client.isManager)
            {
                GameObject clientX = Instantiate(ClientPrefab);
                clientX.transform.parent = ClientContainer.transform;
                clientX.GetComponent<ClientItemController>().setDetails(client.label, client.isManager, client.id, index, res);
                index++;
            }
        }
    }

    void showNonManagers()
    {
        int index = 0;
        foreach (Client client in res.clients)
        {
            if (!client.isManager)
            {
                GameObject clientX = Instantiate(ClientPrefab);
                clientX.transform.parent = ClientContainer.transform;
                clientX.GetComponent<ClientItemController>().setDetails(client.label, client.isManager, client.id, index, res);
                index++;
            }
        }
    }
}
