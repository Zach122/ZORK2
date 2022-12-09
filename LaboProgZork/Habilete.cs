// Classe Habilete
//
// Effet d'une habilete de personnage
//
// Le dommage final d'une habilete est son attribut dmg + le matt du joueur
// L'attribut recuperation est le delai total avant la prochaine utilisation de l'habilete
// L'attribut tour est un compte à rebours des tours restants à attendre avant la prochaine utilisation
// 
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
    public class Habilete
    {
        public int id;
        public string nom;
        public int dmg;
        public int recuperation;
        public int tour;

        // Constructeur
        //
        // Initialise les attributs selons les paramètres reçu du même nom
        // Initialise tour à 0
        // 
        // @param string nom       le nom de l'habilete
        // @param int dmg          le nombre de points de dommage de base pour l'habilete
        // @param int recuperation le nombre de tour entre chaque utilisation de l'habilete
        // @param int id           l'identificateur unique de l'habilete

        public Habilete(string nom, int dmg, int recuperation, int id)
        {
            this.id = id;
            this.nom = nom;
            this.dmg = dmg;
            this.recuperation = recuperation;
            this.tour = 0;
        }

        // executer
        //
        // ajoute le delai de récupération à l'attribut tour
        // calcul le dommage final selon l'attaque magique reçue en paramètre
        //
        // @param int matt l'attaque magique du personnage qui utilise l'habilete
        // @return le nombre de points de dommage total (attaque magique + dommage de l'habilete)

        public int executer(int matt)
        {
            this.tour += this.recuperation;

            this.dmg += matt;

            return this.dmg;
        }

        // recuperer
        //
        // enleve 1 tour d'attente à l'attribut tour si il est plus grand que 0

        public void recuperer()
        {
            if (this.tour > 0)
            {
                this.tour--;
            }
        }
    }
}
