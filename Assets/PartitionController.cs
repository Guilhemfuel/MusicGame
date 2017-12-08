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

    private float startTimer;

    List<float> notetime;
    List<float> notekey;

    // Use this for initialization
    void Start () {

        float nbmeasure;
        float timer = 0f;
        float division = 2f;

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
        int nbNote = notetime.Count;

        string minutes = ((int) t / 60).ToString();
        string seconds = (t % 60).ToString("f2");

        //Affichage du timer en jeu
        timerText.text = minutes + "." + seconds;

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
            }
        }
    }
}
