using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;

public class OpenAIManager : MonoBehaviour
{
    public static OpenAIManager Instance { get; private set; }

    private const string API_URL = "https://api.openai.com/v1/chat/completions";

    private string apiKey;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        apiKey = System.Environment.GetEnvironmentVariable("OPENAI_API_KEY");

        if (string.IsNullOrEmpty(apiKey))
        {
            Debug.LogError("OPENAI_API_KEY is not set");
        }
    }

    public IEnumerator SendMessage(string userMessage, string npcPrompt, System.Action<string> onComplete)
    {
        // OpenAI의 ChatGPT API 호출 (gpt-3.5-turbo 모델)
        var requestData = new ChatRequest
        {
            //model = "gpt-3.5-turbo",
            model = "gpt-4o-mini",
            messages = new Message[]
            {
                new Message { role = "system", content = npcPrompt },
                new Message { role = "user", content = userMessage }
            },
            max_tokens = 200
        };

        string jsonBody = JsonUtility.ToJson(requestData);

        // UnityWebRequest를 이용한 HTTP POST 요청
        using (UnityWebRequest request = new UnityWebRequest(API_URL, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", $"Bearer {apiKey}");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                var response = JsonUtility.FromJson<ChatResponse>(request.downloadHandler.text);
                string assistantMessage = response.choices[0].message.content;
                onComplete?.Invoke(assistantMessage);
            }
            else
            {
                Debug.LogError($"API Error: {request.error}");
                Debug.LogError(request.downloadHandler.text);
                onComplete?.Invoke("죄송합니다, 응답을 받지 못했습니다.");
            }
        }
    }
}

[System.Serializable]
public class ChatRequest
{
    public string model;
    public Message[] messages;
    public int max_tokens;
}

[System.Serializable]
public class Message
{
    public string role;
    public string content;
}

[System.Serializable]
public class ChatResponse
{
    public Choice[] choices;
}

[System.Serializable]
public class Choice
{
    public Message message;
}