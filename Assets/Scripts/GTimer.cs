using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GTimer : MonoBehaviourPunCallbacks
{
    private int _timeLeft;
    [SerializeField] private Text _countdownText;
    private PhotonView _pView;
    [HideInInspector] public bool IsTimeFinished = true;

    private void Awake()
    {
        _pView = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            _timeLeft = 120; // Set the initial time here
            _pView.RPC("UpdateTime", RpcTarget.AllBuffered, _timeLeft);
            StartCountDown();
        }
    }

    public void StartCountDown()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            IsTimeFinished = false;
            StartCoroutine("LoseTime");
        }
    }

    IEnumerator LoseTime()
    {
        while (_timeLeft > 0)
        {
            yield return new WaitForSeconds(1);
            _pView.RPC("ReduceTime", RpcTarget.AllBuffered);
        }

        _countdownText.text = " ";
        TimesUp();
    }

    [PunRPC]
    private void ReduceTime()
    {
        _timeLeft--;
        _countdownText.text = " " + _timeLeft;
    }

    [PunRPC]
    private void UpdateTime(int newTime)
    {
        _timeLeft = newTime;
        _countdownText.text = " " + _timeLeft;
    }

    public void TimesUp()
    {
        IsTimeFinished = true;
    }
}
