using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.MagicLeap;
using UnityEngine.Networking;

public class ControlScript : MonoBehaviour {
  private GameObject _dropdown;
  private MLInputController _controller;
  private const float _rotationSpeed = 30.0f;
  private const float _distance = 2.0f;
  private const float _moveSpeed = 1.2f;
  private bool _enabled = false;
  private bool _bumper = false;
  // public List<bool> binary = new List<bool>() {true, false};
  // public Text selectedName;

  void Start() {
    _dropdown = GameObject.Find("Dropdown");
    // _dropdown.SetActive (false);
    MLInput.Start();
    MLInput.OnControllerButtonDown += OnButtonDown;
    MLInput.OnControllerButtonUp += OnButtonUp;
    _controller = MLInput.GetController(MLInput.Hand.Left);
  }

  void OnDestroy () {
    MLInput.OnControllerButtonDown -= OnButtonDown;
    MLInput.OnControllerButtonUp -= OnButtonUp;
    MLInput.Stop();
  }

  void Update() {
    // if (_bumper && _enabled) {
    //   _dropdown.transform.Rotate(Vector3.up, + _rotationSpeed * Time.deltaTime);
    // }
    // CheckControl();
  }

  // void CheckControl() {
  //   if (_controller.TriggerValue > 0.2f && _enabled) {
  //     _bumper = false;
  //     _dropdown.transform.Rotate(Vector3.up, - _rotationSpeed * Time.deltaTime);
  //   }
  //   else if (_controller.Touch1PosAndForce.z > 0.0f && _enabled) {
  //     float X = _controller.Touch1PosAndForce.x;
  //     float Y = _controller.Touch1PosAndForce.y;
  //     Vector3 forward = Vector3.Normalize(Vector3.ProjectOnPlane(transform.forward, Vector3.up));
  //     Vector3 right = Vector3.Normalize(Vector3.ProjectOnPlane(transform.right, Vector3.up));
  //     Vector3 force = Vector3.Normalize((X * right) + (Y * forward));
  //     _dropdown.transform.position += force * Time.deltaTime * _moveSpeed;
  //   }
  // }

  // public void detectButtonPress(int index) {
  //   if (binary[index % 2]) {
  //     selectedName.color = Color.green;
  //   }
  //   else {
  //     selectedName.color = Color.red;
  //   }
  // }

  public void OnButtonDown(byte controller_id, MLInputControllerButton button) {
    if ((button == MLInputControllerButton.Bumper && _enabled)) {
      StartCoroutine(GetRequest("http://a25f9a65.ngrok.io/open_close?action=0"));
      IEnumerator GetRequest(string uri)
        {
          UnityWebRequest uwr = UnityWebRequest.Get(uri);
          yield return uwr.SendWebRequest();
          if (uwr.isNetworkError) {
              Debug.Log("Error While Sending: " + uwr.error);
          }
          else  {
            Debug.Log("Received: " + uwr.downloadHandler.text);
          }
        }
      print("clicking down button " + button);
    }
  }

  public void OnButtonUp(byte controller_id, MLInputControllerButton button) {
    if (button == MLInputControllerButton.HomeTap) {
      _enabled = true;
      StartCoroutine(GetRequest("http://a25f9a65.ngrok.io/open_close?action=1"));
      IEnumerator GetRequest(string uri)
        {
          UnityWebRequest uwr = UnityWebRequest.Get(uri);
          yield return uwr.SendWebRequest();
          if (uwr.isNetworkError) {
            Debug.Log("Error While Sending: " + uwr.error);
          }
          else  {
            Debug.Log("Received: " + uwr.downloadHandler.text);
          }
        }
      print("clicking up button " + button);
    }
  }
  
}