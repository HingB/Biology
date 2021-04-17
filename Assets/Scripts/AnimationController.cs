using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] private Animation _showChooseMenu;
    [SerializeField] private Animation _openPlayMenu;
    [SerializeField] private Animation _openOnAnswerMenu;
    [SerializeField] private Animation _openFinalPanel;

    public void OpenChooseMenu()
    {
        _showChooseMenu.Play("OpenChooseMenu");
    }

    public void CloseChooseMenu()
    {
        _showChooseMenu.Play("CloseChooseMenu");
    }

    public void OpenGameMenu()
    {
        _openPlayMenu.Play("OpenGameMenu");
    }

    public void CloseGameMenu()
    {
        _openPlayMenu.Play("CloseGameMenu");
    }

    public void ShowRigthAnswer()
    {
        _openOnAnswerMenu.Play("ShowRigthAnswer");
    }

    public void CloseRigthAnswer()
    {
        _openOnAnswerMenu.Play("CloseRigthAnswer");
    }

    public void OpenFinallPanel()
    {
        _openFinalPanel.Play("OpenFinalPanel");
    }
}
