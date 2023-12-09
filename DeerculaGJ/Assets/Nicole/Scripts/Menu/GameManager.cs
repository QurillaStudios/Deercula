using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour 
{
    public static GameManager instance;

    [SerializeField] private bool isGameRunning = true;
    public bool IsGameRunning { get => isGameRunning; set => isGameRunning = value; }
    [SerializeField] private bool isGameOver = false;
    public bool IsGameOver { get => isGameOver; set => isGameOver = value; }

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private TMP_Text gameOverText;

    [SerializeField] private AudioSource gameWin;
    [SerializeField] private AudioSource gameLose;
    [SerializeField] private AudioSource unlockSound;


    [SerializeField] private AudioSource[] gameBackground;


    #region lists
    [SerializeField] private List<ButterFly> butterFlies;
    public List<ButterFly> ButterFlies { get => butterFlies; set => butterFlies = value; }

    [SerializeField] private List<Mouse> mouses;
    public List<Mouse> Mouses { get => mouses; set => mouses = value; }
    private bool isMouseUnLocked;

    [SerializeField] private List<Squirrel> squirrels;
    public List<Squirrel> Squirrels { get => squirrels; set => squirrels = value; }
    private bool isSquirrelsUnLocked;

    [SerializeField] private List<Rabbit> rabbits;
    public List<Rabbit> Rabbits { get => rabbits; set => rabbits = value; }
    private bool isRabbitsUnLocked;

    [SerializeField] private List<Raccoon> raccoons;
    public List<Raccoon> Raccoons { get => raccoons; set => raccoons = value; }
    private bool isRaccoonsUnLocked;

    [SerializeField] private List<BabyDeer> babyDeers;
    public List<BabyDeer> BabyDeers { get => babyDeers; set => babyDeers = value; }
    private bool isBabyDeersUnLocked;

    [SerializeField] private List<Fox> foxes;
    public List<Fox> Foxes { get => foxes; set => foxes = value; }
    private bool isFoxesUnLocked;

    [SerializeField] private List<Wolf> wolves;
    public List<Wolf> Wolves { get => wolves; set => wolves = value; }
    private bool isWolvesUnLocked;

    [SerializeField] private List<Bear> bears;
    public List<Bear> Bears { get => bears; set => bears = value; }
    private bool isBearsUnLocked;
    #endregion

    private void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Time.timeScale = 1.0f;
        isGameRunning = true;
        isGameOver = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && isGameRunning)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
        }

        if (!isGameRunning)
        {
            foreach(var item in gameBackground)
            {
                item.Stop();
            }

            if(isGameOver)
            {
                if(!gameLose.isPlaying)
                {
                    gameLose.Play();
                }
              
                gameOverText.text = "You Lose!";
            }
            else
            {
                if(!gameWin.isPlaying)
                {
                    gameWin.Play();
                }
                gameOverText.text = "You Win!";
            }

            gameOverMenu.SetActive(true);
            Time.timeScale = 0f;
        }

        UnlockAnimal(butterFlies,mouses, ref isMouseUnLocked);
        UnlockAnimal(mouses, squirrels, ref isSquirrelsUnLocked);
        UnlockAnimal(squirrels, rabbits, ref isRabbitsUnLocked);
        UnlockAnimal(rabbits, raccoons, ref isRaccoonsUnLocked);
        UnlockAnimal(raccoons, foxes, ref isFoxesUnLocked);
        UnlockAnimal(foxes, wolves, ref isWolvesUnLocked);
        UnlockAnimal(wolves, bears, ref isBearsUnLocked);
    }

    private void UnlockAnimal(IEnumerable<Animal> currentAnimal, IEnumerable<Animal> nextAnimal, ref bool isUnlocked)
    {
        if (nextAnimal.Count() != 0)
        {
            if (currentAnimal.Count() == 0 && !isUnlocked)
            {
                foreach (var animal in nextAnimal)
                {
                    if(animal != null)
                    {
                        animal.IsBitable = true;
                    }
                }

                unlockSound.Play();
                isUnlocked = true;
            }
        }
    }
}