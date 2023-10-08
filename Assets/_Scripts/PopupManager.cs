using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Popup = PopupSequence.Popup;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance { get; private set; }

    public GameObject TextPopup;
    public List<PopupSequence> PopupSequences;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        //! DEBUGGING!
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Tutorial();
        }
    }

    /// <summary>
    /// Run the tutorial sequence of text boxes
    /// </summary>
    public void Tutorial()
    {
        PopupSequence sequence = PopupSequences.FirstOrDefault(seq => seq.Key.Equals("Tutorial"));

        if (sequence == null)
        {
            Debug.LogWarning("No PopupSequence with name \"Tutorial\" found");
            return;
        }

        // init text across all popups in sequence
        List<(GameObject, PopupRunner)> popupObjects = new();
        foreach (Popup popup in sequence.popups)
        {
            // TODO: will this make the popup flicker?
            GameObject currentPopup = Instantiate(TextPopup, Vector3.zero, Quaternion.identity);

            PopupRunner runner = currentPopup.GetComponent<PopupRunner>();
            runner.InitText(popup.Title, popup.Body);

            if (!ReferenceEquals(popup, sequence.popups[0]))
                currentPopup.SetActive(false);

            popupObjects.Add((currentPopup, runner));
        }

        //! invariant: popupObjects.Count == sequence.popups.Length

        // bind all continue button delegates to deactivate current popup and move on to next popup
        for (int i = 0; i < popupObjects.Count; ++i)
        {
            (GameObject obj, PopupRunner runner) = popupObjects[i];

            runner.continueButton.onClick.AddListener(() =>
            {
                obj.SetActive(false);
                if (i + 1 < popupObjects.Count)
                    popupObjects[i + 1].Item1.SetActive(true);
            });
        }
    }
}
