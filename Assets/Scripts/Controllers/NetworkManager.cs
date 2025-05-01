using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
using System;

// Handles communication between Unity and the backend server

[Serializable]
public class ErrorResponse
{
    public string error;
}

[Serializable]
public class LoginResponse
{
    public string token;
}

[Serializable]
public class LeaderboardEntry
{
    public string username;
    public int high_score;
}

[Serializable]
public class RegisterRequest
{
    public string username;
    public string password;
}

[Serializable]
public class LoginRequest
{
    public string username;
    public string password;
}

[Serializable]
public class ScoreRequest
{
    public int score;
}

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager Instance { get; private set; }

    [Header("Server Configuration")]
    public string serverUrl = "http://localhost:3000/api";

    private string authToken;

    private void Awake()
    {
        // Ensure one persistent instance
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            authToken = PlayerPrefs.GetString("AuthToken", "");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public IEnumerator Register(string username, string password, Action<bool, string> callback)
    {
        var request = new UnityWebRequest($"{serverUrl}/register", "POST");
        var registerRequest = new RegisterRequest { username = username, password = password };
        var json = JsonUtility.ToJson(registerRequest);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            callback(true, "Registration successful!");
        }
        else
        {
            Debug.LogError($"Registration failed: {request.error}");
            Debug.LogError($"Response: {request.downloadHandler.text}");

            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                callback(false, "Could not connect to server. Please make sure the server is running.");
            }
            else
            {
                try
                {
                    var errorResponse = JsonUtility.FromJson<ErrorResponse>(request.downloadHandler.text);
                    callback(false, errorResponse.error);
                }
                catch
                {
                    callback(false, "An error occurred during registration");
                }
            }
        }
    }

    public IEnumerator Login(string username, string password, Action<bool, string> callback)
    {
        var request = new UnityWebRequest($"{serverUrl}/login", "POST");
        var loginRequest = new LoginRequest { username = username, password = password };
        var json = JsonUtility.ToJson(loginRequest);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            var response = JsonUtility.FromJson<LoginResponse>(request.downloadHandler.text);
            authToken = response.token;
            PlayerPrefs.SetString("AuthToken", authToken);
            callback(true, "Login successful!");
        }
        else
        {
            Debug.LogError($"Login failed: {request.error}");
            Debug.LogError($"Response: {request.downloadHandler.text}");

            if (request.result == UnityWebRequest.Result.ConnectionError)
            {
                callback(false, "Could not connect to server. Please make sure the server is running.");
            }
            else
            {
                try
                {
                    var errorResponse = JsonUtility.FromJson<ErrorResponse>(request.downloadHandler.text);
                    callback(false, errorResponse.error);
                }
                catch
                {
                    callback(false, "An error occurred during login");
                }
            }
        }
    }

    public IEnumerator SubmitScore(int score, Action<bool, string> callback)
    {
        if (string.IsNullOrEmpty(authToken))
        {
            callback(false, "Not logged in");
            yield break;
        }

        var request = new UnityWebRequest($"{serverUrl}/scores", "POST");
        var scoreRequest = new ScoreRequest { score = score };
        var json = JsonUtility.ToJson(scoreRequest);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(json);

        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("Authorization", $"Bearer {authToken}");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            callback(true, "Score submitted successfully!");
        }
        else
        {
            var errorResponse = JsonUtility.FromJson<ErrorResponse>(request.downloadHandler.text);
            callback(false, errorResponse.error);
        }
    }

    public IEnumerator GetLeaderboard(Action<bool, LeaderboardEntry[]> callback)
    {
        var request = UnityWebRequest.Get($"{serverUrl}/leaderboard");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            var json = request.downloadHandler.text;
            var entries = JsonHelper.FromJson<LeaderboardEntry>(json);
            callback(true, entries);
        }
        else
        {
            callback(false, null);
        }
    }

    public void Logout()
    {
        authToken = "";
        PlayerPrefs.DeleteKey("AuthToken");
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

        [Serializable]
        private class Wrapper<T>
        {
            public T[] array;
        }
    }
}