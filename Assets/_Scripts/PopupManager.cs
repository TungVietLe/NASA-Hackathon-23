using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Popup = PopupSequence.Popup;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Instance { get; private set; }

    public Canvas canvas;
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
            InitPopupSequence("Tutorial");
        }
    }

    // TODO: make this procedure much more generic - this is a great start, though
    /// <summary>
    /// Run the tutorial sequence of text boxes
    /// </summary>
    public void InitPopupSequence(string key)
    {
        PopupSequence sequence = PopupSequences.FirstOrDefault(seq => seq.Key.Equals(key));

        if (sequence == null)
        {
            Debug.LogWarning($"No PopupSequence with key \"{key}\" found");
            return;
        }

        // init text across all popups in sequence
        List<(GameObject, PopupRunner)> popupObjects = new();
        for (int i = 0; i < sequence.popups.Count; ++i)
        {
            Popup popup = sequence.popups[i];

            // TODO: will this make the popup flicker?
            GameObject popupObj = Instantiate(TextPopup, canvas.transform.position, Quaternion.identity, canvas.transform);

            PopupRunner runner = popupObj.GetComponent<PopupRunner>();
            runner.InitText(popup.Title, popup.Body);

            if (i == 0)
            {
                // if first popup in sequence, set back button invisible
                runner.BackButton.gameObject.SetActive(false);
            }
            else
            {
                popupObj.SetActive(false);
            }

            popupObjects.Add((popupObj, runner));
        }

        //! invariant: popupObjects.Count == sequence.popups.Length

        // bind all continue button delegates to deactivate current popup and move on to next popup
        for (int i = 0; i < popupObjects.Count; ++i)
        {
            (GameObject obj, PopupRunner runner) = popupObjects[i];
            int iCopy = i; // c# lambda captures by reference, so we need to copy first

            runner.BackButton.onClick.AddListener(() =>
            {
                if (iCopy == 0) return;

                // no need to check, since we already know iCopy != 0
                obj.SetActive(false);
                popupObjects[iCopy - 1].Item1.SetActive(true);
            });

            runner.ContinueButton.onClick.AddListener(() =>
            {
                obj.SetActive(false);

                if (iCopy + 1 < popupObjects.Count)
                {
                    popupObjects[iCopy + 1].Item1.SetActive(true);
                }
                else
                {
                    foreach ((GameObject obj, PopupRunner runner) in popupObjects)
                    {
                        Destroy(obj);
                    }
                }
            });
        }
    }
}
