using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class ChercherLeMot
{
    private Dictionary<string, List<string>> dictionnaire;

    public void ChargerDictionnaire(string cheminFichier)
    {
        dictionnaire = new Dictionary<string, List<string>>();

        try
        {
            using (StreamReader lecteur = new StreamReader(cheminFichier))
            {
                string ligne;
                while ((ligne = lecteur.ReadLine()) != null)
                {
                    string motTrié = TrierMot(ligne);
                    if (!dictionnaire.ContainsKey(motTrié))
                        dictionnaire[motTrié] = new List<string>();

                    dictionnaire[motTrié].Add(ligne);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Erreur lors du chargement du dictionnaire : " + ex.Message);
        }
    }

    public List<string> TrouverMot(string mot)
    {
        string motTrié = TrierMot(mot);
        if (dictionnaire.ContainsKey(motTrié))
            return dictionnaire[motTrié];

        return new List<string>();
    }

    private string TrierMot(string mot)
    {
        char[] caractères = mot.ToLower().ToCharArray();
        Array.Sort(caractères);
        return new string(caractères);
    }
}

class Programme
{
    static void Main(string[] args)
    {
        string cheminFichierDictionnaire = "dictionnaire.txt"; // Chemin vers le fichier dictionnaire

        ChercherLeMot chercherMot = new ChercherLeMot();
        chercherMot.ChargerDictionnaire(cheminFichierDictionnaire);

        Console.WriteLine("Entrez les lettres pour trouver les mots (sans espace) :");
        string saisie = Console.ReadLine();

        List<string> mots = chercherMot.TrouverMot(saisie);

        Console.WriteLine("\nRésultats :");
        if (mots.Count > 0)
        {
            foreach (string mot in mots)
            {
                Console.WriteLine(mot);
            }
        }
        else
        {
            Console.WriteLine("Aucun mot trouvé pour les lettres fournies.");
        }

        Console.ReadLine();
    }
}