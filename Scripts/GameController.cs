using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Text _TextComponent;
    public bool _IsPlaying = false;
    public string _NextLevelName;
    int _NumberOfSpecialBlocksAtTheBegining;

    private void Start()
    {
        FixLighting();

       _TextComponent.enabled = false;
        _NumberOfSpecialBlocksAtTheBegining = CountBlocks(special: true);
    }

    public void OnValidate()
    {
        FixLighting();
    }

    void FixLighting()
    {
        RenderSettings.ambientLight = Color.white;
        RenderSettings.ambientMode = UnityEngine.Rendering.AmbientMode.Flat;
    }
    public int CountBlocks(bool special)
    {
        return FindObjectsOfType<Blocks>()
            .Count(block => block.Enabled && block.IsSpecial == special);
    }
    public void OnTriggerExit(Collider other)
    {
        var block = other.GetComponent<Blocks>();

        if (block == null) return;

        block.Enabled = false;

        if(block.IsSpecial)
           StartCoroutine(EndGameCouritne(won: false));
       
        else if(CountBlocks(special: false) == 0)
                StartCoroutine(EndGameCouritne(won: true));
     
        Destroy(block.gameObject);
    }
    IEnumerator EndGameCouritne(bool won)
    {
        if (_IsPlaying == false) yield break;

        _TextComponent.enabled = true;
        _IsPlaying = false;

        if(won)
        {
            for(int i=5; i>0; i--)
            {
                _TextComponent.text = i.ToString();
                yield return new WaitForSeconds(1f);
            }

            if (_NumberOfSpecialBlocksAtTheBegining != CountBlocks(special: true))
                won = false;
        }

        _TextComponent.text = won ? "Wygrałeś" : "Przegrałeś";

        yield return new WaitForSeconds(3f);
            
            if(string.IsNullOrEmpty(_NextLevelName))
        {
            Debug.Log("Koniec gry");
            Application.Quit();
        }
            else
        {
            SceneManager.LoadScene(_NextLevelName);
        }
    }
}
