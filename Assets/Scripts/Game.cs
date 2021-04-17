using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Game : MonoBehaviour
{
    [SerializeField] private float _timeToAnswer;
    [SerializeField] private UIManager _uIManager;
    [SerializeField] private AnimationController _animationController;
    [SerializeField] private QuestionList[] _questionsRound1;
    [SerializeField] private QuestionList[] _questionsRound2;
    [SerializeField] private QuestionList[] _questionsRound3;
    [SerializeField] private GameObject _cube;

    private List<object> _questionList;
    private int _randomQuestion;
    private QuestionList _currentQuestion;
    private int _round = 1;

    private int _redTeamCurrentRoundPoints;
    private int _blueTeamCurrentRoundPoints;

    private int _redTeamAllPoints;
    private int _blueTeamAllPoints;

    private int _redTeamWonRounds;
    private int _blueTeamWonRounds;

    private bool _nowRedTeam;

    public event UnityAction OnRoundFinished;

    public void OnClickPlay(int round)
    {
        switch (round)
        {
            case 1:
                _questionList = new List<object>(_questionsRound1);
                break;
            case 2:
                _questionList = new List<object>(_questionsRound2);
                break;
            case 3:
                _questionList = new List<object>(_questionsRound3);
                break;
        }
        _nowRedTeam = false;

        _animationController.CloseChooseMenu();
        _animationController.OpenGameMenu();
        GenerateQuestion();
    }

    public void GenerateQuestion()
    {
        _uIManager.SetPointText(_redTeamCurrentRoundPoints, _blueTeamCurrentRoundPoints);
        if (_questionList.Count > 0)
        {
            _uIManager.RestartSlider();
            _nowRedTeam = !_nowRedTeam;
            if (_nowRedTeam)
                _cube.transform.position = new Vector2(6, 3.4f);
            else
                _cube.transform.position = new Vector3(-6, 3.4f);
            _randomQuestion = Random.Range(0, _questionList.Count);
            _currentQuestion = _questionList[_randomQuestion] as QuestionList;
            _uIManager.SetQuestionText(_currentQuestion.Question);
            _uIManager.ChangeSliderValue(_timeToAnswer);

            List<string> answers = new List<string>(_currentQuestion.Answers());

            for (int i = 0; i < _currentQuestion.GetAnswersQuantity(); i++)
            {
                int random = Random.Range(0, answers.Count);

                _uIManager.SetAnswerText(i, answers[random]);
                answers.RemoveAt(random);
            }
        }
        else
        {
            OnRoundEnd();
        }
    }

    private void OnRoundEnd()
    {
        if (_redTeamCurrentRoundPoints > _blueTeamCurrentRoundPoints)
        {
            _redTeamWonRounds += 1;
        }
        if (_blueTeamCurrentRoundPoints > _redTeamCurrentRoundPoints)
        {
            _blueTeamWonRounds += 1;
        }
        if (_blueTeamCurrentRoundPoints == _redTeamCurrentRoundPoints)
        {
            _blueTeamWonRounds++; _redTeamWonRounds++;
        }

        _redTeamAllPoints += _redTeamCurrentRoundPoints;
        _blueTeamAllPoints += _blueTeamCurrentRoundPoints;

        _redTeamCurrentRoundPoints = 0;
        _blueTeamCurrentRoundPoints = 0;

        _uIManager.SetWonRoundText(_redTeamWonRounds, _blueTeamWonRounds);

        OnRoundFinished?.Invoke();
        _animationController.OpenChooseMenu();
        _round++;
        if (_round > 3)
        {
            _round = 3;
            _uIManager.OnAllRoundsEnd(_redTeamAllPoints, _blueTeamAllPoints, _redTeamWonRounds, _blueTeamWonRounds);
            _animationController.OpenFinallPanel();
        }

        _animationController.CloseGameMenu();
        _uIManager.OpenRounds(_round);
    }

    public void OnTimeEnd()
    {
        ChangePointValue(-3);
        OnAnswerEnd();
    }

    public void OnAnsweButtonClick(int index)
    {
        if (_uIManager.GetTextUnderId(index).ToString() == _currentQuestion.GetRigthAnswer())
        {
            ChangePointValue(3);
        }
        else
        {
            ChangePointValue(-2);
        }
        OnAnswerEnd();
    }

    private void OnAnswerEnd()
    {
        _uIManager.OpenPanelAnswer();
        _uIManager.SetRigthAnswerText(_currentQuestion.GetRigthAnswer());
        _questionList.RemoveAt(_randomQuestion);
        _animationController.ShowRigthAnswer();
    }

    private void ChangePointValue(int value)
    {
        if (_nowRedTeam)
            _redTeamCurrentRoundPoints += value;
        else
            _blueTeamCurrentRoundPoints += value;
    }

    
}
[System.Serializable]
public class QuestionList
{
    [SerializeField] private string _question;
    [SerializeField] private string[] _answers = new string[4];

    public string Question => _question;

    public string[] Answers()
    {
        string[] array = new string[_answers.Length];

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = _answers[i];
        }

        return array;
    }

    public string GetRigthAnswer()
    {
        return _answers[0];
    }
    public int GetAnswersQuantity()
    {
        return _answers.Length;
    }
}