// %BANNER_BEGIN%
// ---------------------------------------------------------------------
// %COPYRIGHT_BEGIN%
//
// Copyright (c) 2018-present, Magic Leap, Inc. All Rights Reserved.
// Use of this file is governed by the Creator Agreement, located
// here: https://id.magicleap.com/creator-terms
//
// %COPYRIGHT_END%
// ---------------------------------------------------------------------
// %BANNER_END%

using UnityEngine;
using UnityEngine.XR.MagicLeap;
using System.Collections;
using UnityEngine.Networking;


namespace MagicLeap
{
    /// <summary>
    /// Class for tracking a specific Keypose and handling confidence value
    /// based sprite renderer color changes.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer))]
    public class SendHand : MonoBehaviour
    {
        #region Private Variables
        private const float ROTATION_SPEED = 100.0f;
        private const float CONFIDENCE_THRESHOLD = 0.95f;

    
        private MLHandKeyPose _keyPoseToTrack = MLHandKeyPose.OpenHand;

 
        private bool _trackLeftHand = true;

  
        private bool _trackRightHand = true;

        private SpriteRenderer _spriteRenderer = null;
        public static float confidenceValue = 0;
        private int state = 0;
        #endregion

        #region Unity Methods
        /// <summary>
        /// Initialize variables.
        /// </summary>
        void Awake()
        {
           
        }

        /// <summary>
        /// Update color of sprite renderer material based on confidence of the KeyPose.
        /// </summary>
        void Update()
        {

            float confidenceLeft = _trackLeftHand ? GetKeyPoseConfidence(MLHands.Left) : 0.0f;
            float confidenceRight = _trackRightHand ? GetKeyPoseConfidence(MLHands.Right) : 0.0f;
            confidenceValue = Mathf.Max(confidenceLeft, confidenceRight);



            // if (confidenceValue > 0.5f && state == 0)
            // {
            //     StartCoroutine(GetRequest("http://590f34c8.ngrok.io/open_close?action=1"));
            //     state = 1;
            // }
            // else if (confidenceValue < 0.5f && state == 1){
            //     StartCoroutine(GetRequest("http://590f34c8.ngrok.io/open_close?action=0"));
            //     state = 0;
            // }

            
 

        }
        #endregion

        IEnumerator GetRequest(string uri)
        {
            UnityWebRequest uwr = UnityWebRequest.Get(uri);
            yield return uwr.SendWebRequest();

            if (uwr.isNetworkError)
            {
                Debug.Log("Error While Sending: " + uwr.error);
            }
            else
            {
                Debug.Log("Received: " + uwr.downloadHandler.text);
            }
        }

        #region Private Methods
        /// <summary>
        /// Get the confidence value for the hand being tracked.
        /// </summary>
        /// <param name="hand">Hand to check the confidence value on. </param>
        /// <returns></returns>
        private float GetKeyPoseConfidence(MLHand hand)
        {
            if (hand != null)
            {
                if (hand.KeyPose == _keyPoseToTrack)
                {
                    return hand.KeyPoseConfidence;
                }
            }
            return 0.0f;
        }
        #endregion
    }
}
