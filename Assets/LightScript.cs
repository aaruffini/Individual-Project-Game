using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using JetBrains.Annotations;
using UnityEngine.AI;
using UnityEngine.Networking;

public class LightScript : MonoBehaviour
{
    public Light lightToControl;
    public string api = "http://localhost:8080/api/state";
    

    void Start()
    {
        if (lightToControl == null)
        {
            Debug.LogError("PlcLightController: assign the Light in the Inspector.");
            return;
        }

        StartCoroutine(PollPLCState());
    }

    // ai starting code 
    IEnumerator PollPLCState()
    {
        while (true)
        {
            using (UnityEngine.Networking.UnityWebRequest request = UnityEngine.Networking.UnityWebRequest.Get(api))
            {
                yield return request.SendWebRequest();

                if (request.result == UnityWebRequest.Result.Success)
                {
                    string json = request.downloadHandler.text;

                    // Parse JSON: {"PB_2_Value":false}
                    PlcState state = JsonUtility.FromJson<PlcState>(json);

                    // Control light based on PB_2 state
                    lightToControl.enabled = state.PB_2_Value;

                    Debug.Log($"PB_2: {state.PB_2_Value} → Light: {(state.PB_2_Value ? "ON" : "OFF")}");
                }
                else
                {
                    Debug.LogError($"Failed to connect to middleware: {request.error}");
                }
            }

            yield return new WaitForSeconds(0.05f); // Poll every 50ms
        }
    }

    // Helper class for JSON parsing
    [System.Serializable]
    public class PlcState
    {
        public bool PB_2_Value;
    }
}
    

