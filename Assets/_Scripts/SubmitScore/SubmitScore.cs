using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;

public class SubmitScore : MonoBehaviour
{
    public string addScoreURL; //be sure to add a ? to your url
    public GameObject nameJoueur;
    public GameObject score;
    public GameObject emailJoueur;
    public GameObject errorText;
    public GameObject sendButton;

    void Start()
    {

    }

    public void Envoyer()
    {
        sendButton.GetComponent<Button>().enabled = false;
        StartCoroutine(PostScores());
    }

    // remember to use StartCoroutine when calling this function!
    IEnumerator PostScores()
    {
        errorText.SetActive(false);
        if (VerifEmail(emailJoueur.GetComponent<TextMeshProUGUI>().text))
        {
            List<IMultipartFormSection> formData = new List<IMultipartFormSection>();
            if (nameJoueur.GetComponent<TextMeshProUGUI>().text.Substring(0, nameJoueur.GetComponent<TextMeshProUGUI>().text.Length - 1) != "")
            {
                formData.Add(new MultipartFormDataSection("name", nameJoueur.GetComponent<TextMeshProUGUI>().text.Substring(0, nameJoueur.GetComponent<TextMeshProUGUI>().text.Length - 1)));
            }
            else
            {
                formData.Add(new MultipartFormDataSection("name", "anonymous"));
            }

            formData.Add(new MultipartFormDataSection("email", emailJoueur.GetComponent<TextMeshProUGUI>().text.Substring(0, emailJoueur.GetComponent<TextMeshProUGUI>().text.Length - 1)));

            formData.Add(new MultipartFormDataSection("score", score.GetComponent<TextMeshProUGUI>().text));

            UnityWebRequest www = UnityWebRequest.Post(addScoreURL, formData);
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Form upload complete!");
                Debug.Log(www.downloadHandler.text);
            }
            StartCoroutine(iLoadNext());
            
        }
        else
        {
            errorText.SetActive(true);
            sendButton.GetComponent<Button>().enabled = true;
        }
        
    }

    public void LoadNext() => StartCoroutine(iLoadNext());
    IEnumerator iLoadNext()
    {
        TransitionsSystem.Instance.CloseRid();
        yield return new WaitForSeconds(1.3f);
        SceneManager.LoadScene("MAIN_MENU", LoadSceneMode.Single);
    }

    private bool VerifEmail(string mailToVerif)
    {
        if (mailToVerif.IndexOf("@") == -1)
        {
            return false;
        }
        string[] tmpString= mailToVerif.Split('@');
        if (tmpString.Length > 2)
        {
            return false;
        }
        if (tmpString[0].IndexOf('.') == -1)
        {
            return false;
        }
        if(tmpString[1].IndexOf('.') == -1)
        {
            return false;
        }
        return true;
    }

}

