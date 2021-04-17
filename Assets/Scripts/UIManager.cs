using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UIManager : MonoBehaviour
{
    [Header("Gameobjects")]
    [SerializeField] private GameObject _panelOnAnswer;
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _roundChoosePanel;
    [SerializeField] private GameObject _gamePanel;
    [SerializeField] private GameObject _finalPanel;
    [Header("Text")]
    [SerializeField] private Text[] _answersText;
    [SerializeField] private Game _game;
    [SerializeField] private Text _redPointsText;
    [SerializeField] private Text _bluePointsText;
    [SerializeField] private Text _redWonsText;
    [SerializeField] private Text _blueWonsText;
    [SerializeField] private Text _questionText;
    [SerializeField] private Text _rigthAnswerText;
    [Header("Button")]
    [SerializeField] private Button _buttonPlay;
    [SerializeField] private Button _round1Play;
    [SerializeField] private Button _round2Play;
    [SerializeField] private Button _round3Play;
    [Header("Final")]
    [SerializeField] private Text _pointsRelulst;
    [SerializeField] private Text _wonsCountRelulst;
    [SerializeField] private Text _allPointsRed;
    [SerializeField] private Text _allPointsBlue;
    [SerializeField] private Text _wonsCountBlue;
    [SerializeField] private Text _wonsCountRed;
    [Header("Image")]
    [SerializeField] private Image _image;
    [Header("Slider")]
    [SerializeField] private Slider _slider;

    public void SetQuestionText(string text)
    {
        _questionText.text = text;
    }

    public void SetAnswerText(int index, string text)
    {
        _answersText[index].text = text;
    }

    public void SetPointText(int red, int blue)
    {
        _redPointsText.text = red.ToString();
        _bluePointsText.text = blue.ToString();
    }

    public void SetWonRoundText(int red, int blue)
    {
        _redWonsText.text = red.ToString();
        _blueWonsText.text = blue.ToString();
    }

    public string GetTextUnderId(int index)
    {
        return _answersText[index].text.ToString();
    }

    public void ChangeSliderValue(float time)
    {
        _slider.DOValue(_slider.maxValue, time).SetEase(Ease.Linear);
    }

    public void OnSliderValueChanged(float value)
    {
        if (value == _slider.maxValue)
            _game.OnTimeEnd();
    }

    public void SetRigthAnswerText(string value)
    {
        _rigthAnswerText.text = value;
    }
    
    public void OpenPanelAnswer()
    {
        _panelOnAnswer.SetActive(true);
        _slider.DOPause();
    }

    private void BackToChooseRound()
    {
        _roundChoosePanel.SetActive(true);
        _gamePanel.SetActive(false);
    }

    public void OpenRounds(int round)
    {
        if (round == 2)
            _round2Play.interactable = true;
        if (round == 3)
            _round3Play.interactable = true;
    }

    public void OnAllRoundsEnd(int allRedPoints, int allBluePoints, int wonsRedCount, int wonsBlueCount)
    {
        _finalPanel.SetActive(true);
        _roundChoosePanel.SetActive(false);

        _allPointsRed.text = allRedPoints.ToString();
        _allPointsBlue.text = allBluePoints.ToString();
        _wonsCountRed.text = wonsRedCount.ToString();
        _wonsCountBlue.text = wonsBlueCount.ToString();

        if (allRedPoints > allBluePoints)
            _pointsRelulst.text = "Побеждает команда спарава со счетом: " + allRedPoints;
        if(allBluePoints > allRedPoints)
            _pointsRelulst.text = "Побеждает команда слева со счетом: " + allBluePoints;
        if (allBluePoints == allRedPoints)
            _pointsRelulst.text = "Побеждает дружба со счетом: " + allRedPoints;

        if (wonsRedCount > wonsBlueCount)
            _wonsCountRelulst.text = "Побеждает команда справа со счетом: " + wonsRedCount;
        if (wonsBlueCount > wonsRedCount)
            _wonsCountRelulst.text = "Побеждает команда слева со счетом: " + wonsBlueCount;
        if (wonsBlueCount == wonsRedCount)
            _wonsCountRelulst.text = "Побеждает дружба со счетом: " + wonsBlueCount;
    }

    private void OnEnable()
    {
        _game.OnRoundFinished += BackToChooseRound;
    }

    private void OnDisable()
    {
        _game.OnRoundFinished -= BackToChooseRound;
    }

    public void RestartSlider()
    {
        _slider.value = _slider.minValue;
    }
}
