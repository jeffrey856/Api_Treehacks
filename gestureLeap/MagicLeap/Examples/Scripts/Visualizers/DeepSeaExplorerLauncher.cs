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
using MagicLeap;

namespace MagicLeap
{
    /// <summary>
    /// Updates followers to face this object
    /// </summary>
    public class DeepSeaExplorerLauncher : MonoBehaviour
    {
        #region Public Variables
        // public sendHand thumbsup;
        
        #endregion
        
        #region Private Variables
        [SerializeField, Tooltip("Position offset of the explorer's target relative to Reference Transform")]
        private Vector3 _positionOffset = Vector3.zero;

        private int firstPing = 0;

        [SerializeField, Tooltip("Prefab of the Deep Sea Explorer")]
        private GameObject _explorerPrefab = null;
        private FaceTargetPosition[] _followers = null;

        [SerializeField, Tooltip("Desired number of explorers. Each explorer will have a different mass and turning speed combination")]
        private int _numExplorers = 3;
        private float _minMass = 4;
        private float _maxMass = 16;
        private float _minTurningSpeed = 30;
        private float _maxTurningSpeed = 90;

        private int state = 0;
        private int seen = 0;
        #endregion

        #region Unity Methods
        /// <summary>
        /// Validates variables and creates the deep sea explorers
        /// </summary>
        void Awake ()
        {
            if (null == _explorerPrefab)
            {
                Debug.LogError("Error: DeepSeaExplorerLauncher._deepSeaExplorer is not set, disabling script.");
                enabled = false;
                return;
            }
        }

        /// <summary>
        /// Recreate explorers if we are reenabled while a target is found
        /// </summary>
        void OnEnable()
        {   
            // if(firstPing ==1){
            //     // StartCoroutine(GetRequest("http://590f34c8.ngrok.io/open_close?action=1"));
                
            // }
            firstPing = 1;
            // CreateExplorers();
            seen = 1;
        }

        /// <summary>
        /// Destroy all explorers immediately
        /// </summary>
        void OnDisable()
        {
            // DestroyExplorers();
            seen = 0;
        }

        /// <summary>
        /// Update followers of the new position
        /// </summary>
        void Update()
        {
            // Vector3 position = GetPosition();
            // StartCoroutine(GetRequest("http://590f34c8.ngrok.io/open_close?action=1"));
            // foreach (FaceTargetPosition follower in _followers)
            // {
            //     if (follower)
            //     {
            //         follower.TargetPosition = position;
            //     }
            // }
            if (SendHand.confidenceValue > 0.5f && state == 0)
            {
                
                StartCoroutine(GetRequest("http://590f34c8.ngrok.io/open_close?action=1"));
                state = 1;
            }
            else if (SendHand.confidenceValue < 0.5f && state == 1){
                StartCoroutine(GetRequest("http://590f34c8.ngrok.io/open_close?action=0"));
                state = 0;
            }
            print("update");
            // float confidenceValue = thumbsup.getConfidenceValue();
            // StartCoroutine(GetRequest("http://590f34c8.ngrok.io/open_close?action=oof"));
            // if(seen == 1){
            //     StartCoroutine(GetRequest("http://590f34c8.ngrok.io/open_close?action=oof"));
           //
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
        /// Create the Deep Sea Explorers with unique parameters
        /// </summary>
        private void CreateExplorers()
        {
            // if (null == _followers)
            // {
            //     _followers = new FaceTargetPosition[_numExplorers];
            // }

            // float massInc = (_maxMass - _minMass) / _numExplorers;
            // float turningSpeedInc = (_maxTurningSpeed - _minTurningSpeed) / _numExplorers;
            // Vector3 position = GetPosition();
            // for (int i = 0; i < _numExplorers; ++i)
            // {
            //     if (_followers[i])
            //     {
            //         continue;
            //     }

            //     // GameObject explorer = Instantiate(_explorerPrefab, position, Quaternion.identity);

            //     _followers[i] = explorer.AddComponent<FaceTargetPosition>();
            //     _followers[i].TurningSpeed = _minTurningSpeed + (i * turningSpeedInc);

            //     // Mass would be inversely proportional to turning speed (lower mass leads to lower acceleration -> needs higher turning rate)
            //     Rigidbody body = explorer.GetComponent<Rigidbody>();
            //     if (body)
            //     {
            //         body.mass = _maxMass - (i * massInc);
            //     }
            // }
        }

        /// <summary>
        /// Destroy all explorers
        /// </summary>
        private void DestroyExplorers()
        {
            // foreach (FaceTargetPosition follower in _followers)
            // {
            //     if (follower)
            //     {
            //         Destroy(follower.gameObject);
            //     }
            // }
        }

        /// <summary>
        /// Calculate and return the position which the explorers should look at
        /// </summary>
        /// <returns>The absolute position of the new target</returns>
        // private Vector3 GetPosition()
        // {
        //     // return transform.position + transform.TransformDirection(_positionOffset);
        // }
        #endregion
    }
}
