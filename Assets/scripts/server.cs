using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;


public class server : MonoBehaviour
{
    [SerializeField] GameObject welcomePanel;
    [SerializeField] GameObject loginPanel;
    [SerializeField] GameObject gamePanel;
    [SerializeField] GameObject gameoverPanel;
    [SerializeField] GameObject registerPanel;
	[SerializeField] TextMeshProUGUI user;
    [SerializeField] TextMeshProUGUI score;
	[Space]
	[SerializeField] TMP_InputField usernameLogin;
	[SerializeField] TMP_InputField passwordLogin;
    [SerializeField] TMP_InputField usernameRegister;
	[SerializeField] TMP_InputField passwordRegister;

	[SerializeField] TextMeshProUGUI errorMessages;
	[SerializeField] GameObject progressCircle;

	[SerializeField] Button loginButton;

	WWWForm form;

	public void OnLoginButtonClicked ()
	{
		loginButton.interactable = false;
		progressCircle.SetActive (true);
		StartCoroutine(Login());
	}

    public void OnRegisterButtonClicked ()
	{
		loginButton.interactable = false;
		progressCircle.SetActive (true);
		StartCoroutine(RegisterUser());
	}

    public void playGame()
	{
		welcomePanel.SetActive(false);
		gamePanel.SetActive(true);
	}

    public void mainMenu()
	{
        StartCoroutine(UpdateScore());
		gameoverPanel.SetActive(false);
        welcomePanel.SetActive(true);
	}

    public void RegisterMenu()
	{
		loginPanel.SetActive(false);
        registerPanel.SetActive(true);
	}

	IEnumerator Login()
	{
		form = new WWWForm ();

		form.AddField ("username", usernameLogin.text);
		form.AddField ("password", passwordLogin.text);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityDbGame/index.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            errorMessages.text = "404 not found!";
            Debug.Log("<color=red>" + www.error + "</color>"); //error
        }
        else
        {
            if (www.isDone)
            {
                if (www.downloadHandler.text.Contains("error"))
                {
                    errorMessages.text = "invalid username or password!";
                    Debug.Log("<color=red>" + www.downloadHandler.text + "</color>"); //error
                }
                else
                {
                    //open welcom panel
                    loginPanel.SetActive(false);
                    welcomePanel.SetActive(true);
                    user.text = usernameLogin.text;
                    Debug.Log("<color=green>" + www.downloadHandler.text + "</color>"); //user exist
                }
            }
        }
		// WWW w = new WWW (url, form);
		// yield return w;

		// if (w.error != null) {
		// 	errorMessages.text = "404 not found!";
		// 	Debug.Log("<color=red>"+w.text+"</color>");//error
		// } else {
		// 	if (w.isDone) {
		// 		if (w.text.Contains ("error")) {
		// 			errorMessages.text = "invalid username or password!";
		// 			Debug.Log("<color=red>"+w.text+"</color>");//error
		// 		} else {
		// 			//open welcom panel
        //             loginPanel.SetActive(false);
		// 			welcomePanel.SetActive (true);
		// 			user.text = username.text;
		// 			Debug.Log("<color=green>"+w.text+"</color>");//user exist
		// 		}
		// 	}
		// }

		loginButton.interactable = true;
		progressCircle.SetActive (false);

		www.Dispose ();
	}

    IEnumerator RegisterUser()
	{
		form = new WWWForm ();
		form.AddField ("username", usernameRegister.text);
		form.AddField ("password", passwordRegister.text);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityDbGame/register.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            errorMessages.text = "404 not found!";
            Debug.Log("<color=red>" + www.error + "</color>"); //error
        }
        else
        {
            if (www.isDone)
            {
                if (www.downloadHandler.text.Contains("error"))
                {
                    errorMessages.text = "invalid username or password!";
                    Debug.Log("<color=red>" + www.downloadHandler.text + "</color>"); //error
                }
                else
                {
                    //open welcom panel
                    registerPanel.SetActive(false);
                    welcomePanel.SetActive(true);
                    user.text = usernameRegister.text;
                    Debug.Log("<color=green>" + www.downloadHandler.text + "</color>"); //user exist
                }
            }
        }
		loginButton.interactable = true;
		progressCircle.SetActive (false);
		www.Dispose ();
	}

    IEnumerator UpdateScore()
	{
		form = new WWWForm ();
		form.AddField ("username", user.text);
		form.AddField ("score", ScoreManager.instance.UpdateScoreText());

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/UnityDbGame/scores.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            errorMessages.text = "404 not found!";
            Debug.Log("<color=red>" + www.error + "</color>"); //error
        }
        else
        {
            if (www.isDone)
            {
                score.text = www.downloadHandler.text;
                Debug.Log("<color=green>" + www.downloadHandler.text + "</color>");
            }
        }
	}
}
