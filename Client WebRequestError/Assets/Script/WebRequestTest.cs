using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequestTest : MonoBehaviour
{
    public UnityEngine.UI.Toggle forceCertificate;
    public TextMeshProUGUI keyLabel;
    public TextMeshProUGUI domainLabel;

    public TextMeshProUGUI consoleKey;
    public TextMeshProUGUI consoleDomain;

    public void TestKeyCall()
    {
        StartCoroutine(PingServer(0));
    }

    public void TestDomainCall()
    {
        StartCoroutine(PingServer(1));
    }
    
    private IEnumerator PingServer(int value)
    {
        string createUrl = string.Empty;
        
        switch (value)
        {
            case 0: // Test Key
                createUrl = "https://franfndz.com/Unity/pingResponse.php?appView=" + keyLabel.text;
                break;
            case 1: // Test Domain // Will give Error
                createUrl = UnityWebRequest.UnEscapeURL("https://" + domainLabel.text + ".com/Unity/pingResponse.php?appView=domainTest");
                break;
                
        }

        Debug.Log(createUrl);
        
        if (string.IsNullOrEmpty(createUrl))
            yield break;

        
        string answer = string.Empty;

        createUrl = Regex.Replace(createUrl, @"[^\u0000-\u007F]+", string.Empty);
        
        using (UnityWebRequest www = UnityWebRequest.Get(createUrl))
        {
            if(forceCertificate.isOn)
                www.certificateHandler =  new WebRequestCert();
            
            www.SendWebRequest();

            while (!www.isDone)
            {
                yield return new WaitForEndOfFrame();
            }

            answer = createUrl;
            answer += "\n";
            
            if (www.isNetworkError)
            {
                answer += www.error;
            }
            else
            {
                answer += www.downloadHandler.text;
                    
            }
        }

        switch (value)
        {
            case 0: // Test Key
                consoleKey.text  = answer;
                break;
            case 1: // Test Domain
                consoleDomain.text = answer;
                break;   
        }
    }
}
