using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Microsoft.Xbox.Services;
using Microsoft.Xbox.Services.Social.Manager;
using System;
using System.Globalization;
using System.Threading.Tasks;

public class SignIn : MonoBehaviour {

    public Button SignInButton;
    public Image GamerPic;
    public Text StatusText;
    public XboxLiveUserInfo XboxLiveUser;
    public bool AllowGuestAccounts = false;
    private bool SignInCalledOnce;

    public void Awake()
    {
        this.EnsureEventSystem();
        XboxLiveServicesSettings.EnsureXboxLiveServicesSettings();

        if (!XboxLiveUserManager.Instance.IsInitialized)
        {
            XboxLiveUserManager.Instance.Initialize();
        }

        XboxLive.Instance.PresenceWriter.StartWriter();

    }

    // Use this for initialization
    void Start () {

        if (XboxLive.Instance.AppConfig == null || string.IsNullOrEmpty(XboxLive.Instance.AppConfig.AppId) )
        {
            StatusText.text = "Xbox Live is not enabled.\nSee errors for details.";
            Text signInButtonTxt = SignInButton.GetComponentInChildren<Text>();
            signInButtonTxt.text = "Sign-In Broken";
            SignInButton.interactable = false;
        }
        else
        {
            StatusText.text = "Who's Ready to Play? \n Sign In!";
            SignInButton.onClick.AddListener(ButtonClickTask);
        }

        this.Refresh();
    }

    private void XboxLiveUserOnSignOutCompleted(object sender, SignOutCompletedEventArgs signOutCompletedEventArgs)
    {
        this.Refresh();
    }

    // Update is called once per frame
    public void Update()
    {
        if (XboxLiveUserManager.Instance.SingleUserModeEnabled && XboxLiveUserManager.Instance.UserForSingleUserMode != null
            && XboxLiveUserManager.Instance.UserForSingleUserMode.User != null
            && !XboxLiveUserManager.Instance.UserForSingleUserMode.User.IsSignedIn && !this.SignInCalledOnce)
        {
            this.SignInCalledOnce = true;
            this.StartCoroutine(this.SignInAsync());
        }

        if (this.XboxLiveUser != null && this.XboxLiveUser.User != null && !this.XboxLiveUser.User.IsSignedIn && !this.SignInCalledOnce)
        {
            this.SignInCalledOnce = true;
            this.StartCoroutine(this.SignInAsync());
        }

    }

    void ButtonClickTask()
    {
        Debug.Log("You clicked the BUTTON!");
        SignInUser();
    }

    public void SignInUser()
    {
        this.StartCoroutine(this.InitializeXboxLiveUser());
    }

    public IEnumerator InitializeXboxLiveUser()
    {
        yield return null;

        // Disable the sign-in button
        SignInButton.interactable = false;

#if ENABLE_WINMD_SUPPORT
        if (!XboxLiveUserManager.Instance.SingleUserModeEnabled && this.XboxLiveUser != null && this.XboxLiveUser.WindowsSystemUser == null)
        {
            var autoPicker = new Windows.System.UserPicker { AllowGuestAccounts = this.AllowGuestAccounts };
            autoPicker.PickSingleUserAsync().AsTask().ContinueWith(
                    task =>
                        {
                            if (task.Status == TaskStatus.RanToCompletion)
                            {
                                this.XboxLiveUser.WindowsSystemUser = task.Result;
                                this.XboxLiveUser.Initialize();
                            }
                            else
                            {
                                if (XboxLiveServicesSettings.Instance.DebugLogsOn)
                                {
                                    Debug.Log("Exception occured: " + task.Exception.Message);
                                }
                            }
                        });
        }
        else
        {
            if (this.XboxLiveUser == null)
            {
                this.XboxLiveUser = XboxLiveUserManager.Instance.UserForSingleUserMode;
            }
            if (this.XboxLiveUser.User == null)
            {
                this.XboxLiveUser.Initialize();
            }
        }
#else
        if (XboxLiveUserManager.Instance.SingleUserModeEnabled && this.XboxLiveUser == null)
        {
            this.XboxLiveUser = XboxLiveUserManager.Instance.GetSingleModeUser();
        }

        this.XboxLiveUser.Initialize();
#endif
    }

    public IEnumerator SignInAsync()
    {
        SignInStatus signInStatus;
        TaskYieldInstruction<SignInResult> signInSilentlyTask = this.XboxLiveUser.User.SignInSilentlyAsync().AsCoroutine();
        yield return signInSilentlyTask;

        signInStatus = signInSilentlyTask.Result.Status;
        if (signInSilentlyTask.Result.Status != SignInStatus.Success)
        {
            TaskYieldInstruction<SignInResult> signInTask = this.XboxLiveUser.User.SignInAsync().AsCoroutine();
            yield return signInTask;

            signInStatus = signInTask.Result.Status;
        }

        // Throw any exceptions if needed.
        if (signInStatus == SignInStatus.Success)
        {
            XboxLive.Instance.StatsManager.AddLocalUser(this.XboxLiveUser.User);
            XboxLive.Instance.PresenceWriter.AddUser(this.XboxLiveUser.User);
            var addLocalUserTask =
                XboxLive.Instance.SocialManager.AddLocalUser(
                    this.XboxLiveUser.User,
                    SocialManagerExtraDetailLevel.PreferredColor).AsCoroutine();
            yield return addLocalUserTask;

            if (!addLocalUserTask.Task.IsFaulted)
            {
                yield return this.LoadProfileInfo();
            }
        }
    }

    private IEnumerator LoadProfileInfo()
    {
        var userId = ulong.Parse(this.XboxLiveUser.User.XboxUserId);
        var group = XboxLive.Instance.SocialManager.CreateSocialUserGroupFromList(this.XboxLiveUser.User, new List<ulong> { userId });
        var socialUser = group.GetUser(userId);

        var www = new WWW(socialUser.DisplayPicRaw + "&w=128");
        yield return null;

        try
        {
                if (www.isDone && string.IsNullOrEmpty(www.error))
                {
                    var t = www.texture;
                    var r = new Rect(0, 0, t.width, t.height);
                    this.GamerPic.sprite = Sprite.Create(t, r, Vector2.zero);
                }

            this.StatusText.text = this.XboxLiveUser.User.Gamertag + " is Signed In\n and ready to ROCK!";

            if (socialUser.PreferredColor != null)
            {
                this.GetComponent<Image>().color =
                    ColorFromHexString(socialUser.PreferredColor.PrimaryColor);
            }

        }
        catch (Exception ex)
        {
            if (XboxLiveServicesSettings.Instance.DebugLogsOn)
            {
                Debug.Log("There was an error while loading Profile Info. Exception: " + ex.Message);
            }
        }


        this.Refresh();
    }

    private void Refresh()
    {
        var isSignedIn = this.XboxLiveUser != null && this.XboxLiveUser.User != null && this.XboxLiveUser.User.IsSignedIn;
        SignInButton.interactable = !isSignedIn;
    }

    public static Color ColorFromHexString(string color)
    {
        var r = (float)byte.Parse(color.Substring(0, 2), NumberStyles.HexNumber) / 255;
        var g = (float)byte.Parse(color.Substring(2, 2), NumberStyles.HexNumber) / 255;
        var b = (float)byte.Parse(color.Substring(4, 2), NumberStyles.HexNumber) / 255;

        return new Color(r, g, b);
    }

    private void OnApplicationQuit()
    {
        XboxLive.Instance.PresenceWriter.StopWriter();
    }
}