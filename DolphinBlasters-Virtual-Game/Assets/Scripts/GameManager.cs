using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private static float _max_restriction_time = 3f;

    private static float _restriction_timer;

    public static CharacterBehaviour restricted_character;
    public static CharacterBehaviour current_ball_owner;

    public static void SetRestrictedCharacrter(CharacterBehaviour character)
    {
        restricted_character = character;
        _restriction_timer = _max_restriction_time;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if(_restriction_timer <= 0)
        {
            restricted_character = null;
        }
        else
        {
            _restriction_timer -= Time.deltaTime;
        }
    }
}
