using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    [Header("Login")]
    public TMP_InputField loginUsernameInput;
    public TMP_InputField loginPasswordInput;
    public Button loginButton;
    public TextMeshProUGUI loginMessageText;

    [Header("Register")]
    public TMP_InputField registerUsernameInput;
    public TMP_InputField registerPasswordInput;
    public Button registerButton;
    public TextMeshProUGUI registerMessageText;

    [Header("Panels")]
    public GameObject loginPanel;
    public GameObject registerPanel;
    public Button switchToRegisterButton;
    public Button switchToLoginButton;

    private void Start()
    {
        // Setup button listeners
        loginButton.onClick.AddListener(OnLoginClicked);
        registerButton.onClick.AddListener(OnRegisterClicked);
        switchToRegisterButton.onClick.AddListener(SwitchToRegister);
        switchToLoginButton.onClick.AddListener(SwitchToLogin);

        // Show login panel by default
        SwitchToLogin();

        // Add input validation
        loginUsernameInput.onValueChanged.AddListener(_ => ValidateLoginInputs());
        loginPasswordInput.onValueChanged.AddListener(_ => ValidateLoginInputs());
        registerUsernameInput.onValueChanged.AddListener(_ => ValidateRegisterInputs());
        registerPasswordInput.onValueChanged.AddListener(_ => ValidateRegisterInputs());
    }

    private void ValidateLoginInputs()
    {
        bool isValid = !string.IsNullOrEmpty(loginUsernameInput.text) &&
                      !string.IsNullOrEmpty(loginPasswordInput.text);
        loginButton.interactable = isValid;
    }

    private void ValidateRegisterInputs()
    {
        bool isValid = !string.IsNullOrEmpty(registerUsernameInput.text) &&
                      !string.IsNullOrEmpty(registerPasswordInput.text);
        registerButton.interactable = isValid;
    }

    private void SwitchToLogin()
    {
        loginPanel.SetActive(true);
        registerPanel.SetActive(false);
        ClearMessages();
        ValidateLoginInputs();
    }

    private void SwitchToRegister()
    {
        loginPanel.SetActive(false);
        registerPanel.SetActive(true);
        ClearMessages();
        ValidateRegisterInputs();
    }

    private void ClearMessages()
    {
        loginMessageText.text = "";
        registerMessageText.text = "";
    }

    private void OnLoginClicked()
    {
        string username = loginUsernameInput.text;
        string password = loginPasswordInput.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            loginMessageText.text = "Please enter both username and password";
            return;
        }

        loginButton.interactable = false;
        loginMessageText.text = "Logging in...";

        StartCoroutine(NetworkManager.Instance.Login(username, password, (success, message) =>
        {
            loginButton.interactable = true;
            loginMessageText.text = message;
            if (success)
            {
                // Load main menu after successful login
                SceneManager.LoadScene("MainMenu");
            }
        }));
    }

    private void OnRegisterClicked()
    {
        string username = registerUsernameInput.text;
        string password = registerPasswordInput.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            registerMessageText.text = "Please enter both username and password";
            return;
        }

        registerButton.interactable = false;
        registerMessageText.text = "Registering...";

        StartCoroutine(NetworkManager.Instance.Register(username, password, (success, message) =>
        {
            registerButton.interactable = true;
            registerMessageText.text = message;
            if (success)
            {
                // Switch to login panel after successful registration
                SwitchToLogin();
            }
        }));
    }
}