using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CVSImport : MonoBehaviour
{

    static public List<GPSPoint> pointList = new List<GPSPoint>();

    [Header("Input Tables")]
    public TextAsset GPSCSV;

    [Header("Color Scheme")]
    public Color color1 = new Color(0, 0, 1.0f);
    public Color color2 = new Color(.5f, .5f, 1.0f);
    public Color color3 = new Color(.5f, .5f, .5f);
    public Color color4 = new Color(1.0f, .5f, .5f);
    public Color color5 = new Color(1.0f, 0, 0);
    public Color colorDefault = ConsoleColor.black;

    private int[] Steps = { 10000, 20000, 30000, 40000, 60000 };
    // Start is called before the first frame update
    void Start()
    {
        Import();
        DrawGPSPoints(pointList);
    }

    void Import()
    {
        TextAsset rawData = GPSCSV; //CSV Daten werden als TextAsset eingelesen

        //Eingabedaten teilen in Zeile je Array (Trennzeichen Zeilenumbruch '\n')
        string[] data = rawData.text.Split(new char[] { '\n' });

        //Schleife über alle Tabellenzeilen
        for (int i = 1; i < data.Length - 1; i++)
        {   //Eingabezeile teilen in Spalte je Array (Trennzeichen ';')
            string[] row = data[i].Split(new char[] { ';' });
            //Instanz von GPSPoint anlegen
            GPSPoint point = new GPSPoint();
            //Zuweisen der Werte aus der csv-Tabelle an die Instanz

            //Parsen des Strings in Double Wert
            double.TryParse(row[0], out point.time);
            double.TryParse(row[7], out point.lon);
            double.TryParse(row[6], out point.lat);
            double.TryParse(row[4], out point.light);
            double.TryParse(row[5], out point.noise);

            //Hinzufügen der Instanz zur Liste aller GPS-Punkte
            pointList.Add(point);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
