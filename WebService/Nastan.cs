using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Nastan
/// </summary>
public class Nastan
{
    private int id;
    private string naziv;
    private string vreme;
    private string slika;
    private string cas;
    private string kopis;
    private string sopis;
    private string sajt;
    private string video;

    public string Video
    {
        get { return video; }
        set { video = value; }
    }

    public string Sajt
    {
        get { return sajt; }
        set { sajt = value; }
    }

    public string Sopis
    {
        get { return sopis; }
        set { sopis = value; }
    }

    public string Kopis
    {
        get { return kopis; }
        set { kopis = value; }
    }

    public string Cas
    {
        get { return cas; }
        set { cas = value; }
    }

    public string Slika
    {
        get { return slika; }
        set { slika = value; }
    }

    public string Vreme
    {
        get { return vreme; }
        set { vreme = value; }
    }

    public string Naziv
    {
        get { return naziv; }
        set { naziv = value; }
    }
    public int Id
    {
        get { return id; }
        set { id = value; }
    }

    public Nastan()
    {

    }
}