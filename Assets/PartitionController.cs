using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.UI;

public class PartitionController : MonoBehaviour {

    public float tempo;
    public float timeref;
    public Text timerText;
    public int index = 0;
	public GameController gameController;
	public Text pointText;
	public int point = 0;

	private float interval = 0.40f;
    private float startTimer;

    List<float> notetime;
    List<float> notekey;

	List<float> verifnoteA;
	List<float> verifnoteB;
	List<float> verifnoteC;
	List<float> verifnoteD;

    // Use this for initialization
    void Start () {

        float nbmeasure;
        float timer = 5f;
        float division = 2f;

		verifnoteA = new List<float>();
		verifnoteB = new List<float>();
		verifnoteC = new List<float>();
		verifnoteD = new List<float>();

        startTimer = Time.time;

        Debug.Log("START TIMER : " + startTimer);

        notetime = new List<float>();
        notekey = new List<float>();

        XmlDocument document = new XmlDocument();
        document.Load(@"Assets/partition2.xml");

        XmlNode root = document.FirstChild;
        XmlNodeList measures = root.SelectNodes("part/measure");

        foreach (XmlNode measure in measures)
        {
            nbmeasure = float.Parse(measure.Attributes["number"].Value);

            if (nbmeasure == 1)
            {
                XmlNode sound = measure.SelectSingleNode("sound");
                division = float.Parse(measure.SelectSingleNode("attributes/divisions").InnerText);

                if (sound != null)
                {
                    tempo = float.Parse(sound.Attributes["tempo"].Value);

                    //Calcul temps en seconde pour une noire
                    timeref = (60000 / tempo) / 1000f;
                }
                else
                {
                    tempo = 120;
                }
            }

            XmlNodeList notes = measure.SelectNodes("note");

            foreach (XmlNode note in notes)
            {
                float notebutton = float.Parse(note.SelectSingleNode("notations/technical/string").InnerText);

                //Si la note est joué sur la même corde que la précedente
                if (note.SelectSingleNode("chord") != null)
                {
                    //On soustrait le temps de la note actuel pour éviter des décalages
                    timer = timer - ((float.Parse(note.SelectSingleNode("duration").InnerText) * timeref) / division);
                }

                notetime.Add(timer);
                notekey.Add(notebutton);

                //On rajoute le temps passé au timer pour les notes suivantes
                timer = ((float.Parse(note.SelectSingleNode("duration").InnerText) * timeref) / division) + timer;
            }
        }

        Debug.Log("Tempo : " + tempo + " Time ref : " + timeref + " Division : " + division);

        for (int key = 0; key < notetime.Count; key++)
        {
            Debug.Log(" NoteTime : " + notetime[key].ToString("f2") + "sec - NoteKey : " + notekey[key]);
        }

    }
	
	// Update is called once per frame
	void Update () {
        float t = Time.time - startTimer;
		float timerScreen = Time.time - startTimer;
        int nbNote = notetime.Count;

		t = t + 1.6f;

		string minutes = ((int) timerScreen / 60).ToString();
		string seconds = (timerScreen % 60).ToString("f2");

        //Affichage du timer en jeu
        timerText.text = minutes + "." + seconds;

		//Affichage du compteur de point
		pointText.text = "Points : " + point;

        //Le but est de tester si une note aurait déjà du être lancé ou non. On ne peut pas faire un test d'égalité avec le Timer Update car des frames peuvent être sautés
        //On parcourt donc avec un index le tableau de note, quand la note aurait du être joué on la lance puis on incrémente l'index pour passer à la suivante.

        if (index < nbNote)
        {
            if (notetime[index] <= t)
            {
                Debug.Log("Notetime : " + notetime[index] + " Notekey : " + notekey[index] + " Time : " + t);
                ++index;

				//On fait spawn la note au bon endroit (note1, note2, note3, note4)
				gameController.spawnNote (notekey[index]);

				if (notekey[index] == 1)
				{
					verifnoteA.Add (notetime[index]);
				}
				else if (notekey[index] == 2) {
					verifnoteB.Add (notetime[index]);
				}
				else if (notekey[index] == 3) {
					verifnoteC.Add (notetime[index]);
				}
				else if (notekey[index] == 4) {
					verifnoteD.Add (notetime[index]);
				}

				if (verifnoteA.Count > 0) {
					if (verifnoteA[0] > t + interval) {
						verifnoteA.RemoveAt (0);
					}
				}

				if (verifnoteB.Count > 0) {
					if (verifnoteB[0] > t + interval) {
						verifnoteB.RemoveAt (0);
					}
				}

				if (verifnoteC.Count > 0) {
					if (verifnoteC[0] > t + interval) {
						verifnoteC.RemoveAt (0);
					}
				}

				if (verifnoteD.Count > 0) {
					if (verifnoteD[0] > t + interval) {
						verifnoteD.RemoveAt (0);
					}
				}
            }
        }
    }

	public void checkKey(float notekey)
	{
		float t = Time.time - startTimer;

		//On check si la note est comprise dans un interval d'une seconde pour valider le point au joueur
		if (notekey == 1) {
			if (verifnoteA.Count > 0) {
				if (verifnoteA [0] >= t - interval && verifnoteA[0] <= t + interval) {
					print ("+ 1 point : " + verifnoteA[0] + " interval : " + t);
					verifnoteA.RemoveAt (0);
					addPoint ();
				}
			}
		}

		if (notekey == 2) {
			if (verifnoteB.Count > 0) {
				if (verifnoteB [0] >= t - interval && verifnoteB [0] <= t + interval) {
					print ("+ 1 point : " + verifnoteB[0] + " interval : " + t);
					verifnoteB.RemoveAt (0);
					addPoint ();
				}
			}
		}

		if (notekey == 3) {
			if (verifnoteC.Count > 0) {
				if (verifnoteC [0] >= t - interval && verifnoteC [0] <= t + interval) {
					print ("+ 1 point : " + verifnoteC[0] + " interval : " + t);
					verifnoteC.RemoveAt (0);
					addPoint ();
				}
			}
		}

		if (notekey == 4) {
			if (verifnoteD.Count > 0) {
				if (verifnoteD [0] >= t - interval && verifnoteD [0] <= t + interval) {
					print ("+ 1 point : " + verifnoteD[0] + " interval : " + t);
					verifnoteD.RemoveAt (0);
					addPoint ();
				}
			}
		}
	}

	public void addPoint()
	{
		point = point + 1;
	}
}
