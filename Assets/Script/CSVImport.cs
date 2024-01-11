using System.Collections.Generic;
using UnityEngine;

public class CSVImport : MonoBehaviour
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
    public Color colorDefault = Color.black;

    [Header("Data Transformation")]
    private Vector3 rotationValue = new Vector3(0.0f, -0.926f, 0.0f);



    private int[] Steps = { 10000, 20000, 30000, 40000, 60000 };

    // Start is called before the first frame update
    void Start()
    {
        Import();
        DrawGPSPoints(pointList);

        //Position and Scale DataPoints
        gameObject.transform.position = new Vector3(-3.63f, 5.6746f, -6.02f);
        gameObject.transform.rotation = Quaternion.Euler(rotationValue);
        gameObject.transform.localScale = new Vector3(0.1423552f, 0.1423552f, 0.1423552f);

        RemoveSphereColliders();
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

    void RemoveSphereColliders()
    {
        SphereCollider[] colliders = GetComponentsInChildren<SphereCollider>();

        foreach (SphereCollider collider in colliders)
        {
            Destroy(collider);
        }
    }

    void DrawGPSPoints(List<GPSPoint> pointListtoDraw)
    {
        float sizeShpere = 1;
        int nr = 0;

        foreach (GPSPoint p in pointListtoDraw)
        {
            //Primitive für GPS Punkt erstellen
            p.GOPoint = GameObject.CreatePrimitive(PrimitiveType.Sphere);

            //Nummerierung der GPS Punkte zur besseren Unterscheidung
            p.GOPoint.name = "GPS_Point_" + nr;
            nr++;

            //GPS Punkt an Parent-Objekt anfügen
            p.GOPoint.transform.SetParent(gameObject.GetComponent<Transform>());
                        
            //Mittelpunkt neu setzen
            Vector2 localOriginGPS = new Vector2(50.265030410959135f, 10.952349798326864f);
            GPSEncoder.SetLocalOrigin(localOriginGPS);

            //Umrechnen von GPS- in Unity-Koordinaten
            Vector3 positionInUnity = GPSEncoder.GPSToUCS((float)p.lat, (float)p.lon);
            p.GOPoint.transform.position = new Vector3(-positionInUnity.x/29, sizeShpere*0.5f-0.4f, -positionInUnity.z/30-0.5f);

            //Größe für Primitive/Sphere setzen
            p.GOPoint.transform.localScale = new Vector3(sizeShpere/6, sizeShpere/6, sizeShpere/6);

            //Wert des Parameters (der unabh. Variablen) anhand der Farbe kodieren
            Color colorValue = getColorBasedOnParameter(p.light);
            p.GOPoint.GetComponent<Renderer>().material.SetColor("_Color", colorValue);

        }
    }

    Color getColorBasedOnParameter(double light_value)
    {
        if (light_value < Steps[0]) return color1;
        else if (light_value < Steps[1]) return color2;
        else if (light_value < Steps[2]) return color3;
        else if (light_value < Steps[3]) return color4;
        else if (light_value < Steps[4]) return color5;
        else return colorDefault;
    }

}
