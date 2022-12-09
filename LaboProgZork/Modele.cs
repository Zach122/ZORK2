// Classe Modele
//
// Usine qui génère des joueurs et des ennemis à partir des fichiers texte
//
// Les habiletés sont fixées dans le constructeur par simplicité, il serait possible 
// de les générer en ajoutant des fichiers qui contiennent leur définition
//
// Création : 2022/11/19
// Par : Frédérik Taleb
// Modification : 2022/11/24
// Par : Frédérik Taleb

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaboFinal_A22
{
    public class Modele
    {
                 //L'habileté déclarer qq'un
                //
               //$$ J'ai déclarer un joueur pour aller lui assigner son 


        // attributs
        // une liste ou un tableau d'habiletes, le type est donc la classe Habilete
        public List<Habilete> habiletes = new List<Habilete>();
        // Consturcteur
        // 
        // initialise le contenant pour les habiletés
        // il n'y a que 3 habiletés
        // initialise chacune des habiletés et assigne chacune à une case de l'attribut habiletes
        public Modele()
        {
            //initialiser l'attribut habiletes
            this.habiletes = new List<Habilete>();
            //crée une instance de l'habilete : 
            // Coup Héroïque
            // dmg : 25
            // recup : 4
            // id : 0
            Habilete coup = new Habilete("Coup Héroïque", 25, 4, 0);
            //crée une instance de l'habilete : 
            // Attaque Surprise
            // dmg : 40
            // recup : 6
            // id : 1
            Habilete attaque = new Habilete("Attaque Surprise", 40, 6, 1);
            //crée une instance de l'habilete : 
            // Boule De Feu
            // dmg : 50
            // recup : 2
            // id : 2
            Habilete boule = new Habilete("Boule De Feu", 25, 4, 2);
            // ajoute toues les habiletés à l'attribut habiletes
            this.habiletes.Add(coup);
            this.habiletes.Add(attaque);
            this.habiletes.Add(boule);
        }

        // genererJoueur
        //
        // configure une instance de la classe joueur selon le nom du fichier et le nom passé en paramètre
        // la première ligne du fichier donne l'ordre des attributs nécessaires pour la classe
        //
        // retourne l'instance de la classe joueur initialisée avec l'habileté ajoutée
        // car l'habileté n'est pas dans le constructeur de la classe joueur
        //
        // @param string fichier le nom de la profession choisie, il faudra ajouter .txt à la fin du string
        // @param string nom     le nom choisi par le joueur
        // @return une instance de la classe joueur
        public Joueur genererJoueur(string fichier, string nom)
        {
            //=> J'ai déclarer un tableau pour tout les caractéristique du perso choisi
            int[] statsJoueur = new int[6];

            // Déclarer une variable de type Joueur, nous allons créer l'instance plus tard

            // Initialiser la classe pour lire le fichier
            StreamReader lecteur = new StreamReader(fichier + ".txt");

            // Lire la première ligne dans le vide ( on a besoin seulement des stats)
            lecteur.ReadLine();

            // Lire la deuxième ligne et la garder en mémoire
            string fichierLigne2 = lecteur.ReadLine();

            // Transformer la ligne en tableau de string, en utilisant la virgule comme séparateur
            string[] tableauHabilete = fichierLigne2.Split(',');
            lecteur.Close();

            //=> J'ai tryparse le tableau d'habilete dans mon tableau de stats          
            for (int i = 1; i < 7; i++)
            {
              
                int.TryParse(tableauHabilete[i], out statsJoueur[i-1]);
            }

            //=> faire un int id pour mieu comprendre le programme
            int id = statsJoueur[5];

            // utiliser le tableau afin d'obtenir les informations désirées pour utiliser le constructeur de la classe Joueur
            // et finir de créer l'instance du joueur avec ces informations
            Joueur joueur = new Joueur(nom, statsJoueur[0], statsJoueur[1], statsJoueur[2], statsJoueur[3], statsJoueur[4]);

            // ne pas oublier d'assigner l'habilete au joueur selon le id après la construction
            joueur.habilete = habiletes[id];

            // retourner le joueur configuré
            return joueur;
        }

        // genererEnnemi
        //
        // configure une instance de la classe ennemi selon le nom du fichier passé en paramètre
        // la première ligne du fichier donne l'ordre des attributs nécessaires pour l'ennemi
        //
        // retourne l'instance de la classe ennemi initialisée 
        //
        // @param string fichier le nom de l'ennemi choisi, il faudra ajouter .txt à la fin du string
        // @return une instance de la classe ennemi
        public Ennemi genererEnnemi(string fichier)
        {
            // Déclarer une variable de type Ennemi, nous allons créer l'instance plus tard =>***** je l'ai déclarer plus bas
            //------->
            //------------>
            //---------------->

            //====> J'ai déclarer mon tab de stats de l'ennemi
            int[] statsEnnemi = new int[6];

            // Initialiser la classe pour lire le fichier
            StreamReader lecteur2 = new StreamReader(fichier + ".txt");
            // Lire la première ligne dans le vide ( on a besoin seulement des stats)
            lecteur2.ReadLine();
            // Lire la deuxième ligne et la garder en mémoire
            string fichierLigne2 = lecteur2.ReadLine();
            // Transformer la ligne en tableau de string, en utilisant la virgule comme séparateur
            string[] tableauHabilete = fichierLigne2.Split(',');
            lecteur2.Close();

            //=> J'ai tryparse le tableau d'habilete dans mon tableau de stats          
            for (int i = 1; i < 6; i++)
            {
                int.TryParse(tableauHabilete[i], out statsEnnemi[i-1]);
            }

            //=> faire un bool magique pour mieu comprendre le programme puis tryparsse mon string du tableau
            bool magique;

            bool.TryParse(tableauHabilete[6], out magique);


            // utiliser le tableau afin d'obtenir les informations désirées pour utiliser le constructeur de la classe Joueur
            // et finir de créer l'instance du joueur avec ces informations
            Ennemi ennemi = new Ennemi(tableauHabilete[0], statsEnnemi[0], statsEnnemi[1], statsEnnemi[2], statsEnnemi[3], statsEnnemi[4], magique);

            // retourner le joueur configuré
            return ennemi;
        }
    }
}
