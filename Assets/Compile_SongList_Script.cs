using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class GitHubFileList : MonoBehaviour
{
    public List<string> fileList = new List<string>();
    public Startup_Script start;

    private void Start()
    {
        start = gameObject.GetComponent<Startup_Script>();
        StartCoroutine(GetFileList());
    }

    IEnumerator GetFileList()
    {
        string apiUrl = $"https://api.github.com/repos/Beast-Bourne/Music_Storage/contents/";

        UnityWebRequest request = UnityWebRequest.Get(apiUrl);

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError($"Error: {request.error}");
        }
        else
        {
            string jsonResponse = request.downloadHandler.text;

            // Parse the JSON response
            GitHubContent[] contents = JsonHelper.FromJson<GitHubContent>(jsonResponse);

            // Extract file names
            foreach (var content in contents)
            {
                fileList.Add(content.name.Replace(".mp3", "").ToUpper());
            }
        }

        start.listSplitter();
    }
}

[System.Serializable]
public class GitHubContent
{
    public string name;
    // Add other properties if needed
}

// Helper class for JSON array deserialization
public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        string newJson = "{ \"array\": " + json + "}";
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
        return wrapper.array;
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }
}