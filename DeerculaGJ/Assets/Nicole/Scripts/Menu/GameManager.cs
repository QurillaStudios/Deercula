using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour 
{
    public static GameManager instance;

    private bool isGameRunning = true;
    public bool IsGameRunning { get => isGameRunning; set => isGameRunning = value; }
    private bool isGameOver = false;
    public bool IsGameOver { get => isGameOver; set => isGameOver = value; }

    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverMenu;
    [SerializeField] private TMP_Text gameOverText;

    #region lists
    [SerializeField] private List<ButterFly> butterFlies;
    public List<ButterFly> ButterFlies { get => butterFlies; set => butterFlies = value; }

    [SerializeField] private List<Mouse> mouses;
    public List<Mouse> Mouses { get => mouses; set => mouses = value; }

    [SerializeField] private List<Squirrel> squirrels;
    public List<Squirrel> Squirrels { get => squirrels; set => squirrels = value; }

    [SerializeField] private List<Rabbit> rabbits;
    public List<Rabbit> Rabbits { get => rabbits; set => rabbits = value; }

    [SerializeField] private List<Raccoon> raccoons;
    public List<Raccoon> Raccoons { get => raccoons; set => raccoons = value; }

    [SerializeField] private List<BabyDeer> babyDeers;
    public List<BabyDeer> BabyDeers { get => babyDeers; set => babyDeers = value; }

    [SerializeField] private List<Fox> foxes;
    public List<Fox> Foxes { get => foxes; set => foxes = value; }

    [SerializeField] private List<Wolf> wolves;
    public List<Wolf> Wolves { get => wolves; set => wolves = value; }

    [SerializeField] private List<Bear> bears;
    public List<Bear> Bears { get => bears; set => bears = value; }
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
            if(isGameOver)
            {
                gameOverText.text = "You Lose!";
            }
            else
            {
                gameOverText.text = "You Win!";
            }

            gameOverMenu.SetActive(true);
            Time.timeScale = 0f;
        }

        UnlockAnimal(butterFlies,mouses);
        UnlockAnimal(mouses, squirrels);
        UnlockAnimal(squirrels, rabbits);
        UnlockAnimal(rabbits, raccoons);
        UnlockAnimal(raccoons, foxes);
        UnlockAnimal(foxes, wolves);
        UnlockAnimal(wolves, bears);
    }

    private void UnlockAnimal(IEnumerable<Animal> currentAnimal, IEnumerable<Animal> nextAnimal)
    {
        if (nextAnimal.Count() != 0)
        {
            if (currentAnimal.Count() == 0)
            {
                foreach (var animal in nextAnimal)
                {
                    if(animal != null)
                    {
                        animal.IsBitable = true;
                    }
                   
                }
            }
        }
    }
}